//
// Microsoft.JScript.Runtime
//
// Author:
//   Olivier Dufour (olivier.duff@gmail.com)
//
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
using NUnit.Core;
using NUnit.Framework;
using Microsoft.JScript.Runtime;
using MSc = Microsoft.Scripting;

namespace MonoTests.Microsoft.JScript.Runtime
{
	[TestFixture]
	public class JSCompilerHelpersTest
	{
		[Test]
		public void ConstructArrayFromArrayLiteral ()
		{
			JSArrayObject result = JSCompilerHelpers.ConstructArrayFromArrayLiteral (new MSc.CodeContext (new MSc.Scope (), null), new object[] { });
			//Assert.AreEqual( result.SymbolAttributes

		}

		[Test]
		public void ConstructObjectFromLiteral ()
		{
			object key1 = new String ("test name".ToCharArray ());
			object val1 = new String ("test value".ToCharArray ());
			object obj = JSCompilerHelpers.ConstructObjectFromLiteral (new MSc.CodeContext (new MSc.Scope (), null), new object[] { key1 }, new object[] { val1});
			Assert.IsInstanceOfType (typeof (JSObject), obj, "#1");
			JSObject jsobj = (JSObject)obj;
			foreach (object o in jsobj.Keys) {
				Assert.AreEqual(key1, o, "#2");
				Assert.AreEqual (val1, jsobj[o], "#3");
			}
		}

		[Test]
		public void Delete ()
		{

		}

		[Test]
		public void In ()
		{

		}

		[Test]
		public void InstanceOf ()
		{

		}

		[Test]
		public void Is ()
		{

		}

		[Test]
		public void IsNot ()
		{

		}

		[Test]
		public void MakeRegex ()
		{

		}

		[Test]
		public void Negate ()
		{
			Assert.AreEqual(-5.0, JSCompilerHelpers.Negate (5),"#1");
		}

		[Test]
		public void Not ()
		{

		}

		[Test]
		public void OnesComplement ()
		{

		}

		[Test]
		public void Positive ()
		{
			Assert.AreEqual (5.0, JSCompilerHelpers.Positive (5), "#1");
		}

		[Test]
		public void TypeOf ()
		{

		}

		[Test]
		public void Void ()
		{
			Assert.AreEqual (UnDefined.Value, JSCompilerHelpers.Void (new object()), "#1");
		}
	}
}
