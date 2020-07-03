﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trees
{
    /// <summary>
    /// A tree node
    /// </summary>
    /// <typeparam name="T">type of value stored in node</typeparam>
    class TreeNode<T>
    {
        #region Fields

        T value;
        TreeNode<T> parent;
        List<TreeNode<T>> children;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">value for the node</param>
        /// <param name="parent">parent for the node</param>
        public TreeNode(T value, TreeNode<T> parent)
        {
            this.value = value;
            this.parent = parent;
            children = new List<TreeNode<T>>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value stored in the node
        /// </summary>
        public T Value
        {
            get { return value; }
        }

        /// <summary>
        /// Gets and sets the parent of the node
        /// </summary>
        public TreeNode<T> Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        /// <summary>
        /// Gets a read-only list of the children of the node
        /// </summary>
        public IList<TreeNode<T>> Children
        {
            get { return children.AsReadOnly(); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds the given node as a child this node
        /// </summary>
        /// <param name="child">child to add</param>
        /// <returns>true if the child was added, false otherwise</returns>
        public bool AddChild(TreeNode<T> child)
        {
            // don't add duplicate children
            if (children.Contains(child))
            {
                return false;
            }
            else if (child == this)
            {
                // don't add self as child
                return false;
            }
            else
            {
                // add as child and add self as parent
                children.Add(child);
                child.Parent = this;
                return true;
            }
        }

        /// <summary>
        /// Removes the given node as a child this node
        /// </summary>
        /// <param name="child">child to remove</param>
        /// <returns>true if the child was removed, false otherwise</returns>
        public bool RemoveChild(TreeNode<T> child)
        {
            // only remove children in list
            if (children.Contains(child))
            {
                child.Parent = null;
                return children.Remove(child);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Removes all the children for the node
        /// </summary>
        /// <returns>true if the children were removed, false otherwise</returns>
        public bool RemoveAllChildren()
        {
            for (int i = children.Count - 1; i >= 0; i--)
            {
                children[i].Parent = null;
                children.RemoveAt(i);
            }
            return true;
        }

        /// <summary>
        /// Converts the node to a string
        /// </summary>
        /// <returns>the string</returns>
        public override string ToString()
        {
            StringBuilder nodeString = new StringBuilder();
            nodeString.Append("[Node Value: " + value +
                " Parent: ");
            if (parent != null)
            {
                nodeString.Append(parent.Value);
            }
            else
            {
                nodeString.Append("null");
            }
            nodeString.Append(" Children: ");
            for (int i = 0; i < children.Count; i++)
            {
                nodeString.Append(children[i].Value + " ");
            }
            nodeString.Append("]");
            return nodeString.ToString();
        }

        #endregion
    }
}
