namespace example
{
    using System;
    // you can also use other imports, for example:
    // using System.Collections.Generic;

    // you can write to stdout for debugging purposes, e.g.
    // Console.WriteLine("this is a debug message");



    using System.Collections.Generic;

    public class MaxSegmentsSolution
    {
        public int solution(int[] A)
        {
            int n = A.Length;
            Dictionary<int, int> sumFrequencies = new Dictionary<int, int>();
            int maxSegments = 0;

            for (int i = 0; i < n - 1; i++)
            {
                int sum = A[i] + A[i + 1];

                if (sumFrequencies.ContainsKey(sum))
                {
                    sumFrequencies[sum]++;
                }
                else
                {
                    sumFrequencies.Add(sum, 1);
                }

                maxSegments = Math.Max(maxSegments, sumFrequencies[sum]);
            }

            return maxSegments;
        }

    }
    class Program
    {
        public static void Main()
        {
            MaxSegmentsSolution sol= new MaxSegmentsSolution();

            // Test cases
            int[] A1 = { 10, 1, 3, 1, 2, 2, 1, 0, 4 };
            Console.WriteLine(sol.solution(A1)); // Output: 3

            int[] A2 = { 5, 3, 1, 3, 2, 3 };
            Console.WriteLine(sol.solution(A2)); // Output: 1

            int[] A3 = { 9, 9, 9, 9 };
            Console.WriteLine(sol.solution(A3)); // Output: 2

            int[] A4 = { 1, 5, 2, 4, 3, 3 };
            Console.WriteLine(sol.solution(A4)); // Output: 3
        }
    }
}

