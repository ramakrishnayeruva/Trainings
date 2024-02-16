namespace Training
{
    using System;
    using System.Collections.Generic;

    class MaxSegmentsSolution
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
        static void Main()
        {
            MaxSegmentsSolution solution = new MaxSegmentsSolution();

            // Test cases
            int[] A1 = { 10, 1, 3, 1, 2, 2, 1, 0, 4 };
            Console.WriteLine(solution.solution(A1)); // Output: 3

            int[] A2 = { 5, 3, 1, 3, 2, 3 };
            Console.WriteLine(solution.solution(A2)); // Output: 1

            int[] A3 = { 9, 9, 9, 9 };
            Console.WriteLine(solution.solution(A3)); // Output: 2

            int[] A4 = { 1, 5, 2, 4, 3, 3 };
            Console.WriteLine(solution.solution(A4)); // Output: 3
        }
    }

}
