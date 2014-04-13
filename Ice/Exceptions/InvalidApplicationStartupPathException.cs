// Copyright (c) Jonathan Thompson 2014

using System;

namespace Ice.Exceptions
{
    public class InvalidApplicationStartupPathException : Exception
    {
        public InvalidApplicationStartupPathException()
            : base() { }

        public InvalidApplicationStartupPathException(string message)
            : base(message) { }

        public InvalidApplicationStartupPathException(string message, Exception inner)
            : base(message, inner) { }
    }
}
