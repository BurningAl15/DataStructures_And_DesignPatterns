using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public abstract class DynamicArray
{
       private const int ExpandMultilyFactor = 2;
       protected int[] items;
       protected int count;

       #region Constructor

       /// <summary>
       /// Constructor
       /// </summary>
       protected DynamicArray()
       {
              items = new int[4];
              count = 0;
       }
       
       #endregion

       #region Properties

       /// <summary>
       /// Gets the number of elements
       /// </summary>
       public int Count
       {
              get
              {
                     return count;
              }
       }

       #endregion

       #region Public Methods

       public abstract void Add(int item);
       public abstract bool Remove(int item);
       public abstract int IndexOf(int item);

       /// <summary>
       /// Removes all the items from the IntDynamicArray
       /// </summary>
       public void Clear()
       {
              count = 0;
       }


       /// <summary>
       /// Converts the IntDynamic to a comma-separated string
       /// values
       /// </summary>
       /// <returns>The comma-separated string of values</returns>
       public override string ToString()
       {
              StringBuilder builder=new StringBuilder();
              for (int i = 0; i < count; i++)
              {
                     builder.Append(items[i]);
                     if (i < count - 1)
                     {
                            builder.Append(",");
                     }
              }
              return builder.ToString();
       }

       #endregion

       #region Protected Methods

       //Expands the array
       protected void Expand()
       {
              //We create a new array with the double of the original size
              int[] newItems = new int[items.Length * ExpandMultilyFactor];

              //Copy elements from old array into new array
              for (int i = 0; i < items.Length; i++)
              {
                     newItems[i] = items[i];
              }
              
              //Change to use new array
              items = newItems;
       }
       

       #endregion
}