using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderedIntDynamicArray : IntDynamicArray
{

    #region Constructor

    /// <summary>
    /// Constructor
    /// </summary>
    public OrderedIntDynamicArray():base(){}

    #endregion

    #region Public Methods

    /// <summary>
    /// Adds the given item to the IntDynamicArray
    /// </summary>
    /// <param name="item">The item to add</param>
    public override void Add(int item)
    {
        //Expand array if necessary
        if(count==items.Length)
            Expand();
        
        //Find location at which to add the item
        int addLocation = 0;
        while ((addLocation < count) && (items[addLocation] < item))
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
    public override bool Remove(int item)
    {
        //Check for given item in array
        int itemLocation = IndexOf(item);
        if (itemLocation == -1)
            return false;
        
        //Shift all the elements above the removed one down
        //And change count
        ShiftDown(itemLocation + 1);
        count--;
        return true;
    }

    /// <summary>
    /// Determines the index of the given item using binary search
    /// </summary>
    /// <param name="item">The item to find</param>
    /// <returns>The index of the item or -1 if it's not found</returns>
    public override int IndexOf(int item)
    {
        int lowerBound = 0;
        int upperBound = count - 1;
        int location = -1;

        //Second part of Boolean expression added from defect disc
        //loop until found value or exhausted array
        while ((location == -1) && (lowerBound <= upperBound))
        {
            //Find the middle
            int middleLocation = lowerBound + (upperBound - lowerBound) / 2;
            int middleValue = items[middleLocation];
            
            //Check for match
            if (middleValue == item)
                location = middleLocation;
            else
            {
                //Split data set to searcu appropriate side
                if (middleValue > item)
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
