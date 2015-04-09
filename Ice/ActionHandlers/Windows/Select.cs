// Copyright (c) Jonathan Thompson 2014

using System;
using System.Windows.Automation;

namespace Ice.ActionHandlers.Windows
{
    class Select : BaseActionHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Select"/> class.
        /// </summary>
        /// <param name="ae"></param>
        public Select(AutomationElement ae)
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
        /// Selects an item from a select drop-down.
        /// </summary>
        /// <param name="value">Value to select (int or string).</param>
        protected override void DoAction(object value)
        {
            object valuePattern;

            // Check input data type
            //
            if (value is int)
            {
                //TODO: Implement remainder of Select control.
            }
            else if (value is string)
            {
            }
            else
            {
                throw new ArgumentOutOfRangeException(value.ToString() + " out of range for select controls!");
            }
        }

        /// <summary>
        /// Returns a string representing the control's currently selected item.
        /// </summary>
        /// <returns></returns>
        public override string Value
        {
            get
            {
                object valuePattern;

                if (this.ae.TryGetCurrentPattern(ValuePattern.Pattern, out valuePattern))
                {
                    return ((ValuePattern)valuePattern).Current.Value;
                }

                return null;
            }
        }
    }
}
