// Copyright (c) Jonathan Thompson 2014

using System;
using System.Runtime.InteropServices;

using mshtml;

namespace Ice.ActionHandlers.InternetExplorer
{
    class Text : BaseActionHandler
    {
        private IHTMLInputElement ihie;
        private IHTMLElement3 ihe3;

        /// <summary>
        /// Initializes a new instance of the <see cref="Text"/> class.
        /// </summary>
        /// <param name="ae"></param>
        public Text(IHTMLElement ihe)
            : base(ihe)
        {
            this.ihie = (IHTMLInputElement)ihe;
            this.ihe3 = (IHTMLElement3)ihe;
        }

        /// <summary>
        /// Calls PerformDefaultAction(object) to send nothing to the control.
        /// </summary>
        protected override void DoAction()
        {
            //DoAction(null);
        }

        /// <summary>
        /// Send new text to the control
        /// </summary>
        /// <param name="value"></param>
        protected override void DoAction(object value)
        {
            if (value is string)
            {
                this.ihie.value = value.ToString();
                this.ihe3.FireEvent("onChange", null);
            }
            else
            {
                throw new InvalidCastException("Invalid argument for InternetExplorer.Text ActionHandler");
            }
        }

        /// <summary>
        /// Returns the text control's contents
        /// </summary>
        /// <returns></returns>
        public override string Value
        {
            get
            {
                return this.ihie.value;
            }
        }

        /// <summary>
        /// Releases any extra COM objects generated in this class' implementation.
        /// </summary>
        protected override void ReleaseCOMObjects()
        {
            Marshal.ReleaseComObject(this.ihie);
            Marshal.ReleaseComObject(this.ihe3);
        }
    }
}
