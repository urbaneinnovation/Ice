// Copyright (c) Jonathan Thompson 2014

using System;
using System.Collections.Generic;
using System.Windows.Automation;

namespace Ice.ActionHandlers.Windows
{
    public class Button : BaseActionHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Button"/> class.
        /// </summary>
        /// <param name="ae"></param>
        public Button(AutomationElement ae)
            : base(ae)
        { }

        /// <summary>
        /// Calls PerformDefaultAction(object) to click the control.
        /// </summary>
        protected override void DoAction()
        {
            DoAction(null);
        }

        /// <summary>
        /// Default action is to invoke (click) this control.
        /// </summary>
        /// <param name="value"></param>
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
        /// Returns the button's text
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
