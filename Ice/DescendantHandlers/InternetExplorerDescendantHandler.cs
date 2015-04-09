// Copyright (c) Jonathan Thompson 2014

using System;
using System.Collections.Generic;

using mshtml;

using Ice.Interfaces.DescendantHandlers;
using Ice.Interfaces.Elements;

namespace Ice.DescendantHandlers
{
    internal class InternetExplorerDescendantHandler : IElementDescendantHandler
    {
        private IHTMLElement ihe;

        /// <summary>
        /// Initializes an instance of the <see cref="InternetExplorerDescendantHandler"/> class.
        /// </summary>
        /// <param name="doc">DOM object</param>
        internal InternetExplorerDescendantHandler(IHTMLElement ihe)
        {
            this.ihe = ihe;
        }

        // Get the element's children
        //
        public List<IElement> GetChildren()
        {
            //TODO: Implement GetChildren() for IE.
            return null;
        }

        // Get the element's descendants
        //
        public List<IElement> GetDescendants()
        {
            //TODO: Implement GetDescendants() for IE.
            return null;
        }

        // Get the child element at a certain index
        //
        public IElement GetChild(int whichChild)
        {
            //TODO: Implement GetChild(int) for IE.
            return null;
        }

        // Get the child element by its Name/Id
        //
        public IElement GetChild(string identifier)
        {
            //TODO: Implement GetChild(string) for IE.
            return null;
        }

        // Gets a descendant provided an ancestor tree.
        //
        public IElement GetDescendant(string[] ancestors)
        {
            //TODO: Implement GetDescendant(string[]) for IE.
            return null;
        }
    }
}
