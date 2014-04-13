// Copyright (c) Jonathan Thompson 2014

using System;
using System.Runtime.InteropServices;
using System.Windows;

using mshtml;

using Ice.Interfaces.ActionHandlers;

namespace Ice.ActionHandlers.InternetExplorer
{
    public abstract class BaseActionHandler : IElementActionHandler
    {
        // Fundamental automation element
        //
        protected IHTMLElement ihe;
        protected string identifier;

        // Abstract methods left to be implemented by subclasses.
        //
        protected abstract void DoAction();
        protected abstract void DoAction(object value);
        protected abstract void ReleaseCOMObjects();
        public abstract string Value { get; }

        /// <summary>
        /// Constructor for the <see cref="BaseActionHandler"/> class.
        /// </summary>
        /// <param name="ihe"></param>
        protected BaseActionHandler(IHTMLElement ihe)
        {
            this.ihe = ihe;
        }

        // Calls the DoAction() method to perform the appropriate control action. Also inserts a pausable
        // point in the code.
        //
        public void PerformDefaultAction()
        {
            ScriptControl.AddPausePoint();
            this.DoAction();
        }

        // Calls the DoAction(object) method to perform the appropriate control action. Also inserts a
        // pausable point in the code.
        //
        public void PerformDefaultAction(object value)
        {
            ScriptControl.AddPausePoint();
            this.DoAction(value);
        }

        // Gets a clickable point for the IHTMLElement.
        //
        public Point GetClickablePoint()
        {
            int xPos = this.ihe.offsetLeft;
            int yPos = this.ihe.offsetTop;

            IHTMLElement parent = this.ihe.offsetParent;

            // Use recursion to successively get offset from parent element.
            //
            while (parent != null)
            {
                IHTMLElement tmp;
                xPos += parent.offsetLeft; // Increment offset
                yPos += parent.offsetTop;  // Increment offset

                // Get new parent
                //
                tmp = parent;
                parent = parent.offsetParent;

                // Release old parent (now child)
                //
                Marshal.ReleaseComObject(tmp);
            }

            Marshal.ReleaseComObject(parent);
            return new Point(xPos + 2, yPos + 2);
        }

        // Returns the element's .id attribute.
        //
        public string Identifier
        {
            get
            {
                return this.identifier;
            }
        }

        // Returns typeof(IHTMLElement)
        //
        public Type ExposeCardinalElement(out object element)
        {
            element = this.ihe;
            return this.ihe.GetType();
        }

        #region IDisposable Members

        private bool disposed = false;

        // Dispose function
        //
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                // release managed resources
            }

            // release unmanaged resources
            Marshal.ReleaseComObject(this.ihe);
            this.ReleaseCOMObjects();
            
            GC.SuppressFinalize(this);
        }

        // Override IDisposable.Dispose()
        //
        public void Dispose()
        {
            if (!disposed)
            {
                Dispose(true);
                disposed = true;
            }
        }

        // Finalizer
        //
        ~BaseActionHandler()
        {
            Dispose(false);
        }

        #endregion
    }
}
