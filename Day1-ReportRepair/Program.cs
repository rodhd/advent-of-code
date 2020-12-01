using System;
using System.IO;
using System.Linq;

namespace Day1_ReportRepair
{
    class Program
    {
        static void Main(string[] args)
        {
            string currentDir = "/Users/user/Projects/personal/advent-of-code/Day1-ReportRepair/";
            string[] lines = System.IO.File.ReadAllLines( currentDir + "input.txt");
            var input = lines.Select(Int32.Parse).ToList();
            
            //First puzzle
            var firstResult = Helper.TwoNumbers(input, 2020);
            Console.WriteLine($"{firstResult.Item1} * {firstResult.Item2} = {firstResult.Item1 * firstResult.Item2}");
            
            //Second puzzle
            var secondResult = Helper.ThreeNumbers(input);
            Console.WriteLine($"{secondResult.Item1} * {secondResult.Item2} * {secondResult.Item3} = {secondResult.Item1 * secondResult.Item2 * secondResult.Item3}");
        }
    }
}