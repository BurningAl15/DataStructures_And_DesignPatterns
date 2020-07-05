using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Th event manager
/// </summary>
public static class EventManager
{
    #region Fields

    static List<DontTakeTheLastTeddy> takeTurnInvokers =
        new List<DontTakeTheLastTeddy>();
    static List<UnityAction<PlayerName, Configuration>> takeTurnListeners =
        new List<UnityAction<PlayerName, Configuration>>();

    static List<Player> turnOverInvokers = new List<Player>();
    static List<UnityAction<PlayerName, Configuration>> turnOverListeners =
        new List<UnityAction<PlayerName, Configuration>>();

    static List<DontTakeTheLastTeddy> gameOverInvokers = 
        new List<DontTakeTheLastTeddy>();
    static List<UnityAction<PlayerName>> gameOverListeners =
        new List<UnityAction<PlayerName>>();
    
    #endregion

    #region Methods

    /// <summary>
    /// Adds the parameter as a TakeTurn event invoker
    /// </summary>
    /// <param name="invoker">invoker</param>
    public static void AddTakeTurnInvoker(DontTakeTheLastTeddy invoker)
    {
        takeTurnInvokers.Add(invoker);
        foreach (UnityAction<PlayerName, Configuration> listener in takeTurnListeners)
        {
            invoker.AddTakeTurnListener(listener);
        }
    }

    /// <summary>
    /// Adds the parameter as a TakeTurn event listener
    /// </summary>
    /// <param name="listener">listener</param>
    public static void AddTakeTurnListener(UnityAction<PlayerName, Configuration> listener)
    {
        takeTurnListeners.Add(listener);
        foreach(DontTakeTheLastTeddy invoker in takeTurnInvokers)
        {
            invoker.AddTakeTurnListener(listener);
        }
    }

    /// <summary>
    /// Adds the parameter as a TurnOver event invoker
    /// </summary>
    /// <param name="invoker">invoker</param>
    public static void AddTurnOverInvoker(Player invoker)
    {
        turnOverInvokers.Add(invoker);
        foreach (UnityAction<PlayerName, Configuration> listener in turnOverListeners)
        {
            invoker.AddTurnOverListener(listener);
        }
    }

    /// <summary>
    /// Adds the parameter as a TurnOver event listener
    /// </summary>
    /// <param name="listener">listener</param>
    public static void AddTurnOverListener(
        UnityAction<PlayerName, Configuration> listener)
    {
        turnOverListeners.Add(listener);
        foreach (Player invoker in turnOverInvokers)
        {
            invoker.AddTurnOverListener(listener);
        }
    }

    /// <summary>
    /// Adds the parameter as a GameOver event invoker
    /// </summary>
    /// <param name="invoker">invoker</param>
    public static void AddGameOverInvoker(DontTakeTheLastTeddy invoker)
    {
        gameOverInvokers.Add(invoker);
        foreach (UnityAction<PlayerName> listener in gameOverListeners)
        {
            invoker.AddGameOverListener(listener);
        }
    }

    /// <summary>
    /// Adds the parameter as a GameOver event listener
    /// </summary>
    /// <param name="listener">listener</param>
    public static void AddGameOverListener(UnityAction<PlayerName> listener)
    {
        gameOverListeners.Add(listener);
        foreach(DontTakeTheLastTeddy invoker in gameOverInvokers)
        {
            invoker.AddGameOverListener(listener);
        }
    }

    #endregion
}
