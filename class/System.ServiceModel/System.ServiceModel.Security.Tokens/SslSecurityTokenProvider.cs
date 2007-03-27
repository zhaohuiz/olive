//
// SslSecurityTokenProvider.cs
//
// Author:
//	Atsushi Enomoto <atsushi@ximian.com>
//
// Copyright (C) 2006-2007 Novell, Inc.  http://www.novell.com
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Security;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;
using System.Xml;

using ReqType = System.ServiceModel.Security.Tokens.ServiceModelSecurityTokenRequirement;

namespace System.ServiceModel.Security.Tokens
{
	class SslSecurityTokenProvider : CommunicationSecurityTokenProvider
	{
		SslCommunicationObject comm;

		public SslSecurityTokenProvider ()
		{
			comm = new SslCommunicationObject ();
		}

		public override ProviderCommunicationObject Communication {
			get { return comm; }
		}

		public override SecurityToken GetOnlineToken (TimeSpan timeout)
		{
			return comm.GetToken (timeout);
		}
	}

	class SslCommunicationObject : ProviderCommunicationObject
	{
		WSTrustSecurityTokenServiceProxy proxy;

		public SslCommunicationObject ()
		{
		}

		class TlsnegoClientSessionContext
		{
			XmlDocument doc = new XmlDocument ();
			XmlDsigExcC14NTransform t = new XmlDsigExcC14NTransform ();
			MemoryStream stream = new MemoryStream ();

			public void StoreMessage (XmlReader reader)
			{
				doc.RemoveAll ();
				doc.AppendChild (doc.ReadNode (reader));
				t.LoadInput (doc);
				MemoryStream s = (MemoryStream) t.GetOutput ();
				byte [] bytes = s.ToArray ();
				stream.Write (bytes, 0, bytes.Length);
			}

			public byte [] GetC14NResults ()
			{
				return stream.ToArray ();
			}
		}

