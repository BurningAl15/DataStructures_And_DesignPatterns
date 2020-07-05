using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// An event for when a player is done taking their turn
/// </summary>
public class TurnOver : UnityEvent<PlayerName, Configuration>
{
}
