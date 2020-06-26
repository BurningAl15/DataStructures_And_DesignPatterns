using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedLists
{
    /// <summary>
    /// A sorted linked list
    /// </summary>
    class SortedLinkedList<T> : LinkedList<T> where T:IComparable
    {
        #region Constructors

        public SortedLinkedList() : base()
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Adds the given item to the list
        /// </summary>
        /// <param name="item">item to add</param>
        /// Explanation:
        /// If a head not exist, then create a new node and make it the head
        ///     this new node Next and Previous will point to null and a value will be set
        /// else if head's value is greater than 0, then 
        ///     set head's node Next point to head and Previous to null,
        ///     then make the head's Next.Previous point to head
        /// else    
        ///     set PreviousNode to head
        ///     set CurrentNode to head's Next node
        ///     Check while -> CurrentNode !=null and CurrentNode.Value.CompareTo(item) less than 0
        ///         set PreviousNode to CurrentNode
        ///         set CurrentNode to CurrentNode.Next
        ///     Check if currentNode ==null
        ///         set PreviousNode.Next Next to null and Previous to PreviousNode
        ///         set PreviousNode.Next Next to CurrentNode and Previous to PreviousNode
        ///         then set the currentNode.Previous to previousNode.Next
        public override void Add(T item)
        {
            // adding to empty list
            if (head == null)
            {
                head = new LinkedListNode<T>(item, null,null);
            }
            else if (head.Value.CompareTo(item) > 0)
            {
                // adding before head
                head = new LinkedListNode<T>(item, head,null);
                head.Next.Previous = head;
            }
            else
            {
                // find place to add new node
                // need previous node for adding to end of list
                LinkedListNode<T> previousNode = head;
                LinkedListNode<T> currentNode = head.Next;
                while (currentNode != null &&
                    currentNode.Value.CompareTo(item) < 0)
                {
                    previousNode = currentNode;
                    currentNode = currentNode.Next;
                }

                // check for adding to end of list
                if (currentNode == null)
                {
                    previousNode.Next = new LinkedListNode<T>(item, null, previousNode);
                }
                else
                {
                    // link in new node between previous node and current node
                    previousNode.Next = new LinkedListNode<T>(item, currentNode, previousNode);
                    currentNode.Previous = previousNode.Next;
                }
            }
            count++;
        }

        #endregion
    }
}

