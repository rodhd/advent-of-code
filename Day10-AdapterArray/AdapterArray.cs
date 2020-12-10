using System;
using System.Collections.Generic;
using System.Linq;

namespace Day10_AdapterArray
{
    public class AdapterArray
    {
        public List<int> Adapters { get; set; }

        public const int OutletJoltage = 0;

        public int DeviceJoltage => Adapters.Max() + 3;

        public AdapterArray(string[] input)
        {
            Adapters = input.Select(x => int.Parse(x)).ToList();
        }

        public int FirstAnswer()
        {
            List<int> tem = new List<int>();
            tem.Add(OutletJoltage);
            tem.AddRange(Adapters.OrderBy(x => x).ToList());
            tem.Add(DeviceJoltage);

            var diffs = tem
                .Zip(tem.Skip(1), (a, b) => Tuple.Create(a, b))
                .Select(x => x.Item2 - x.Item1)
                .ToList();
            int oneJolts = diffs.Count(x => x == 1);
            int threeJolts = diffs.Count(x => x == 3);

            return oneJolts * threeJolts;
        }

        public ulong SecondAnswer()
        {
            List<int> tem = new List<int>();
            tem.Add(OutletJoltage);
            tem.AddRange(Adapters.OrderBy(x => x).ToList());
            tem.Add(DeviceJoltage);
            
            var diffs = tem
                .Zip(tem.Skip(1), (a, b) => Tuple.Create(a, b))
                .Select(x => (x.Item2 - x.Item1) == 1)
                .ToList();

            int help = 0;
            ulong result = 1;

            foreach (var d in diffs)
            {
                if (d)
                {
                    help++;
                }
                else
                {
                    result *= Combination(help);
                    help = 0;
                }
            }
            result *= Combination(help);
            return result;
        }
        private ulong Combination(int i)
        {
            switch (i)
            {
                case 4:
                    return 7;
                case 3:
                    return 4;
                case 2:
                    return 2;
            }

            return 1;
        }

    }
}