// Copyright (c) Jonathan Thompson 2014

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Automation;

using Ice.Exceptions;
using Ice.Interfaces.ActionHandlers;
using Ice.Interfaces.Elements;

namespace Ice.ActionHandlers.Windows
{
    /// <summary>
    /// Base class to implement functionality common to all Ice Elements
    /// </summary>
    public abstract class BaseActionHandler : IElementActionHandler
    {
        // Fundamental automation element
        //
        protected AutomationElement ae;
        protected string identifier;

        // Abstract methods left to be implemented by subclasses.
        //
        protected abstract void DoAction();
        protected abstract void DoAction(object value);
        public abstract string Value { get; }

        /// <summary>
        /// Constructor for <see cref="BaseActionHandler"/> class.
        /// </summary>
        /// <param name="ae">Injected element</param>
        protected BaseActionHandler(AutomationElement ae)
        {
            this.ae = ae;
            this.identifier = (ae.Current.AutomationId == null) 
                ? ae.Current.Name 
                : ae.Current.AutomationId;
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

        // Returns a clickable point for the element. If not found, returns the lower-right boundary
        // of the element.
        //
        public Point GetClickablePoint()
        {
            Point pt;

            if (this.ae.TryGetClickablePoint(out pt))
            {
                return pt;
            }
            else
            {
                return this.ae.Current.BoundingRectangle.BottomRight;
            }
        }

        // Gets the identifer unique to the element's sibling group.
        //
        public string Identifier
        {
            get
            {
                return this.identifier;
            }
        }

        // Returns typeof(AutomationElement)
        //
        public Type ExposeCardinalElement(out object element)
        {
            element = this.ae;
            return this.ae.GetType();
        }
    }
}
