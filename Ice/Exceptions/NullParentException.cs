// Copyright (c) Jonathan Thompson 2014

using System;

namespace Ice.Exceptions
{
    // Exception to be thrown when a child item is looked for from a null parent.
    public class NullParentException : Exception
    {
        public NullParentException()
            : base() { }

        public NullParentException(string message)
            : base(message) { }

        public NullParentException(string message, Exception inner)
            : base(message, inner) { }
    }
}
