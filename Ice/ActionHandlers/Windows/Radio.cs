// Copyright (c) Jonathan Thompson 2014

using System;
using System.Windows.Automation;

namespace Ice.ActionHandlers.Windows
{
    class Radio : BaseActionHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Radio"/> class.
        /// </summary>
        /// <param name="ae"></param>
        public Radio(AutomationElement ae)
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
        /// <param name="value">Set to true or do nothing.</param>
        protected override void DoAction(object value)
        {
            object selectionItemPattern;
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
            if (this.ae.TryGetCurrentPattern(SelectionItemPattern.Pattern, out selectionItemPattern))
            {
                // Select radio button if desired
                //
                if (desiredState)
                {
                    ((SelectionItemPattern)selectionItemPattern).Select();
                }
            }
        }

        /// <summary>
        /// Returns the radio's toggle state.
        /// </summary>
        /// <returns></returns>
        public override string Value
        {
            get
            {
                object selectionItemPattern;
                
                // Return the radio's toggle state
                //
                if (this.ae.TryGetCurrentPattern(SelectionItemPattern.Pattern, out selectionItemPattern))
                {
                    ((SelectionItemPattern)selectionItemPattern).Current.IsSelected.ToString();
                }

                return null;
            }
        }
    }
}
