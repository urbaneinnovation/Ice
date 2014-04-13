// Copyright (c) Jonathan Thompson 2014

using System;
using System.Collections.Generic;

using Ice.Interfaces.Elements;

namespace Ice.Interfaces.DescendantHandlers
{
    public interface IElementDescendantHandler
    {
        /// <summary>
        /// Gets all of the element's children.
        /// </summary>
        /// <returns>The element's children</returns>
        List<IElement> GetChildren();

        /// <summary>
        /// Gets all element descendants. If possible, use GetChild[ren]() or GetDescendant(string[]) instead.
        /// </summary>
        /// <returns>A list of the element's descendants</returns>
        List<IElement> GetDescendants();

        /// <summary>
        /// Gets the child at a particular index.
        /// </summary>
        /// <param name="whichChild">Identifying index</param>
        /// <returns>The element's child at a particular index</returns>
        IElement GetChild(int whichChild);

        /// <summary>
        /// Gets a particular child based on the identifying name.
        /// </summary>
        /// <param name="automationID">Identifying name</param>
        /// <returns>The element's child which matches the identifying name</returns>
        IElement GetChild(string automationID);

        /// <summary>
        /// Gets a particular descendent based on a string array
        /// of identifying names.
        /// </summary>
        /// <param name="path">Identifying names</param>
        /// <returns>The element's child matching the identifying names</returns>
        IElement GetDescendant(string[] path);
    }
}
