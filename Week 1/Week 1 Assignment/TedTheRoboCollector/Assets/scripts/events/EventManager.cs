using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// The event manager
/// </summary>
public class EventManager : MonoBehaviour
{
    #region Fields

    // save lists of invokers and listeners
    static List<PickupSpawner> invokers_Pickup = new List<PickupSpawner> ();
    static List<UnityAction<GameObject>> listeners_Pickup = new List<UnityAction<GameObject>> ();
    
    static Collector invokers_AddPoints = new Collector();
    static UnityAction<int> listeners_AddPoints;

    #endregion

    #region Public methods

    /// <summary>
    /// Adds the given script as an invoker
    /// </summary>
    /// <param name="invoker">the invoker</param>
    public static void AddInvoker_Pickup(PickupSpawner invoker)
    {
        // add invoker to list and add all listeners to invoker
        invokers_Pickup.Add(invoker);
        foreach (UnityAction<GameObject> listener in listeners_Pickup)
        {
            invoker.AddListener_Pickup(listener);
        }
    }

    /// <summary>
    /// Adds the given event handler as a listener
    /// </summary>
    /// <param name="handler">the event handler</param>
    public static void AddListener_Pickup(UnityAction<GameObject> handler)
    {       
        // add listener to list and to all invokers
        listeners_Pickup.Add(handler);
        foreach (PickupSpawner invoker in invokers_Pickup)
        {
            invoker.AddListener_Pickup(handler);
        }
    }
    
    /// <summary>
    /// Adds the given script as an invoker
    /// </summary>
    /// <param name="invoker">the invoker</param>
    public static void AddInvoker_AddPoints(Collector invoker)
    {
        // add invoker to list and add all listeners to invoker
        invokers_AddPoints=invoker;
        invoker.AddListener_AddPoints(listeners_AddPoints);
    }

    /// <summary>
    /// Adds the given event handler as a listener
    /// </summary>
    /// <param name="handler">the event handler</param>
    public static void AddListener_AddPoints(UnityAction<int> handler)
    {       
        // add listener to list and to all invokers
        listeners_AddPoints=handler;
        invokers_AddPoints.AddListener_AddPoints(handler);
    }

    #endregion
}
