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
// Authors:
//
//	Copyright (C) 2006 Jordi Mas i Hernandez <jordimash@gmail.com>
//

using System;
using System.IO;
using System.ComponentModel;
using System.Collections.Generic;
using System.Workflow.ComponentModel;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

#if RUNTIME_DEP
using System.Workflow.Runtime;
#endif

namespace System.Workflow.ComponentModel
{
	[Serializable]
	public class Activity : DependencyObject
	{
		private static DependencyProperty NameProperty;
		private static DependencyProperty DescriptionProperty;
		private static DependencyProperty EnabledProperty;
		private static DependencyProperty ExecutionResultProperty;
		private static DependencyProperty ExecutionStatusProperty;
		private Guid workflow_id;
		private Queue <Activity> exec_activities;
		private bool needs_exec;
		private Activity parallel_parent;
		private CompositeActivity parent;

		static Activity ()
		{
			NameProperty = DependencyProperty.Register ("Name", typeof (string),
				typeof (Activity), new PropertyMetadata ());

			DescriptionProperty = DependencyProperty.Register ("Description", typeof (string),
				typeof (Activity), new PropertyMetadata ());

			EnabledProperty = DependencyProperty.Register ("Enabled", typeof (bool),
				typeof (Activity), new PropertyMetadata ());

			ExecutionResultProperty = DependencyProperty.Register ("ActivityExecutionResult", typeof (ActivityExecutionResult),
				typeof (Activity), new PropertyMetadata ());

			ExecutionStatusProperty = DependencyProperty.Register ("ActivityExecutionStatus", typeof (ActivityExecutionStatus),
				typeof (Activity), new PropertyMetadata ());

		#if !RUNTIME_DEP
			Console.WriteLine ("*** Warning: You are using a version of System.Workflow.ComponentModel");
			Console.WriteLine ("*** library built without System.Workflow.Runtime dependencies");
			Console.WriteLine ("*** You should use a version built with the Runtime dependencies");
		#endif
		}

		// Constructors
		public Activity ()
		{
			Init ();
			Name = GetType().Name;
		}

		public Activity (string name)
		{
			Init ();
			Name = name;
		}

		private void Init ()
		{
			exec_activities = new Queue <Activity> ();
			Enabled = true;
			needs_exec = true;
			parallel_parent = null;
			Description = string.Empty;
			SetValue (ExecutionResultProperty, ActivityExecutionResult.None);
			SetValue (ExecutionStatusProperty, ActivityExecutionStatus.Initialized);
		}

		// Properties
		public CompositeActivity Parent {
			get {
				return parent;
			}
		}

      		public string Description {
      			get {
				return (string) GetValue (DescriptionProperty);

      			}
      			set {
				SetValue (DescriptionProperty, value);
      			}
      		}

      		public bool Enabled {
      			get {
				return (bool) GetValue (EnabledProperty);

      			}
      			set {
				SetValue (EnabledProperty, value);
      			}
      		}

      		public ActivityExecutionResult ExecutionResult {
      			get {
				return (ActivityExecutionResult) GetValue (ExecutionResultProperty);
      			}
      		}
      		public ActivityExecutionStatus ExecutionStatus {
      			get {
				return (ActivityExecutionStatus) GetValue (ExecutionStatusProperty);
      			}
      		}

		[MonoTODO]
      		public bool IsDynamicActivity {
      			get {return false;}
      		}

      		public string Name {
      			get {
				return (string) GetValue (Activity.NameProperty);

      			}
      			set {
				SetValue (Activity.NameProperty, value);
      			}
      		}

		public string QualifiedName {
			get {
				return Name;
			}
		}

		protected Guid WorkflowInstanceId {
		 	get {
		 		return workflow_id;
		 	}
		}

		// Private properties
		// TODO: This breaks API compatibility
		public Queue <Activity> ActivitiesToExecute {
			get {
				return exec_activities;
			}
		}

		// TODO: This breaks API compatibility
		public bool NeedsExecution {
			get {
				return needs_exec;
			}
			set {
				needs_exec = value;
			}
		}

		// TODO: This breaks API compatibility
		public Activity ParallelParent {
			get {
				return parallel_parent;
			}
			set {
				parallel_parent = value;
			}
		}

		// Methods
		[MonoTODO]
		public Activity Clone ()
		{
			throw new NotImplementedException ();
		}

		protected internal virtual ActivityExecutionStatus Cancel (ActivityExecutionContext executionContext)
		{
			return ActivityExecutionStatus.Canceling;
		}

		protected internal virtual ActivityExecutionStatus Execute (ActivityExecutionContext executionContext)
		{
			needs_exec = false;
			return ActivityExecutionStatus.Closed;
		}

		protected internal virtual void Initialize (IServiceProvider provider)
		{

		}

		public Activity GetActivityByName (string activityQualifiedName)
		{
			return GetActivityByName (activityQualifiedName, true);
		}

