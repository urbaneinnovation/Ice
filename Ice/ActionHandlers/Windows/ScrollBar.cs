// Copyright (c) Jonathan Thompson 2014

using System;
using System.Text.RegularExpressions;
using System.Windows.Automation;

namespace Ice.ActionHandlers.Windows
{
    class ScrollBar : BaseActionHandler
    {
        private static Regex scrollBarRangeRegex = new Regex("[0-100]");

        /// <summary>
        /// Initializes a new instance of the <see cref="ScrollBar"/> class.
        /// </summary>
        /// <param name="ae"></param>
        public ScrollBar(AutomationElement ae)
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
        /// Sets the scroll bar to a given percent between 0 - 100.
        /// </summary>
        /// <param name="value">Percent to scroll (int)</param>
        protected override void DoAction(object value)
        {
            object valuePattern;
            string newValue = value.ToString();

            // Check that @value is within range
            //
            if (value is int)
            {
                if ((int)value < 0 || (int)value > 100)
                {
                    throw new ArgumentOutOfRangeException(newValue + " out of range for scrollbars!");
                }
            }
            else if (value is string)
            {
                if (!scrollBarRangeRegex.IsMatch(newValue))
                {
                    throw new ArgumentOutOfRangeException(newValue + " out of range for scrollbars!");
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException(newValue + " out of range for scrollbars!");
            }

            // If the passed object is a string or int within range, do appropriate things!
            //
            if (this.ae.TryGetCurrentPattern(ValuePattern.Pattern, out valuePattern))
            {
                ((ValuePattern)valuePattern).SetValue(newValue);
            }
        }

        /// <summary>
        /// Returns the control's current percentage set.
        /// </summary>
        /// <returns></returns>
        public override string Value
        {
            get
            {
                object valuePattern;

                // If the passed object is a string or int, do appropriate things!
                //
                if (this.ae.TryGetCurrentPattern(ValuePattern.Pattern, out valuePattern))
                {
                    return ((ValuePattern)valuePattern).Current.Value;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
