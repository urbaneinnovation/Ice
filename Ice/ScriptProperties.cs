// Copyright (c) Jonathan Thompson 2014

using System;

namespace Ice
{
    // Static class to hold script properties
    public static class ScriptProperties
    {

        /// <summary>
        /// Time (in ms) allowed to try to find an object.
        /// Default is 5s.
        /// </summary>
        public static long Timeout = 5000;      // in ms

        /// <summary>
        /// Time (in ms) between mouse up and mouse down
        /// on a click. Default is 50 ms.
        /// </summary>
        public static int MouseDown = 50;

        /// <summary>
        /// Time (in ms) between key presses when inserting
        /// text. Default is 50 ms.
        /// </summary>
        public static int KeyDown = 50;

        /// <summary>
        /// Time (in ms) between clicks on a double-click.
        /// Default is 50 ms.
        /// </summary>
        public static int DoubleClickTime = 50;

        /// <summary>
        /// Sets the script start time. Default is start of execution.
        /// </summary>
        public static DateTime StartDate = DateTime.Now;

        /// <summary>
        /// If true, displays the script playback GUI during script
        /// execution.
        /// </summary>
        public static bool DisplayScriptPlaybackGUI = false;

        /// <summary>
        /// If true, creates a log file (as text) which mirrors the output
        /// of the Log GUI.
        /// </summary>
        public static bool CreateLogFile = false;
    }
}
