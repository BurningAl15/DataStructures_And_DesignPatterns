using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays the statistics
/// </summary>
public class StatisticsDisplay : MonoBehaviour
{
    [SerializeField]
    Text easyEasyPlayer1Wins;
    [SerializeField]
    Text easyEasyPlayer2Wins;

    [SerializeField]
    Text mediumMediumPlayer1Wins;
    [SerializeField]
    Text mediumMediumPlayer2Wins;

    [SerializeField]
    Text hardHardPlayer1Wins;
    [SerializeField]
    Text hardHardPlayer2Wins;

    [SerializeField]
    Text easyMediumPlayer1Wins;
    [SerializeField]
    Text easyMediumPlayer2Wins;

    [SerializeField]
    Text easyHardPlayer1Wins;
    [SerializeField]
    Text easyHardPlayer2Wins;

    [SerializeField]
    Text mediumHardPlayer1Wins;
    [SerializeField]
    Text mediumHardPlayer2Wins;

    /// <summary>
	/// Use this for initialization
	/// </summary>
	void Start()
	{
		// EventManager.AddGameStartedListener(HandleGameStartedEvent);
    }
    
    // void HandleGameStartedEvent(PlayerName player)
    // {
	   //  // if (player == PlayerName.Player1)
	   //  // {
		  //  //  gameOverText.text = "Player 1 Won!";
	   //  // }
	   //  // else
	   //  // {
		  //  //  gameOverText.text = "Player 2 Won!";
	   //  // }
	   //  // gameOverText.enabled = true;
    // }
}
