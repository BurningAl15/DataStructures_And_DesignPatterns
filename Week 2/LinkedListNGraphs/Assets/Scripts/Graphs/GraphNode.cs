using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class GraphNode<T>
{
    #region Fields

    private T value;
    private List<GraphNode<T>> neighbors;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="value">Value for the node</param>
    public GraphNode(T value)
    {
        this.value = value;
        neighbors=new List<GraphNode<T>>();
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets the value stored in the node
    /// </summary>
    public T Value => value;

    public IList<GraphNode<T>> Neighbors => neighbors.AsReadOnly();
    
    #endregion

    #region Public Methods

    /// <summary>
    /// Adds the given node as a neighbor for this node
    /// </summary>
    /// <param name="neighbor">neighbor to add</param>
    /// <returns>true if the neighbor was added,
    ///         false otherwise</returns>
    public bool AddNeighbor(GraphNode<T> neighbor)
    {
        //Don't add duplicate nodes
        if (neighbors.Contains(neighbor))
            return false;
        else
        {
            neighbors.Add(neighbor);
            return true;
        }
    }

    /// <summary>
    /// Removes the given node as a neighbor for this node
    /// </summary>
    /// <param name="neighbor">Neighbor to remove</param>
    /// <returns>True if the neighbor was removed,
    ///         false otherwise</returns>
    public bool RemoveNeighbor(GraphNode<T> neighbor)
    {
        //Only remove neighbours in list
        return neighbors.Remove(neighbor);
    }

    /// <summary>
    /// Removes all the neighbors for the code
    /// </summary>
    /// <returns>True if the neighbors were removed
    ///         false otherwise</returns>
    public bool RemoveAllNeighbors()
    {
        for (int i = neighbors.Count - 1; i >= 0; i--)
        {
            neighbors.RemoveAt(i);
        }

        return true;
    }

    /// <summary>
    /// Converts the node to a string
    /// </summary>
    /// <returns>The string</returns>
    public override string ToString()
    {
        StringBuilder nodeString=new StringBuilder();
        nodeString.Append("[Node Value: " + value + " Neighbors: ");
        for (int i = 0; i < neighbors.Count; i++)
        {
            nodeString.Append(neighbors[i].value + " ");
        }

        nodeString.Append("]");
        return nodeString.ToString();
    }

    #endregion
}
