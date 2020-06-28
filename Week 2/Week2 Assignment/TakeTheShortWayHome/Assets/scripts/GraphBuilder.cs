using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Builds the graph
/// </summary>
public class GraphBuilder : MonoBehaviour
{
    static Graph<Waypoint> graph;

    /// <summary>
    /// Awake is called before Start
    /// </summary>
    void Awake()
    {
        // add nodes (all waypoints, including start and end) to graph

        // add edges to graph
    }

    /// <summary>
    /// Gets the graph
    /// </summary>
    /// <value>graph</value>
    public static Graph<Waypoint> Graph
    {
        get { return graph; }
    }
}
