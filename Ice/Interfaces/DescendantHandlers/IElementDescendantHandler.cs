// Copyright (c) Jonathan Thompson 2014

using System;
using System.Collections.Generic;

using Ice.Interfaces.Elements;

namespace Ice.Interfaces.DescendantHandlers
{
    public interface IElementDescendantHandler
    {
        /// <summary>
        /// Gets all element children.
        /// </summary>
        /// <returns></returns>
        List<IElement> GetChildren();

        /// <summary>
        /// Gets all element descendants. If possible, use GetChild[ren]() or GetDescendant(string[]) instead.
        /// </summary>
        /// <returns></returns>
        List<IElement> GetDescendants();

        /// <summary>
        /// Gets the child at a particular index.
        /// </summary>
        /// <param name="whichChild"></param>
        /// <returns></returns>
        IElement GetChild(int whichChild);

        /// <summary>
        /// Gets a particular child based on the identifier name.
        /// </summary>
        /// <param name="automationID"></param>
        /// <returns></returns>
        IElement GetChild(string automationID);

        /// <summary>
        /// Gets a particular descendent based on a string array
        /// of identifier names.
        /// </summary>
        /// <param name="path">Identifier names</param>
        /// <returns></returns>
        IElement GetDescendant(string[] path);
    }
}
