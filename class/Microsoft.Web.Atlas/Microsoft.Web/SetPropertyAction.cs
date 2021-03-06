//
// Microsoft.Web.SetPropertyAction
//
// Author:
//   Chris Toshok (toshok@ximian.com)
//
//
// Copyright (C) 2005 Novell, Inc (http://www.novell.com)
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

#if NET_2_0

using System;

namespace Microsoft.Web
{
	public sealed class SetPropertyAction : Action
	{
		public SetPropertyAction ()
		{
		}

		string property = "";
		public string Property {
			get {
				return property;
			}
			set {
				property = (value == null ? "" : value);
			}
		}

		string propertyKey = "";
		public string PropertyKey {
			get {
				return propertyKey;
			}
			set {
				propertyKey = (value == null ? "" : value);
			}
		}

		public override string TagName {
			get {
				return "setProperty";
			}
		}

		string value = "";
		public string Value {
			get {
				return Value;
			}
			set {
				this.value = (value == null ? "" : value);
			}
		}


		protected override void AddAttributesToElement (ScriptTextWriter writer)
		{
			base.AddAttributesToElement (writer);

			if (Property != "")
				writer.WriteAttributeString ("property", Property);

			if (PropertyKey != "")
				writer.WriteAttributeString ("propertykey", PropertyKey);

			if (Value != "")
				writer.WriteAttributeString ("value", Value);
		}

		protected override void InitializeTypeDescriptor (ScriptTypeDescriptor typeDescriptor)
		{
			base.InitializeTypeDescriptor (typeDescriptor);

			typeDescriptor.AddProperty (new ScriptPropertyDescriptor ("property", ScriptType.String, false, "Property"));
			typeDescriptor.AddProperty (new ScriptPropertyDescriptor ("propertykey", ScriptType.String, false, "PropertyKey"));
			typeDescriptor.AddProperty (new ScriptPropertyDescriptor ("value", ScriptType.String, false, "Value"));
		}
	}
}

#endif
