using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Graph<T>
{
    #region Fields

    List<GraphNode<T>> nodes=new List<GraphNode<T>>();
    
    #endregion

    #region Constructor

    /// <summary>
    /// Constructor
    /// </summary>
    public Graph(){}

    #endregion

    #region Properties

    /// <summary>
    /// Gets the number of nodes in the graph
    /// </summary>
    public int Count
    {
        get { return nodes.Count; }
    }

    /// <summary>
    /// Gets a read-only list of the nodes in the graph
    /// </summary>
    public IList<GraphNode<T>> Nodes
    {
        get { return nodes.AsReadOnly(); }
    }
    
    #endregion

    #region Public Methods
    
    /// <summary>
    /// Clears all the nodes from the graph
    /// </summary>
    public void Clear()
    {
        //Remove all the neighbors from each node
        //so nodes can be garbage collected
        foreach (GraphNode<T> node in nodes)
        {
            node.RemoveAllNeighbors();
        }

        //Now remove all the nodes from the graph
        for (int i = nodes.Count - 1; i >= 0; i--)
        {
            nodes.RemoveAt(i);
        }
    }

    /// <summary>
    /// Adds a node with the given value to the graph.
    /// If a node with the same value is in the graph,
    /// the value isn't added and the method returns false
    /// </summary>
    /// <param name="value">Value to add</param>
    /// <returns>True if the value is added, false otherwise</returns>
    public bool AddNode(T value)
    {
        if (Find(value) != null)
        {
            //Duplicate value
            return false;
        }
        else
        {
            nodes.Add(new GraphNode<T>(value));
            return true;
        }
    }

    /// <summary>
    /// Adds an edge between the nodes with the given values
    /// in the graph.
    /// If one or both of the values don't exist in the graph
    /// the method returns false.
    /// If an edge already exists between the nodes the edge
    /// isn't added and the method returns false
    /// </summary>
    /// <param name="value1">First value to connect</param>
    /// <param name="value2">Second value to connect</param>
    /// <returns>True if the edge is added,
    ///         false otherwise</returns>
    public bool AddEdge(T value1, T value2)
    {
        GraphNode<T> node1 = Find(value1);
        GraphNode<T> node2 = Find(value2);

        if (node1 == null || node2 == null)
        {
            return false;
        }
        else if (node1.Neighbors.Contains(node2))
        {
            //Edge already exists
            return false;
        }
        else
        {
            //Undirected graph, so add as neighbors to each other
            node1.AddNeighbor(node2);
            node2.AddNeighbor(node1);
            return true;
        }
    }

    /// <summary>
    /// Removes the node with the given value from the graph.
    /// If the node isn't found in the graph, the method returns false.
    /// </summary>
    /// <param name="value">Value to remove</param>
    /// <returns>True if the value is removed,
    ///         false otherwise</returns>
    public bool RemoveNode(T value)
    {
        GraphNode<T> removeNode = Find(value);
        if (removeNode == null)
        {
            return false;
        }
        else
        {
            //Need to remove as neighbor for all nodes in graph
            nodes.Remove(removeNode);
            foreach (GraphNode<T> node in nodes)
            {
                node.RemoveNeighbor(removeNode);
            }

            return true;
        }
    }


    /// <summary>
    /// Removes an edge between the nodes with the given values
    /// from the graph.
    /// If one or both of the values don't exist in the graph
    /// the method returns false
    /// </summary>
    /// <param name="value1">First value to disconnect</param>
    /// <param name="value2">Second value to disconnect</param>
    /// <returns>True if the edge is removed,
    ///         False otherwise</returns>
    public bool RemoveEdge(T value1, T value2)
    {
        GraphNode<T> node1 = Find(value1);
        GraphNode<T> node2 = Find(value2);
        if (node1 == null || node2 == null)
        {
            return false;
        }
        else if (!node1.Neighbors.Contains(node2))
        {
            //Edge doesn't exist
            return false;
        }
        else
        {
            //Undirected graph, so remove as neighbors
            //to each other
            node1.RemoveNeighbor(node2);
            node2.RemoveNeighbor(node1);
            return true;
        }
    }
    

    /// <summary>
    /// Finds the graph node with the given value
    /// </summary>
    /// <param name="value">Value to find</param>
    /// <returns>Graph node or null if not found</returns>
    /// <exception cref="NotImplementedException"></exception>
    public GraphNode<T> Find(T value)
    {
        foreach (GraphNode<T> node in nodes)
        {
            if (node.Value.Equals(value))
            {
                return node;
            }
        }

        return null;
    }

    public override string ToString()
    {
        StringBuilder builder=new StringBuilder();
        for (int i = 0; i < Count; i++)
        {
            builder.Append(nodes[i].ToString());
            if (i < Count - 1)
            {
                builder.Append(",");
            }
        }

        return builder.ToString();
    }

    #endregion

}
