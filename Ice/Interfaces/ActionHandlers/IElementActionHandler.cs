// Copyright (c) Jonathan Thompson 2014

using System;
using System.Collections.Generic;
using System.Windows;

using Ice.Interfaces.Elements;

namespace Ice.Interfaces.ActionHandlers
{
    /// <summary>
    /// Contains methods used in the manipulation of controls.
    /// </summary>
    public interface IElementActionHandler
    {
        /// <summary>
        /// Invokes the element's default action.
        /// </summary>
        void PerformDefaultAction();

        /// <summary>
        /// Invokes the element's default action with args.
        /// </summary>
        /// <param name="value">Value to inject (if applicable).</param>
        void PerformDefaultAction(object value);

        /// <summary>
        /// Gets a clickable point for the element.
        /// </summary>
        /// <returns></returns>
        Point GetClickablePoint();

        /// <summary>
        /// The element's value. If "value" is undefined
        /// for an element, this returns the element's id.
        /// </summary>
        string Value { get; }

        /// <summary>
        /// Parameter identifying this element.
        /// </summary>
        /// <returns></returns>
        string Identifier { get; }

        /// <summary>
        /// Allows the user to get the base element wrapped by Ice in the event
        /// that the user needs it for functionality not implemented by Ice.
        /// </summary>
        /// <param name="element">Base element.</param>
        /// <returns></returns>
        Type ExposeCardinalElement(out object element);
    }
}
