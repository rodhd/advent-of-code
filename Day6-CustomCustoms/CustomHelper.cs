using System;
using System.Linq;

namespace Day6_CustomCustoms
{
    public static class CustomHelper
    {
        public static int YesCounter(string[] input)
        {
            var result = input
                .Select(x => x.Replace("\n", ""))
                .Select(x => x.Distinct().Count())
                .Aggregate((a,b) => a + b);

            return result;
        }
        
        public static int AllYesCounter(string[] input)
        {
            var groups = input
                .Select(x => x.Split("\n"))
                .Select(x => x.Where(x => !String.IsNullOrWhiteSpace(x)).ToArray());
            int result = 0;
            foreach (var g in groups)
            {
                int count = 0;
                char[] letters = g.Aggregate((a, b) => a + b).Distinct().ToArray();
                foreach (var l in letters)
                {
                    if (g.All(x => x.Contains(l)))
                    {
                        count += 1;
                    }
                }
                result += count;
            }
            return result;
        }
    }
}