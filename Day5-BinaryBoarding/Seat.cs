using System;

namespace Day5_BinaryBoarding
{
    public class Seat
    {
        public int Row { get; set; }
        public int Column { get; set; }
        
        public string Code { get; set; }

        public int Id => (Row * 8) + Column;

        public Seat(string code)
        {
            Code = code;
            
            int row = Convert.ToInt32(code
                .Substring(0, 7)
                .Replace("F", "0")
                .Replace("B", "1"),2);
            
            int column = Convert.ToInt32(code
                .Substring(7, 3)
                .Replace("L", "0")
                .Replace("R", "1"),2);

            Row = row;
            Column = column;
        }

    }
}