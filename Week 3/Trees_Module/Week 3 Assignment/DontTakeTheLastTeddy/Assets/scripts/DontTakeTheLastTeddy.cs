using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
    [SerializeField] int n = 5;

    private int counter = 0;
    
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
        // AddTimerGameStarting(HandleThinkingTimerFinished);

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
        int diff1 = Random.Range(0, 3);
        int diff2 = Random.Range(0, 3);

        StartGame((PlayerName) (counter % 2)
            , (Difficulty) diff1
            , (Difficulty) diff2);
        
        timerGame.Duration = GameConstants.AiThinkSeconds;
        timerGame.Run();
        
        print((PlayerName) (counter % 2) + " started");
        print("Player 1 Difficulty: "+(Difficulty)diff1);
        print("Player 2 Difficulty: "+(Difficulty)diff2);
    }

    private float currentWaitTime = 0;
    private float waitTime = 3;
    
    private void Update()
    {
        if (timerGame.Finished)
        {
            currentWaitTime += Time.deltaTime;
            
            if (counter < n && currentWaitTime>=waitTime)
            {
                counter++;
                int diff1 = Random.Range(0, 3);
                int diff2 = Random.Range(0, 3);
                
                StartGame((PlayerName) (counter % 2)
                    , (Difficulty) diff1
                    , (Difficulty) diff2);
                
                print((PlayerName) (counter % 2) + " started");
                print("Player 1 Difficulty: " + (Difficulty) diff1);
                print("Player 2 Difficulty: " + (Difficulty) diff2);
                
                timerGame.Duration = GameConstants.AiThinkSeconds;
                timerGame.Run();
                gameStarting.Invoke();
                currentWaitTime = 0;
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
                print("------- Player 2 Win! --------");
            }
            else
            {
                gameOverEvent.Invoke(PlayerName.Player1);
                print("------- Player 1 Win! --------");
            }
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
