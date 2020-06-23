using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A sorted list
/// </summary>
[Serializable]
public class SortedList<T> where T:IComparable
{
    [SerializeField] List<T> items = new List<T>();

    // used in Add method
    [SerializeField] List<T> tempList = new List<T>();
	
    #region Constructors

    /// <summary>
    /// No argument constructor
    /// </summary>
    public SortedList()
    {
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets the number of items in the list
    /// </summary>
    /// <value>number of items in the list</value>
    public int Count
    {
        get { return items.Count; }
    }
	
    /// <summary>
    /// Gets the item in the array at the given index
    /// This property allows access using [ and ]
    /// </summary>
    /// <param name="index">index of item</param>
    /// <returns>item at the given index</returns>
    public T this[int index]
    {
        get { return items[index]; }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Adds the given item to the list
    /// </summary>
    /// <param name="item">item</param>
    public void Add(T item)
    {
        // add your implementation below
        int addLocation = 0;
        // O(n)
        while ((addLocation < items.Count) && (items[addLocation].CompareTo(item) < 0))
        {
            addLocation++;
        }
        
        //Copy items pieces and new item into temp List
        tempList.Clear(); //Clean tempList

        //Add current elements in the tempList
        // O(n)
        for (int i = 0; i < addLocation; i++)
        {
            tempList.Add(items[i]);
        }
        
        //Add the new item at the end
        tempList.Add(item);

        //Add new elements until we reach the end of the items list
        // O(n)
        for (int i = addLocation; i < items.Count; i++)
        {
            tempList.Add(items[i]);
        }
        
        //Copy temp list back into items
        items.Clear(); //We clean the items list
        items.AddRange(tempList); //We add the whole new list in items.
    }

    /// <summary>
    /// Removes the item at the given index from the list
    /// </summary>
    /// <param name="index">index</param>
    public void RemoveAt(int index)
    {
        // add your implementation below
        // O(1)
        bool exists = items[index] != null ? true : false;
        if (!exists)
            return;
        
        items.RemoveAt(index);
    }

    /// <summary>
    /// Determines the index of the given item using binary search
    /// </summary>
    /// <param name="item">the item to find</param>
    /// <returns>the index of the item or -1 if it's not found</returns>
    public int IndexOf(T item)
    {
        int lowerBound = 0;
        int upperBound = items.Count - 1;
        int location = -1;

        // loop until found value or exhausted array
        while ((location == -1) &&
            (lowerBound <= upperBound))
        {
            // find the middle
            int middleLocation = lowerBound + (upperBound - lowerBound) / 2;
            T middleValue = items[middleLocation];

            // check for match
            if (middleValue.CompareTo(item) == 0)
            {
                location = middleLocation;
            }
            else
            {
                // split data set to search appropriate side
                if (middleValue.CompareTo(item) > 0)
                {
                    upperBound = middleLocation - 1;
                }
                else
                {
                    lowerBound = middleLocation + 1;
                }
            }
        }
        return location;
    }

    /// <summary>
    /// Sorts the list
    /// </summary>
    public void Sort()
    {
        items.Sort();
    }
    #endregion
}
