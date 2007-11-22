
/* this file is generated by gen-animation-types.cs.  do not modify */

using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Markup;

namespace System.Windows.Media.Animation {


public class DiscreteCharKeyFrame : CharKeyFrame
{
	char value;
	KeyTime keyTime;

	public DiscreteCharKeyFrame ()
	{
	}

	public DiscreteCharKeyFrame (char value)
	{
		this.value = value;
		// XXX keytime?
	}

	public DiscreteCharKeyFrame (char value, KeyTime keyTime)
	{
		this.value = value;
		this.keyTime = keyTime;
	}

	protected override Freezable CreateInstanceCore ()
	{
		throw new NotImplementedException ();
	}

	protected override char InterpolateValueCore (char baseValue, double keyFrameProgress)
	{
		return keyFrameProgress == 1.0 ? value : baseValue;
	}
}


}