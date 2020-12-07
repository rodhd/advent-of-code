using System;

namespace Day7_HandyHaversacks
{
    public class Bag
    {
        public string Shade { get; set; }
        public string Color { get; set; }

        public bool Compare(Bag b)
        {
            return Shade == b.Shade && Color == b.Color;
        }
    }
}