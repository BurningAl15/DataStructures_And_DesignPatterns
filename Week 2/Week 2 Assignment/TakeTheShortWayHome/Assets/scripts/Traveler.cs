using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

/// <summary>
/// A traveler
/// </summary>
public class Traveler : MonoBehaviour
{
	public enum SearchType
	{
		BreadthFirst,
		DepthFirst
	}
	
    // events fired by class
    PathFoundEvent pathFoundEvent = new PathFoundEvent();
    PathTraversalCompleteEvent pathTraversalCompleteEvent = new PathTraversalCompleteEvent();	
    
    [FormerlySerializedAs("start")] [SerializeField] private Waypoint _start;
    [FormerlySerializedAs("end")] [SerializeField] private Waypoint _end;
    private LinkedList<Waypoint> _path = new LinkedList<Waypoint>();
    private LinkedList<float> _distances = new LinkedList<float>();
    
    [SerializeField] List<Waypoint> simpleList=new List<Waypoint>();
    [SerializeField] List<Waypoint> waypointList=new List<Waypoint>();
    [SerializeField] List<float> distances=new List<float>();
    private int counter = 0;
    private int distanceCounter = 0;
    
    float SumDistances = 0;
    
    private bool init = false;
    private bool endLoop = false;

    private float toPoint = 0;
    
	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start()
	{
		EventManager.AddPathFoundInvoker(this);
		EventManager.AddPathTraversalCompleteInvoker(this);
		
		Graph<Waypoint> graph= GraphBuilder.Graph;


		_path = Search(_start, _end, graph);
		// InitSimpleList(simpleList);
		InitList(waypointList);
	}
	
	void Update()
	{
		if (!endLoop)
		{
			if (!init)
			{
				pathFoundEvent.Invoke(SumDistances);
				init = true;
			}
		
		
			if (!this.transform.position.Equals(waypointList[counter].Position))
			{
				toPoint += Time.deltaTime;
				this.transform.position = Vector2.Lerp(this.transform.position, waypointList[counter].Position, toPoint);
			}
			else
			{
				toPoint = 0;
				distanceCounter = counter - 1;
				
				if (counter < waypointList.Count - 1)
				{
					counter++;
				}
				else if (counter >= waypointList.Count-1)
				{
					pathTraversalCompleteEvent.Invoke();					
					
					endLoop = true;
				}
				
				SumDistances -= distances[distanceCounter];
				pathFoundEvent.Invoke(SumDistances);
			}
		}
		else
		{
			
			pathFoundEvent.Invoke(0);
		}
	}
	
	void InitList(List<Waypoint> _temp)
	{
		foreach (Waypoint waypoint in _path)
		{
			_temp.Add(waypoint);
			print(waypoint.Id);
		}
		
		for (int i = 0; i < _temp.Count-1; i++)
		{
			distances.Add(Vector2.Distance(_temp[i].Position,_temp[i+1].Position));
		}
		distances.Add(Vector2.Distance(_temp[_temp.Count-1].Position,_end.Position));
		
		for (int i = 0; i < distances.Count; i++)
		{
			SumDistances += distances[i];
		}
		
		this.transform.position = _temp[counter].Position;
		counter++;
		distanceCounter = 0;
	}

	/// <summary>
	/// Does a search for a path from start to end on
	/// graph
	/// </summary>
	/// <param name="start">start value</param>
	/// <param name="finish">finish value</param>
	/// <param name="graph">graph to search</param>
	/// <returns>string for path or empty string if there is no path</returns>
	LinkedList<Waypoint> Search(Waypoint start, Waypoint end, Graph<Waypoint> graph)
	{
        SortedLinkedList<SearchNode<Waypoint>> searchList =
            new SortedLinkedList<SearchNode<Waypoint>>();
        Dictionary<GraphNode<Waypoint>, SearchNode<Waypoint>> mapping =
            new Dictionary<GraphNode<Waypoint>, SearchNode<Waypoint>>();

        // save references to start and end graph nodes
        GraphNode<Waypoint> startNode = graph.Find(start);
        GraphNode<Waypoint> endNode = graph.Find(end);

        // add search nodes for all graph nodes to list
        foreach (GraphNode<Waypoint> node in graph.Nodes)
        {
            SearchNode<Waypoint> searchNode = new SearchNode<Waypoint>(node);
            if (node == startNode)
            {
                searchNode.Distance = 0;
            }
            searchList.Add(searchNode);
            mapping.Add(node, searchNode);
        }

        string debug =  ConvertSearchListToString(searchList);

        // search until find end node or list is empty
        while (searchList.Count > 0)
        {
            // front of search list has smallest distance
            SearchNode<Waypoint> currentSearchNode = searchList.First.Value;
            searchList.RemoveFirst();
            GraphNode<Waypoint> currentGraphNode = currentSearchNode.GraphNode;
            mapping.Remove(currentGraphNode);

            // check for found end node
            if (currentGraphNode == endNode)
            {
                pathFoundEvent.Invoke(currentSearchNode.Distance);
                return BuildWaypointPath(currentSearchNode);
            }

            // loop through the current graph node's neighbors
            foreach (GraphNode<Waypoint> neighbor in currentGraphNode.Neighbors)
            {
                // only process neighbors still in the search list
                if (mapping.ContainsKey(neighbor))
                {
                    // check for new shortest distance on path from start to neighbor
                    float currentDistance = currentSearchNode.Distance +
                                            currentGraphNode.GetEdgeWeight(neighbor);
                    SearchNode<Waypoint> neighborSearchNode = mapping[neighbor];
                    if (currentDistance < neighborSearchNode.Distance)
                    {
                        // found a shorter path to the neighbor
                        neighborSearchNode.Distance = currentDistance;
                        neighborSearchNode.Previous = currentSearchNode;
                        searchList.Reposition(neighborSearchNode);

                        debug =  ConvertSearchListToString(searchList);
                    }
                    
                }
            }
        }

        // didn't find a path from start to end nodes
        return null;
	}
	
