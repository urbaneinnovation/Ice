// Copyright (c) Jonathan Thompson 2014

using mshtml;

namespace Ice.ActionHandlers.InternetExplorer
{
    class Button : BaseActionHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Button"/> class.
        /// </summary>
        /// <param name="ae"></param>
        public Button(IHTMLElement ihe)
            : base(ihe)
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
            this.ihe.click();
        }

        /// <summary>
        /// Returns the button's text
        /// </summary>
        /// <returns></returns>
        public override string Value
        {
            get
            {
                return "";
            }
        }

        /// <summary>
        /// Releases any extra COM objects generated in this class' implementation.
        /// </summary>
        protected override void ReleaseCOMObjects()
        {
            //TODO: Finish implementing Button
        }
    }
}
