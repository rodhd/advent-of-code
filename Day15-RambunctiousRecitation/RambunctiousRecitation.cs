using System;
using System.Collections.Generic;
using System.Linq;

namespace Day15_RambunctiousRecitation
{
    public class RambunctiousRecitation
    {
        private int[] StartingNumbers { get; set; }
        private List<int> NumbersSpoken { get; set; }

        public RambunctiousRecitation(string input)
        {
            StartingNumbers = input.Split(",").Select(x => Int32.Parse(x)).ToArray();
            NumbersSpoken = StartingNumbers.ToList();

        }

        private void Recite()
        {
            int last = NumbersSpoken.Last();
            if (NumbersSpoken.Count(x => x == last) == 1)
            {
                NumbersSpoken.Add(0);
            }
            else
            {
                var temp = NumbersSpoken
                    .GetRange(0, NumbersSpoken.Count - 1)
                    .LastIndexOf(last);
                
                NumbersSpoken.Add(NumbersSpoken.LastIndexOf(last) - temp);
            }
        }

        public int FirstAnswer()
        {
            while (NumbersSpoken.Count() < 2020)
            {
                Recite();
            }

            return NumbersSpoken.Last();
        }

    }
}