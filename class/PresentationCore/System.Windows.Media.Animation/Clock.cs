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

using System.Windows;
using System.Windows.Threading;

namespace System.Windows.Media.Animation {

	public class Clock : DispatcherObject {
		Timeline timeline;

		public Clock (Timeline timeline)
		{
			this.timeline = timeline;
		}

		protected virtual void DiscontinuousTimeMovement ()
		{
			throw new NotImplementedException ();
		}

		protected virtual bool GetCanSlip ()
		{
			throw new NotImplementedException ();
		}

		protected virtual TimeSpan GetCurrentTimeCore ()
		{
			throw new NotImplementedException ();
		}

		protected virtual void SpeedChanged ()
		{
			throw new NotImplementedException ();
		}

		protected virtual void Stopped ()
		{
			throw new NotImplementedException ();
		}


		public ClockController Controller {
			get {
				throw new NotImplementedException ();
			}
		}

		public Nullable<double> CurrentGlobalSpeed {
			get {
				throw new NotImplementedException ();
			}
		}

		protected TimeSpan CurrentGlobalTime {
			get {
				throw new NotImplementedException ();
			}
		}

		public Nullable<int> CurrentIteration {
			get {
				throw new NotImplementedException ();
			}
		}

		public Nullable<double> CurrentProgress {
			get {
				throw new NotImplementedException ();
			}
		}
		public ClockState CurrentState {
			get {
				throw new NotImplementedException ();
			}
		}
		public Nullable<TimeSpan> CurrentTime { 
			get {
				throw new NotImplementedException ();
			}
		}
		public bool HasControllableRoot {
			get {
				throw new NotImplementedException ();
			}
		}
		public bool IsPaused {
			get {
				throw new NotImplementedException ();
			}
		}
		public Duration NaturalDuration {
			get {
				throw new NotImplementedException ();
			}
		}
		public Clock Parent {
			get {
				throw new NotImplementedException ();
			}
		}
		public Timeline Timeline {
			get { return timeline; }
		}

		public event EventHandler Completed;
		public event EventHandler CurrentGlobalSpeedInvalidated;
		public event EventHandler CurrentStateInvalidated;
		public event EventHandler CurrentTimeInvalidated;
		public event EventHandler RemoveRequested;
	}

}
