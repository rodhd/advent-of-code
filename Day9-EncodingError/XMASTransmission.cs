using System;
using System.Collections.Generic;
using System.Linq;

namespace Day9_EncodingError
{
    public class XMASTransmission
    {
        public List<long> Sequence { get; set; }
        
        private int ActiveIndex { get; set; }
        
        public int Preamble { get; set; }

        private List<long> ActiveRange => Sequence.GetRange(ActiveIndex - Preamble, Preamble);
        
        private List<long> ValidNumbers
        {
            get
            {
                List<long> vn = new List<long>();
                foreach (var n in this.ActiveRange)
                {
                    vn.AddRange(this.ActiveRange.Where(x => x != n).Select(x => n + x));
                }

                return vn;
            }
        }

        public bool IsCurrentNumberValid()
        {
            return ValidNumbers.Any(x => x == Sequence[ActiveIndex]);
        }

        public XMASTransmission(string[] input, int preamble)
        {
            Sequence = input.Select(x => long.Parse(x)).ToList();
            ActiveIndex = preamble;
            Preamble = preamble;
        }

        public long SolveFirst()
        {
            while (ActiveIndex < Sequence.Count)
            {
                if (IsCurrentNumberValid())
                {
                    ActiveIndex += 1;
                }
                else
                {
                    return Sequence[ActiveIndex];
                }
            }

            return 0;
        }

        public long SolveSecond(long i)
        {
            while (ActiveIndex < Sequence.Count)
            {
                int x = 1;
                while (x < ActiveIndex)
                {
                    if (Sequence.GetRange(ActiveIndex - x, x + 1).Sum() == i)
                    {
                        return Sequence.GetRange(ActiveIndex - x, x + 1).Min() +
                               Sequence.GetRange(ActiveIndex - x, x + 1).Max();
                    }

                    x += 1;
                }

                ActiveIndex += 1;
            }

            return 0;
        }
    }
}