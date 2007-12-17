
/* this file is generated by gen-animation-types.cs.  do not modify */

using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using NUnit.Framework;

namespace MonoTests.System.Windows.Media.Animation {


[TestFixture]
public class SingleAnimationTest
{
	[Test]
	public void Properties ()
	{
		Assert.AreEqual ("By", SingleAnimation.ByProperty.Name, "1");
		Assert.AreEqual (typeof (SingleAnimation), SingleAnimation.ByProperty.OwnerType, "2");
		Assert.AreEqual (typeof (float?), SingleAnimation.ByProperty.PropertyType, "3");

		Assert.AreEqual ("From", SingleAnimation.FromProperty.Name, "4");
		Assert.AreEqual (typeof (SingleAnimation), SingleAnimation.FromProperty.OwnerType, "5");
		Assert.AreEqual (typeof (float?), SingleAnimation.FromProperty.PropertyType, "6");

		Assert.AreEqual ("To", SingleAnimation.ToProperty.Name, "7");
		Assert.AreEqual (typeof (SingleAnimation), SingleAnimation.ToProperty.OwnerType, "8");
		Assert.AreEqual (typeof (float?), SingleAnimation.ToProperty.PropertyType, "9");
	}
}


}