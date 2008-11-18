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
// Copyright (c) 2007 Novell, Inc. (http://www.novell.com)
//
// Authors:
//	Chris Toshok (toshok@ximian.com)
//

using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

using System.Windows.Input;

namespace System.Windows {

	public class UIElement : Visual, IInputElement, IAnimatable {

		public static readonly DependencyProperty AllowDropProperty;
		public static readonly DependencyProperty BitmapEffectInputProperty;
		public static readonly DependencyProperty BitmapEffectProperty;
		public static readonly DependencyProperty ClipProperty;
		public static readonly DependencyProperty ClipToBoundsProperty;
		public static readonly DependencyProperty FocusableProperty;
		public static readonly DependencyProperty IsEnabledProperty;
		public static readonly DependencyProperty IsFocusedProperty;
		public static readonly DependencyProperty IsHitTestVisibleProperty;
		public static readonly DependencyProperty IsKeyboardFocusedProperty;
		public static readonly DependencyProperty IsKeyboardFocusWithinProperty;
		public static readonly DependencyProperty IsMouseCapturedProperty;
		public static readonly DependencyProperty IsMouseCaptureWithinProperty;
		public static readonly DependencyProperty IsMouseDirectlyOverProperty;
		public static readonly DependencyProperty IsMouseOverProperty;
		public static readonly DependencyProperty IsStylusCapturedProperty;
		public static readonly DependencyProperty IsStylusCaptureWithinProperty;
		public static readonly DependencyProperty IsStylusDirectlyOverProperty;
		public static readonly DependencyProperty IsStylusOverProperty;
		public static readonly DependencyProperty IsVisibleProperty;
		public static readonly DependencyProperty OpacityMaskProperty;
		public static readonly DependencyProperty OpacityProperty;
		public static readonly DependencyProperty RenderTransformOriginProperty;
		public static readonly DependencyProperty RenderTransformProperty;
		public static readonly DependencyProperty SnapsToDevicePixelsProperty;
		public static readonly DependencyProperty VisibilityProperty;

