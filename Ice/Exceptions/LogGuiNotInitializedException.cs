// Copyright (c) Jonathan Thompson 2014

using System;

namespace Ice.Exceptions
{
    public class LogGuiNotInitializedException : Exception
    {
        public LogGuiNotInitializedException()
            : base() { }

        public LogGuiNotInitializedException(string message)
            : base(message) { }

        public LogGuiNotInitializedException(string message, Exception inner)
            : base(message, inner) { }
    }
}
