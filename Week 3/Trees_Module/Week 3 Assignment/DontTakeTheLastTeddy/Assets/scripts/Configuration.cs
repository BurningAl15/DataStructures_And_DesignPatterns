using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// A configuration of the game "board"
/// </summary>
[Serializable]
public class Configuration
{
    #region Fields

    [SerializeField] List<int> bins = new List<int>();

    #endregion

    #region Constructor

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="binContents">contents of each bin</param>
    public Configuration(List<int> binContents)
    {
        // copy bin contents into bins
        bins.AddRange(binContents);
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets a read-only list of the bin contents
    /// </summary>
    public IList<int> Bins
    {
        get { return bins.AsReadOnly(); }
    }

    /// <summary>
    /// Gets whether all the bins in the configuration are empty
    /// </summary>
    public bool Empty
    {
        get
        {
            foreach (int bin in bins)
            {
                if (bin > 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
    
    public List<int> NonEmptyBins
    {
        get
        {
            List<int> temp=new List<int>();
            foreach (int bin in bins)
            {
                if (bin > 0)
                {
                    temp.Add(bin);
                }
            }

            return temp;
        }
    }


    #endregion

    #region Methods

    /// <summary>
    /// Converts the configuration to a string
    /// </summary>
    /// <returns>the string</returns>
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("[Configuration: ");
        for (int i = 0; i < bins.Count; i++)
        {
            builder.Append(bins[i]);
            if (i < bins.Count - 1)
            {
                builder.Append(" ");
            }
        }
        builder.Append("]");
        return builder.ToString();
    }

    #endregion
}
