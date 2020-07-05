using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise10
{
    /// <summary>
    /// Exercise 10 solution
    /// </summary>
    class Program
    {
        /// <summary>
        /// Selects best first move using minimax
        /// </summary>
        /// <param name="args">command-line arguments</param>
        static void Main(string[] args)
        {
            // build and mark the tree with minimax scores
            MinimaxTree<char> tree = BuildTree();
            //Attach scores to each of the nodes in the tree
            Minimax(tree.Root, true);

            // find child node with maximum score
            IList<MinimaxTreeNode<char>> children = tree.Root.Children; //Save the children of the root
            MinimaxTreeNode<char> maxChildNode = children[0]; //Max child node is the first child (Assuming that the left choice is the best)

            //Just in case we are wrong we check here if the minimax score is right and the maxchild is the greatest, if not, then reassign it
            for(int i = 1; i < children.Count; i++)
            {
                if (children[i].MinimaxScore> maxChildNode.MinimaxScore)
                {
                    maxChildNode = children[i];
                }
            }

            // print best move
            Console.WriteLine("Best move is to char " + maxChildNode.Value);


            Console.ReadLine();
        }

        /// <summary>
        /// Builds the tree
        /// </summary>
        /// <returns>tree</returns>
        static MinimaxTree<char> BuildTree()
        {
            MinimaxTree<char> tree = new MinimaxTree<char>('A');
            MinimaxTreeNode<char> bNode = new MinimaxTreeNode<char>('B', tree.Root);
            tree.AddNode(bNode);
            MinimaxTreeNode<char> cNode = new MinimaxTreeNode<char>('C', tree.Root);
            tree.AddNode(cNode);
            MinimaxTreeNode<char> dNode = new MinimaxTreeNode<char>('D', tree.Root);
            tree.AddNode(dNode);
            MinimaxTreeNode<char> eNode = new MinimaxTreeNode<char>('E', bNode);
            tree.AddNode(eNode);
            MinimaxTreeNode<char> fNode = new MinimaxTreeNode<char>('F', bNode);
            tree.AddNode(fNode);
            MinimaxTreeNode<char> gNode = new MinimaxTreeNode<char>('G', bNode);
            tree.AddNode(gNode);
            MinimaxTreeNode<char> hNode = new MinimaxTreeNode<char>('H', cNode);
            tree.AddNode(hNode);
            MinimaxTreeNode<char> iNode = new MinimaxTreeNode<char>('I', cNode);
            tree.AddNode(iNode);
            MinimaxTreeNode<char> jNode = new MinimaxTreeNode<char>('J', dNode);
            tree.AddNode(jNode);
            MinimaxTreeNode<char> kNode = new MinimaxTreeNode<char>('K', dNode);
            tree.AddNode(kNode);
            MinimaxTreeNode<char> lNode = new MinimaxTreeNode<char>('L', eNode);
            tree.AddNode(lNode);
            MinimaxTreeNode<char> mNode = new MinimaxTreeNode<char>('M', eNode);
            tree.AddNode(mNode);
            MinimaxTreeNode<char> nNode = new MinimaxTreeNode<char>('N', fNode);
            tree.AddNode(nNode);
            MinimaxTreeNode<char> oNode = new MinimaxTreeNode<char>('O', fNode);
            tree.AddNode(oNode);
            MinimaxTreeNode<char> pNode = new MinimaxTreeNode<char>('P', gNode);
            tree.AddNode(pNode);
            MinimaxTreeNode<char> qNode = new MinimaxTreeNode<char>('Q', gNode);
            tree.AddNode(qNode);
            MinimaxTreeNode<char> rNode = new MinimaxTreeNode<char>('R', hNode);
            tree.AddNode(rNode);
            MinimaxTreeNode<char> sNode = new MinimaxTreeNode<char>('S', hNode);
            tree.AddNode(sNode);
            MinimaxTreeNode<char> tNode = new MinimaxTreeNode<char>('T', iNode);
            tree.AddNode(tNode);
            MinimaxTreeNode<char> uNode = new MinimaxTreeNode<char>('U', iNode);
            tree.AddNode(uNode);
            MinimaxTreeNode<char> vNode = new MinimaxTreeNode<char>('V', jNode);
            tree.AddNode(vNode);
            MinimaxTreeNode<char> wNode = new MinimaxTreeNode<char>('W', jNode);
            tree.AddNode(wNode);
            MinimaxTreeNode<char> xNode = new MinimaxTreeNode<char>('X', kNode);
            tree.AddNode(xNode);
            MinimaxTreeNode<char> yNode = new MinimaxTreeNode<char>('Y', kNode);
            tree.AddNode(yNode);
            return tree;
        }


        /// <summary>
        /// Assigns the minimax scores to the tree nodes
        /// </summary>
        /// <param name="tree">tree to mark with scores</param>
        /// <param name="maximizing">whether or not we're maximizing</param>
        static void Minimax(MinimaxTreeNode<char> tree, bool maximizing)
        {
            //Recurse the children
            IList<MinimaxTreeNode<char>> children = tree.Children;
            if (children.Count > 0)
            {
                foreach(MinimaxTreeNode<char> child in children)
                {
                    //Toggle maximizing as we move down
                    Minimax(child, !maximizing);
                }

                //Set default node minimax score
                if (maximizing)
                {
                    tree.MinimaxScore = int.MinValue;
                }
                else
                {
                    tree.MinimaxScore = int.MaxValue;
                }

                //Find maximum or minimum value in children
                foreach(MinimaxTreeNode<char> child in children)
                {
                    if (maximizing)
                    {
                        //Check for higher minimax score
                        if (child.MinimaxScore > tree.MinimaxScore)
                        {
                            tree.MinimaxScore = child.MinimaxScore;
                        }
                    }
                    else
                    {
                        //Minimizing, check for lower minimax score
                        if (child.MinimaxScore < tree.MinimaxScore)
                        {
                            tree.MinimaxScore = child.MinimaxScore;
                        }
                    }
                }
            }
            else
            {
                //Leaf nodes are the base case
                AssignMinimaxScore(tree);
            }
        }

        /// <summary>
        /// Assigns a minimax score to the given node
        /// </summary>
        /// <param name="node">node to mark with score</param>
        static void AssignMinimaxScore(MinimaxTreeNode<char> node)
        {
            switch (node.Value)
            {
                case 'L':
                    node.MinimaxScore = 7;
                    break;
                case 'M':
                    node.MinimaxScore = 6;
                    break;
                case 'N':
                    node.MinimaxScore = 8;
                    break;
                case 'O':
                    node.MinimaxScore = 5;
                    break;
                case 'P':
                    node.MinimaxScore = 2;
                    break;
                case 'Q':
                    node.MinimaxScore = 3;
                    break;
                case 'R':
                    node.MinimaxScore = 0;
                    break;
                case 'S':
                    node.MinimaxScore = -2;
                    break;
                case 'T':
                    node.MinimaxScore = 6;
                    break;
                case 'U':
                    node.MinimaxScore = 2;
                    break;
                case 'V':
                    node.MinimaxScore = 5;
                    break;
                case 'W':
                    node.MinimaxScore = 8;
                    break;
                case 'X':
                    node.MinimaxScore = 9;
                    break;
                case 'Y':
                    node.MinimaxScore = 2;
                    break;
            }
        }
    }
}
