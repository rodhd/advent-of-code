using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day19_MonsterMessages
{
    public class MMStack
    {
        private Dictionary<int, string> Rules { get; set; }
        private List<string> Messages { get; set; }
        private static Regex RulePattern => new Regex(@"(?<ind>\d+): (?<rules>[\d\s\|]+)");
        private static Regex FinalRulePattern => new Regex("(?<ind>\\d+): (?<letter>\"[a,b]\")");
        private static Regex MessagePattern => new Regex(@"^[a,b]+");

        public MMStack(string[] input)
        {
            Rules = new Dictionary<int, string>();
            Messages = new List<string>();
            foreach (var s in input)
            {
                if(RulePattern.IsMatch(s))
                {
                    var m = RulePattern.Match(s);
                    Rules.Add(int.Parse(m.Result("${ind}")), m.Result("${rules}"));
                }

                if (FinalRulePattern.IsMatch(s))
                {
                    var m = FinalRulePattern.Match(s);
                    Rules.Add(int.Parse(m.Result("${ind}")),m.Result("${letter}").Replace("\"",""));
                }

                if (MessagePattern.IsMatch(s))
                {
                    Messages.Add(s);
                }
                
            }
        }

        private List<List<int>> RuleToList(string r)
        {
            return r
                .Split("|")
                .Select(x => x.Trim().Split(" ").Select(y => int.Parse(y)).ToList()).ToList();
        }

        private List<List<int>> Resolve(List<int> rule)
        {
            var r = rule.First();
            var newRules = RuleToList(Rules[r]);
            List<List<int>> result = new List<List<int>>();
            foreach (var nr in newRules)
            {
                var tem = rule.ToList();
                tem.RemoveAt(0);
                tem.InsertRange(0, nr);
                result.Add(tem);
            }

            return result;
        }

        private bool IsLetter(List<int> rule)
        {
            return Rules[rule.First()] == "a" || Rules[rule.First()] == "b";
        }

        private bool IsMatch(List<int> rule, string message, int ind)
        {
            return ind < message.Length && Rules[rule.First()] == message[ind].ToString();
        }

        private bool CheckMessage(string message, int startRule)
        {
            List<List<int>> first = RuleToList(Rules[startRule]);
            Stack<(List<int>, int ind)> stack = new Stack<(List<int>, int ind)>();
            HashSet<string> alreadyVisited = new HashSet<string>();
            stack.Push((first.First(),0));

            while (stack.Any())
            {
                var p = stack.Pop();

                if (!p.Item1.Any())
                {
                    continue;
                }
                
                if (alreadyVisited.Contains(GetRuleString(p.Item1, p.Item2)))
                {
                    continue;
                }

                alreadyVisited.Add(GetRuleString(p.Item1, p.Item2));
                if (IsLetter(p.Item1))
                {
                    if (IsMatch(p.Item1, message, p.Item2))
                    {
                        p.Item1.RemoveAt(0);
                        if (!p.Item1.Any() && p.Item2 == message.Length - 1)
                        {
                            return true;
                        }
                        p.Item2 += 1;
                        stack.Push(p);
                    }
                }
                else
                {
                    var newRules = Resolve(p.Item1);
                    foreach (var nr in newRules)
                    {
                        stack.Push((nr,p.Item2));
                    }
                }
            }

            return false;
        }

        public string GetRuleString(List<int> rules, int ind)
        {
            return $"{string.Join(",", rules)}:{ind}";
        }

        public void FirstAnswer()
        {
            var count = 0;
            foreach (var m in Messages)
            {
                if (CheckMessage(m, 0))
                {
                    count++;
                }
            }

            Console.WriteLine($"{count}");
        }

        public void SecondAnswer()
        {
            var count = 0;
            Rules[8] = "42 | 42 8";
            Rules[11] = "42 31 | 42 11 31";
            
            foreach (var m in Messages)
            {
                if (CheckMessage(m, 0))
                {
                    count++;
                }
            }

            Console.WriteLine($"{count}");

        }
    }
}