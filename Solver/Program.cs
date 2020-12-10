using System;
using Common;
using Day10_AdapterArray;
using Day6_CustomCustoms;
using Day7_HandyHaversacks;
using Day8_HandheldHalting;
using Day9_EncodingError;

namespace Solver
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Helpers.ReadInput("Day10.txt");
            var tem = new AdapterArray(input);
            //var answer = tem.FirstAnswer();
            //Console.WriteLine($"Answer 1: {answer}");

            var answer2 = tem.SecondAnswer();
            Console.WriteLine($"Answer 2: {answer2}");
        }
    }
}