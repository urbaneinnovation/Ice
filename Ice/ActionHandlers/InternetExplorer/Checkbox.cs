// Copyright (c) Jonathan Thompson 2014

using System;
using System.Runtime.InteropServices;

using mshtml;

namespace Ice.ActionHandlers.InternetExplorer
{
    public class Checkbox : BaseActionHandler
    {
        private IHTMLInputElement ihie;

        /// <summary>
        /// Initializes a new instance of the <see cref="Checkbox"/> class.
        /// </summary>
        /// <param name="ae"></param>
        public Checkbox(IHTMLElement ihe)
            : base(ihe)
        {
            this.ihie = (IHTMLInputElement)ihe;
        }

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
            bool checkedState;

            if (value is bool)
            {
                this.ihie.@checked = (bool)value;
            }
            else if (value is string)
            {
                if (!Boolean.TryParse((string)value, out checkedState))
                {
                    throw new InvalidCastException("Invalid argument for InternetExplorer.Checkbox ActionHandler");
                }
                else
                {
                    this.ihie.@checked = checkedState;
                }
            }
            else
            {
                throw new InvalidCastException("Invalid argument for InternetExplorer.Checkbox ActionHandler");
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
                return ihie.@checked.ToString();
            }
        }

        /// <summary>
        /// Releases any extra COM objects generated in this class' implementation.
        /// </summary>
        protected override void ReleaseCOMObjects()
        {
            Marshal.ReleaseComObject(this.ihie);
        }
    }
}