		public static readonly RoutedEvent DragEnterEvent = DragDrop.DragEnterEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent DragLeaveEvent = DragDrop.DragLeaveEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent DragOverEvent = DragDrop.DragOverEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent DropEvent = DragDrop.DropEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent GiveFeedbackEvent = DragDrop.GiveFeedbackEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent GotFocusEvent = FocusManager.GotFocusEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent GotKeyboardFocusEvent = Keyboard.GotKeyboardFocusEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent GotMouseCaptureEvent = Mouse.GotMouseCaptureEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent GotStylusCaptureEvent = Stylus.GotStylusCaptureEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent KeyDownEvent = Keyboard.KeyDownEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent KeyUpEvent = Keyboard.KeyUpEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent LostFocusEvent = FocusManager.LostFocusEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent LostKeyboardFocusEvent = Keyboard.LostKeyboardFocusEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent LostMouseCaptureEvent = Mouse.LostMouseCaptureEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent LostStylusCaptureEvent = Stylus.LostStylusCaptureEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent MouseDownEvent = Mouse.MouseDownEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent MouseEnterEvent = Mouse.MouseEnterEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent MouseLeaveEvent = Mouse.MouseLeaveEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent MouseLeftButtonDownEvent = new RoutedEvent ("MouseLeftButtonDown",
											       typeof (MouseButtonEventHandler),
											       typeof (UIElement),
											       RoutingStrategy.Direct);
		public static readonly RoutedEvent MouseLeftButtonUpEvent = new RoutedEvent ("MouseLeftButtonUp",
											       typeof (MouseButtonEventHandler),
											       typeof (UIElement),
											       RoutingStrategy.Direct);
		public static readonly RoutedEvent MouseMoveEvent = Mouse.MouseMoveEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent MouseRightButtonDownEvent = new RoutedEvent ("MouseRightButtonDown",
											       typeof (MouseButtonEventHandler),
											       typeof (UIElement),
											       RoutingStrategy.Direct);
		public static readonly RoutedEvent MouseRightButtonUpEvent = new RoutedEvent ("MouseRightButtonUp",
											       typeof (MouseButtonEventHandler),
											       typeof (UIElement),
											       RoutingStrategy.Direct);
		public static readonly RoutedEvent MouseUpEvent = Mouse.MouseUpEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent MouseWheelEvent = Mouse.MouseWheelEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent PreviewDragEnterEvent =  DragDrop.PreviewDragEnterEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent PreviewDragLeaveEvent = DragDrop.PreviewDragLeaveEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent PreviewDragOverEvent = DragDrop.PreviewDragOverEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent PreviewDropEvent = DragDrop.PreviewDropEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent PreviewGiveFeedbackEvent = DragDrop.PreviewGiveFeedbackEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent PreviewGotKeyboardFocusEvent = Keyboard.PreviewGotKeyboardFocusEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent PreviewKeyDownEvent = Keyboard.PreviewKeyDownEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent PreviewKeyUpEvent = Keyboard.PreviewKeyUpEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent PreviewLostKeyboardFocusEvent = Keyboard.PreviewLostKeyboardFocusEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent PreviewMouseDownEvent = Mouse.PreviewMouseDownEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent PreviewMouseLeftButtonDownEvent = new RoutedEvent ("PreviewMouseLeftButtonDown",
												      typeof (MouseButtonEventHandler),
												      typeof (UIElement),
												      RoutingStrategy.Direct);
		public static readonly RoutedEvent PreviewMouseLeftButtonUpEvent = new RoutedEvent ("PreviewMouseLeftButtonUp",
												    typeof (MouseButtonEventHandler),
												    typeof (UIElement),
												    RoutingStrategy.Direct);
		public static readonly RoutedEvent PreviewMouseMoveEvent = Mouse.PreviewMouseMoveEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent PreviewMouseRightButtonDownEvent = new RoutedEvent ("PreviewMouseRightButtonDown",
												       typeof (MouseButtonEventHandler),
												       typeof (UIElement),
												       RoutingStrategy.Direct);
		public static readonly RoutedEvent PreviewMouseRightButtonUpEvent = new RoutedEvent ("PreviewMouseRightButtonUp",
												     typeof (MouseButtonEventHandler),
												     typeof (UIElement),
												     RoutingStrategy.Direct);
		public static readonly RoutedEvent PreviewMouseUpEvent = Mouse.PreviewMouseUpEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent PreviewMouseWheelEvent = Mouse.PreviewMouseWheelEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent PreviewQueryContinueDragEvent = DragDrop.PreviewQueryContinueDragEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent PreviewStylusButtonDownEvent = Stylus.PreviewStylusButtonDownEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent PreviewStylusButtonUpEvent = Stylus.PreviewStylusButtonUpEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent PreviewStylusDownEvent = Stylus.PreviewStylusDownEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent PreviewStylusInAirMoveEvent = Stylus.PreviewStylusInAirEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent PreviewStylusInRangeEvent = Stylus.PreviewStylusInRangeEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent PreviewStylusMoveEvent = Stylus.PreviewStylusMoveEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent PreviewStylusOutOfRangeEvent = Stylus.PreviewStylusOutOfRangeEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent PreviewStylusSystemGestureEvent = Stylus.PreviewStylusSystemGestureEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent PreviewStylusUpEvent = Stylus.PreviewStylusUpEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent PreviewTextInputEvent = TextCompositionManager.PreviewTextInputEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent QueryContinueDragEvent = DragDrop.QueryContinueDragEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent QueryCursorEvent = Mouse.QueryCursorEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent StylusButtonDownEvent = Stylus.StylusButtonDownEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent StylusButtonUpEvent = Stylus.StylusButtonUpEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent StylusDownEvent = Stylus.StylusDownEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent StylusEnterEvent = Stylus.StylusEnterEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent StylusInAirMoveEvent = Stylus.StylusInAirEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent StylusInRangeEvent = Stylus.StylusInRangeEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent StylusLeaveEvent = Stylus.StylusLeaveEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent StylusMoveEvent = Stylus.StylusMoveEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent StylusOutOfRangeEvent = Stylus.StylusOutOfRangeEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent StylusSystemGestureEvent = Stylus.StylusSystemGestureEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent StylusUpEvent = Stylus.StylusUpEvent.AddOwner (typeof (UIElement));
		public static readonly RoutedEvent TextInputEvent = TextCompositionManager.TextInputEvent.AddOwner (typeof (UIElement));

