using System;
using System.Collections.Generic;
using System.Linq;

namespace Day3_TobogganTrajectory
{
    public static class TrajectoryFinder
    {
        public static int Slide(string[] input, int x0, int horizontalIncrement, int verticalIncrement)
        {
            var count = 0;
            var x = x0;
            for (int y = 0; y < input.Length; y += verticalIncrement)
            {
                if (input[y][x].Equals(Char.Parse("#")))
                {
                    count += 1;
                }

                if (x + horizontalIncrement < input[y].Length)
                {
                    x += horizontalIncrement;
                }
                else
                {
                    x += horizontalIncrement - (input[y].Length);
                }
            }

            return count;
        }

        public static long SlideMany(string[] input, Tuple<int, int>[] patterns)
        {
            List<int> result = new List<int>();

            foreach (var p in patterns)
            {
                result.Add(Slide(input, 0 , p.Item1, p.Item2));
            }

            var newResult = result.Select(x => (long) x);

            long mult = newResult.Aggregate((long) 1, (total, next) => total * next);
            return mult;
        }
    }
}