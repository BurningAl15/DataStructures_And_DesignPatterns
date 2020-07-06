using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A player
/// </summary>
public class Player : MonoBehaviour
{
    private PlayerName myName;
    private Timer thinkingTimer;

    // minimax search support
    private Difficulty difficulty;
    private int searchDepth = 0;
    private MinimaxTree<Configuration> tree;

    // events invoked by class
    private TurnOver turnOverEvent = new TurnOver();

    // saved for efficiency
    private LinkedList<MinimaxTreeNode<Configuration>> nodeList =
        new LinkedList<MinimaxTreeNode<Configuration>>();

    private List<int> binContents = new List<int>();

    private List<Configuration> newConfigurations =
        new List<Configuration>();

    /// <summary>
    /// Awake is called before Start
    /// </summary>
    private void Awake()
    {
        // set name
        if (CompareTag("Player1"))
            myName = PlayerName.Player1;
        else
            myName = PlayerName.Player2;

        // add timer component
        thinkingTimer = gameObject.AddComponent<Timer>();
        thinkingTimer.Duration = GameConstants.AiThinkSeconds;
        thinkingTimer.AddTimerFinishedListener(HandleThinkingTimerFinished);

        // register as invoker and listener
        EventManager.AddTurnOverInvoker(this);
        EventManager.AddTakeTurnListener(HandleTakeTurnEvent);
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update()
    {
    }

    /// <summary>
    /// Gets and sets the difficulty for the player
    /// </summary>
    public Difficulty Difficulty
    {
        get => difficulty;
        set
        {
            difficulty = value;
            switch (difficulty)
            {
                case Difficulty.Easy:
                    searchDepth = GameConstants.EasyMinimaxDepth;
                    break;
                case Difficulty.Medium:
                    searchDepth = GameConstants.MediumMinimaxDepth;
                    break;
                case Difficulty.Hard:
                    searchDepth = GameConstants.HardMinimaxDepth;
                    break;
                default:
                    searchDepth = GameConstants.EasyMinimaxDepth;
                    break;
            }
        }
    }

    /// <summary>
    /// Adds the given listener for the TurnOver event
    /// </summary>
    /// <param name="listener">listener</param>
    public void AddTurnOverListener(
        UnityAction<PlayerName, Configuration> listener)
    {
        turnOverEvent.AddListener(listener);
    }

    /// <summary>
    /// Handles the TakeTurn event
    /// </summary>
    /// <param name="player">whose turn it is</param>
    /// <param name="boardConfiguration">current board configuration</param>
    private void HandleTakeTurnEvent(PlayerName player,
        Configuration boardConfiguration)
    {
        // only take turn if it's our turn
        if (player == myName)
        {
            tree = BuildTree(boardConfiguration);
            thinkingTimer.Run();
        }
    }

    /// <summary>
    /// Builds the tree
    /// </summary>
    /// <param name="boardConfiguration">current board configuration</param>
    /// <returns>tree</returns>
    private MinimaxTree<Configuration> BuildTree(
        Configuration boardConfiguration)
    {
        // build tree to appropriate depth
        var tree =
            new MinimaxTree<Configuration>(boardConfiguration);
        nodeList.Clear();
        nodeList.AddLast(tree.Root);
        while (nodeList.Count > 0)
        {
            var currentNode =
                nodeList.First.Value;
            nodeList.RemoveFirst();
            var children =
                GetNextConfigurations(currentNode.Value);
            foreach (var child in children)
            {
                // STUDENTS: only add to tree if within search depth
                var childNode =
                    new MinimaxTreeNode<Configuration>(
                        child, currentNode);
                if (ChildDepth(childNode) <= searchDepth)
                {
                    tree.AddNode(childNode);
                    nodeList.AddLast(childNode);
                }
            }
        }

        return tree;
    }

    private int ChildDepth(MinimaxTreeNode<Configuration> child)
    {
        if (child.Parent == null) return 0;

        var depth = 0;
        var currentNode = child;

        while (currentNode.Parent != null)
        {
            depth++;
            currentNode = currentNode.Parent;
        }

        return depth;
    }

    /// <summary>
    /// Handles the thinking timer finishing
    /// </summary>
    private void HandleThinkingTimerFinished()
    {
        // do the search and pick the move
        Minimax(tree.Root, true);

        // find child node with maximum score
        var children =
            tree.Root.Children;
        var maxChildNode = children[0];
        for (var i = 1; i < children.Count; i++)
            if (children[i].MinimaxScore > maxChildNode.MinimaxScore)
                maxChildNode = children[i];

        // provide new configuration as second argument
        turnOverEvent.Invoke(myName, maxChildNode.Value);
    }

    /// <summary>
    /// Gets a list of the possible next configurations
    /// given the current configuration
    /// </summary>
    /// <param name="currentConfiguration">current configuration</param>
    /// <returns>list of next configurations</returns>
    private List<Configuration> GetNextConfigurations(
        Configuration currentConfiguration)
    {
        newConfigurations.Clear();
        var currentBins = currentConfiguration.Bins;
        for (var i = 0; i < currentBins.Count; i++)
        {
            var currentBinCount = currentBins[i];
            while (currentBinCount > 0)
            {
                // take one teddy from current bin
                currentBinCount--;

                // add new next configuration to list
                binContents.Clear();
                binContents.AddRange(currentBins);
                binContents[i] = currentBinCount;
                newConfigurations.Add(
                    new Configuration(binContents));
            }
        }

        return newConfigurations;
    }

    /// <summary>
    /// Assigns minimax scores to the tree nodes
    /// </summary>
    /// <param name="tree">tree to mark with scores</param>
    /// <param name="maximizing">whether or not we're maximizing</param>
    private void Minimax(MinimaxTreeNode<Configuration> tree,
        bool maximizing)
    {
        // recurse on children
        var children = tree.Children;
        if (children.Count > 0)
        {
            foreach (var child in children)
            // toggle maximizing as we move down
                Minimax(child, !maximizing);

            // set default node minimax score
            if (maximizing)
                tree.MinimaxScore = int.MinValue;
            else
                tree.MinimaxScore = int.MaxValue;

            // find maximum or minimum value in children
            foreach (var child in children)
                if (maximizing)
                {
                    // check for higher minimax score
                    if (child.MinimaxScore > tree.MinimaxScore) tree.MinimaxScore = child.MinimaxScore;
                }
                else
                {
                    // minimizing, check for lower minimax score
                    if (child.MinimaxScore < tree.MinimaxScore) tree.MinimaxScore = child.MinimaxScore;
                }
        }
        else
        {
            // leaf nodes are the base case
            AssignHeuristicMinimaxScore(tree, maximizing);
        }
    }

    /// <summary>
    /// Assigns the end of game minimax score
    /// </summary>
    /// <param name="node">node to mark with score</param>
    /// <param name="maximizing">whether or not we're maximizing</param>
    private void AssignEndOfGameMinimaxScore(MinimaxTreeNode<Configuration> node,
        bool maximizing)
    {
        if (maximizing)
            // other player took the last teddy
            node.MinimaxScore = 1;
        else
            // we took the last teddy
            node.MinimaxScore = 0;
    }

    /// <summary>
    /// Assigns a heuristic minimax score to the given node
    /// </summary>
    /// <param name="node">node to mark with score</param>
    /// <param name="maximizing">whether or not we're maximizing</param>
    private void AssignHeuristicMinimaxScore(
        MinimaxTreeNode<Configuration> node,
        bool maximizing)
    {
        // might have reached an end-of-game configuration
        if (node.Value.Empty)
        {
            AssignEndOfGameMinimaxScore(node, maximizing);
        }
        else
        {
            // save list of non-empty bins
            var bins = node.Value.NonEmptyBins;

            // use a heuristic evaluation function to score the node
            // we always want our opponent to take the last teddy

            // Check number of bins
            if (bins.Count == 1)
            {
                // Single bin, proceed with eval
                node.MinimaxScore = CheckBins(bins[0], maximizing);
            }
            else if (bins.Count == 2)
            {
                // Two bins
                // If each bin has only one teddy, any move we make means we win
                // Thus this is a good move for maximizing and bad for minimizing
                if (bins[0] == 1 && bins[1] == 1)
                    node.MinimaxScore = 0.9f;
                // In case there's more teddies in one bin then the other
                // you can just take all the teddies from that bin and win
                else if (bins[0] > bins[1] || bins[0] < bins[1])
                    node.MinimaxScore = 0.95f;
                // In any other case, board state score is sum of individual bin scores
                else
                    node.MinimaxScore = CheckBins(bins[0], maximizing) +
                                        CheckBins(bins[1], maximizing);
            }
            else if (bins.Count == 3)
            {
                // Three bins
                // Opposite of two bins, if there's one teddy in each we lose every time
                // Bad for maximizing and good for minimizing
                if (bins[0] == 1 && bins[1] == 1 && bins[2] == 1)
                    node.MinimaxScore = 0.1f;
                // In any other case, board state score is sum of individual bin scores
                else
                    node.MinimaxScore = CheckBins(bins[0], maximizing) +
                                        CheckBins(bins[1], maximizing) + CheckBins(bins[2], maximizing);
            }
            else
            {
                // Four bins
                // Same as two bins for equal reasons
                if (bins[0] == 1 && bins[1] == 1)
                    node.MinimaxScore = 0.9f;
                // In any other case, board state score is sum of individual bin scores
                else
                    node.MinimaxScore = CheckBins(bins[0], maximizing) + CheckBins(bins[1], maximizing) +
                                        CheckBins(bins[2], maximizing) + CheckBins(bins[3], maximizing);
            }
        }
    }

    /// <summary>
    /// Check the given bin
    /// </summary>
    /// <param name="bins">bin to evaluate</param>
    /// <param name="maximizing">whether we're maximizing or now</param>
    /// <returns>a float point value between 0 and 1</returns>
    private float CheckBins(int bin, bool maximizing)
    {
        // If we're maximizing we want higher value
        // If we're minimizing we want lower
        // Adjusted by this weight
        int weight;
        if (maximizing)
            weight = 1;
        else
            weight = -1;

        // If only one bin is present, check number of teddies inside
        switch (bin)
        {
            case 1:
                return 0.1f + weight;
            case 2:
                return 0.9f + weight;
            case 3:
                return 0.7f + weight;
        }

        return 0.5f;
    }
}