using System;
using Common;
using Day6_CustomCustoms;

namespace Solver
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Helpers.ReadParagraphs("Day6.txt");
            var answer_1 = CustomHelper.YesCounter(input);
            Console.WriteLine($"Answer 1: {answer_1}");

            var answer_2 = CustomHelper.AllYesCounter(input);
            Console.WriteLine($"Answer 2: {answer_2}");
        }
    }
}