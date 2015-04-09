// Copyright (c) Jonathan Thompson 2014

using System.IO;
using System.Threading;

using Ice.Exceptions;
using Ice.GUI;
using Ice.Logs;

namespace Ice
{
    public static class Script
    {
        private static LogGUI gui;
        private static Thread guiThread;

        /// <summary>
        /// Initialize log, playback GUI, etc.
        /// </summary>
        public static void Initialize()
        {
            if (ScriptProperties.CreateLogFile)
            {
                // Get log path
                //
                Log.LogPath = GUI.FileDialogs.SaveDialog();

                if (Log.LogPath.Equals(string.Empty))
                {
                    // Ends execution if user cancels.
                    //
                    ScriptControl.Stop();
                }
                else
                {
                    // Overwrite file contents
                    //
                    File.WriteAllText(Log.LogPath, "");
                }
            }

            // Display Ice playback GUI
            //
            if (ScriptProperties.DisplayScriptPlaybackGUI)
            {
                guiThread = new Thread(new ThreadStart(RunGUI));
                guiThread.Start();
            }
        }

        /// <summary>
        /// Inserts a pausable point in the script. By default,
        /// all Ice actions (get element values, click things, etc.)
        /// contain pausable points.
        /// </summary>
        public static void InsertPausablePoint()
        {
            ScriptControl.AddPausePoint();
        }

        /// <summary>
        /// Adds text to the GUI.
        /// NOTE: Calling this method does NOT add the text to the log.
        /// Use Log::LogMessage(string) to do so (which also adds the string
        /// to the GUI).
        /// </summary>
        /// <param name="textToAdd">Text to display on GUI</param>
        internal static void AddTextToGUI(string textToAdd)
        {
            // Invoke if Ice::gui is initialized
            //
            if (gui != null)
            {
                gui.LogText = textToAdd;
            }
        }

        /// <summary>
        /// Instantiates and runs a new Log GUI.
        /// </summary>
        private static void RunGUI()
        {
            gui = new LogGUI();
            gui.ShowDialog();
        }
    }
}
