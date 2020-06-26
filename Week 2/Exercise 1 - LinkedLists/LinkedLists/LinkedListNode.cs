using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedLists
{
    /// <summary>
    /// A linked list node
    /// </summary>
    class LinkedListNode<T>
    {
        #region Fields

        T value;
        LinkedListNode<T> next;
        LinkedListNode<T> previous;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates new node with given value and next node
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="next">next node</param>
        public LinkedListNode(T value, LinkedListNode<T> next, LinkedListNode<T> previous)
        {
            this.value = value;
            this.next = next;
            this.previous = previous;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the node value
        /// </summary>
        /// <value>node value</value>
        public T Value
        {
            get { return value; }
        }

        /// <summary>
        /// Gets and sets the next node
        /// </summary>
        /// <value>next node</value>
        public LinkedListNode<T> Next
        {
            get { return next; }
            set { next = value; }
        }

        public LinkedListNode<T> Previous
        {
            get { return previous; }
            set { previous = value; }
        }

        #endregion
    }
}