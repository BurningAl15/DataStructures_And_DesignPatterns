using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A traveler
/// </summary>
public class Traveler : MonoBehaviour
{
    // events fired by class
    PathFoundEvent pathFoundEvent = new PathFoundEvent();
    PathTraversalCompleteEvent pathTraversalCompleteEvent = new PathTraversalCompleteEvent();	
	
	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start()
	{
		
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
}
