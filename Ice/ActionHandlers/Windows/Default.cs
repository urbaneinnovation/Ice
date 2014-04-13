// Copyright (c) Jonathan Thompson 2014

using System.Windows.Automation;

namespace Ice.ActionHandlers.Windows
{
    class Default : BaseActionHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Default"/> class.
        /// </summary>
        /// <param name="ae"></param>
        public Default(AutomationElement ae)
            : base(ae)
        { }

        /// <summary>
        /// Calls PerformDefaultAction(object)
        /// </summary>
        protected override void DoAction()
        {
            DoAction(null);
        }

        /// <summary>
        /// Default action is to invoke (click) this control.
        /// </summary>
        /// <param name="value">Not used.</param>
        protected override void DoAction(object value)
        {
            object invokePattern;

            // If the element supports InvokePattern, then call Invoke()
            //
            if (this.ae.TryGetCurrentPattern(InvokePattern.Pattern, out invokePattern))
            {
                ((InvokePattern)invokePattern).Invoke();
            }
        }

        /// <summary>
        /// Returns the control's name.
        /// </summary>
        /// <returns></returns>
        public override string Value
        {
            get
            {
                return this.ae.Current.Name;
            }
        }
    }
}
