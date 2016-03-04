using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SortingALgorithms
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Enter the number of elements to sort");
            int num = int.Parse(Console.ReadLine());

            int[] array = new int[num];

            Console.WriteLine("Enter repeat times");
            int repeatTimes = int.Parse(Console.ReadLine());
            Stopwatch stopwatch1 = new Stopwatch(); //creates and start the instance of Stopwatch
            Stopwatch stopwatch2 = new Stopwatch(); //creates and start the instance of Stopwatch

            Random seeder = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = seeder.Next();
            }
            List<long> insertionTimes = new List<long>(repeatTimes);
            List<long> mergeTimes = new List<long>(repeatTimes);

            while (repeatTimes-- > 0)
            {
                // Fill the array using random values
                Random rand = new Random(seeder.Next());
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = rand.Next();
                }

                stopwatch1.Start();
                InsertionSort(array);
                stopwatch1.Stop();
                //Console.WriteLine("{2,-10} : {0,10} | {1,4}", stopwatch1.ElapsedTicks, stopwatch1.ElapsedMilliseconds,"Insertion");
                insertionTimes.Add(stopwatch1.ElapsedTicks);

                stopwatch2.Start();
                MergeSort(array);
                stopwatch2.Stop();
                //Console.WriteLine("{2,-10} : {0,10} | {1,4}", stopwatch2.ElapsedTicks, stopwatch2.ElapsedMilliseconds, "Merge");
                mergeTimes.Add(stopwatch2.ElapsedTicks);

                //Console.WriteLine();
                stopwatch1.Reset();
                stopwatch2.Reset();
            }

            double insertionN = insertionTimes.Average();
            double megeN = mergeTimes.Average();
            Console.WriteLine("Merge:{0} , Insertion: {1}", megeN.ToString(), insertionN.ToString());

            // Don't auto terminate
            Console.ReadKey();
        }

        /// <summary>
        /// Sorts an array using Insertion Sort
        /// </summary>
        /// <param name="array"></param>
        public static int[] InsertionSort(int[] array)
        {
            // For all the items in the array
            for (int i = 0; i < array.Length; i++)
            {
                // Select an element
                int selected = array[i];

                // While I didn't reach the start of the array and the element is not in its place
                while (0 < i && selected < array[i - 1])
                {
                    // Put the bigger element infront of the selected element
                    array[i] = array[i - 1];

                    // Decrement
                    i--;
                }
                // Place the element in its position
                array[i] = selected;
            }
            return array;
        }

        private static int[] MergeSort(int[] array)
        {
            // Divide the array into sub arrays
            if (array.Length == 1)
            {
                return array;
            }

            int arrayLength = array.Length;
            int halfLength = array.Length / 2;

            int[] leftHalf = new int[halfLength];
            int[] rightHalf = new int[arrayLength - halfLength];

            // Copy the left side to a smaller array
            for (int i = 0; i < halfLength; i++)
            {
                leftHalf[i] = array[i];
            }

            // Copy the right side to a smaller array, regarding odd numbers
            for (int i = halfLength; i < arrayLength; i++)
            {
                rightHalf[i - halfLength] = array[i];
            }

            leftHalf = MergeSort(leftHalf);
            rightHalf = MergeSort(rightHalf);

            return Merge(leftHalf, rightHalf);
        }

        private static int[] Merge(int[] leftArray, int[] rightArray)
        {
            int[] merged = new int[leftArray.Length + rightArray.Length];

            int i = 0, j = 0, k = 0;

            // While the indexes are inside the bounds of their arrays
            while (i < leftArray.Length && j < rightArray.Length)
            {
                // Find the smaller element add it to the merged list Point to the next element of
                // the small array

                if (leftArray[i] < rightArray[j])
                {
                    merged[k] = leftArray[i];
                    // Increment the counter of the left array
                    i++;
                }
                else
                {
                    merged[k] = rightArray[j];
                    // Increment the counter of the right array
                    j++;
                }
                // Point to the next empty element in the merged array
                k++;
            }

            // Put the remaining elements in the array
            while (i < leftArray.Length)
            {
                merged[k] = leftArray[i];
                k++; i++;
            }

            // Put the remaining elements in the array
            while (j < rightArray.Length)
            {
                merged[k] = rightArray[j];
                k++; j++;
            }

            return merged;
        }

        public static void PrintArray<T>(T items) where T : System.Collections.IEnumerable
        {
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }
    }
}