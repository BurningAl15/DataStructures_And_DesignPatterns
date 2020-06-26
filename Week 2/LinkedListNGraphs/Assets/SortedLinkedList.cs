using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SortedLinkedList<T> : LinkedList<T> where T:IComparable
{
    #region Constructors

    public SortedLinkedList():base(){}

    #endregion

    #region Public Methods

    public override void Add(T item)
    {
        //Adding to empty list
        if(head==null)
            head=new LinkedListNode<T>(item,null);
        else if(head.Value.CompareTo(item)>0)
        {
            //Adding before head
            head = new LinkedListNode<T>(item, head);
        }
        else
        {
            //Find place to add new node
            LinkedListNode<T> previousNode = null;
            LinkedListNode<T> currentNode = head;
            //O(n)
            while (currentNode != null && currentNode.Value.CompareTo(item) < 0)
            {
                previousNode = currentNode;
                currentNode = currentNode.Next;
            }
            
            //Link in new node between previous node and current node
            previousNode.Next=new LinkedListNode<T>(item,currentNode);
        }

        count++;
    }

    #endregion
}
