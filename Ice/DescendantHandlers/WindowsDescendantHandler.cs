// Copyright (c) Jonathan Thompson 2014

using System.Collections.Generic;
using System.Windows.Automation;

using Ice.Exceptions;
using Ice.Interfaces.Elements;
using Ice.Interfaces.DescendantHandlers;
using Ice.Logs;

namespace Ice.DescendantHandlers
{
    internal class WindowsDescendantHandler : IElementDescendantHandler
    {
        private AutomationElement ae;

        /// <summary>
        /// Initializes an instance of the <see cref="WindowsDescendantHandler"/> class.
        /// </summary>
        internal WindowsDescendantHandler(AutomationElement ae)
        {
            this.ae = ae;
        }

        // Gets the element's children.
        //
        public List<IElement> GetChildren()
        {
            Log.LogMessage("Looking for children of '" + this.ae.Current.Name + "'.");

            var walker = TreeWalker.RawViewWalker;
            var children = new List<IElement>();
            var currentElement = walker.GetFirstChild(this.ae);

            // Get siblings while they exist
            //
            while (currentElement != null)
            {
                children.Add(Factories.ElementFactory.GetIElementFromBase(currentElement));

                currentElement = walker.GetNextSibling(currentElement);
            }

            return children;
        }

        // Gets the element's descendants through AutomationElement::FindAll()
        //
        public List<IElement> GetDescendants()
        {
            Log.LogMessage("Getting all descendants for '" + this.ae.Current.Name + "'.");

            List<IElement> descendants = new List<IElement>();

            // Get all descendants as AutomationElements
            //
            var aec = this.ae.FindAll(TreeScope.Descendants, Automation.RawViewCondition);

            // Create corresponding IElements
            //
            foreach (AutomationElement ae in aec)
            {
                descendants.Add(Factories.ElementFactory.GetIElementFromBase(ae));
            }

            return descendants;
        }

        // Get the child element at a certain index.
        //
        public IElement GetChild(int whichChild)
        {
            Log.LogMessage("Looking for child " + whichChild + " of '" + this.ae.Current.Name + "'.");

            var walker = TreeWalker.RawViewWalker;
            var position = 0;
            var currentElement = walker.GetFirstChild(this.ae);

            // Get siblings while they exist
            //
            while (currentElement != null && position < whichChild)
            {
                currentElement = walker.GetNextSibling(currentElement);
                position++;
            }

            // Throw exception if element was not found correctly.
            //
            if (currentElement == null || position != whichChild)
            {
                throw new ChildNotFoundException("Child " + whichChild + " not found for parent '" +
                    this.ae.Current.Name + "'.");
            }

            return Factories.ElementFactory.GetIElementFromBase(currentElement);
        }

        // Get the child element by its Name/AutomationId
        //
        public IElement GetChild(string identifier)
        {
            Log.LogMessage("Looking for child '" + identifier+ "' of '" + this.ae.Current.Name + "'.");

            var walker = new TreeWalker(this.GenerateOrCondition(identifier));
            var currentElement = walker.GetFirstChild(this.ae);

            // Throw exception if element was not found correctly.
            //
            if (currentElement == null)
            {
                throw new ChildNotFoundException("Child '" + identifier + "' not found for parent '" +
                    this.ae.Current.Name + "'.");
            }

            return Factories.ElementFactory.GetIElementFromBase(currentElement);
        }

        // Gets a descendant provided an ancestor tree.
        //
        public IElement GetDescendant(string[] ancestors)
        {
            return Factories.ElementFactory.GetIElementFromBase(
                this.GetDescendant(ancestors, 0, this.ae));
        }

        // Recursive implementation of the above function
        //
        private AutomationElement GetDescendant(string[] path, int index, AutomationElement currentElement)
        {
            if (index < path.Length)
            {
                // Log some information
                //
                Log.LogMessage("Looking for descendant '" + path[index] + "' of '" + this.ae.Current.Name +
                    "'.");

                // Find matching child
                //
                currentElement = currentElement.FindFirst(TreeScope.Children, this.GenerateOrCondition(path[index]));
                index++;

                // Call self again
                //
                return this.GetDescendant(path, index, currentElement);
            }

            return currentElement;
        }

        // Generates an or condition to match an AutomationElement based on either
        // its name or its AutomationId.
        //
        private Condition GenerateOrCondition(string identifier)
        {
            return new OrCondition(new Condition[] {
                new PropertyCondition(AutomationElement.NameProperty, identifier),          // Name property
                new PropertyCondition(AutomationElement.AutomationIdProperty, identifier)   // AutomationId Property
            });
        }
    }
}
