// Copyright (c) Jonathan Thompson 2014

using System;
using System.Windows.Automation;
using System.Windows.Forms;

namespace Ice.ActionHandlers.Windows
{
    class Text : BaseActionHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Text"/> class.
        /// </summary>
        /// <param name="ae"></param>
        public Text(AutomationElement ae)
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
        /// Inserts text into a text control.
        /// </summary>
        /// <param name="value">Value to inject.</param>
        protected override void DoAction(object value)
        {
            object valuePattern;

            if (this.ae.TryGetCurrentPattern(ValuePattern.Pattern, out valuePattern))
            {
                // Try to set via ValuePattern
                //
                ((ValuePattern)valuePattern).SetValue(value.ToString());
            }
            else if (this.ae.Current.IsKeyboardFocusable)
            {
                // Set through keyboard robot
                //
                SendKeys.SendWait("^{HOME}");
                SendKeys.SendWait("^+{END}");
                SendKeys.SendWait("^{DEL}");
                SendKeys.SendWait(value.ToString());
            }
        }

        /// <summary>
        /// Returns a string representing the control's current text.
        /// </summary>
        /// <returns></returns>
        public override string Value
        {
            get
            {
                object textPattern;

                if (this.ae.TryGetCurrentPattern(ValuePattern.Pattern, out textPattern))
                {
                    // Try to get text via ValuePattern
                    //
                    return ((ValuePattern)textPattern).Current.Value;
                }
                else if (this.ae.TryGetCurrentPattern(TextPattern.Pattern, out textPattern))
                {
                    // Try to get text via TextPattern
                    //
                    return ((TextPattern)this.ae.GetCurrentPattern(TextPattern.Pattern)).DocumentRange.GetText(-1);
                }
                else
                {
                    // Return null as default
                    //
                    return null;
                }
            }
        }
    }
}
