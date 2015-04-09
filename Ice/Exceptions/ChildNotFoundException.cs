// Copyright (c) Jonathan Thompson 2014

using System;

namespace Ice.Exceptions
{
    public class ChildNotFoundException : Exception
    {
        public ChildNotFoundException()
            : base() { }

        public ChildNotFoundException(string message)
            : base(message) { }

        public ChildNotFoundException(string message, Exception inner)
            : base(message, inner) { }
    }
}
