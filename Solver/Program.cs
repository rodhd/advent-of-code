using System;
using Common;
using Day6_CustomCustoms;
using Day7_HandyHaversacks;
using Day8_HandheldHalting;

namespace Solver
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Helpers.ReadInput("Day8.txt");
            var tem = new InstructionList(input);
            //var answer = tem.ExecuteUntilRepeat();
            //Console.WriteLine($"Answer 1: {answer}");

            var answer2 = tem.ExecuteUntilFix();
            Console.WriteLine($"Answer 2: {answer2}");
        }
    }
}