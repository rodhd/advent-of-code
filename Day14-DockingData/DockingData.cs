using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day14_DopckingData
{
    public class DockingData
    {
        private Dictionary<ulong, ulong> Memory { get; set; }
        private string CurrentMask { get; set; }
        private string[] Input { get; set; }

        public DockingData(string[] input)
        {
            Memory = new Dictionary<ulong, ulong>();
            CurrentMask = "";
            Input = input;
        }

        private void ProcessStep(string s)
        {
            if (s.StartsWith("mask"))
            {
                UpdateMask(s);
            }
            else
            {
                UpdateMemory(s);
            }
        }

        private void UpdateMask(string m)
        {
            CurrentMask = m.Split("=").ElementAt(1).Trim();
        }

        private void UpdateMemory(string n)
        {
            ulong address = ulong.Parse(Regex.Matches(n, @"\[\d+\]").First().ToString().Replace("[","").Replace("]",""));
            if (Memory.ContainsKey(address))
            {
                Memory[address] = ApplyMask(n.Split("=").ElementAt(1).Trim());
            }
            else
            {
                Memory.Add(address, ApplyMask(n.Split("=").ElementAt(1).Trim()));
            }
        }

        private ulong ApplyMask(string i)
        {
            string tem = Convert.ToString(Convert.ToInt32(i),2);
            string padding = new int[36 - tem.Length].Select(x => x.ToString()).Aggregate((a,b) => a + b);

            string value = padding + tem;

            var new_value = value
                .Zip(CurrentMask, (a, b) => b != 'X' ? b : a);
            
            return Convert.ToUInt64(string.Join("", new_value), 2);
        }

        public ulong FirstAnswer()
        {
            foreach (var i in Input)
            {
                ProcessStep(i);
            }

            return Memory.Select(x => x.Value).Aggregate((a, b) => a + b);
        }

        public void ProcessStepNew(string s)
        {
            if (s.StartsWith("mask"))
            {
                UpdateMask(s);
            }
            else
            {
                UpdateMemoryNew(s);
            } 
        }

        private void UpdateMemoryNew(string n)
        {
            long address = long.Parse(Regex.Matches(n, @"\[\d+\]").First().ToString().Replace("[","").Replace("]",""));
            ulong[] allAddresses = GetAddresses(address);

            foreach (var a in allAddresses)
            {
                if (Memory.ContainsKey(a))
                {
                    Memory[a] = Convert.ToUInt64(n.Split("=").ElementAt(1).Trim());
                }
                else
                {
                    Memory.Add(a, Convert.ToUInt64(n.Split("=").ElementAt(1).Trim()));
                }
            }
        }

        private ulong[] GetAddresses(long address)
        {
            string tem = Convert.ToString(address, 2);
            string padding = new int[36 - tem.Length].Select(x => x.ToString()).Aggregate((a,b) => a + b);

            string value = padding + tem;

            var new_value = value
                .Zip(CurrentMask, (a, b) => b != '0' ? b : a);

            string mask = string.Join("", new_value);
            List<ulong> result = new List<ulong>();
            int floats = new_value.Count(x => x == 'X');

            for (int i = 0; i < Math.Pow(2, floats); i++)
            {
                string t = Convert.ToString(i, 2);
                string pad = string.Join("", new int[floats - t.Length]);
                var r = pad + t;
                var z = ZipBins(mask, r);
                result.Add(Convert.ToUInt64(z,2));
            }

            return result.ToArray();
        }

        private string ZipBins(string m, string r)
        {
            var divMask = m.Split("X");
            string result = "";
            for (int i = 0; i < divMask.Length; i++)
            {
                result += divMask[i];
                if (i < r.Length)
                {
                    result += r[i];
                }
            }

            return result;
        }

        public ulong SecondAnswer()
        {
            foreach (var i in Input)
            {
                ProcessStepNew(i);
            }

            return Memory.Select(x => Convert.ToUInt64(x.Value)).Aggregate((a, b) => a + b);
        }
    }
}