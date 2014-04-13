// Copyright (c) Jonathan Thompson 2014

using System.Windows.Automation;

using mshtml;

using Ice.Interfaces.DescendantHandlers;

namespace Ice.Factories
{
    /// <summary>
    /// Factory to build IElementDescendantHandler instances.
    /// </summary>
    internal static class ElementDescendantHandlerFactory
    {
        /// <summary>
        /// Builds an IElementDescendantHandler based on the provided AutomationElement.
        /// </summary>
        /// <param name="ae">Instance of AutomationElement on which the DescendantHandler is built.</param>
        /// <returns>Instance of IElementDescendantHandler</returns>
        internal static IElementDescendantHandler GetDescendantHandler(AutomationElement ae)
        {
            return new DescendantHandlers.WindowsDescendantHandler(ae);
        }

        /// <summary>
        /// Builds an IElementDescendantHandler based on on the provided HTMLDocument.
        /// </summary>
        /// <param name="doc">Instance of HTMLDocument on which the DescendantHandler is built.</param>
        /// <returns>Instance of IElementDescendantHandler</returns>
        internal static IElementDescendantHandler GetDescendantHandler(IHTMLElement ihe)
        {
            return new DescendantHandlers.InternetExplorerDescendantHandler(ihe);
        }
    }
}
