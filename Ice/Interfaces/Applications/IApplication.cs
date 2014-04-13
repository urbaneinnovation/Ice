// Copyright (c) Jonathan Thompson 2014

using System;

using Ice.Interfaces.Elements;

namespace Ice.Interfaces.Applications
{
    public interface IApplication : IDisposable
    {
        /// <summary>
        /// Exposes the class' base IElement for handling.
        /// </summary>
        IElement BaseWindowIElement { get; }

        /// <summary>
        /// Start application.
        /// </summary>
        void StartApplication();

        /// <summary>
        /// Start application with arguments.
        /// </summary>
        /// <param name="args">Arguments.</param>
        void StartApplication(string args);

        /// <summary>
        /// Maximize the current window (if possible).
        /// </summary>
        void Maximize();

        /// <summary>
        /// Minimize the current window (if possible).
        /// </summary>
        void Minimize();

        /// <summary>
        /// Bring the current window to the front (if possible).
        /// </summary>
        void BringToFront();

        /// <summary>
        /// Returns the process' main window handle.
        /// </summary>
        /// <returns></returns>
        IntPtr GetWindowHandle();

        /// <summary>
        /// Returns the 
        /// </summary>
        /// <returns></returns>
        int GetHWND();

        /// <summary>
        /// Returns the main process ID for the application.
        /// </summary>
        /// <returns></returns>
        int GetProcessID();
    }
}
