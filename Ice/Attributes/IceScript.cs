// Copyright (c) Jonathan Thompson 2014

using System;

namespace Ice.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]

    // Class to mark methods to be used with Ice.
    public class IceScript : Attribute
    {
    }
}
