using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A bin on the board
/// </summary>
public class Bin : MonoBehaviour
{
    [SerializeField]
    GameObject prefabTeddyBear;

    List<GameObject> teddyBears = new List<GameObject>();
    int count = 0;

    // saved for efficiency
    float binWidth;
    float binHeight;
    Vector3 lowerLeftCorner;
    float spaceSize;
    float teddyBearWidth;
    float teddyBearHeight;

	/// <summary>
	/// Awake is called before start
	/// </summary>
	void Awake()
    {
        // cache bin dimensions and corner
        BoxCollider2D binSize = GetComponent<BoxCollider2D>();
        binWidth = binSize.size.x;
        binHeight = binSize.size.y;

		// cache teddy bear dimensions
        BoxCollider2D teddyBearSize = prefabTeddyBear.GetComponent<BoxCollider2D>();
        teddyBearWidth = teddyBearSize.size.x;
        teddyBearHeight = teddyBearSize.size.y;

        // make sure we have equal spacing horizontally
        spaceSize = (binWidth - 2 * teddyBearWidth) / 3;
        lowerLeftCorner = new Vector3(transform.position.x - binWidth / 2 + spaceSize,
            transform.position.y - binHeight / 2, 0);
	}

    #region Properties

    /// <summary>
    /// Gets the bin width
    /// </summary>
    /// <value>bin width</value>
    public float Width
    {
        get { return binWidth; }
    }

    /// <summary>
    /// Sets the bin x location
    /// </summary>
    /// <value>x location</value>
    public float X
    {
        set
        {
            Vector3 position = transform.position;
            position.x = value;
            transform.position = position;
            lowerLeftCorner.x = value - binWidth / 2 + spaceSize;
        }
    }

    /// <summary>
    /// Sets the count for the bin
    /// </summary>
    /// <value>bin count</value>
    public int Count
    {
        set
        {
            if (value > count)
            {
                // add teddy bears to bin
                while (count < value)
                {
                    AddTeddyBear();
                    count++;
                }
            }
            else if (value < count)
            {
                // remove teddy bears from bin
                while (count > value)
                {
                    Destroy(teddyBears[count - 1]);
                    teddyBears.RemoveAt(count - 1);
                    count--;
                }
            }
        }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Adds a teddy bear to the bin
    /// </summary>
    void AddTeddyBear()
    {
        // calculate teddy bear offset
        int row = count / 2;
        int column = count % 2;
        Vector3 offset = new Vector3(
            teddyBearWidth / 2 + column * (teddyBearWidth + spaceSize),
            teddyBearHeight / 2 + row * teddyBearHeight,
            0);

        // add new teddy bear to list
        teddyBears.Add(Instantiate(prefabTeddyBear, 
            lowerLeftCorner + offset, Quaternion.identity));
    }

    #endregion
}
