using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnsortedLinkedList<T> : LinkedList<T>
{
    #region Constructors

    public UnsortedLinkedList():base(){}

    #endregion

    #region Public Methods

    /// <summary>
    /// Adds the given item to the list
    /// </summary>
    /// <param name="item">Item to add</param>
    /// O(1)
    public override void Add(T item)
    {
        //adding to empty list
        if(head==null)
            head=new LinkedListNode<T>(item,null);
        else
        {
            //Add to front of the list            
            head=new LinkedListNode<T>(item,head);
        }

        count++;
    }

    #endregion
}
