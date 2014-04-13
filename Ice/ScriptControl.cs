// Copyright (c) Jonathan Thompson 2014

using System.Threading;

namespace Ice
{
    internal static class ScriptControl
    {
        private static EventWaitHandle ewh = new EventWaitHandle(true, EventResetMode.ManualReset);

        /// <summary>
        /// Pauses the current Ice script at the next pausable point.
        /// </summary>
        internal static void Pause()
        {
            ewh.Reset();
        }

        /// <summary>
        /// Signals all waiting events, thus continuing Ice script execution.
        /// </summary>
        internal static void Resume()
        {
            ewh.Set();
            //ewh.Reset();
        }

        /// <summary>
        /// Ceases the current process.
        /// </summary>
        internal static void Stop()
        {
            System.Environment.Exit(0);
        }

        /// <summary>
        /// Adds a pausable point at whatever point this method is called.
        /// </summary>
        internal static void AddPausePoint()
        {
            ewh.WaitOne();
        }
    }
}
