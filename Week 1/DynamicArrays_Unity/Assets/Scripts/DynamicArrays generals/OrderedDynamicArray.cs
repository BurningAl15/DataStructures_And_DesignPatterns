using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderedDynamicArray<T> : DynamicArray<T> where T:IComparable
{
    #region Constructor

    /// <summary>
    /// Constructor
    /// </summary>
    public OrderedDynamicArray():base(){}

    #endregion


    #region Public Methods

    /// <summary>
    /// Adds the given item to the IntDynamicArray
    /// </summary>
    /// <param name="item">The item to add</param>
    public override void Add(T item)
    {
        //Expand array if necessary
        if(count==items.Length)
            Expand();
        
        //Find location at which to add the item
        int addLocation = 0;
        while ((addLocation < count) && (items[addLocation].CompareTo(item) < 0))
        {
            addLocation++;
        }
        
        //Shift array, add new item and increment count
        ShiftUp(addLocation);
        items[addLocation] = item;
        count++;
    }

    /// <summary>
    /// Removes the first ocurrence of the given item from the
    /// IntDynamicArray
    /// </summary>
    /// <param name="item">The item to remvove</param>
    /// <returns>True if the item is successfully removed,
    ///          False otherwise </returns>
    public override bool Remove(T item)
    {
        //Check for given item in array
        int itemLocation = IndexOf(item);
        if (itemLocation == -1)
            return false;
        
        //Shift all the elements above the removed one down
        ShiftDown(itemLocation + 1);
        count--;
        return true;
    }

    /// <summary>
    /// Determines the index of the given item using binary search
    /// </summary>
    /// <param name="item">The item to find</param>
    /// <returns>The index of the item or -1 if it's not found</returns>
    public override int IndexOf(T item)
    {
        int lowerBound = 0;
        int upperBound = count - 1;
        int location = -1;

        while ((location == -1) && (lowerBound <= upperBound))
        {
            int middleLocation = lowerBound + (upperBound - lowerBound) / 2;
            T middleValue = items[middleLocation];

            //Check for match
            if (middleValue.CompareTo(item) == 0)
            {
                location = middleLocation;
            }
            else
            {
                if (middleValue.CompareTo(item) > 0)
                    upperBound = middleLocation - 1;
                else
                {
                    lowerBound = middleLocation + 1;
                }
            }
        }

        return location;
    }
    
    #endregion

    #region Private Methods


    /// <summary>
    /// Shifts all the array elements from the given index to the end
    /// of the array up one space
    /// </summary>
    /// <param name="index">The index at which to start shifting up</param>
    void ShiftUp(int index)
    {
        for (int i = count; i > index; i--)
        {
            items[i] = items[i - 1];
        }
    }

    /// <summary>
    /// Shifts all the array elements from the given index to the end
    /// of the array down one space
    /// </summary>
    /// <param name="index">The index at which to start shifting down</param>
    void ShiftDown(int index)
    {
        for (int i = index; i < count; i++)
        {
            items[i - 1] = items[i];
        }
    }
    
    #endregion
}
