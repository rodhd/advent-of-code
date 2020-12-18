using System;
using Common;
using Day10_AdapterArray;
using Day11_SeatingSystem;
using Day12_RainRisk;
using Day12_ShuttleSearch;
using Day14_DopckingData;
using Day15_RambunctiousRecitation;
using Day16_TicketTranslation;
using Day17_ConwayCubes;
using Day18_OperationOrder;
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
            var input = Helpers.ReadInput("Day18.txt");
            var tem = new OperationOrder(input);
            //tem.FirstAnswer();
            //Console.WriteLine($"Answer 1: {answer}");

            tem.SecondAnswer();
            //Console.WriteLine($"Answer 2: {answer2}");
        }
    }
    
}