		// XXX these should have explicit add/remove methods
		// that add to the corresponding RoutedEvents.
		public event DragEventHandler DragEnter;
		public event DragEventHandler DragLeave;
		public event DragEventHandler DragOver;
		public event DragEventHandler Drop;
		public event DependencyPropertyChangedEventHandler FocusableChanged;
		public event GiveFeedbackEventHandler GiveFeedback;
		public event RoutedEventHandler GotFocus;
		public event KeyboardFocusChangedEventHandler GotKeyboardFocus;
		public event MouseEventHandler GotMouseCapture;
		public event StylusEventHandler GotStylusCapture;
		public event DependencyPropertyChangedEventHandler IsEnabledChanged;
		public event DependencyPropertyChangedEventHandler IsHitTestVisibleChanged;
		public event DependencyPropertyChangedEventHandler IsKeyboardFocusedChanged;
		public event DependencyPropertyChangedEventHandler IsKeyboardFocusWithinChanged;
		public event DependencyPropertyChangedEventHandler IsMouseCapturedChanged;
		public event DependencyPropertyChangedEventHandler IsMouseCaptureWithinChanged;
		public event DependencyPropertyChangedEventHandler IsMouseDirectlyOverChanged;
		public event DependencyPropertyChangedEventHandler IsStylusCapturedChanged;
		public event DependencyPropertyChangedEventHandler IsStylusCaptureWithinChanged;
		public event DependencyPropertyChangedEventHandler IsStylusDirectlyOverChanged;
		public event DependencyPropertyChangedEventHandler IsVisibleChanged;
		public event KeyEventHandler KeyDown;
		public event KeyEventHandler KeyUp;
		public event EventHandler LayoutUpdated;
		public event RoutedEventHandler LostFocus;
		public event KeyboardFocusChangedEventHandler LostKeyboardFocus;
		public event MouseEventHandler LostMouseCapture;
		public event StylusEventHandler LostStylusCapture;
		public event MouseButtonEventHandler MouseDown;
		public event MouseEventHandler MouseEnter;
		public event MouseEventHandler MouseLeave;
		public event MouseButtonEventHandler MouseLeftButtonDown;
		public event MouseButtonEventHandler MouseLeftButtonUp;
		public event MouseEventHandler MouseMove;
		public event MouseButtonEventHandler MouseRightButtonDown;
		public event MouseButtonEventHandler MouseRightButtonUp;
		public event MouseButtonEventHandler MouseUp;
		public event MouseWheelEventHandler MouseWheel;
		public event DragEventHandler PreviewDragEnter;
		public event DragEventHandler PreviewDragLeave;
		public event DragEventHandler PreviewDragOver;
		public event DragEventHandler PreviewDrop;
		public event GiveFeedbackEventHandler PreviewGiveFeedback;
		public event KeyboardFocusChangedEventHandler PreviewGotKeyboardFocus;
		public event KeyEventHandler PreviewKeyDown;
		public event KeyEventHandler PreviewKeyUp;
		public event KeyboardFocusChangedEventHandler PreviewLostKeyboardFocus;
		public event MouseButtonEventHandler PreviewMouseDown;
		public event MouseButtonEventHandler PreviewMouseLeftButtonDown;
		public event MouseButtonEventHandler PreviewMouseLeftButtonUp;
		public event MouseEventHandler PreviewMouseMove;
		public event MouseButtonEventHandler PreviewMouseRightButtonDown;
		public event MouseButtonEventHandler PreviewMouseRightButtonUp;
		public event MouseButtonEventHandler PreviewMouseUp;
		public event MouseWheelEventHandler PreviewMouseWheel;
		public event QueryContinueDragEventHandler PreviewQueryContinueDrag;
		public event StylusButtonEventHandler PreviewStylusButtonDown;
		public event StylusButtonEventHandler PreviewStylusButtonUp;
		public event StylusDownEventHandler PreviewStylusDown;
		public event StylusEventHandler PreviewStylusInAirMove;
		public event StylusEventHandler PreviewStylusInRange;
		public event StylusEventHandler PreviewStylusMove;
		public event StylusEventHandler PreviewStylusOutOfRange;
		public event StylusSystemGestureEventHandler PreviewStylusSystemGesture;
		public event StylusEventHandler PreviewStylusUp;
		public event TextCompositionEventHandler PreviewTextInput;
		public event QueryContinueDragEventHandler QueryContinueDrag;
		public event QueryCursorEventHandler QueryCursor;
		public event StylusButtonEventHandler StylusButtonDown;
		public event StylusButtonEventHandler StylusButtonUp;
		public event StylusDownEventHandler StylusDown;
		public event StylusEventHandler StylusEnter;
		public event StylusEventHandler StylusInAirMove;
		public event StylusEventHandler StylusInRange;
		public event StylusEventHandler StylusLeave;
		public event StylusEventHandler StylusMove;
		public event StylusEventHandler StylusOutOfRange;
		public event StylusSystemGestureEventHandler StylusSystemGesture;
		public event StylusEventHandler StylusUp;
		public event TextCompositionEventHandler TextInput;

