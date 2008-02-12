//
// UriTemplateTable.cs
//
// Author:
//	Atsushi Enomoto  <atsushi@ximian.com>
//
// Copyright (C) 2008 Novell, Inc (http://www.novell.com)
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
using System.Collections.ObjectModel;
using System.Collections.Specialized;

using Pair = System.Collections.Generic.KeyValuePair<System.UriTemplate, object>;

namespace System
{
	public class UriTemplateTable
	{
		public UriTemplateTable ()
		{
		}

		public UriTemplateTable (Uri baseAddress)
		{
			BaseAddress = baseAddress;
		}

		public UriTemplateTable (IEnumerable<Pair> keyValuePairs)
		{
			IList<Pair> l = keyValuePairs as IList<Pair>;
			if (l == null)
				l = new List<Pair> (keyValuePairs);
		}

		public UriTemplateTable (Uri baseAddress, IEnumerable<Pair> keyValuePairs)
			: this (keyValuePairs)
		{
			BaseAddress = baseAddress;
		}

		void CheckReadOnly ()
		{
			if (is_readonly)
				throw new InvalidOperationException ("This UriTemplateTable is read-only");
		}

		bool is_readonly;
		Uri base_address;
		IList<Pair> key_value_pairs;

		public Uri BaseAddress {
			get { return base_address; }
			set {
				CheckReadOnly ();
				base_address = value;
			}
		}

		public bool IsReadOnly {
			get { return is_readonly; }
		}

		public IList<Pair> KeyValuePairs {
			get { return key_value_pairs; }
		}

		[MonoTODO]
		public void MakeReadOnly (bool allowDuplicateEquivalentUriTemplates)
		{
			throw new NotImplementedException ();
		}

		[MonoTODO]
		public Collection<UriTemplateMatch> Match (Uri uri)
		{
			throw new NotImplementedException ();
		}

		[MonoTODO]
		public UriTemplateMatch MatchSingle (Uri uri)
		{
			throw new NotImplementedException ();
		}
	}
}