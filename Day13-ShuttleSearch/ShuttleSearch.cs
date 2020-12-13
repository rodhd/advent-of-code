using System;
using System.Linq;

namespace Day12_ShuttleSearch
{
    public class ShuttleSearch
    {
        public int EarliestTs { get; set; }
        
        public int[] Buslines { get; set; }
        
        private int[] Diffs { get; set; }

        public ShuttleSearch(string[] input)
        {
            EarliestTs = int.Parse(input.First());
            Buslines = input[1]
                .Split(",")
                .Where(x => x != "x")
                .Select(x => int.Parse(x))
                .ToArray();

            int[] temp = Enumerable.Range(0, input[1].Length).ToArray();
            bool[] nums = input[1]
                .Split(",")
                .Select(x => x != "x")
                .ToArray();
            Diffs = temp
                .Zip(nums, (a, b) => Tuple.Create(a,b))
                .Where(x => x.Item2)
                .Select(x => x.Item1)
                .ToArray();
        }

        public int TimeToNext(int i)
        {
            return Convert.ToInt32(Math.Ceiling((decimal) EarliestTs / (decimal) i)) * i - EarliestTs;
        }

        public int FirstAnswer()
        {
            var ord = Buslines
                .OrderBy(x => TimeToNext(x));

            return ord.First() * TimeToNext(ord.First());

        }

        public bool IsAnswer(long t)
        {
            bool answer = Buslines
                .Zip(Diffs, (a, b) => (t + b) % a)
                .All(x => x == 0);

            return answer;
        }

        public long SecondAnswer()
        {
            long seed = 0;
            long step = Buslines.First();
            int i = 1;

            while (!IsAnswer(seed))
            {
                seed += step;
                if (seed % Buslines.First() == 0 && (seed + Diffs[i]) % Buslines.ElementAt(i) == 0)
                {
                    step = Buslines[0..(i+1)].Select(x => Convert.ToInt64(x)).Aggregate((a, b) => a * b);
                    i++;
                }
            }

            return seed;
        }
        
        
        
    }
}