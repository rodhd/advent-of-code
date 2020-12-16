using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Schema;
using Google.OrTools.LinearSolver;
using Google.OrTools.Sat;
using Constraint = Google.OrTools.Sat.Constraint;

namespace Day16_TicketTranslation
{
    public class TicketTranslation
    {
        private List<(string name,(int minF, int maxF),(int minS, int maxS))> Rules { get; }
        private int[] MyTicket { get; }
        private List<int[]> OtherTickets { get; }
        private static string RulePattern = @"^(?<name>[\w\s]+): (?<minF>\d+)-(?<maxF>\d+) or (?<minS>\d+)-(?<maxS>\d+)";
        private List<int[]> ValidTickets => OtherTickets.Where(x => IsTicketValid(x)).ToList();

        public TicketTranslation(string[] input)
        {
            Rules = new List<(string name, (int minF, int maxF), (int minS, int maxS))>();
            OtherTickets = new List<int[]>();
            foreach (var s in input)
            {
                if (Regex.IsMatch(s, RulePattern))
                {
                    var r = Regex.Match(s, RulePattern);
                    string name = r.Result("${name}");
                    var p1 = (int.Parse(r.Result("${minF}")), int.Parse(r.Result("${maxF}")));
                    var p2 = (int.Parse(r.Result("${minS}")), int.Parse(r.Result("${maxS}")));
                    Rules.Add((name, p1, p2));
                }
                
                if (MyTicket != null && Regex.IsMatch(s, @"(\d+,)+\d"))
                {
                    OtherTickets.Add(s.Split(",").Select(x => int.Parse(x)).ToArray());
                }

                if (MyTicket == null && Regex.IsMatch(s, @"(\d+,)+\d"))
                {
                    MyTicket = s.Split(",").Select(x => int.Parse(x)).ToArray();
                }

            }
        }

        private bool IsValidNumber(int n)
        {
            return Rules.Any(x => (n >= x.Item2.minF && n <= x.Item2.maxF) || (n >= x.Item3.minS && n <= x.Item3.maxS));
        }

        private int CheckNumber(int[] ticket)
        {
            return ticket.Where(x => !IsValidNumber(x)).Sum();
        }

        public int FirstAnswer()
        {
            return OtherTickets.Select(x => CheckNumber(x)).Aggregate((a, b) => a + b);
        }

        private bool IsTicketValid(int[] ticket)
        {
            return ticket.All(x => IsValidNumber(x));
        }

        private bool IsFieldCorrect(int field, (string name, (int mn, int mx) first, (int mn, int mx) second) rule)
        {
            List<int> values = new List<int>();
            values.Add(MyTicket.ElementAt(field));
            values.AddRange(ValidTickets.Select(x => x[field]));

            bool res = values.All(x =>
                (x >= rule.first.mn && x <= rule.first.mx) || (x >= rule.second.mn && x <= rule.second.mx));

            return res;
        }

        public long SecondAnswer()
        {
            List<(string, int)> result = new List<(string, int)>();
            for (int i = 0; i < MyTicket.Length; i++)
            {
                result.AddRange(Rules.Where(x => IsFieldCorrect(i, x)).Select(x => (x.name, i)).ToList());
            }

            Dictionary<string, int[]> fields = new Dictionary<string, int[]>();

            foreach (var rule in Rules)
            {
                var f = result
                    .Where(x => x.Item1 == rule.name)
                    .Select(x => x.Item2);
                int[] tem = new int[MyTicket.Length];
                foreach (var n in f)
                {
                    tem[n] = 1;
                }
                fields.Add(rule.name, tem);
            }

            Solver solver = Solver.CreateSolver("SCIP");
            Variable[,] vars = new Variable[MyTicket.Length, Rules.Count()];
            int[,] coefs = new int[MyTicket.Length, Rules.Count()];

            for (int i = 0; i < MyTicket.Length; i++)
            {
                for (int j = 0; j < Rules.Count; j++)
                {
                    vars[i,j] = solver.MakeBoolVar($"x_{i}{j}");
                    coefs[i, j] = result.Any(x => x.Item1 == Rules[j].name && x.Item2 == i) ? 1 : 0;
                }
            }

            

            for (int i = 0; i < MyTicket.Length; i++)
            {
                var constraint = solver.MakeConstraint(0,1);
                for (int j = 0; j < Rules.Count; j++)
                {
                    constraint.SetCoefficient(vars[i,j], coefs[i,j]);
                }
            }
            
            for (int j = 0; j < Rules.Count; j++)
            {
                var constraint = solver.MakeConstraint(0,1);
                for (int i = 0; i < MyTicket.Length; i++)
                {
                    constraint.SetCoefficient(vars[i,j], coefs[i,j]);
                }
            }

            Objective objective = solver.Objective();
            
            for (int i = 0; i < MyTicket.Length; i++)
            {
                for (int j = 0; j < Rules.Count; j++)
                {
                    objective.SetCoefficient(vars[i,j], coefs[i,j]);
                }
            }
            objective.SetMaximization();

            Solver.ResultStatus resultStatus = solver.Solve();

            long res = 1;
            
            for (int i = 0; i < MyTicket.Length; i++)
            {
                string st = "";
                for (int j = 0; j < Rules.Count; j++)
                {
                    st += $"{vars[i,j].SolutionValue()}\t";
                    if (vars[i, j].SolutionValue() == 1 && Rules[j].name.Contains("departure"))
                    {
                        res *= MyTicket[i];
                    }
                }
                Console.WriteLine(st);
            }

            return res;
        }
    }
}