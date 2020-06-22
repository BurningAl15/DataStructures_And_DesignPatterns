using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise1
{
    /// <summary>
    /// Exercise 1 solution
    /// </summary>
    class Program
    {
        /// <summary>
        /// Tests LastIndexOf and AllIndexesOf methods
        /// </summary>
        /// <param name="args">command-line arguments</param>
        static void Main(string[] args)
        {
            // build test dynamic array
            UnorderedIntDynamicArray testArray = new UnorderedIntDynamicArray();
            testArray.Add(1);
            testArray.Add(2);
            testArray.Add(3);
            testArray.Add(2);
            testArray.Add(1);

            // test LastIndexOf with one item in dynamic array
            int lastIndex = testArray.LastIndexOf(3);
            Console.WriteLine("Last index of 3: " + lastIndex);

            // test LastIndexOf with multiple items in dynamic array
            lastIndex = testArray.LastIndexOf(2);
            Console.WriteLine("Last index of 2: " + lastIndex);

            // test LastIndexOf with item not in dynamic array
            lastIndex = testArray.LastIndexOf(0);
            Console.WriteLine("Last index of 0: " + lastIndex);
            Console.WriteLine();

            // test AllIndexesOf with one item in dynamic array
            List<int> indexes = testArray.AllIndexesOf(3);
            Console.Write("All indexes of 3:");
            PrintIndexes(indexes);
            Console.WriteLine();

            // test AllIndexesOf with multiple items in dynamic array
            indexes = testArray.AllIndexesOf(2);
            Console.Write("All indexes of 2:");
            PrintIndexes(indexes);
            Console.WriteLine();

            // test AllIndexesOf with item not in dynamic array
            indexes = testArray.AllIndexesOf(0);
            Console.Write("All indexes of 0:");
            PrintIndexes(indexes);
            Console.WriteLine();

            Console.ReadLine();
        }

        /// <summary>
        /// Prints the indexes contained in the given list
        /// </summary>
        /// <param name="indexes">indexes</param>
        static void PrintIndexes(List<int> indexes)
        {
            if (indexes.Count == 0)
            {
                Console.Write(" no indexes in list");
            }
            else
            {
                foreach (int index in indexes)
                {
                    Console.Write(" " + index);
                }
            }
        }
    }
}