		public bool AllowDrop {
			get { return (bool)GetValue (AllowDropProperty); }
			set { SetValue (AllowDropProperty, value); }
		}

		public BitmapEffect BitmapEffect {
			get { return (BitmapEffect)GetValue (BitmapEffectProperty); }
			set { SetValue (BitmapEffectProperty, value); }
		}

		public bool Focusable {
			get { return (bool)GetValue (FocusableProperty); }
			set { SetValue (FocusableProperty, value); }
		}
		public bool HasAnimatedProperties {
			get { throw new NotImplementedException (); }
		}
		public bool IsArrangeValid {
			get { throw new NotImplementedException (); }
		}
		public bool IsEnabled {
			get { throw new NotImplementedException (); }
		}
		public bool IsEnabledCore {
			get { throw new NotImplementedException (); }
		}
		public bool IsFocused {
			get { throw new NotImplementedException (); }
		}
		public bool IsHitTestVisible {
			get { throw new NotImplementedException (); }
		}
		public bool IsInputMethodEnabled {
			get { throw new NotImplementedException (); }
		}
		public bool IsKeyboardFocused {
			get { throw new NotImplementedException (); }
		}
		public bool IsKeyboardFocusWithin {
			get { throw new NotImplementedException (); }
		}
		public bool IsMeasureValid {
			get { throw new NotImplementedException (); }
		}
		public bool IsMouseCaptured {
			get { throw new NotImplementedException (); }
		}
		public bool IsMouseCaptureWithin {
			get { throw new NotImplementedException (); }
		}
		public bool IsMouseDirectlyOver {
			get { throw new NotImplementedException (); }
		}
		public bool IsMouseOver {
			get { throw new NotImplementedException (); }
		}
		public bool IsStylusCaptured {
			get { throw new NotImplementedException (); }
		}
		public bool IsStylusCaptureWithin {
			get { throw new NotImplementedException (); }
		}
		public bool IsStylusDirectlyOver {
			get { throw new NotImplementedException (); }
		}
		public bool IsStylusOver {
			get { throw new NotImplementedException (); }
		}
		public bool IsVisible {
			get { throw new NotImplementedException (); }
		}

		public UIElement ()
		{
		}

		/* XXX tons of properties and Add*Handler/Remove*Handler calls */

		internal static void AddHandler (DependencyObject d, RoutedEvent routedEvent, Delegate handler)
		{
			if (d is UIElement)
				((UIElement)d).AddHandler (routedEvent, handler);
			else if (d is ContentElement)
				((ContentElement)d).AddHandler (routedEvent, handler);
			else if (d is IInputElement)
				((IInputElement)d).AddHandler (routedEvent, handler);
			else
				throw new ArgumentException (String.Format ("type '{0}' is not subclass of UIElement or ContentElement, and doesn't implement the IInputElement interface, so you can't add RoutedEvent handlers to it.", d.GetType()));
		}

