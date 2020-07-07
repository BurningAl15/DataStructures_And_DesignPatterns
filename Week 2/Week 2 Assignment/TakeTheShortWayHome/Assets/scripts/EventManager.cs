using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// The event manager
/// </summary>
public static class EventManager
{
    #region Fields

    // path found event support
    static List<Traveler> pathFoundInvokers = new List<Traveler>();
    static List<UnityAction<float>> pathFoundListeners = new List<UnityAction<float>>();

    // path traversal complete event support
    static List<Traveler> pathTraversalCompleteInvokers = new List<Traveler>();
    static List<UnityAction> pathTraversalCompleteListeners = new List<UnityAction>();

    #endregion

    #region Methods

    /// <summary>
    /// Adds the invoker for the PathFoundEvent
    /// </summary>
    /// <param name="invoker">invoker</param>
    public static void AddPathFoundInvoker(Traveler invoker)
    {
        pathFoundInvokers.Add(invoker);
        foreach (UnityAction<float> listener in pathFoundListeners)
        {
            invoker.AddPathFoundListener(listener);
        }
    }

    /// <summary>
    /// Adds the listener for the PathFoundEvent
    /// </summary>
    /// <param name="listener">listener</param>
    public static void AddPathFoundListener(UnityAction<float> listener)
    {
        pathFoundListeners.Add(listener);
        foreach (Traveler invoker in pathFoundInvokers)
        {
            invoker.AddPathFoundListener(listener);
        }
    }

    /// <summary>
    /// Adds the invoker for the PathTraversalCompleteEvent
    /// </summary>
    /// <param name="invoker">invoker</param>
    public static void AddPathTraversalCompleteInvoker(Traveler invoker)
    {
        pathTraversalCompleteInvokers.Add(invoker);
        foreach (UnityAction listener in pathTraversalCompleteListeners)
        {
            invoker.AddPathTraversalCompleteListener(listener);
        }
    }

    /// <summary>
    /// Adds the listener for the PathTraversalCompleteEvent
    /// </summary>
    /// <param name="listener">listener</param>
    public static void AddPathTraversalCompleteListener(UnityAction listener)
    {
        pathTraversalCompleteListeners.Add(listener);
        foreach (Traveler invoker in pathTraversalCompleteInvokers)
        {
            invoker.AddPathTraversalCompleteListener(listener);
        }
    }

    #endregion
}
