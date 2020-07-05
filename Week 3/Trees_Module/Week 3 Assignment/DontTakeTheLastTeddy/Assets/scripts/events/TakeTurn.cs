using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// An event for when a player should take a turn
/// </summary>
public class TakeTurn : UnityEvent<PlayerName, Configuration>
{
}