		internal static void RemoveHandler (DependencyObject d, RoutedEvent routedEvent, Delegate handler)
		{
			if (d is UIElement)
				((UIElement)d).RemoveHandler (routedEvent, handler);
			else if (d is ContentElement)
				((ContentElement)d).RemoveHandler (routedEvent, handler);
			else if (d is IInputElement)
				((IInputElement)d).RemoveHandler (routedEvent, handler);
			else
				throw new ArgumentException (String.Format ("type '{0}' is not subclass of UIElement or ContentElement, and doesn't implement the IInputElement interface, so you can't add RoutedEvent handlers to it.", d.GetType()));
		}

		public void AddHandler (RoutedEvent routedEvent, Delegate handler)
		{
			throw new NotImplementedException ();
		}

		public void AddHandler (RoutedEvent routedEvent, Delegate handler, bool handledEventsToo)
		{
			throw new NotImplementedException ();
		}

		public void AddToEventRoute (EventRoute eventRoute, RoutedEventArgs args)
		{
			throw new NotImplementedException ();
		}

		public virtual void ApplyAnimationClock (DependencyProperty dp, AnimationClock clock)
		{
			throw new NotImplementedException ();
		}

		public virtual void ApplyAnimationClock (DependencyProperty dp, AnimationClock clock, HandoffBehavior handoffBehavior)
		{
			throw new NotImplementedException ();
		}

		public void Arrange (Rect finalRect)
		{
			throw new NotImplementedException ();
		}

		protected virtual void ArrangeCore (Rect finalRect)
		{
			throw new NotImplementedException ();
		}

		public virtual void BeginAnimation (DependencyProperty dp, AnimationTimeline animation, HandoffBehavior handoffBehavior)
		{
			throw new NotImplementedException ();
		}

		public virtual void BeginAnimation (DependencyProperty dp, AnimationTimeline animation)
		{
			throw new NotImplementedException ();
		}

		public bool CaptureMouse ()
		{
			throw new NotImplementedException ();
		}

		public bool CaptureStylus ()
		{
			throw new NotImplementedException ();
		}

		public bool Focus ()
		{
			throw new NotImplementedException ();
		}

		public object GetAnimationBaseValue (DependencyProperty dp)
		{
			throw new NotImplementedException ();
		}

		public void InvalidateArrange ()
		{
			throw new NotImplementedException ();
		}

		public void InvalidateMeasure ()
		{
			throw new NotImplementedException ();
		}

		public void InvalidateVisual ()
		{
			throw new NotImplementedException ();
		}

		public void Measure (Size availableSize)
		{
			throw new NotImplementedException ();
		}

		protected virtual Size MeasureCore (Size availableSize)
		{
			throw new NotImplementedException ();
		}

#if notyet
		public bool MoveFocus (TraversalRequest request)
		{
			throw new NotImplementedException ();
		}
#endif