		public SecurityToken GetToken (TimeSpan timeout)
		{
			TlsnegoClientSessionContext tlsctx =
				new TlsnegoClientSessionContext ();
			TlsClientSession tls = new TlsClientSession (IssuerAddress.Uri.ToString ());
			WstRequestSecurityToken rst =
				new WstRequestSecurityToken ();

			// send ClientHello
			rst.BinaryExchange = new WstBinaryExchange (Constants.WstBinaryExchangeValueTls);
			rst.BinaryExchange.Value = tls.ProcessClientHello ();

			Message request = Message.CreateMessage (IssuerBinding.MessageVersion, Constants.WstIssueAction, rst);
			request.Headers.MessageId = new UniqueId ();
			request.Headers.ReplyTo = new EndpointAddress (Constants.WsaAnonymousUri);
			request.Headers.To = TargetAddress.Uri;
			MessageBuffer buffer = request.CreateBufferedCopy (0x10000);
			tlsctx.StoreMessage (buffer.CreateMessage ().GetReaderAtBodyContents ());
			Message response = proxy.Issue (buffer.CreateMessage ());

			// FIXME: use correct limitation
			buffer = response.CreateBufferedCopy (0x10000);
			tlsctx.StoreMessage (buffer.CreateMessage ().GetReaderAtBodyContents ());

			// receive ServerHello
			WSTrustRequestSecurityTokenResponseReader reader =
				new WSTrustRequestSecurityTokenResponseReader (Constants.WstTlsnegoProofTokenType, buffer.CreateMessage ().GetReaderAtBodyContents (), SecurityTokenSerializer, null);
			reader.Read ();
			if (reader.Value.RequestedSecurityToken != null)
				return reader.Value.RequestedSecurityToken;

			tls.ProcessServerHello (reader.Value.BinaryExchange.Value);

			// send ClientKeyExchange
			WstRequestSecurityTokenResponse rstr =
				new WstRequestSecurityTokenResponse (SecurityTokenSerializer);
			rstr.Context = reader.Value.Context;
			rstr.BinaryExchange = new WstBinaryExchange (Constants.WstBinaryExchangeValueTls);
			rstr.BinaryExchange.Value = tls.ProcessClientKeyExchange ();

			request = Message.CreateMessage (IssuerBinding.MessageVersion, Constants.WstIssueReplyAction, rstr);
			request.Headers.ReplyTo = new EndpointAddress (Constants.WsaAnonymousUri);
			request.Headers.To = TargetAddress.Uri;

			buffer = request.CreateBufferedCopy (0x10000);
			tlsctx.StoreMessage (buffer.CreateMessage ().GetReaderAtBodyContents ());
//Console.WriteLine (System.Text.Encoding.UTF8.GetString (tlsctx.GetC14NResults ()));

			// FIXME: regeneration of this instance is somehow required, but should not be.
			proxy = new WSTrustSecurityTokenServiceProxy (
				IssuerBinding, IssuerAddress);
			response = proxy.IssueReply (buffer.CreateMessage ());
			// FIXME: use correct limitation
			buffer = response.CreateBufferedCopy (0x10000);
			// don't store this message in tlsctx (it's not part
			// of exchange)

			WstRequestSecurityTokenResponseCollection coll =
				new WstRequestSecurityTokenResponseCollection ();
			coll.Read (Constants.WstTlsnegoProofTokenType, buffer.CreateMessage ().GetReaderAtBodyContents (), SecurityTokenSerializer, null);
			if (coll.Responses.Count != 2)
				throw new SecurityNegotiationException (String.Format ("Expected response is RequestSecurityTokenResponseCollection which contains two RequestSecurityTokenResponse items, but it actually contains {0} items", coll.Responses.Count));

			WstRequestSecurityTokenResponse r = coll.Responses [0];
			tls.ProcessServerFinished (r.BinaryExchange.Value);
			SecurityContextSecurityToken sctSrc =
				r.RequestedSecurityToken;

			// Authenticate token.
			byte [] key = tls.MasterSecret; //(sctSrc.SecurityKeys [0] as SymmetricSecurityKey).GetSymmetricKey ();
//foreach (byte b in key) Console.Write ("{0:X02} ", b); Console.WriteLine ();

			// the RequestedProofToken is represented as 32 bytes
			// of TLS ApplicationData.
			byte [] proof = tls.ProcessApplicationData (
				(byte []) r.RequestedProofToken);

			byte [] actual = coll.Responses [1].Authenticator;
			if (actual == null)
				throw new SecurityNegotiationException ("Token authenticator is expected in the RequestSecurityTokenResponse but not found.");

			// H = sha1(exc14n(RST..RSTRs))
			byte [] hash = SHA1.Create ().ComputeHash (tlsctx.GetC14NResults ());
			byte [] referent = tls.CreateHash (key, hash, "AUTH-HASH");
foreach (byte b in hash) Console.Write ("{0:X02} ", b); Console.WriteLine ();
foreach (byte b in referent) Console.Write ("{0:X02} ", b); Console.WriteLine ();
foreach (byte b in actual) Console.Write ("{0:X02} ", b); Console.WriteLine ();
			bool mismatch = referent.Length != actual.Length;
			if (!mismatch)
				for (int i = 0; i < referent.Length; i++)
					if (referent [i] != actual [i])
						mismatch = true;
			if (mismatch)
				throw new SecurityNegotiationException ("The CombinedHash does not match the expected value.");

			return sctSrc;
		}

		protected internal override TimeSpan DefaultCloseTimeout {
			get { throw new NotImplementedException (); }
		}

		protected internal override TimeSpan DefaultOpenTimeout {
			get { throw new NotImplementedException (); }
		}

		protected override void OnAbort ()
		{
			throw new NotImplementedException ();
		}

		protected override void OnOpen (TimeSpan timeout)
		{
			if (State == CommunicationState.Opened)
				throw new InvalidOperationException ("Already opened.");

			EnsureProperties ();

			proxy = new WSTrustSecurityTokenServiceProxy (
				IssuerBinding, IssuerAddress);
		}

		protected override IAsyncResult OnBeginOpen (TimeSpan timeout, AsyncCallback callback, object state)
		{
			throw new NotImplementedException ();
		}

		protected override void OnEndOpen (IAsyncResult result)
		{
			throw new NotImplementedException ();
		}

		protected override void OnClose (TimeSpan timeout)
		{
			if (proxy != null)
				proxy.Close ();
		}

		protected override IAsyncResult OnBeginClose (TimeSpan timeout, AsyncCallback callback, object state)
		{
			throw new NotImplementedException ();
		}

		protected override void OnEndClose (IAsyncResult result)
		{
			throw new NotImplementedException ();
		}
	}
}