		public Activity GetActivityByName (string activityQualifiedName, bool withinThisActivityOnly)
		{
			List <Activity> list = new List <Activity> ();
			Activity current;

			if (withinThisActivityOnly) {
				current = this;
			} else {
				current = GetRootActivity ();
			}

			while (current != null) {
				if (current.Name.Equals (activityQualifiedName)) {
					return current;
				}

				if (IsBasedOnType (current, typeof (CompositeActivity))) {
					CompositeActivity  composite = (CompositeActivity) current;
					foreach (Activity activity in composite.Activities) {
						list.Add (activity);
					}
				}

				if (list.Count == 0) {
					break;
				}

				current = list [0];
				list.Remove (current);
			}

			return null;
		}

		protected internal virtual ActivityExecutionStatus HandleFault (ActivityExecutionContext executionContext,	Exception exception)
		{
			  return ActivityExecutionStatus.Closed;
		}

		//public void RegisterForStatusChange (DependencyProperty dependencyProp, IActivityEventListener<ActivityExecutionStatusChangedEventArgs> activityStatusChangeListener)

		protected internal virtual void OnActivityExecutionContextLoad (IServiceProvider provider)
		{

		}

		public override string ToString ()
		{
			return Name + " [" + base.ToString ()+ "]";
		}

		//public void UnregisterForStatusChange (DependencyProperty dependencyProp, IActivityEventListener<ActivityExecutionStatusChangedEventArgs> activityStatusChangeListener)

		// Private methods
		internal void SetParent (CompositeActivity actity)
		{
			parent = actity;
		}

		protected IComparable SetTimer (ActivityExecutionContext executionContext, DateTime expiresAt)
		{
		#if RUNTIME_DEP
			TimerEventSubscription te;
			WorkflowQueue queue;

			te = new TimerEventSubscription (executionContext.ExecutionContextManager.Workflow.InstanceId,
				expiresAt);

			WorkflowQueuingService qService = executionContext.GetService <WorkflowQueuingService> ();
		    	queue = qService.CreateWorkflowQueue (te.QueueName, true);
		    	queue.QueueItemArrived += OnQueueTimerItemArrived;
			executionContext.ExecutionContextManager.Workflow.TimerEventSubscriptionCollection.Add (te);
			return te.QueueName;
		#else
			return null;
		#endif

		}

		// TODO: This breaks API compatibility
		virtual protected void OnQueueTimerItemArrived (Object sender, object args)
		{

		}

		public ActivityExecutionStatus ExecuteInternal (ActivityExecutionContext executionContext)
		{
			return Execute (executionContext);
		}

		public void InitializeInternal (IServiceProvider provider)
		{
			Initialize (provider);
		}

		public void SetWorkflowInstanceId (Guid guid)
		{
		 	workflow_id = guid;
		}

		// Private methods
		static internal bool IsBasedOnType (object obj, Type target)
		{
			for (Type type = obj.GetType (); type != null; type = type.BaseType) {
				if (type == target) {
					return true;
				}
			}

			return false;
		}

		internal Activity GetRootActivity ()
		{
			Activity activity = this;

			while (activity.Parent != null) {
				activity = activity.Parent;
			}

			return activity;
		}
		
		/// <summary>
		/// Save activity to the given stream
		/// </summary>
		/// <param name="stream">stream to store data</param>
		public void Save (Stream stream) {
			BinaryFormatter formatter = new BinaryFormatter ();
			Save (stream, formatter);
		}

		/// <summary>
		/// Save activity to the given stream using given formmatter
		/// </summary>
		/// <param name="stream">stream to store data</param>
		/// <param name="formatter">formatter to be used during serialization</param>
		public void Save (Stream stream, IFormatter formatter) {
			if (stream == null)
				throw new ArgumentNullException ("stream");
			if (formatter == null)
				throw new ArgumentNullException ("formatter");
			formatter.Serialize (stream, this);
		}

		/// <summary>
		/// Load activity from the given stream
		/// </summary>
		/// <param name="stream">stream with serialized activity</param>
		/// <returns>deserialized activity</returns>
		public static Activity Load (Stream stream, Activity outerActivity) {
			BinaryFormatter formatter = new BinaryFormatter ();
			return Load (stream, outerActivity, formatter);
		}

		/// <summary>
		/// Load activity from the given stream
		/// </summary>
		/// <param name="stream">stream with serialized activity</param>
		/// <param name="formatter">formatter to be used during deserialization</param>
		/// <returns>deserialized activity</returns>
		public static Activity Load (Stream stream, Activity outerActivity, IFormatter formatter) {
			if (stream == null)
				throw new ArgumentNullException ("stream");
			if (formatter == null)
				throw new ArgumentNullException ("formatter");
			return (Activity)formatter.Deserialize (stream);
		}
	}
}
