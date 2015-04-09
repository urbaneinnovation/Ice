// Copyright (c) Jonathan Thompson 2014

using System;

namespace Ice.Exceptions
{
    // Thrown when an element is not found.
    public class ElementNotFoundException : Exception
    {
        public ElementNotFoundException()
            : base() { }

        public ElementNotFoundException(string message)
            : base(message) { }

        public ElementNotFoundException(string message, Exception inner)
            : base(message, inner) { }
    }
}
