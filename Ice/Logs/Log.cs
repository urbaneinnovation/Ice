// Copyright (c) Jonathan Thompson 2014

using System;
using System.IO;

namespace Ice.Logs
{
    public static class Log
    {
        internal static string LogPath { get; set; }

        /// <summary>
        /// Logs a message.
        /// </summary>
        /// <param name="messageText">Message to log</param>
        public static void LogMessage(string messageText)
        {
            LogData("MESSAGE", messageText);
        }

        /// <summary>
        /// Logs an error.
        /// </summary>
        /// <param name="errorText">Error to log</param>
        public static void LogError(string errorText)
        {
            LogData("ERROR", errorText);
        }

        /// <summary>
        /// Logs a warning.
        /// </summary>
        /// <param name="warningText">Error to log</param>
        public static void LogWarning(string warningText)
        {
            LogData("WARNING", warningText);
        }

        /// <summary>
        /// Creates verification point that compares the expected object
        /// to the actual object.
        /// </summary>
        /// <param name="vpName">Verification Point name</param>
        /// <param name="expected">Expected value</param>
        /// <param name="actual">Actual value</param>
        public static void VerificationPoint(string vpName, object expected, object actual)
        {
            string passStatus = expected.Equals(actual)
                ? "PASS"
                : "FAIL" + Environment.NewLine + "\t-Expected: " + expected.ToString() +
                   Environment.NewLine + "\t-Actual: " + actual.ToString();

            LogData(vpName.ToUpper(), passStatus);
        }

        /// <summary>
        /// Adds data to the script log and to the GUI.
        /// </summary>
        /// <param name="title">Log message title</param>
        /// <param name="textToLog">String to add to the log</param>
        private static void LogData(string title, string textToLog)
        {
            string completeMessage = title + " (" + System.DateTime.Now + "): " + textToLog;
            
            // Add to GUI
            //
            Script.AddTextToGUI(completeMessage);

            // Add to log file
            //
            if (ScriptProperties.CreateLogFile)
            {
                File.AppendAllText(LogPath, completeMessage + "\r\n");
            }
        }
    }
}
