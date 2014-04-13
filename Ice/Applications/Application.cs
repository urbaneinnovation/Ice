// Copyright Jonathan Thompson (c) 2014

using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Automation;

using Ice.Exceptions;
using Ice.Interfaces.Applications;
using Ice.Interfaces.Elements;

namespace Ice.Applications
{
    public class Application : IApplication
    {
        private string applicationIdentifier;
        private bool startedFromFilePath;
        private bool foundByRegex;
        private Process p;
        private AutomationElement window;
        private IElement windowHandler;
        private Regex windowTitleRegex;

        /// <summary>
        /// Internal Constructor. Sets application path.
        /// </summary>
        /// <param name="appPath">Determines whether the application
        /// was started from a file path, or found by searching window
        /// titles.
        /// </param>
        internal Application(string applicationIdentifier, bool startedFromFilePath)
        {
            this.applicationIdentifier = applicationIdentifier;
            this.startedFromFilePath = startedFromFilePath;
            this.foundByRegex = false;

            if (!startedFromFilePath)
            {
                InitializeHandler();
            }
        }

        internal Application(Regex windowTitleRegex)
        {
            this.startedFromFilePath = false;
            this.foundByRegex = true;
            this.windowTitleRegex = windowTitleRegex;
            InitializeHandler();
        }

        // Exposes the class' base element for handling.
        //
        public IElement BaseWindowIElement
        {
            get
            {
                return this.windowHandler;
            }
        }

        // Start application
        //
        public virtual void StartApplication()
        {
            if (this.startedFromFilePath)
            {
                StartApplication(null);
            }
        }

        // Start the application with command-line args
        //
        public void StartApplication(string args)
        {
            if (this.startedFromFilePath)
            {
                try
                {
                    Stopwatch stopwatch = new Stopwatch();
                    p = (args == null)
                        ? Process.Start(this.applicationIdentifier)
                        : Process.Start(this.applicationIdentifier, args);

                    p.WaitForInputIdle();

                    stopwatch.Start();

                    // Wait for application to load
                    //
                    while (this.GetWindowHandle().Equals(IntPtr.Zero) && stopwatch.ElapsedMilliseconds < ScriptProperties.Timeout)
                    {
                        Thread.Sleep(100);
                    }

                    if (this.GetWindowHandle().Equals(IntPtr.Zero))
                    {
                        throw new InvalidOperationException("Process exited early!");
                    }
                }
                catch (InvalidOperationException e)
                {
                    throw new InvalidApplicationStartupPathException(e.Message, e);
                }
            }

            InitializeHandler();
        }

        // Find the application's base IElement.
        //
        private void InitializeHandler()
        {
            Condition condition = Condition.FalseCondition;
            TreeWalker walker;
            bool windowFound = false;
            var stopWatch = new Stopwatch();

            // Initialize startup conditions for an application started from file path
            //
            if (this.startedFromFilePath)
            {
                p.Refresh();
                this.window = AutomationElement.FromHandle(this.GetWindowHandle());

                /* Not needed - changed to grab element directly from the window handle.
                 * 
                System.Windows.Forms.MessageBox.Show(new IntPtr(this.GetWindowHandle()).ToString());
                condition = new AndCondition(new Condition[] {
                    new PropertyCondition(AutomationElement.NativeWindowHandleProperty, this.GetWindowHandle()),
                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window)
                });
                */
            }
            // Initialize startup conditions for an application that's already running
            //
            else
            {
                condition = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window);
            }

            // Initialize walker
            //
            walker = new TreeWalker(condition);

            // Get AutomationElement corresponding to Application Under Test
            //
            stopWatch.Start();

            //TODO: THIS
            while (this.window == null && stopWatch.ElapsedMilliseconds < ScriptProperties.Timeout)
            {
                AutomationElement current = walker.GetFirstChild(AutomationElement.RootElement);
                string name;
                while (current != null && !windowFound)
                {
                    // Check names, if necessary
                    //
                    if (!this.startedFromFilePath)
                    {
                        name = current.Current.Name;

                        if (this.foundByRegex)
                        {
                            // Check regex match
                            //
                            if (this.windowTitleRegex.IsMatch(name))
                            {
                                windowFound = true;
                            }
                        }
                        else
                        {
                            // Check name match
                            //
                            if (this.applicationIdentifier.Equals(name))
                            {
                                windowFound = true;
                            }
                        }
                    }
                    else
                    {
                        // Found by pID, and is guaranteed to be unique, so no assignment is required
                        // and loop will break at the appropriate time.
                        //
                        windowFound = true;
                    }

                    // Get next sibling
                    //
                    current = walker.GetNextSibling(current);
                }

                this.window = current;
            }

            stopWatch.Stop();

            // Throw exception if application wasn't found.
            //
            if (this.window == null)
            {
                throw new WindowNotFoundException("Windows application for '" + 
                    this.applicationIdentifier + "' not found!");
            }

            // Initialize windowHandler
            //
            this.windowHandler = Factories.ElementFactory.GetIElementFromHandlers(
                Factories.ElementActionHandlerFactory.GetActionHandler(window),
                Factories.ElementDescendantHandlerFactory.GetDescendantHandler(window)
            );
        }

        // Maximize the current window
        //
        public void Maximize()
        {
            SetWindowState(WindowVisualState.Maximized);
        }

        // Minimize the current window
        //
        public void Minimize()
        {
            SetWindowState(WindowVisualState.Minimized);
        }

        // Bring the current window to the front
        //
        public void BringToFront()
        {
            SetWindowState(WindowVisualState.Normal);
        }

        // Sets the current window state
        //
        private void SetWindowState(WindowVisualState state)
        {
            object windowPattern;
            // Check if main window form supports WindowPattern
            //
            if (this.window.TryGetCurrentPattern(WindowPattern.Pattern, out windowPattern))
            {
                // Bring window to front
                //
                ((WindowPattern)windowPattern).SetWindowVisualState(state);
            }
        }

        // Gets the application window handle
        //
        public IntPtr GetWindowHandle()
        {
            return this.p.MainWindowHandle;
        }

        // Gets the application's process ID.
        //
        public int GetProcessID()
        {
            return this.p.Id;
        }

        public int GetHWND()
        {
            return this.window.Current.NativeWindowHandle;
        }

        // Ends the current window.
        //
        public void Dispose()
        {
            this.p.CloseMainWindow();
        }
    }
}
