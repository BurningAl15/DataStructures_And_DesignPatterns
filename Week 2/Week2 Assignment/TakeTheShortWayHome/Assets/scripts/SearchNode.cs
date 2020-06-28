using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A node for searching using Dijkstra's algorithm
/// </summary>
/// <typeparam>type for search node</typeparam>
public class SearchNode<T> : IComparable
{
    #region Fields

    GraphNode<T> graphNode;
    float distance;
    SearchNode<T> previous;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructs a new saerch node with the given graph node.
    /// Distance is set to the max float value and previous is
    /// set to null
    /// </summary>
    /// <param name="graphNode">graph node</param>
    public SearchNode(GraphNode<T> graphNode)
    {
        this.graphNode = graphNode;
        distance = float.MaxValue;
        previous = null;
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets the graph node
    /// </summary>
    /// <value>graph node</value>
    public GraphNode<T> GraphNode
    {
        get { return graphNode; }
    }

    /// <summary>
    /// Gets and sets the distance for the node
    /// </summary>
    /// <value>distance</value>
    public float Distance
    {
        get { return distance; }
        set { distance = value; }
    }

    /// <summary>
    /// Gets and sets the previous node for the
    /// path to the graph node
    /// </summary>
    /// <value>previous</value>
    public SearchNode<T> Previous
    {
        get { return previous; }
        set { previous = value; }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Compares this instance to the provided object and
    /// returns their relative order
    /// </summary>
    /// <returns>relative order</returns>
    /// <param name="obj">object to compare to</param>
    public int CompareTo(object obj)
    {
        // instance is always greater than null
        if (obj == null)
        {
            return 1;
        }

        // check for correct object type
        SearchNode<T> otherSearchNode = obj as SearchNode<T>;
        if (otherSearchNode != null)
        {
            if (distance < otherSearchNode.Distance)
            {
                return -1;
            }
            else if (distance == otherSearchNode.Distance)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
        else
        {
            throw new ArgumentException("Object is not a SearchNode");
        }        
    }

    #endregion
}
