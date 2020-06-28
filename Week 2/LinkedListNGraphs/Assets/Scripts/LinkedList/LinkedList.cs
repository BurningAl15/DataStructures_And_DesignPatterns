using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions.Must;

/// <summary>
/// Linked list ofa  data type
/// </summary>
/// <typeparam name="T">A data type</typeparam>
public abstract class LinkedList<T>
{
    protected LinkedListNode<T> head;
    protected int count;

    #region Constructor

    /// <summary>
    /// Constructor
    /// </summary>
    public LinkedList()
    {
        head = null;
        count = 0;
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets the number of nodes in the list
    /// </summary>
    public int Count => count;

    /// <summary>
    /// Gets the head of the list
    /// </summary>
    public LinkedListNode<T> Head => head;

    #endregion

    #region Public Methods

    public abstract void Add(T item);

    /// <summary>
    /// Removes all the items from the linked list
    /// </summary>
    public void Clear()
    {
        //Unlink all nodes
        if (head != null)
        {
            LinkedListNode<T> previousNode = head;
            LinkedListNode<T> currentNode = head.Next;

            previousNode.Next = null;
            //O(n)
            while (currentNode!=null)
            {
                previousNode = currentNode;
                currentNode = currentNode.Next;
                previousNode.Next = null;
            }
        }
        
        //Reset head and count
        head = null;
        count = 0;
    }

    /// <summary>
    /// Removes the given item from the list
    /// </summary>
    /// <param name="item">item to remove</param>
    /// <returns></returns>
    public bool Remove(T item)
    {
        //Can't remove from an empty list
        if (head == null)
        {
            return false;
        }else if (head.Value.Equals(item))
        {
            //Remove from head of list
            head = head.Next;
            count--;

            return true;
        }
        else
        {
            LinkedListNode<T> previousNode = head;
            LinkedListNode<T> currentNode = head.Next;

            //O(n)
            while (currentNode != null && !currentNode.Value.Equals(item))
            {
                previousNode = currentNode;
                currentNode = currentNode.Next;
            }

            if (currentNode == null)
            {
                return false;
            }
            else
            {
                //Set link and reduce count
                previousNode.Next = currentNode.Next;
                count--;

                return true;
            }
            
        }
    }

    /// <summary>
    /// Finds the given item in the list. Returns null
    /// if the item wasn't found in the list
    /// </summary>
    /// <param name="item">item to find</param>
    /// <returns></returns>
    public LinkedListNode<T> Find(T item)
    {
        LinkedListNode<T> currentNode = head;
        
        //O(n)
        while (currentNode != null && !currentNode.Value.Equals(item))
        {
            currentNode = currentNode.Next;
        }
        
        //Return node for item if found
        if (currentNode != null)
        {
            return currentNode;
        }
        else
            return null;
    }

    /// <summary>
    /// Converts the linked list to a comma-separated string of values
    /// </summary>
    /// <returns>Comma-Separated string of values</returns>
    public override string ToString()
    {
        StringBuilder builder=new StringBuilder();
        LinkedListNode<T> currentNode = head;
        int nodeCount = 0;

        //O(n)
        while (currentNode != null)
        {
            nodeCount++;
            builder.Append(currentNode.Value);
            if (nodeCount < count)
                builder.Append(",");

            currentNode = currentNode.Next;
        }

        return builder.ToString();
    }

    #endregion
}
