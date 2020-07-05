using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The HUD
/// </summary>
public class Hud : MonoBehaviour
{
    [SerializeField]
    Text player1Text;
    [SerializeField]
    Text player2Text;
    [SerializeField]
    Text gameOverText;

    /// <summary>
    /// Awake is called before Start
    /// </summary>
    void Awake()
    {
        // register listeners
        EventManager.AddTakeTurnListener(HandleTakeTurnEvent);
        EventManager.AddTurnOverListener(HandleTurnOverEvent);
        EventManager.AddGameOverListener(HandleGameOverEvent);

        // hide game over text
        gameOverText.enabled = false;
    }

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
    /// Highlights the text for the current player
    /// </summary>
    /// <param name="player">whose turn it is</param>
    /// <param name="unused">unused</param>
    void HandleTakeTurnEvent(PlayerName player, Configuration unused)
    {
        if (player == PlayerName.Player1)
        {
            player1Text.color = Color.green;
        }
        else
        {
            player2Text.color = Color.green;
        }
    }

    /// <summary>
    /// Unhighlights the text for the player who just
    /// finished their turn
    /// </summary>
    /// <param name="player">who just finished their turn</param>
    /// <param name="boardConfiguration">the new board configuration</param>
    void HandleTurnOverEvent(PlayerName player, Configuration boardConfiguration)
    {
        if (player == PlayerName.Player1)
        {
            player1Text.color = Color.white;
        }
        else
        {
            player2Text.color = Color.white;
        }
    }

    /// <summary>
    /// Displays the game over test
    /// </summary>
    /// <param name="player">who won the game</param>
    void HandleGameOverEvent(PlayerName player)
    {
        if (player == PlayerName.Player1)
        {
            gameOverText.text = "Player 1 Won!";
        }
        else
        {
            gameOverText.text = "Player 2 Won!";
        }
        gameOverText.enabled = true;
    }
}
