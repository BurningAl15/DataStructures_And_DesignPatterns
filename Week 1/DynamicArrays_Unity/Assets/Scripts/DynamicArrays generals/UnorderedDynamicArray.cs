using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnorderedDynamicArray<T> : DynamicArray<T>
{
   #region Constructor

   public UnorderedDynamicArray() : base()
   {
   }

   #endregion

   #region Public Methods

   public override void Add(T item)
   {
      //Expand array if necessary
      if(count==items.Length)
         Expand();

      //Add new item and increment count
      items[count] = item;
      count++;
   }

   public override bool Remove(T item)
   {
      int itemLocation = IndexOf(item);
      if (itemLocation == -1)
         return false;
      
      //Move last element in array here and change court
      items[itemLocation] = items[count - 1];
      count--;
      return true;
   }

   public override int IndexOf(T item)
   {
      //Look for first occurrence of item in array
      for (int i = 0; i < count; i++)
      {
         if (items[i].Equals(item))
         {
            return i;
         }
      }
      
      //Didn't find the item in the array
      return -1;
   }

   #endregion
}
