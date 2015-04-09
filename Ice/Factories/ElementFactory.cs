// Copyright (c) Jonathan Thompson 2014

using System.Windows.Automation;

using mshtml;

using Ice.Interfaces.Elements;
using Ice.Interfaces.ActionHandlers;
using Ice.Interfaces.DescendantHandlers;
using Ice.Elements;

namespace Ice.Factories
{
    /// <summary>
    /// Class responsible for generating elements for GUI manipulation.
    /// </summary>
    internal static class ElementFactory
    {
        /// <summary>
        /// Get a new IElement from a base AutomationElement.
        /// </summary>
        /// <param name="ae">Base AutomationElement</param>
        /// <returns>An implementation of IElement.</returns>
        internal static IElement GetIElementFromBase(AutomationElement ae)
        {
            return GetIElementFromHandlers(
                Factories.ElementActionHandlerFactory.GetActionHandler(ae),
                Factories.ElementDescendantHandlerFactory.GetDescendantHandler(ae)
            );
        }

        /// <summary>
        /// Get a new IElement from a base IHTMLElement.
        /// </summary>
        /// <param name="ihe">Base IHTMLElement</param>
        /// <returns>An implementation of IElement.</returns>
        internal static IElement GetIElementFromBase(IHTMLElement ihe)
        {
            return GetIElementFromHandlers(
                Factories.ElementActionHandlerFactory.GetActionHandler(ihe),
                Factories.ElementDescendantHandlerFactory.GetDescendantHandler(ihe)
            );
        }

        /// <summary>
        /// Get a new IElement from injected <see cref="IElementActionHandler"/> and
        /// <see cref="IElementDescendantHandler"/> instances.
        /// </summary>
        /// <param name="actionHandler">Instance of <see cref="IElementActionHandler"/></param>
        /// <param name="descendantHandler">Instance of <see cref="IElementDescendantHandler"/></param>
        /// <returns></returns>
        internal static IElement GetIElementFromHandlers(IElementActionHandler actionHandler,
             IElementDescendantHandler descendantHandler)
        {
            return new IceElement(actionHandler, descendantHandler);
        }
    }
}
