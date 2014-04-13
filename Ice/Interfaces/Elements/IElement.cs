// Copyright (c) Jonathan Thompson 2014

using System;
using System.Collections.Generic;

using Ice.Interfaces.ActionHandlers;
using Ice.Interfaces.DescendantHandlers;

namespace Ice.Interfaces.Elements
{
    public interface IElement
    {
        IElementActionHandler ActionHandler { get; }
        IElementDescendantHandler Descendants { get; }
    }
}
