using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

/// <summary>
/// Game manager
/// </summary>
public class DontTakeTheLastTeddy : MonoBehaviour
{
    Board board;
    Player player1;
    Player player2;

    // events invoked by class
    TakeTurn takeTurnEvent = new TakeTurn();
    GameOver gameOverEvent = new GameOver();
    GameStarting gameStarting=new GameStarting();

    private Timer timerGame;
    [SerializeField] int n = 100;

    private int counter = 0;
    private bool gameEnd = false;
    
    List<int> winCounts=new List<int>();

    [SerializeField] private Text lapText;
    
    /// <summary>
    /// Awake is called before Start
    /// </summary>
    void Awake()
    {
        // retrieve board and player references
        board = GameObject.FindGameObjectWithTag("Board").GetComponent<Board>();
        player1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<Player>();
        player2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player>();

        timerGame = gameObject.AddComponent<Timer>();

        for (int i = 0; i < 12; i++)
        {
            winCounts.Add(0);
        }

        // register as invoker and listener
        EventManager.AddTakeTurnInvoker(this);
        EventManager.AddGameOverInvoker(this);
        EventManager.AddGameStartedInvoker(this);
        
        EventManager.AddTurnOverListener(HandleTurnOverEvent);
    }

    public void AddTimerGameStartingListener(UnityAction listener)
    {
        gameStarting.AddListener(listener);
    }
    
	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start()
    {
        Difficulty diff1;
        Difficulty diff2;

        if (counter < 100)
        {
            diff1 = Difficulty.Easy;
            diff2 = Difficulty.Easy;
        }
        else if (counter >= 100 && counter < 200)
        {
            diff1 = Difficulty.Medium;
            diff2 = Difficulty.Medium;
        }
        else if (counter >= 200 && counter < 300)
        {
            diff1 = Difficulty.Hard;
            diff2 = Difficulty.Hard;
        }
        else if (counter >= 300 && counter < 400)
        {
            diff1 = Difficulty.Medium;
            diff2 = Difficulty.Easy;
        }
        else if (counter >= 400 && counter < 500)
        {
            diff1 = Difficulty.Hard;
            diff2 = Difficulty.Easy;
        }
        else 
        {
            diff1 = Difficulty.Hard;
            diff2 = Difficulty.Medium;
        }
        lapText.text = "Lap: " + counter + " of 600";

        StartGame((PlayerName) (counter % 2)
            , diff1
            , diff2);

        timerGame.Duration = GameConstants.AiThinkSeconds;
        timerGame.Run();
    }

    private float currentWaitTime = 0;
    private float waitTime = .01f;
    
