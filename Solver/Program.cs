using System;
using Common;
using Day6_CustomCustoms;
using Day7_HandyHaversacks;

namespace Solver
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Helpers.ReadInput("Day7.txt");
            var answer = HandbagRulesReader.TotalBagsInside(input);
            Console.WriteLine($"Answer 2: {answer}");
        }
    }
}