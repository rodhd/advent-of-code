using System;
using System.Collections.Generic;
using System.Linq;

namespace Day18_OperationOrder
{
    public class OperationOrder
    {
        public string[] Input { get; set; }

        public OperationOrder(string[] input)
        {
            Input = input.Select(x => x.Replace(" ","")).ToArray();
        }

        public string ResolveParenthesis(string p)
        {
            if (p.Contains('('))
            {
                int last = p.LastIndexOf('(');
                int next = p.IndexOf(')', last);
                var expr = Solve(p.Substring(last + 1, next - last - 1));
                string k = p[..(last)] + expr + p[(next+1)..];
                return Solve(ResolveParenthesis(k)).ToString();
            }

            return Solve(p).ToString();
        }

        public long Solve(string expression)
        {
            int i = 0;
            string rt = "";
            while (i < expression.Length && char.IsDigit(expression[i]))
            {
                rt += expression[i];
                i++;
            }
            long r = long.Parse(rt);
            string t = "";
            char op = ' ';
            
            while (i < expression.Length)
            {
                if (expression[i] == '+' || expression[i] == '*')
                {
                    
                    if (op == '+')
                    {
                        r += long.Parse(t);
                    }

                    if (op == '*')
                    {
                        r *= long.Parse(t);
                    }
                    op = expression[i];
                    t = "";
                }
                else
                {
                    if (char.IsDigit(expression[i]))
                    {
                        t += expression[i];
                    }
                }

                i++;
            }

            if (op == '+')
            {
                r += long.Parse(t);
            }

            if (op == '*')
            {
                r *= long.Parse(t);
            }

            return r;
        }

        public long SolveAlt(string expression)
        {
            var tem = expression.Split("*");

            List<string> tem2 = new List<string>();

            foreach (var t in tem)
            {
                if (!t.Contains("+"))
                {
                    tem2.Add(t);
                }
                else
                {
                    tem2.Add(t.Split("+").Select(x => long.Parse(x)).Sum().ToString());
                }
            }

            return tem2.Select(x => long.Parse(x)).Aggregate((a, b) => a * b);
        }
        
        public string ResolveParenthesisAlt(string p)
        {
            if (p.Contains('('))
            {
                int last = p.LastIndexOf('(');
                int next = p.IndexOf(')', last);
                var expr = SolveAlt(p.Substring(last + 1, next - last - 1));
                string k = p[..(last)] + expr + p[(next+1)..];
                return SolveAlt(ResolveParenthesisAlt(k)).ToString();
            }

            return SolveAlt(p).ToString();
        }
        

        public void FirstAnswer()
        {
            long result = Input
                .Select(x => long.Parse(ResolveParenthesis(x)))
                .Sum();
            Console.WriteLine($"Answer 1:{result}");
        }

        public void SecondAnswer()
        {
            var result = Input
                .Select(x => long.Parse(ResolveParenthesisAlt(x)));

            foreach (var r in result)
            {
                Console.WriteLine(r);
            }
            
            Console.WriteLine($"Answer 2:{result.Sum()}");
        }
    }
}