    private void Update()
    {
        if (timerGame.Finished && gameEnd)
        {
            currentWaitTime += Time.deltaTime;
            
            if (counter < n && currentWaitTime>=waitTime)
            {
                counter++;
                Difficulty diff1;
                Difficulty diff2;

                if (counter < 100)
                {
                    diff1 = Difficulty.Easy;
                    diff2 = Difficulty.Easy;
                }
                else if (counter >= 100 && counter < 200)
                {
                    diff1 = Difficulty.Medium;
                    diff2 = Difficulty.Medium;
                }
                else if (counter >= 200 && counter < 300)
                {
                    diff1 = Difficulty.Hard;
                    diff2 = Difficulty.Hard;
                }
                else if (counter >= 300 && counter < 400)
                {
                    diff1 = Difficulty.Medium;
                    diff2 = Difficulty.Easy;
                }
                else if (counter >= 400 && counter < 500)
                {
                    diff1 = Difficulty.Hard;
                    diff2 = Difficulty.Easy;
                }
                else 
                {
                    diff1 = Difficulty.Hard;
                    diff2 = Difficulty.Medium;
                }
                
                StartGame((PlayerName) (counter % 2)
                    , diff1
                    , diff2);
                
                lapText.text = "Lap: " + counter + " of 600";

                timerGame.Duration = GameConstants.AiThinkSeconds;
                timerGame.Run();
                gameStarting.Invoke();
                gameEnd = false;
                currentWaitTime = 0;
            }
            else if (counter >= n)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
            }
        }
    }


    /// <summary>
    /// Adds the given listener for the TakeTurn event
    /// </summary>
    /// <param name="listener">listener</param>
    public void AddTakeTurnListener(UnityAction<PlayerName, Configuration> listener)
    {
        takeTurnEvent.AddListener(listener);
    }

    /// <summary>
    /// Adds the given listener for the GameOver event
    /// </summary>
    /// <param name="listener">listener</param>
    public void AddGameOverListener(UnityAction<PlayerName> listener)
    {
        gameOverEvent.AddListener(listener);
    }

    /// <summary>
    /// Starts a game with the given player taking the
    /// first turn
    /// </summary>
    /// <param name="firstPlayer">player taking first turn</param>
    /// <param name="player1Difficulty">difficulty for player 1</param>
    /// <param name="player2Difficulty">difficulty for player 2</param>
    void StartGame(PlayerName firstPlayer, Difficulty player1Difficulty,
        Difficulty player2Difficulty)
    {
        // set player difficulties
        player1.Difficulty = player1Difficulty;
        player2.Difficulty = player2Difficulty;

        // create new board
        board.CreateNewBoard();
        takeTurnEvent.Invoke(firstPlayer,
            board.Configuration);
    }

    /// <summary>
    /// Handles the TurnOver event by having the 
    /// other player take their turn
    /// </summary>
    /// <param name="player">who finished their turn</param>
    /// <param name="newConfiguration">the new board configuration</param>
    void HandleTurnOverEvent(PlayerName player, 
        Configuration newConfiguration)
    {
        board.Configuration = newConfiguration;

        // check for game over
        if (newConfiguration.Empty)
        {
            // fire event with winner
            if (player == PlayerName.Player1)
            {
                gameOverEvent.Invoke(PlayerName.Player2);
                // print("------- Player 2 Win! --------");
            }
            else
            {
                gameOverEvent.Invoke(PlayerName.Player1);
                // print("------- Player 1 Win! --------");
            }

            if (counter < 100)
            {
                if (player == PlayerName.Player1)
                {
                    winCounts[0]++;
                    PlayerPrefs.SetInt(PlayerPrefsInitials.ezez+"1", winCounts[0]);
                }
                else
                {
                    winCounts[1]++;
                    PlayerPrefs.SetInt(PlayerPrefsInitials.ezez+"2", winCounts[1]);
                }
            }
            else if (counter >= 100 && counter < 200)
            {
                if (player == PlayerName.Player1)
                {
                    winCounts[2]++;
                    PlayerPrefs.SetInt(PlayerPrefsInitials.mdmd+"1", winCounts[2]);
                }
                else
                {
                    winCounts[3]++;
                    PlayerPrefs.SetInt(PlayerPrefsInitials.mdmd+"2", winCounts[3]);
                }
            }
            else if (counter >= 200 && counter < 300)
            {
                if (player == PlayerName.Player1)
                {
                    winCounts[4]++;
                    PlayerPrefs.SetInt(PlayerPrefsInitials.hdhd+"1", winCounts[4]);
                }
                else
                {
                    winCounts[5]++;
                    PlayerPrefs.SetInt(PlayerPrefsInitials.hdhd+"2", winCounts[5]);
                }
            }
            else if (counter >= 300 && counter < 400)
            {
                if (player == PlayerName.Player1)
                {
                    winCounts[6]++;
                    PlayerPrefs.SetInt(PlayerPrefsInitials.ezmd+"1", winCounts[6]);
                }
                else
                {
                    winCounts[7]++;
                    PlayerPrefs.SetInt(PlayerPrefsInitials.ezmd+"2", winCounts[7]);
                }
            }
            else if (counter >= 400 && counter < 500)
            {
                if (player == PlayerName.Player1)
                {
                    winCounts[8]++;
                    PlayerPrefs.SetInt(PlayerPrefsInitials.ezhd+"1", winCounts[8]);
                }
                else
                {
                    winCounts[9]++;
                    PlayerPrefs.SetInt(PlayerPrefsInitials.ezhd+"2", winCounts[9]);
                }
            }
            else 
            {
                if (player == PlayerName.Player1)
                {
                    winCounts[10]++;
                    PlayerPrefs.SetInt(PlayerPrefsInitials.mdhd+"1", winCounts[10]);
                }
                else
                {
                    winCounts[11]++;
                    PlayerPrefs.SetInt(PlayerPrefsInitials.mdhd+"2", winCounts[11]);
                }
            }
            
            gameEnd = true;
        }
        else
        {
            // game not over, so give other player a turn
            if (player == PlayerName.Player1)
            {
                takeTurnEvent.Invoke(PlayerName.Player2,
                    newConfiguration);
            }
            else
            {
                takeTurnEvent.Invoke(PlayerName.Player1,
                    newConfiguration);
            }
        }
    }
}
