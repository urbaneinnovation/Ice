// Copyright (c) Jonathan Thompson 2014

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Automation;

using mshtml;

using Ice.ActionHandlers.InternetExplorer;
using Ice.ActionHandlers.Windows;
using Ice.Interfaces.ActionHandlers;

namespace Ice.Factories
{
    internal static class ElementActionHandlerFactory
    {
        private static Dictionary<ControlType, Type> windowsActionHandlers;
        private static Dictionary<string, Type> mshtmlActionHandlers;

        /// <summary>
        /// Static constructor to initialize map from control type (e.g., System.Windows.Forms.Checkbox
        /// or HTML.INPUT.checkbox) to appropriate ElementActionHandler type.
        /// </summary>
        static ElementActionHandlerFactory()
        {
            windowsActionHandlers = new Dictionary<ControlType, Type>()
            {
                { ControlType.Button,  typeof(ActionHandlers.Windows.Button) },
                { ControlType.CheckBox, typeof(ActionHandlers.Windows.Checkbox) },
                //{ ControlType.ComboBox, null },
                //{ ControlType.Edit, null },
                //{ ControlType.List, null },
                //{ ControlType.ListItem, null },
                //{ ControlType.Menu, null },
                //{ ControlType.MenuItem, null },
                //{ ControlType.Pane, null },
                //{ ControlType.RadioButton, null },
                //{ ControlType.ScrollBar, null },
                //{ ControlType.StatusBar, null },
                //{ ControlType.Tree, null },
                //{ ControlType.TreeItem, null },
                { ControlType.Window, typeof(Window) }
            };

            mshtmlActionHandlers = new Dictionary<string, Type>()
            {
                { "html.input.submit", typeof(ActionHandlers.InternetExplorer.Button) },
                { "html.input.checkbox", typeof(ActionHandlers.InternetExplorer.Checkbox) },
                { "html.input.radio", typeof(ActionHandlers.InternetExplorer.RadioButton) },
                { "html.input.text", typeof(ActionHandlers.InternetExplorer.Text) },
                { "html.select", typeof(ActionHandlers.InternetExplorer.Select) },
                { "html.textarea", typeof(ActionHandlers.InternetExplorer.Text) }
            };
        }

        /// <summary>
        /// Creates an IElementActionHandler based on a UI AutomationElement as whatever the input 
        /// parameter's control type maps to.
        /// </summary>
        /// <param name="ae">AutomationElement to fetch an IActionHandler for.</param>
        /// <returns>An implementation of IActionHandler</returns>
        internal static IElementActionHandler GetActionHandler(AutomationElement ae)
        {
            Type type;

            // If type exists in ElementHandlerFactory, create the thing it maps to.
            //
            if (windowsActionHandlers.TryGetValue(ae.Current.ControlType, out type))
            {
                return (IElementActionHandler)Activator.CreateInstance(type, new object[] { ae });
            }
            // Otherwise, create a default handler.
            //
            else
            {
                return (IElementActionHandler)Activator.CreateInstance(typeof(ActionHandlers.Windows.Default), new object[] { ae });
            }
        }

        /// <summary>
        /// Creates an IElementActionHandler based on an mshtml IHTMLElement, based on the HTML
        /// control type.
        /// </summary>
        /// <param name="ihe">IHTMLElement to fetch an ActionHandler for.</param>
        /// <returns>An implementation of IActionHandler</returns>
        internal static IElementActionHandler GetActionHandler(IHTMLElement ihe)
        {
            Type type;
            string elementClass = "html." + ihe.tagName.ToLower();

            // Check whether the element's an input element, and append text
            // appropriately.
            //
            if (elementClass.EndsWith("input"))
            {
                elementClass += "." + ((IHTMLInputElement)ihe).type;
            }

            //TODO: Remove
            Logs.Log.LogMessage(elementClass);

            // If type exists in ElementActionHandlerFactory, create the thing it maps to.
            //
            if (mshtmlActionHandlers.TryGetValue(elementClass, out type))
            {
                return (IElementActionHandler)Activator.CreateInstance(type, new object[] { ihe });
            }
            // Otherwise, create a default handler.
            //
            else
            {
                return (IElementActionHandler)Activator.CreateInstance(typeof(ActionHandlers.InternetExplorer.Default), new object[] { ihe });
            }
        }
    }
}
