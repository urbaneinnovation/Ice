// Copyright (c) Jonathan Thompson 2014

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

using mshtml;
using SHDocVw;

using Ice.Exceptions;
using Ice.Interfaces.Applications;
using Ice.Interfaces.Elements;

namespace Ice.Applications
{
    public class InternetExplorerApplication : Application, IInternetExplorerApplication
    {
        private string windowIdentifier;
        private bool isURL;
        private InternetExplorer ie;
        private HTMLDocument doc;
        private IElement ieHandler;

        /// <summary>
        /// Constructor for the <see cref="InternetExplorerApplication"/> class.
        /// </summary>
        /// <param name="windowIdentifier">String identifying the InternetExplorer window.</param>
        /// <param name="isURL">Set to true if windowIdentifier designates a window title.
        /// Set to false if windowIdentifier is the page URL to navigate to.</param>
        internal InternetExplorerApplication(string windowIdentifier, bool isURL)
            : base(DetermineBaseArgs(windowIdentifier, isURL), isURL)
        {
            this.windowIdentifier = windowIdentifier;
            this.isURL = isURL;

            if (!this.isURL)
            {
                InitializeHandler();
            }
        }

        /// <summary>
        /// Overrides Application::StartApplication to start up an instance of InternetExplorer
        /// with the provided URL.
        /// </summary>
        public override void StartApplication()
        {
            if (this.isURL)
            {
                //base.StartApplication(windowIdentifier);
                base.StartApplication("-nomerge " + windowIdentifier);
            }

            InitializeHandler();
        }

        /// <summary>
        /// Navigate to a given URL.
        /// </summary>
        /// <param name="url">URL to navigate to.</param>
        public void NavigateToURL(string url)
        {
            ie.Navigate(url);
        }

        /// <summary>
        /// Refresh the web page.
        /// </summary>
        public void Refresh()
        {
            ie.Refresh();
        }

        /// <summary>
        /// Move forward on the web page.
        /// </summary>
        public void Forward()
        {
            ie.GoForward();
        }

        /// <summary>
        /// Move backward on the web page.
        /// </summary>
        public void Back()
        {
            ie.GoBack();
        }

        /// <summary>
        /// Gets a page element by its id attribute.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An IElement</returns>
        public IElement GetElementByID(string id)
        {
            IHTMLElement element = this.GetElementByID(id, this.doc);

            // Throw exception if element not found
            //
            if (element == null)
            {
                throw new ElementNotFoundException("Element with id '" + id + "' not found!");
            }

            return Factories.ElementFactory.GetIElementFromBase(element);
        }

        // Recursive implementation of the above function.
        //
        private IHTMLElement GetElementByID(string id, HTMLDocument parentDoc)
        {
            string[] frameTags = new string[] {"FRAME", "IFRAME"};
            IHTMLElement element = null;

            // Try getting element directly from doc...
            //
            element = parentDoc.getElementById(id);

            // Otherwise, search all current frames.
            //
            foreach(string tag in frameTags)
            {
                if (element == null)
                {
                    FramesCollection frames = parentDoc.frames;

                    int i = 0;
                    while (i < frames.length && element == null)
                    {
                        // Get document from frame
                        //
                        object refID = i;
                        HTMLWindow2 frame = (HTMLWindow2)frames.item(ref refID);
                        HTMLDocument currentDocument = (HTMLDocument)frame.document;

                        // Wait for document to load
                        //
                        // TODO: Handle frames which can't access readyState
                        while (!currentDocument.readyState.Equals("complete")) { Thread.Sleep(500); }

                        // Get element
                        //
                        element = currentDocument.getElementById(id);

                        // Release COM objects as appropriate
                        //
                        Marshal.ReleaseComObject(frame);

                        // Check sub-frames and release appropriately
                        //
                        if (element == null)
                        {
                            // Check child frames
                            //
                            element = this.GetElementByID(id, currentDocument);

                            if (element == null)
                            {
                                Marshal.ReleaseComObject(currentDocument);
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }

                        i++;
                    }

                    Marshal.ReleaseComObject(frames);
                }
            }

            return element;
        }

        /// <summary>
        /// Intializes ieHandler to be able to work with
        /// HTML objects.
        /// </summary>
        private void InitializeHandler()
        {
            // Iterate through open explorer windows to find the one that
            // matches the base process ID.
            //
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            while (this.ie == null && stopwatch.ElapsedMilliseconds < ScriptProperties.Timeout)
            {
                ShellWindows sh = new ShellWindows();

                foreach (InternetExplorer browser in sh)
                {
                    if (browser.Name.StartsWith("Internet"))
                    {
                        // Check the browser's HWND vs. that of the started application
                        // to ensure uniqueness, if the window was started through Ice.
                        //
                        if (this.isURL)
                        {
                            if (browser.HWND == base.GetHWND())
                            {
                                this.ie = browser;
                                break;
                            }
                        }
                        // If not started through Ice, check the tab's name vs. the
                        // window identifier.
                        //
                        else
                        {
                            if (browser.LocationName.Equals(this.windowIdentifier))
                            {
                                this.ie = browser;
                                break;
                            }
                        }
                    }
                    // Release current iteration of sh
                    //
                    Marshal.ReleaseComObject(browser);
                }

                // Release sh
                //
                Marshal.ReleaseComObject(sh);
            }

            // Check if browser was found
            //
            if (this.ie == null)
            {
                throw new WindowNotFoundException("Internet Explorer browser window for '" +
                    this.windowIdentifier + "' not found!");
            }
            // If so, wait for load
            //
            else
            {
                while (ie.ReadyState != tagREADYSTATE.READYSTATE_COMPLETE)
                {
                    Thread.Sleep(100);
                }
            }

            // Retrieve mshtml.Document from mshtml.InternetExplorer
            //
            object objDoc = this.ie.Document;
            this.doc = (HTMLDocument)objDoc;
        }

        /// <summary>
        /// Determine which identifier for Internet Explorer to pass to the base
        /// constructor. If @identifier is a URL, 'iexplore' is passed, otherwise
        /// it's the identifier itself which is, in the case of this class,
        /// the tab title.
        /// </summary>
        /// <param name="identifier">Window/Tab identifier</param>
        /// <param name="isURL">If true, @identifier is a URL.</param>
        /// <returns>Argument to use in the base constructor.</returns>
        private static string DetermineBaseArgs(string identifier, bool isURL)
        {
            if (isURL)
            {
                return "iexplore";
            }

            return identifier;
        }
    }
}
