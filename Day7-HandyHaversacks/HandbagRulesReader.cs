using System;
using System.Collections.Generic;
using System.Linq;

namespace Day7_HandyHaversacks
{
    public static class HandbagRulesReader
    {
        public static int TotalBags(string[] input)
        {
            List<BagRule> rules = new List<BagRule>();
            foreach (var l in input)
            {
                var split = l.Split("contain");
                
                var p1 = split[0].Split(" ");
                var bag_1 = new Bag()
                {
                    Shade = p1[0],
                    Color = p1[1]
                };

                var p2 = split[1].Split(",");
                foreach (var p in p2)
                {
                    if(!p.Contains("no other"))
                    {
                        var pn = p.Trim().Split(" ");
                        var bag_2 = new Bag()
                        {
                            Shade = pn[1],
                            Color = pn[2]
                        };
                        rules.Add(new BagRule()
                        {
                            Amount = int.Parse(pn[0]),
                            ContainerBag = bag_1,
                            ContainedBag = bag_2
                        });
                    }
                }
            }

            Bag start = new Bag() {Color = "gold", Shade = "shiny"};
            List<Bag> checkedBags = new List<Bag>();
            List<BagRule> checkedRules = new List<BagRule>();
            var result = FindContainers(start, rules, checkedRules, checkedBags);
            return result.Distinct().Count();

        }

        public static List<Bag> FindContainers(Bag start, List<BagRule> rules, List<BagRule> checkedRules, List<Bag> checkedBags)
        {
            foreach (var r in rules.Where(x => x.ContainedBag.Compare(start)))
            {
                if (!checkedRules.Contains(r))
                {
                    checkedBags.Add(r.ContainerBag);
                    checkedRules.Add(r);
                    if (rules.Any(x => x.ContainedBag.Compare(r.ContainerBag)))
                    {
                        FindContainers(r.ContainerBag, rules, checkedRules, checkedBags);
                    }
                }
            }

            return checkedBags;
        }
        
        public static int TotalBagsInside(string[] input)
        {
            List<BagRule> rules = new List<BagRule>();
            foreach (var l in input)
            {
                var split = l.Split("contain");
                
                var p1 = split[0].Split(" ");
                var bag_1 = new Bag()
                {
                    Shade = p1[0],
                    Color = p1[1]
                };

                var p2 = split[1].Split(",");
                foreach (var p in p2)
                {
                    if(!p.Contains("no other"))
                    {
                        var pn = p.Trim().Split(" ");
                        var bag_2 = new Bag()
                        {
                            Shade = pn[1],
                            Color = pn[2]
                        };
                        rules.Add(new BagRule()
                        {
                            Amount = int.Parse(pn[0]),
                            ContainerBag = bag_1,
                            ContainedBag = bag_2
                        });
                    }
                }
            }

            Bag start = new Bag() {Color = "gold", Shade = "shiny"};
            return CountBags(start, rules);
        }

        public static int CountBags(Bag start, List<BagRule> rules)
        {
            var s = rules.Where(x => x.ContainerBag.Compare(start));
            return 1 + s.Select(x => x.Amount * CountBags(x.ContainedBag, rules)).Sum();
        }
    }
}