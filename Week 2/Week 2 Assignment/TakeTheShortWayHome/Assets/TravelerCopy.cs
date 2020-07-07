using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public class TravelerCopy : MonoBehaviour
{
    // events fired by class
    PathFoundEvent pathFoundEvent = new PathFoundEvent();
    PathTraversalCompleteEvent pathTraversalCompleteEvent = new PathTraversalCompleteEvent();	
    
    [SerializeField] private Waypoint start, end;
    private LinkedList<Waypoint> path = new LinkedList<Waypoint>();
    
	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start()
	{
		// EventManager.AddPathFoundInvoker(this);
		// EventManager.AddPathTraversalCompleteInvoker(this);
		
		Graph<Waypoint> graph= GraphBuilder.Graph;

		// print(graph.Count);

		path = Search(start, end, graph);

		// print(path.Count);
		// foreach (Waypoint point in path)
		// {
		// 	print("A");
		// 	print(point.Id);
		// }
	}
	
	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update()
	{
	}

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

    LinkedList<Waypoint> Search(Waypoint _start, Waypoint _goal, Graph<Waypoint> _graph)
    {
	    SortedLinkedList<SearchNode<Waypoint>> searchList = new SortedLinkedList<SearchNode<Waypoint>>();
	    Dictionary<GraphNode<Waypoint>, SearchNode<Waypoint>> searchNodesDictionary =
		    new Dictionary<GraphNode<Waypoint>, SearchNode<Waypoint>>();

	    GraphNode<Waypoint> startNode = _graph.Find(_start);
	    GraphNode<Waypoint> endNode = _graph.Find(_goal);
	 
	    // SearchNode<Waypoint> searchNode = new SearchNode<Waypoint>(startNode);

	    // searchList.AddFirst(searchNode);
	    // wayPointDictionary.Add(startNode,searchNode);

	    
	    //We add elements to search list, and search dictionary,
	    //if the node is startNode, set the distance to 0
	    foreach (GraphNode<Waypoint> node in _graph.Nodes)
	    {
		    SearchNode<Waypoint> temp = new SearchNode<Waypoint>(node);
		    print("Checking previous "+ temp.Previous);
			if (node == startNode)
			{
				temp.Distance = 0;
			}
			searchList.Add(temp);
			searchNodesDictionary.Add(node,temp);
			// print("Key: "+node.Value.Id+" Value: "+temp.GraphNode.Value.Id);
	    }
	    
	    //Initial Checking
	    
	    // for (int i = 0; i < searchNodesDictionary.Count; i++)
	    // {
		   //  GraphNode<Waypoint> node = searchNodesDictionary.ElementAt(i).Key;
		   //  SearchNode<Waypoint> temp;
		   //  if (searchNodesDictionary.TryGetValue(node, out temp))
		   //  {
			  //   print(i+" - ID: "+temp.GraphNode.Value.Id+" Graph Node: "+temp.GraphNode.Value);
		   //  }
	    // }
	    
	    //While search list is not empty (>0)
	    //
	    while (searchList.Count > 0)
	    {
		    //Instructions 1
		    SearchNode<Waypoint> currentSearchNode = searchList.First.Value;
		    // SearchNode<Waypoint> currentSearchNode = searchList.Last.Value;
		    //Start Node is current search node
		    print("Current Node: " + currentSearchNode.GraphNode.Value+ " - ID: "+currentSearchNode.GraphNode.Value.Id);
		    searchList.RemoveFirst();
		    GraphNode<Waypoint> currentGraphNode = currentSearchNode.GraphNode;
		    // searchNodesDictionary.Remove(currentGraphNode);

		    // for (int i = 0; i < searchNodesDictionary.Count; i++)
		    // {
			   //  GraphNode<Waypoint> node = searchNodesDictionary.ElementAt(i).Key;
			   //  SearchNode<Waypoint> temp;
			   //  if (searchNodesDictionary.TryGetValue(node, out temp))
			   //  {
				  //   print(i+" - ID: "+temp.GraphNode.Value.Id+" Graph Node: "+temp.GraphNode.Value);
			   //  }
		    // }
		    
		    
		    
		    if (currentGraphNode.Equals(endNode))
		    {
			    pathFoundEvent.Invoke(currentSearchNode.Distance);
		    
			    LinkedList<Waypoint> returnList = ConvertPathToLinkedList(endNode, searchNodesDictionary);
			    // searchNodesDictionary.Remove(currentGraphNode);
			    return returnList;
		    }
		    
		    foreach (GraphNode<Waypoint> neighbor in currentGraphNode.Neighbors)
		    {
			    SearchNode<Waypoint> _temp;
			    if (searchNodesDictionary.TryGetValue(neighbor, out _temp))
			    {
				    // print("4.1 ");
				    float dist = _temp.Distance + currentGraphNode.GetEdgeWeight(neighbor);
				    if (dist < _temp.Distance)
				    {
					    _temp.Distance = dist;
					    _temp.Previous = currentSearchNode;
					    searchList.Reposition(_temp);
					    print("4.2 ");
				    }
			    }
		    }
	    }

	    return null;
    }
    
    private LinkedList<Waypoint> ConvertPathToLinkedList(GraphNode<Waypoint> endNode, Dictionary<GraphNode<Waypoint>, SearchNode<Waypoint>> pathNodes)
    {
	    print(pathNodes.Count);

	    // for (int i = 1; i < pathNodes.Count; i++)
	    // {
		   //  GraphNode<Waypoint> node = pathNodes.ElementAt(i).Key;
		   //  SearchNode<Waypoint> temp;
		   //  if (pathNodes.TryGetValue(node, out temp))
		   //  {
			  //   print("Graph Node: "+temp.GraphNode.Value+ " - "+ i);
		   //  }
	    // }
	    
	    //Build linked list for path in correct order
	    LinkedList<GraphNode<Waypoint>> path = new LinkedList<GraphNode<Waypoint>>();

	    path.AddFirst(endNode);
	    // print(path.Count);
	    // print(pathNodes[endNode]);
	    
	    // GraphNode<Waypoint> previous = pathNodes[endNode].Previous.GraphNode;
	    //
	    // while (previous!=null)
	    // {
		   //  path.AddFirst(previous);
		   //  previous = pathNodes[previous].Previous.GraphNode;
	    // }

	    GraphNode<Waypoint> previous = pathNodes[endNode].GraphNode;
        
	    //Build and return string
	    LinkedList<Waypoint> returnList=new LinkedList<Waypoint>();
	    LinkedListNode<GraphNode<Waypoint>> currentNode = path.First;
	    int nodeCount = 0;
	    while (currentNode != null)
	    {
		    nodeCount++;
		    returnList.AddFirst(currentNode.Value.Value);
		    if (nodeCount < path.Count)
		    {
			    continue;
			    // returnList.AddFirst(currentNode.Value.Value);
		    }
	    
		    currentNode = currentNode.Next;
	    }

	    return returnList;
    }

    LinkedList<Waypoint> Search_2(Waypoint _start, Waypoint _goal, Graph<Waypoint> _graph, int option)
    {
	      LinkedList<GraphNode<Waypoint>> searchList =
            new LinkedList<GraphNode<Waypoint>>();
        
        //Special case for start and finish the same
        if (_start == _goal)
        {
	        LinkedList<Waypoint> temp=new LinkedList<Waypoint>();
	        temp.AddFirst(_start);
            return temp;
        }else if (_graph.Find(start) == null || _graph.Find(_goal) == null)
        {
            //Start or finish not in graph
            return null;
        }
        else
        {
            //Add start node to dictionary and search list
            GraphNode<Waypoint> startNode = _graph.Find(start);
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
                        return ConvertPathToLinkedList(neighbor, pathNodes);
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
                        if (option == 0)
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
    
        string _Search(Waypoint start, Waypoint finish,
            Graph<Waypoint> graph, Traveler.SearchType searchType)
        {
            LinkedList<GraphNode<Waypoint>> searchList =
                new LinkedList<GraphNode<Waypoint>>();

            // special case for start and finish the same
            if (start == finish)
            {
                return start.ToString();
            }
            else if (graph.Find(start) == null ||
                graph.Find(finish) == null)
            {
                // start or finish not in graph
                return "";
            }
            else
            {
                // add start node to dictionary and search list
                GraphNode<Waypoint> startNode = graph.Find(start);
                Dictionary<GraphNode<Waypoint>, SearchNode<Waypoint>> pathNodes =
                    new Dictionary<GraphNode<Waypoint>, SearchNode<Waypoint>>();
               
                pathNodes.Add(startNode, new SearchNode<Waypoint>(null));
                searchList.AddFirst(startNode);

                // loop until we exhaust all possible paths
                while (searchList.Count > 0)
                {
                    // extract front of search list
                    GraphNode<Waypoint> currentNode = searchList.First.Value;
                    searchList.RemoveFirst();

                    // explore each neighbor of this node
                    foreach (GraphNode<Waypoint> neighbor in currentNode.Neighbors)
                    {
                        // check for found finish
                        if (neighbor.Value == finish)
                        {
                            pathNodes.Add(neighbor, new SearchNode<Waypoint>(currentNode));
                            return ConvertPathToString(neighbor, pathNodes);
                        }
                        else if (pathNodes.ContainsKey(neighbor))
                        {
                            // found a cycle, so skip this neighbor
                            continue;
                        }
                        else
                        {
                            // link neighbor to current node in path
                            pathNodes.Add(neighbor, new SearchNode<Waypoint>(currentNode));

                            // add neighbor to front or back of search list
                            if (searchType == Traveler.SearchType.DepthFirst)
                            {
                                searchList.AddFirst(neighbor);
                            }
                            else
                            {
                                searchList.AddLast(neighbor);
                            }
                            // print("Just added " + neighbor.Value + " to search list");
                        }
                    }
                }

                // didn't find a path from start to finish
                return "";
            }
        }

        string ConvertPathToString(GraphNode<Waypoint> endNode, Dictionary<GraphNode<Waypoint>, SearchNode<Waypoint>> pathNodes)
        {
	        // build linked list for path in correct order
	        LinkedList<GraphNode<Waypoint>> path = new LinkedList<GraphNode<Waypoint>>();
	        path.AddFirst(endNode);
	        GraphNode<Waypoint> previous = pathNodes[endNode].GraphNode;
	        while (previous != null)
	        {
		        path.AddFirst(previous);
		        previous = pathNodes[previous].GraphNode;
	        }

	        // build and return string
	        StringBuilder pathString = new StringBuilder();
	        LinkedListNode<GraphNode<Waypoint>> currentNode = path.First;
	        int nodeCount = 0;
	        float sumDistance = 0;
	        while (currentNode != null)
	        {
		        nodeCount++;
		        pathString.Append(currentNode.Value.Value.Id);
		        sumDistance += pathNodes[currentNode.Value].Distance;
		        //Take off this for the real algorithm
		        if (nodeCount < path.Count)
		        {
			        pathString.Append(" -- ");
		        }
		        currentNode = currentNode.Next;
	        }
	        print("Total Distance: "+sumDistance);
	        return pathString.ToString();
        }

}
