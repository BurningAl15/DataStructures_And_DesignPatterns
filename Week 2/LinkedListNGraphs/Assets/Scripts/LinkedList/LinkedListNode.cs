using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedListNode<T>
{
    #region Fields

    private T value;
    private LinkedListNode<T> next;

    #endregion

    #region Constructors

    /// <summary>
    /// Creates a new node with given value and next node
    /// </summary>
    /// <param name="_value">Value</param>
    /// <param name="_next">Next Node</param>
    public LinkedListNode(T _value, LinkedListNode<T> _next)
    {
        this.value = _value;
        this.next = _next;
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets the node value
    /// </summary>
    public T Value
    {
        get => value;
    }

    /// <summary>
    /// Gets and sets the next node
    /// </summary>
    public LinkedListNode<T> Next
    {
        get => next;
        set => next = value;
    }

    #endregion
}
