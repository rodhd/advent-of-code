using System;
using System.Linq;

namespace Day3_TobogganTrajectory
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "/Users/user/Projects/personal/advent-of-code/Day3-TobogganTrajectory/Input.txt";
            string[] lines = System.IO.File.ReadAllLines(path);

            var answer1 = TrajectoryFinder.Slide(lines, 0, 3, 1);
            Console.WriteLine($"First answer: {answer1} trees");

            Tuple<int, int>[] patterns = new[]
            {
                new Tuple<int, int>(1, 1),
                new Tuple<int, int>(3, 1),
                new Tuple<int, int>(5, 1),
                new Tuple<int, int>(7, 1),
                new Tuple<int, int>(1, 2)
            };

            var answer2 = TrajectoryFinder.SlideMany(lines, patterns);
            Console.WriteLine($"Second answer: {answer2}");

        }
    }
}
