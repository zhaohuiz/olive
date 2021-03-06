
/* this file is generated by gen-animation-types.cs.  do not modify */

using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Markup;

namespace System.Windows.Media.Animation {


public abstract class StringAnimationBase : AnimationTimeline
{
	protected StringAnimationBase () { }

	public override sealed Type TargetPropertyType { get { return typeof (String); } }

	public new StringAnimationBase Clone ()
	{
		throw new NotImplementedException ();
	} 

	public override sealed object GetCurrentValue (object defaultOriginValue, object defaultDestinationValue, AnimationClock animationClock)
	{
		return GetCurrentValue ((String)defaultOriginValue, (String) defaultDestinationValue, animationClock);
	}

	protected abstract String GetCurrentValueCore (String defaultOriginValue, String defaultDestinationValue, AnimationClock animationClock);


	public String GetCurrentValue (String defaultOriginValue, String defaultDestinationValue, AnimationClock animationClock)
	{
		throw new NotImplementedException ();
	}


}


}
