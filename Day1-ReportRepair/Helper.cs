#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace Day1_ReportRepair
{
    public static class Helper
    {
        public static Tuple<int, int, int>? ThreeNumbers(List<int> input)
        {
            var orderedList = input.OrderByDescending(x => x).ToList();

            foreach (var x in orderedList)
            {
                var rest = 2020 - x;
                var remaining = orderedList.Where(r => r < rest).ToList();
                if (remaining.Count < 3)
                {
                    continue;
                }

                var test = TwoNumbers(remaining, rest);
                if (test != null)
                {
                    return new Tuple<int, int, int>(test.Item1, test.Item2, x);
                }
            }
            return null;
        }

        public static Tuple<int, int>? TwoNumbers(List<int> input, int target)
        {
            var lowerHalf = input.Where(x => x < Decimal.Round(target / 2)).OrderBy(x => x).ToList();
            var upperHalf = input.Where(x => x >= Decimal.Round(target / 2)).OrderBy(x => x).ToList();

            if (lowerHalf.Count < 1 || upperHalf.Count < 1)
            {
                return null;
            }
            
            foreach (var x in upperHalf)
            {
                foreach (var y in lowerHalf)
                {
                    if (x + y == target)
                    {
                        return new Tuple<int, int>(x, y);
                    }

                    if (x + y > target)
                    {
                        break;
                    }
                }
            }

            return null;
        }
    }
}