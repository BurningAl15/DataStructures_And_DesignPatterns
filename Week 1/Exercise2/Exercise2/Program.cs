using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise2
{
    /// <summary>
    /// Exercise 2 solution
    /// </summary>
    class Program
    {
        /// <summary>
        /// Uses ordered generic dynamic array
        /// </summary>
        /// <param name="args">command-line arguments</param>
        static void Main(string[] args)
        {
            #region CompareTo testing

            Rectangle testRectangle = new Rectangle(4, 3);
            Rectangle smallerRectangle = new Rectangle(3, 2);
            Rectangle equalRectangle = new Rectangle(3, 4);
            Rectangle largerRectangle = new Rectangle(5, 4);

            // compare to null
            int compareResult = testRectangle.CompareTo(null);
            if (compareResult > 0)
            {
                Console.WriteLine("Compare to null passed");
            }
            else
            {
                Console.WriteLine("COMPARE TO NULL FAILED!");
            }

            // compare to smaller
            compareResult = testRectangle.CompareTo(smallerRectangle);
            if (compareResult > 0)
            {
                Console.WriteLine("Compare to smaller passed");
            }
            else
            {
                Console.WriteLine("COMPARE TO SMALLER FAILED!");
            }

            // compare to equal
            compareResult = testRectangle.CompareTo(equalRectangle);
            if (compareResult == 0)
            {
                Console.WriteLine("Compare to equal passed");
            }
            else
            {
                Console.WriteLine("COMPARE TO EQUAL FAILED!");
            }

            // compare to larger
            compareResult = testRectangle.CompareTo(largerRectangle);
            if (compareResult < 0)
            {
                Console.WriteLine("Compare to larger passed");
            }
            else
            {
                Console.WriteLine("COMPARE TO LARGER FAILED!");
            }

            // compare to string
            try
            {
                compareResult = testRectangle.CompareTo("Bob");
                Console.WriteLine("COMPARE TO STRING FAILED!");
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine("Compare to string passed");
            }
            Console.WriteLine();

            #endregion

            #region Using the generic

            OrderedDynamicArray<Rectangle> testArray =
                new OrderedDynamicArray<Rectangle>();
            testArray.Add(largerRectangle);
            testArray.Add(equalRectangle);
            testArray.Add(smallerRectangle);
            testArray.Add(testRectangle);
            testArray.ToString();

            #endregion

            Console.ReadLine();
        }
    }
}
