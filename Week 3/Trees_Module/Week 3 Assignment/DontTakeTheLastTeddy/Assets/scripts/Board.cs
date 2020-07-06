using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The game board (bins)
/// </summary>
public class Board : MonoBehaviour
{
    [SerializeField]
    GameObject prefabBin;

    [SerializeField] List<Bin> bins = new List<Bin>();
    [SerializeField] Configuration configuration;

    // saved for efficiency
    [SerializeField] float binWidth;

	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start()
	{
        // bin width may already be set
        if (binWidth == 0)
        {
            SetBinWidth();
        }
	}
	
    #region Properties

    /// <summary>
    /// Gets and sets the board configuration
    /// </summary>
    /// <value>board configuration</value>
    public Configuration Configuration
    {
        get { return configuration; }
        set
        { 
            configuration = value; 
            SetBinCounts(configuration.Bins);
        }
    }

    #endregion

    /// <summary>
    /// Creates a new board
    /// </summary>
    public void CreateNewBoard()
    {
        // destroy existing board
        for (int i = bins.Count - 1; i >= 0; i--)
        {
            Destroy(bins[i].gameObject);
        }
        bins.Clear();

        // bin width may not be set yet
        if (binWidth == 0)
        {
            SetBinWidth();
        }

        // STUDENTS: for the assignment, change this to randomly pick 
        // between GameConstants.MinBins and GameConstants.MaxBins 
		// bins, inclusive. Be sure to center the bins properly
        int iTemp = Random.Range(GameConstants.MinBins, GameConstants.MaxBins + 1);
        float binX = transform.position.x - binWidth * (iTemp-1) / 2;
        
        for (int i = 0; i < iTemp; i++)
        {
            GameObject binObject = Instantiate<GameObject>(prefabBin,
                transform.position, Quaternion.identity);
            Bin bin = binObject.GetComponent<Bin>();
            bin.X = binX;
            bins.Add(bin);
            binX += binWidth;
        }

        // STUDENTS: for assignment, change this to randomly pick
        // between 1 and GameConstants.MaxBearsPerBin for each bin
        List<int> binContents = new List<int>();
        for (int i = 0; i < iTemp; i++)
        {
            binContents.Add(Random.Range(1, GameConstants.MaxBearsPerBin + 1)); //2
        }
        configuration = new Configuration(binContents);
        
        // set counts for bin game objects
        SetBinCounts(binContents);
    }

    /// <summary>
    /// Sets the bin width
    /// </summary>
    void SetBinWidth()
    {
        // cache bin width
        GameObject tempBinObject = Instantiate<GameObject>(prefabBin);
        Bin tempBin = tempBinObject.GetComponent<Bin>();
        binWidth = tempBin.Width;
        Destroy(tempBinObject);
    }

    /// <summary>
    /// Sets the bin counts for the board
    /// </summary>
    /// <param name="binContents">bin contents</param>
    void SetBinCounts(IList<int> binContents)
    {
        for (int i = 0; i < bins.Count; i++)
        {
            bins[i].Count = binContents[i];
        }
    }
}
