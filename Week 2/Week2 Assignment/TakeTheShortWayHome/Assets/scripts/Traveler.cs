using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

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
    
    [SerializeField] private Waypoint start, end;
    private LinkedList<Waypoint> _path = new LinkedList<Waypoint>();
    private LinkedList<float> _distances = new LinkedList<float>();
    
    [SerializeField] List<Waypoint> simpleList=new List<Waypoint>();
    [SerializeField] List<float> distances=new List<float>();
    private int counter = 0;
    private int distanceCounter = 0;
	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start()
	{
		EventManager.AddPathFoundInvoker(this);
		EventManager.AddPathTraversalCompleteInvoker(this);
		
		Graph<Waypoint> graph= GraphBuilder.Graph;

		_path = Search_2(start, end, graph,SearchType.BreadthFirst);

		foreach (Waypoint waypoint in _path)
		{
			simpleList.Add(waypoint);
			print(waypoint.Id);
		}

		// foreach (float distance in _distances)
		// {
		// 	distances.Add(distance);
		// }

		for (int i = 0; i < simpleList.Count-1; i++)
		{
			distances.Add(Vector2.Distance(simpleList[i].Position,simpleList[i+1].Position));
		}
		distances.Add(Vector2.Distance(simpleList[simpleList.Count-1].Position,end.Position));
		pathFoundEvent.Invoke(distances[0]);
		
		this.transform.position = simpleList[counter].Position;
		counter++;
		distanceCounter = counter - 1;
		// _path = Search(start, end, graph);
		// print( "BFS: "+_Search(start, end, graph, SearchType.BreadthFirst));
		// print( "DFS: "+_Search(start, end, graph, SearchType.DepthFirst));
	}

	private float toPoint = 0;
	
	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update()
	{
		if (!this.transform.position.Equals(simpleList[counter].Position))
		{
			toPoint += Time.deltaTime;
			this.transform.position = Vector2.Lerp(this.transform.position, simpleList[counter].Position, toPoint);
		}
		else
		{
			toPoint = 0;
			if (counter < simpleList.Count - 1)
			{
				counter++;
				distanceCounter = counter - 1;
				pathFoundEvent.Invoke(distances[distanceCounter]);
			}
		}
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
    
    LinkedList<Waypoint> Search_2(Waypoint _start, Waypoint _goal, Graph<Waypoint> _graph, SearchType searchType)
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
    
    private LinkedList<Waypoint> ConvertPathToLinkedList(GraphNode<Waypoint> endNode, Dictionary<GraphNode<Waypoint>, SearchNode<Waypoint>> pathNodes)
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