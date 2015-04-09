// Copyright (c) Jonathan Thompson 2014

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Ice.GUI
{
    public partial class LogGUI : Form
    {
        private List<string> logLines = new List<string>();

        public LogGUI()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Internal representation of the rich text box used for a log.
        /// </summary>
        internal string LogText
        {
            get
            {
                // Thread-safe invocation
                //
                if (!this.InvokeRequired)
                {
                    return this.IceLogText.Text;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
            set
            {
                // Thread-safe invocation
                //
                this.Invoke((MethodInvoker)delegate
                {
                    this.logLines.Add(value);
                    this.IceLogText.AppendText(value + "\r\n");
                });
            }
        }

        /// <summary>
        /// Stop button handler. Kills the Ice script thread and GUI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
            ScriptControl.Stop();
        }

        /// <summary>
        /// Resume button handler. Opens the EventWaitHandler gate.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResumeButton_Click(object sender, EventArgs e)
        {
            this.ResumeButton.Enabled = false;
            this.PauseButton.Enabled = true;
            ScriptControl.Resume();
        }

        /// <summary>
        /// Pause button handler. Closes the EventWaitHandler gate.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PauseButton_Click(object sender, EventArgs e)
        {
            this.PauseButton.Enabled = false;
            this.ResumeButton.Enabled = true;
            ScriptControl.Pause();
        }

        /// <summary>
        /// Presents a dialog box to save log results.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            string logText = this.LogText;
            string path = FileDialogs.SaveDialog();

            if (File.Exists(path))
            {
                File.WriteAllLines(FileDialogs.SaveDialog(), logLines);
            }
        }
    }
}
