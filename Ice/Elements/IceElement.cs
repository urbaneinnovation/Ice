// Copyright (c) Jonathan Thompson 2014

using Ice.Interfaces.ActionHandlers;
using Ice.Interfaces.Elements;
using Ice.Interfaces.DescendantHandlers;

namespace Ice.Elements
{
    public class IceElement : IElement
    {
        private IElementActionHandler actionHandler;
        private IElementDescendantHandler descendantHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="IceElement"/> class.
        /// </summary>
        /// <param name="actionHandler"></param>
        internal IceElement(IElementActionHandler actionHandler, IElementDescendantHandler descendantHandler)
        {
            this.actionHandler = actionHandler;
            this.descendantHandler = descendantHandler;
        }

        /// <summary>
        /// Returns the IElementActionHandler associated with this instance.
        /// </summary>
        public IElementActionHandler ActionHandler
        {
            get
            {
                return this.actionHandler;
            }
        }

        /// <summary>
        /// Returns the IElementDescendantHandler associated with this instance.
        /// </summary>
        public IElementDescendantHandler Descendants
        {
            get
            {
                return this.descendantHandler;
            }
        }
    }
}
