// Copyright (c) Jonathan Thompson 2014

using System;

namespace Ice.Exceptions
{
    public class WindowNotFoundException : Exception
    {
        public WindowNotFoundException()
            : base() { }

        public WindowNotFoundException(string message)
            : base(message) { }

        public WindowNotFoundException(string message, Exception inner)
            : base(message, inner) { }
    }
}