	/// <summary>
	/// Builds a waypoint path from the start node to the given end node
	/// </summary>
	/// <returns>waypoint path</returns>
	/// <param name="endNode">end node</param>
	private LinkedList<Waypoint> BuildWaypointPath(SearchNode<Waypoint> endNode)
	{
		LinkedList<Waypoint> path=new LinkedList<Waypoint>();
		path.AddFirst(endNode.GraphNode.Value);
		SearchNode<Waypoint> previous = endNode.Previous;
		while (previous != null)
		{
			path.AddFirst(previous.GraphNode.Value);
			previous = previous.Previous;
		}

		return path;
	}
    
	/// <summary>
	/// Converts the search list to string
	/// </summary>
	/// <returns>string for the search list</returns>
	/// <param name="searchList">search list</param>
	string ConvertSearchListToString(SortedLinkedList<SearchNode<Waypoint>> searchList)
	{
		StringBuilder listString = new StringBuilder();
		LinkedListNode<SearchNode<Waypoint>> currentNode = searchList.First;
		while (currentNode != null)
		{
			listString.Append("[");
			listString.Append(currentNode.Value.GraphNode.Value.Id + " ");
			listString.Append(currentNode.Value.Distance + "] ");
			currentNode = currentNode.Next;
		}
		return listString.ToString();
	}

    LinkedList<Waypoint> Search_DFS_BFS(Waypoint _start, Waypoint _goal, Graph<Waypoint> _graph, SearchType searchType)
    {
	      LinkedList<GraphNode<Waypoint>> searchList =
            new LinkedList<GraphNode<Waypoint>>();
        
        //Special case for start and finish the same
        if (_start == _goal)
        {
	        LinkedList<Waypoint> temp=new LinkedList<Waypoint>();
	        temp.AddFirst(_start);
            return temp;
        }else if (_graph.Find(this._start) == null || _graph.Find(_goal) == null)
        {
            //Start or finish not in graph
            return null;
        }
        else
        {
            //Add start node to dictionary and search list
            GraphNode<Waypoint> startNode = _graph.Find(this._start);
            Dictionary<GraphNode<Waypoint>, SearchNode<Waypoint>> pathNodes =
	            new Dictionary<GraphNode<Waypoint>, SearchNode<Waypoint>>();
            
            pathNodes.Add(startNode,new SearchNode<Waypoint>(null));
            searchList.AddFirst(startNode);
            
            //Loop until we exhaust all possible paths
            while (searchList.Count > 0)
            {
                //Extract front of search list
                GraphNode<Waypoint> currentNode = searchList.First.Value;
                searchList.RemoveFirst();
                
                //Explore each neighbor of this node
                foreach (GraphNode<Waypoint> neighbor in currentNode.Neighbors)
                {
                    //Check for found finish
                    if (neighbor.Value == _goal)
                    {
                        pathNodes.Add(neighbor,new SearchNode<Waypoint>(currentNode));
                        return ConvertPathToLinkedList_DFS_BFS(neighbor, pathNodes);
                    }
                    else if (pathNodes.ContainsKey(neighbor))
                    {
                        //Found a cycle, so skip this neighbor
                        continue;
                    }
                    else
                    {
                        //Link neighbor to current node in path
                        pathNodes.Add(neighbor,new SearchNode<Waypoint>(currentNode));
                        
                        //Add neighbor to front or back of search list
                        if (searchType == SearchType.DepthFirst)
                            searchList.AddFirst(neighbor);
                        else
                        {
                            searchList.AddLast(neighbor);
                        }
                        
                        // print("Just added "+neighbor.Value+" to search list");
                    }
                }
            }
            //Didn't find a path from start to finish
            return null;
        }
    }
    
    private LinkedList<Waypoint> ConvertPathToLinkedList_DFS_BFS(GraphNode<Waypoint> endNode, Dictionary<GraphNode<Waypoint>, SearchNode<Waypoint>> pathNodes)
    {
	    //Build linked list for path in correct order
	    LinkedList<GraphNode<Waypoint>> path = new LinkedList<GraphNode<Waypoint>>();
	    path.AddFirst(endNode);
	    
	    GraphNode<Waypoint> previous = pathNodes[endNode].GraphNode;
	    while (previous!=null)
	    {
		    path.AddLast(previous);
		    previous = pathNodes[previous].GraphNode;
	    }
        
	    //Build and return string
	    LinkedList<Waypoint> returnList=new LinkedList<Waypoint>();
	    LinkedListNode<GraphNode<Waypoint>> currentNode = path.First;
	    int nodeCount = 0;
	    while (currentNode != null)
	    {
		    nodeCount++;
		    returnList.AddFirst(currentNode.Value.Value);
		    _distances.AddFirst(pathNodes[currentNode.Value].Distance);
		    currentNode = currentNode.Next;
	    }

	    return returnList;
    }
    
    
    #region Events

    /// <summary>
    /// Adds the given listener for the PathFoundEvent
    /// </summary>
    /// <param name="listener">listener</param>
    public void AddPathFoundListener(UnityAction<float> listener)
    {
	    pathFoundEvent.AddListener(listener);
    }

    /// <summary>
    /// Adds the given listener for the PathTraversalCompleteEvent
    /// </summary>
    /// <param name="listener">listener</param>
    public void AddPathTraversalCompleteListener(UnityAction listener)
    {
	    pathTraversalCompleteEvent.AddListener(listener);
    }

    #endregion    
}