// Copyright (c) Jonathan Thompson 2014

using Ice.Interfaces.Elements;

namespace Ice.Interfaces.Applications
{
    public interface IInternetExplorerApplication : IApplication
    {
        /// <summary>
        /// Exposes the base Internet Explorer IElement to access
        /// web application data. Note that the only useful method
        /// to invoke on this method is the GetChild method, which
        /// retrieves an HTML element. All other methods in the
        /// Descendant handler call GetChild.
        /// </summary>
        //TODO: Determine if this is necessary.
        //IElement BaseInternetExplorerIElement { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IElement GetElementByID(string id);

        /// <summary>
        /// Refreshes the current page.
        /// </summary>
        void Refresh();

        /// <summary>
        /// Navigates to the provided URL.
        /// </summary>
        /// <param name="url"></param>
        void NavigateToURL(string url);

        /// <summary>
        /// Moves back one page.
        /// </summary>
        void Back();

        /// <summary>
        /// Moves forward one page.
        /// </summary>
        void Forward();
    }
}
