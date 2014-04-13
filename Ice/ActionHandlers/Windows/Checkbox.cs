// Copyright (c) Jonathan Thompson 2014

using System;
using System.Windows.Automation;

namespace Ice.ActionHandlers.Windows
{
    class Checkbox : BaseActionHandler
    {
        bool currentToggleState;

        /// <summary>
        /// Initializes a new instance of the <see cref="Checkbox"/> class.
        /// </summary>
        /// <param name="ae"></param>
        public Checkbox(AutomationElement ae)
            : base(ae)
        { }

        /// <summary>
        /// Sets the control to true.
        /// </summary>
        protected override void DoAction()
        {
            DoAction(true);
        }

        /// <summary>
        /// Default action is to toggle this control.
        /// </summary>
        /// <param name="value">Set to true or false</param>
        protected override void DoAction(object value)
        {
            object togglePattern;
            bool desiredState;

            // Check if input is a bool
            //
            if (value is bool)
            {
                desiredState = (bool)value;
            }
            // Check if input is a string parseable as a bool
            //
            else if (value is string)
            {
                if (!Boolean.TryParse((string)value, out desiredState))
                {
                    throw new ArgumentOutOfRangeException(value.ToString() + " out of range for checkboxes!");
                }
            }
            // Otherwise, throw an exception.
            //
            else
            {
                throw new ArgumentOutOfRangeException(value.ToString() + " out of range for checkboxes!");
            }

            // If the passed value was determinable, toggle as appropriate.
            //
            if (this.ae.TryGetCurrentPattern(TogglePattern.Pattern, out togglePattern))
            {
                currentToggleState = ((TogglePattern)togglePattern).Current.ToggleState.Equals(ToggleState.On);

                if (currentToggleState != desiredState)
                {
                    ((TogglePattern)togglePattern).Toggle();
                }
            }
        }

        /// <summary>
        /// Returns the checkbox's toggle state.
        /// </summary>
        /// <returns></returns>
        public override string Value
        {
            get
            {
                object togglePattern;

                if (this.ae.TryGetCurrentPattern(TogglePattern.Pattern, out togglePattern))
                {
                    return ((TogglePattern)togglePattern).Current.ToggleState.Equals(ToggleState.On).ToString();
                }

                return null;
            }
        }
    }
}
