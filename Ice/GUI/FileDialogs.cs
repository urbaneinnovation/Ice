// Copyright (c) Jonathan Thompson 2014

using System.Threading;
using System.Windows.Forms;

namespace Ice.GUI
{
    internal static class FileDialogs
    {
        /// <summary>
        /// Creates a new save file dialog on a separate thread to get a file
        /// path.
        /// </summary>
        /// <returns>File path. Empty string if user cancelled.</returns>
        internal static string SaveDialog()
        {
            string filePath = "";

            // Generate thread to save log file
            //
            Thread saveLogThread = new Thread(new ThreadStart(() =>
            {
                using (var saveDialog = new SaveFileDialog())
                {
                    saveDialog.Title = "Save Ice Log";
                    saveDialog.DefaultExt = "log";
                    saveDialog.Filter = "Log File (*.log)|*.log|Text File (*.txt)|*.txt|All Files (*.*)|*.*";

                    // Write log contents to file
                    //
                    if (saveDialog.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                    {
                        filePath = saveDialog.FileName;
                    }
                }
            }));

            // Set apartment state
            //
            saveLogThread.SetApartmentState(ApartmentState.STA);

            // Start thread
            //
            saveLogThread.Start();

            // Join thread
            //
            saveLogThread.Join();

            return filePath;
        }
    }
}