		protected virtual void OnAccessKey (AccessKeyEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnChildDesiredSizeChanged (UIElement child)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnDragEnter (DragEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnDragLeave (DragEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnDragOver (DragEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnDrop (DragEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnGiveFeedback (GiveFeedbackEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnGotFocus (RoutedEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnGotKeyboardFocus (KeyboardFocusChangedEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnGotMouseCapture (MouseEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnGotStylusCapture (StylusEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnIsKeyboardFocusedChanged (DependencyPropertyChangedEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnIsKeyboardFocusWithinChanged (DependencyPropertyChangedEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnIsMouseCapturedChanged (DependencyPropertyChangedEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnIsMouseCaptureWithinChanged (DependencyPropertyChangedEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnIsMouseDirectlyOverChanged (DependencyPropertyChangedEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnIsStylusCapturedChanged (DependencyPropertyChangedEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnIsStylusCaptureWithinChanged (DependencyPropertyChangedEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnIsStylusDirectlyOverChanged (DependencyPropertyChangedEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnKeyDown (KeyEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnKeyUp (KeyEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnLostFocus (RoutedEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnLostKeyboardFocus (KeyboardFocusChangedEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnLostMouseCapture (MouseEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnLostStylusCapture (StylusEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnMouseDown (MouseButtonEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnMouseEnter (MouseEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnMouseLeave (MouseEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnMouseLeftButtonDown (MouseButtonEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnMouseLeftButtonUp (MouseButtonEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnMouseMove (MouseEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnMouseRightButtonDown (MouseButtonEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnMouseRightButtonUp (MouseButtonEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnMouseUp (MouseButtonEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnMouseWheel (MouseWheelEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnPreviewDragEnter (DragEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnPreviewDragLeave (DragEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnPreviewDragOver (DragEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnPreviewDrop (DragEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnPreviewGiveFeedback (GiveFeedbackEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnPreviewGotKeyboardFocus (KeyboardFocusChangedEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnPreviewKeyDown (KeyEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnPreviewKeyUp (KeyEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnPreviewLostKeyboardFocus (KeyboardFocusChangedEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnPreviewMouseDown (MouseButtonEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnPreviewMouseLeftButtonDown (MouseButtonEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnPreviewMouseLeftButtonUp (MouseButtonEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnPreviewMouseMove (MouseEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnPreviewMouseRightButtonDown (MouseButtonEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnPreviewMouseRightButtonUp (MouseButtonEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnPreviewMouseUp (MouseButtonEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnPreviewMouseWheel (MouseWheelEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnPreviewQueryContinueDrag (QueryContinueDragEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnPreviewStylusButtonDown (StylusButtonEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnPreviewStylusButtonUp (StylusButtonEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnPreviewStylusDown (StylusDownEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnPreviewStylusInAirMove (StylusEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnPreviewStylusInRange (StylusEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnPreviewStylusMove (StylusEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnPreviewStylusOutOfRange (StylusEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnPreviewStylusSystemGesture (StylusSystemGestureEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnPreviewStylusUp (StylusEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnPreviewTextInput (TextCompositionEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnQueryContinueDrag (QueryContinueDragEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnQueryCursor (QueryCursorEventArgs e)
		{
			throw new NotImplementedException ();
		}

#if notyet
		protected virtual void OnRender (DrawingContext drawingContext)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnRenderSizeChanged (SizeChangeInfo info)
		{
			throw new NotImplementedException ();
		}
#endif

		protected virtual void OnStylusButtonDown (StylusButtonEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnStylusButtonUp (StylusButtonEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnStylusDown (StylusDownEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnStylusEnter (StylusEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnStylusInAirMove (StylusEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnStylusInRange (StylusEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnStylusLeave (StylusEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnStylusMove (StylusEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnStylusOutOfRange (StylusEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnStylusSystemGesture (StylusSystemGestureEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnStylusUp (StylusEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected virtual void OnTextInput (TextCompositionEventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected internal override void OnVisualParentChanged (DependencyObject oldParent)
		{
			throw new NotImplementedException ();
		}

		protected virtual DependencyObject PredictFocus (FocusNavigationDirection direction)
		{
			throw new NotImplementedException ();
		}

		public void RaiseEvent (RoutedEventArgs e)
		{
			throw new NotImplementedException ();
		}

		public void ReleaseMouseCapture ()
		{
			throw new NotImplementedException ();
		}

		public void ReleaseStylusCapture ()
		{
			throw new NotImplementedException ();
		}

		public void RemoveHandler (RoutedEvent routedEvent, Delegate handler)
		{
			throw new NotImplementedException ();
		}

		public bool ShouldSerializeCommandBindings ()
		{
			throw new NotImplementedException ();
		}

		public bool ShouldSerializeInputBindings ()
		{
			throw new NotImplementedException ();
		}

		public Point TranslatePoint (Point point, UIElement relativeTo)
		{
			throw new NotImplementedException ();
		}

		public void UpdateLayout ()
		{
			throw new NotImplementedException ();
		}
	}
}
