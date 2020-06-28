using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Draws all the edges in a graph
/// </summary>
public class EdgeRenderer : MonoBehaviour
{
    List<GameObject> lineRenderers;

	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start()
	{
        // add a line renderer for each graph edge
        lineRenderers = new List<GameObject>();
        Graph<Waypoint> graph = GraphBuilder.Graph;
        foreach (GraphNode<Waypoint> node in graph.Nodes)
        {
            foreach (GraphNode<Waypoint> neighbor in node.Neighbors)
            {
                // add line renderer and draw line
                GameObject lineObj = new GameObject("LineObj");
                LineRenderer lineRenderer = lineObj.AddComponent<LineRenderer>();
                lineRenderer.material = new Material(Shader.Find("Hidden/Internal-Colored"));
                lineRenderers.Add(lineObj);

                //Set color
                lineRenderer.startColor = Color.black;
                lineRenderer.endColor = Color.black;

                //Set width
                lineRenderer.startWidth = 0.05f;
                lineRenderer.endWidth = 0.05f;

                //Set line count which is 2
                lineRenderer.positionCount = 2;

                //Set the postion of both two lines
                lineRenderer.SetPosition(0, node.Value.transform.position);
                lineRenderer.SetPosition(1, neighbor.Value.transform.position);
            }
        }
	}

    /// <summary>
    /// Stops drawing the graph edges
    /// </summary>
    public void StopDrawingEdges()
    {
        // done drawing edges, so destroy all line renderers
        for (int i = lineRenderers.Count - 1; i >= 0; i--)
        {
            Destroy(lineRenderers[i]);
        }
    }
}
