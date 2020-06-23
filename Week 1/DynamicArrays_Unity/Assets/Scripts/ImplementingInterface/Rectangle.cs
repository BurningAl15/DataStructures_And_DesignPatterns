using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rectangle : IComparable
{
    #region Fields

    private int width;
    private int height;    

    #endregion

    #region Constructors

    public Rectangle(int _width, int _height)
    {
        this.width = _width;
        this.height = _height;
    }

    #endregion

    #region Properties

    public int Width
    {
        get => width;
    }

    public int Height => height;
    
    #endregion

    #region Methods

    public Vector2 GetRectangle()
    {
        return new Vector2(width,height);
    }

    public override string ToString()
    {
        return string.Format("[Rectangle:Width={0}, Height={1}]",width,height);
    }

    /// <summary>
    /// Compares this rectangle to the parameter
    /// </summary>
    /// <param name="obj">Relative Order</param>
    /// <returns>Object to compare to</returns>
    public int CompareTo(object obj)
    {
        //Always greater than null
        if (obj == null) return 1;
        
        //Check object type
        Rectangle otherRectangle = obj as Rectangle;

        if (otherRectangle != null)
        {
            if (width * height < otherRectangle.Width * otherRectangle.Height)
                return -1;
            else if (width * height == otherRectangle.Width * otherRectangle.height)
                return 0;
            else
            {
                return 1;
            }
        }
        else
            throw new ArgumentException("Object is not a rectangle");
    }

    #endregion
}
