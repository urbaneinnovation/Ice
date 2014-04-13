// Copyright (c) Jonathan Thompson 2014

using System.Text.RegularExpressions;

using Ice.Applications;
using Ice.Interfaces.Applications;

namespace Ice.Factories
{
    public static class ApplicationFactory
    {
        /// <summary>
        /// Get an IApplication from a provided file path. Use for
        /// Windows applications.
        /// </summary>
        /// <param name="filePath">File path to find executable.</param>
        /// <returns>IApplication representing the application.</returns>
        public static IApplication InitializeNewApplicationFromFilePath(string filePath)
        {
            return new Application(filePath, true);
        }

        /// <summary>
        /// Gets an IApplication from the window title. Use for Windows
        /// applications.
        /// </summary>
        /// <param name="windowTitle">Application title</param>
        /// <returns>IApplication representing the application.</returns>
        public static IApplication GetExistingApplicationFromWindowTitle(string windowTitle)
        {
            return new Application(windowTitle, false);
        }

        /// <summary>
        /// Gets an IApplication from the window title matching the passed regular expression.
        /// Use for Windows applications.
        /// </summary>
        /// <param name="windowTitleRegex">Regex to match against window title</param>
        /// <returns>IApplication representing the application.</returns>
        public static IApplication GetExistingApplicationFromWindowTitle(Regex windowTitleRegex)
        {
            return new Application(windowTitleRegex);
        }

        /// <summary>
        /// Gets an IInternetExplorerApplication from the url. Use for Internet Explorer
        /// applications.
        /// </summary>
        /// <param name="startURL">URL used when IInternetExplorerApplication::StartApplication
        /// is called.</param>
        /// <returns>IInternetExplorerApplication representing the web page.</returns>
        public static IInternetExplorerApplication InitializeNewWebApplicationFromURL(string startURL)
        {
            return new InternetExplorerApplication(startURL, true);
        }

        /// <summary>
        /// Gets an IInternetExplorerApplication from the tab's title. Use for Internet Explorer
        /// applications.
        /// </summary>
        /// <param name="title">Tab title.</param>
        /// <returns>IInternetExplorerApplication representing the web page.</returns>
        public static IInternetExplorerApplication GetExistingWebApplicationFromTabTitle(string title)
        {
            return new InternetExplorerApplication(title, false);
        }
    }
}
