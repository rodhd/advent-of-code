using System;
using System.Collections.Generic;
using System.Linq;

namespace Day15_RambunctiousRecitation
{
    public class SmartRecitation
    {
        private (int LastMentioned, int PreviousMention)[] NumbersSpoken { get; set; }
        private int[] StartingNumbers { get; set; }
        private int Target { get; set; }
        
        public SmartRecitation(string input, int target)
        {
            StartingNumbers = input.Split(",").Select(x => Int32.Parse(x)).ToArray();
            NumbersSpoken = new (int LastMentioned, int PreviousMention)[target];
            Target = target;
        }
        public int SecondAnswer()
        {
            int turn = 0;

            foreach (var n in StartingNumbers)
            {
                NumbersSpoken[n] = (turn++, -1);
            }

            int prev = StartingNumbers[^1];

            while (turn < Target)
            {
                var p = NumbersSpoken[prev];
                if (p.PreviousMention == -1)
                {
                    NumbersSpoken[0].PreviousMention = NumbersSpoken[0] == (0,0) ? -1 : NumbersSpoken[0].LastMentioned;
                    NumbersSpoken[0].LastMentioned = turn;
                    prev = 0;
                }
                else
                {
                    int newNumber = NumbersSpoken[prev].LastMentioned - NumbersSpoken[prev].PreviousMention;
                    if (NumbersSpoken[newNumber] == (0, 0))
                    {
                        NumbersSpoken[newNumber] = (turn, -1);
                    }
                    else
                    {
                        NumbersSpoken[newNumber].PreviousMention = NumbersSpoken[newNumber].LastMentioned;
                        NumbersSpoken[newNumber].LastMentioned = turn;
                    }
                    prev = newNumber;
                }
                ++turn;
                if (turn % 10000 == 0)
                {
                    Console.WriteLine($"{turn} : {prev}");
                }
            }

            return prev;
        }
        
    }
}