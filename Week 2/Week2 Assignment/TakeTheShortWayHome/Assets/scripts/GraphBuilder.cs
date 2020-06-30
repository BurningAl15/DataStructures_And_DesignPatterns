using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Builds the graph
/// </summary>
public class GraphBuilder : MonoBehaviour
{
    static Graph<Waypoint> graph;
    [SerializeField] List<Waypoint> wayPoints= new List<Waypoint>();
    /// <summary>
    /// Awake is called before Start
    /// </summary>
    void Awake()
    {
        graph = new Graph<Waypoint>();
        // add nodes (all waypoints, including start and end) to graph
        for (int i = 0; i < wayPoints.Count; i++)
            graph.AddNode(wayPoints[i]);

        // add edges to graph
        for (int i = 0; i < wayPoints.Count; i++)
            for (int j = 0; j < wayPoints.Count; j++)
                if (CanBeEdge(wayPoints[i].transform, wayPoints[j].transform))
                    graph.AddEdge(wayPoints[i], wayPoints[j],
                        (int) GetDistance(wayPoints[i].transform, wayPoints[j].transform));
    }

    bool CanBeEdge(Transform pointA, Transform pointB)
    {
        return Mathf.Abs(pointA.position.x - pointB.position.x) <= 3.5f &&
               Mathf.Abs(pointA.position.y - pointB.position.y) <= 3f;
    }

    float GetDistance(Transform pointA, Transform pointB)
    {
        return Vector3.Distance(pointA.position, pointB.position);
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
