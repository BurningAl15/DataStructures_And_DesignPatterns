using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnorderedIntDynamicArray : IntDynamicArray
{
    //Little Explanation
    //The count field told us:
    // 1) The location of the next value to be added
    // 2) How many "real" values are currently in the array
    
    #region Constructor

    /// <summary>
    /// Constructor
    /// </summary>
    public UnorderedIntDynamicArray() : base()
    {
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Adds the given item to the IntDynamicArray
    /// </summary>
    /// <param name="item">The item to add</param>
    public override void Add(int item)
    {
        //Expand array if necessary
        if (count == items.Length)
        {
            Expand();
        }
        
        //Add new item and increment count
        items[count] = item;
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
        
        //If item is not located
        if (itemLocation == -1)
        {
            return false;
        }

        //Move last element in array here and change count
        items[itemLocation] = items[count - 1];
        count--;
        return true;
    }

    /// <summary>
    /// Determines the index of the given item
    /// </summary>
    /// <param name="item">The item to find</param>
    /// <returns>The index of the item or -1 if it's not found</returns>
    public override int IndexOf(int item)
    {
        //Look for first ocurrence of the item in array
        for (int i = 0; i < count; i++)
        {
            if (items[i] == item)
            {
                return i;
            }
        }
        
        //Didn't find the item in the array
        return -1;
    }

    #endregion
}
