// Copyright (c) Jonathan Thompson 2014

using System.Windows.Automation;

namespace Ice.ActionHandlers.Windows
{
    public class Window : BaseActionHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Window"/> class.
        /// </summary>
        /// <param name="ae"></param>
        public Window(AutomationElement ae)
            : base(ae) { }

        /// <summary>
        /// Calls PerformDefaultAction(null)
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
            // Do nothing
            //
        }

        /// <summary>
        /// Returns the window's title
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
