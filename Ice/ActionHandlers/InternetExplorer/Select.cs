// Copyright (c) Jonathan Thompson 2014

using System;
using System.Runtime.InteropServices;

using mshtml;

namespace Ice.ActionHandlers.InternetExplorer
{
    class Select : BaseActionHandler
    {
        private IHTMLSelectElement ihse;
        private HTMLSelectElement hse;

        /// <summary>
        /// Initializes a new instance of the <see cref="Select"/> class.
        /// </summary>
        /// <param name="ae"></param>
        public Select(IHTMLElement ihe)
            : base(ihe)
        {
            this.ihse = (IHTMLSelectElement)ihe;
            this.hse = (HTMLSelectElement)ihe;
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
            if (value is string)
            {
                this.ihse.value = value.ToString();
                this.hse.FireEvent("onSelect", null);
            }
            else if (value is int)
            {
                this.ihse.selectedIndex = (int)value;
                this.hse.FireEvent("onSelect", null);
            }
            else
            {
                throw new InvalidCastException("Invalid argument for InternetExplorer.Select ActionHandler");
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
                return this.ihse.value;
            }
        }

        /// <summary>
        /// Releases any extra COM objects generated in this class' implementation.
        /// </summary>
        protected override void ReleaseCOMObjects()
        {
            Marshal.ReleaseComObject(this.ihse);
            Marshal.ReleaseComObject(this.hse);
        }
    }
}
