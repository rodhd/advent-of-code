using System;
using Common;
using Day4_PassportProcessing;
using Day5_BinaryBoarding;

namespace Solver
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Helpers.ReadInput("Day5.txt");
            var answer1 = BoardingPassReader.GetMaxId(input);
            Console.WriteLine($"Answer 1: Max Id is {answer1}");
            
            var answer2 = BoardingPassReader.GetFreeSeat(input);
            Console.WriteLine($"Answer 2: Free Id is {answer2}");
        }
    }
}