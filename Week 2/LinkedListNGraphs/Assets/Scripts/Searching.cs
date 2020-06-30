using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public class Searching : MonoBehaviour
{
    public enum SearchType
    {
        BreadthFirst,
        DepthFirst
    }
    
    void Start()
    {
        Graph<int> graph = BuildGraph();

        // search type-independent tests
        print("Type-Independent Searches\n" + "-------------------------\n" + "Path from 4 to 4: " +
              Search(4, 4, graph, SearchType.DepthFirst) + "\n" + "Path from 4 to 0: " +
              Search(4, 0, graph, SearchType.DepthFirst) + "\n" + "Path from 4 to 11: " +
              Search(4, 11, graph, SearchType.DepthFirst) + "\n" + "Path from 4 to 42: " +
              Search(4, 42, graph, SearchType.BreadthFirst) + "\n");

        // depth-first search
        print("Depth-First Search\n"+"------------------\n"+"Path from 4 to 1: " +
              Search(4, 1, graph, SearchType.DepthFirst)
              +"\n");

        // breadth-first search
        print("Breadth-First Search\n"+"------------------\n"+"Path from 4 to 1: " +
              Search(4, 1, graph, SearchType.BreadthFirst));
    }

    Graph<int> BuildGraph()
    {
        Graph<int> graph=new Graph<int>();

        graph.AddNode(1);
        graph.AddNode(4);
        graph.AddNode(5);
        
        graph.AddNode(7);
        graph.AddNode(10);
        graph.AddNode(11);
        graph.AddNode(12);
        graph.AddNode(42);

        graph.AddEdge(1, 5);
        graph.AddEdge(4, 11);
        graph.AddEdge(4, 42);
        graph.AddEdge(5, 11);
        graph.AddEdge(5, 12);
        graph.AddEdge(5, 42);
        graph.AddEdge(7, 10);
        graph.AddEdge(7, 11);
        graph.AddEdge(10, 11);
        graph.AddEdge(11, 42);
        graph.AddEdge(12, 11);

        return graph;
    }

    /// <summary>
    /// Does a search for a path from start to finish on
    /// graph using given search type
    /// </summary>
    /// <param name="start">start value</param>
    /// <param name="finish">finish value</param>
    /// <param name="graph">graph to search</param>
    /// <returns>string for path or empty string if there is no path</returns>
    string Search(int start, int finish, Graph<int> graph, SearchType searchType)
    {
        System.Collections.Generic.LinkedList<GraphNode<int>> searchList =
            new System.Collections.Generic.LinkedList<GraphNode<int>>();
        
        //Special case for start and finish the same
        if (start == finish)
        {
            return start.ToString();
        }
        else if (graph.Find(start) == null || graph.Find(finish) == null)
        {
            //Start or finish not in graph
            return "";
        }
        else
        {
            //Add start node to dictionary and search list
            GraphNode<int> startNode = graph.Find(start);
            Dictionary<GraphNode<int>, PathNodeInfo<int>> pathNodes =
                new Dictionary<GraphNode<int>, PathNodeInfo<int>>();
            
            pathNodes.Add(startNode,new PathNodeInfo<int>(null));
            searchList.AddFirst(startNode);
            
            //Loop until we exhaust all possible paths
            while (searchList.Count > 0)
            {
                //Extract front of search list
                GraphNode<int> currentNode = searchList.First.Value;
                print("Current Node: " + currentNode.Value);
                searchList.RemoveFirst();
                
                //Explore each neighbor of this node
                foreach (GraphNode<int> neighbor in currentNode.Neighbors)
                {
                    //Check for found finish
                    if (neighbor.Value == finish)
                    {
                        pathNodes.Add(neighbor,new PathNodeInfo<int>(currentNode));
                        return ConvertPathToString(neighbor, pathNodes);
                    }
                    else if (pathNodes.ContainsKey(neighbor))
                    {
                        //Found a cycle, so skip this neighbor
                        continue;
                    }
                    else
                    {
                        //Link neighbor to current node in path
                        pathNodes.Add(neighbor,new PathNodeInfo<int>(currentNode));
                        
                        //Add neighbor to front or back of search list
                        if (searchType == SearchType.DepthFirst)
                            searchList.AddFirst(neighbor);
                        else
                        {
                            searchList.AddLast(neighbor);
                        }
                        
                        print("Just added "+neighbor.Value+" to search list");
                    }
                }
            }
            //Didn't find a path from start to finish
            return "";
        }
    }

    /// <summary>
    /// Converts the given end node and path node information
    /// to a path from the start node to the end node
    /// </summary>
    /// <param name="path">path to convert</param>
    /// <returns>string for path</returns>
    private string ConvertPathToString(GraphNode<int> endNode, Dictionary<GraphNode<int>, PathNodeInfo<int>> pathNodes)
    {
        //Build linked list for path in correct order
        System.Collections.Generic.LinkedList<GraphNode<int>> path =
            new System.Collections.Generic.LinkedList<GraphNode<int>>();

        path.AddFirst(endNode);
        GraphNode<int> previous = pathNodes[endNode].Previous;
        while (previous!=null)
        {
            path.AddFirst(previous);
            previous = pathNodes[previous].Previous;
        }
        
        //Build and return string
        StringBuilder pathString = new StringBuilder();
        System.Collections.Generic.LinkedListNode<GraphNode<int>> currentNode = path.First;
        int nodeCount = 0;
        while (currentNode != null)
        {
            nodeCount++;
            pathString.Append(currentNode.Value.Value);
            if (nodeCount < path.Count)
            {
                pathString.Append(" ");
            }

            currentNode = currentNode.Next;
        }

        return pathString.ToString();
    }
}
