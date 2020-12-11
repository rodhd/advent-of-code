using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Day11_SeatingSystem
{
    public class SeatingSystem
    {
        public static int FLOOR = 0;
        public static int FREE = 1;
        public static int OCCUPIED = 2;
        
        private static IList<(int x, int y)> nextSeats = new List<(int x, int y)>
        {
            (-1, -1), (-1, 0), (-1, 1),
            (0, -1), (0, 1),
            (1, -1), (1, 0), (1, 1)
        };
        public int[,] SeatsCurrent { get; set; }

        public SeatingSystem(string[] input)
        {
            int h = input.Length;
            int w = input.First().Length;
            SeatsCurrent = new int[h, w];
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    if (input[i][j] == 'L')
                    {
                        SeatsCurrent[i, j] = FREE;
                    }
                }
            }
        }

        public int[,] NextSeatingArrangement()
        {
            int[,] clone = new int[SeatsCurrent.GetLength(0), SeatsCurrent.GetLength(1)];
            for (int i = 0; i < SeatsCurrent.GetLength(0); i++)
            {
                for (int j = 0; j < SeatsCurrent.GetLength(1); j++)
                {
                    if (OccupiedSeats(i, j) == 0 && SeatsCurrent[i,j] == FREE)
                    {
                        clone[i,j] = OCCUPIED;
                    }

                    else if (OccupiedSeats(i, j) > 3 && SeatsCurrent[i,j] == OCCUPIED)
                    {
                        clone[i,j] = FREE;
                    }

                    else
                    {
                        clone[i,j] = SeatsCurrent[i,j];
                    }
                }
            }

            return clone;
        }
        
        public int[,] NextFirstSeatingArrangement()
        {
            int[,] clone = new int[SeatsCurrent.GetLength(0), SeatsCurrent.GetLength(1)];
            for (int i = 0; i < SeatsCurrent.GetLength(0); i++)
            {
                for (int j = 0; j < SeatsCurrent.GetLength(1); j++)
                {
                    if (FirstOccupiedSeats(i, j) == 0 && SeatsCurrent[i,j] == FREE)
                    {
                        clone[i,j] = OCCUPIED;
                    }

                    else if (FirstOccupiedSeats(i, j) >= 5 && SeatsCurrent[i,j] == OCCUPIED)
                    {
                        clone[i,j] = FREE;
                    }

                    else
                    {
                        clone[i,j] = SeatsCurrent[i,j];
                    }
                }
            }

            return clone;
        }
        private int OccupiedSeats(int i, int j)
        {
            return nextSeats
                .Where(t => IsValidPos(t.x + i, t.y + j))
                .Select(k => SeatsCurrent[i + k.x,j + k.y] == OCCUPIED ? 1 : 0)
                .Sum();
        }

        private int FirstOccupiedSeats(int i, int j)
        {
            List<int> seats = new List<int>();
            foreach (var n in nextSeats)
            {
                int k = 1;
                while (IsValidPos(i + n.x * k, j + n.y * k))
                {
                    if (SeatsCurrent[i + n.x * k, j + n.y * k] != FLOOR)
                    {
                        seats.Add(SeatsCurrent[i + n.x * k, j +  n.y * k] );
                        break;
                    }
                    k++;
                }
            }
            return seats.Count(x => x == OCCUPIED);
        }
        
        private bool IsValidPos(int x, int y)
        {
            return x >= 0 && x < SeatsCurrent.GetLength(0)
                          && y >= 0 && y < SeatsCurrent.GetLength(1);
        }

        public bool SeatsEqual(int[,] other)
        {
            return SeatsCurrent.Cast<int>().SequenceEqual(other.Cast<int>());
        }

        public void Copy(int[,] other)
        {
            for (int i = 0; i < SeatsCurrent.GetLength(0); i++)
            {
                for (int j = 0; j < SeatsCurrent.GetLength(1); j++)
                {
                    SeatsCurrent[i, j] = other[i, j];
                }
            }
        }

        public void Print(int[,] input)
        {
            for (int i = 0; i < input.GetLength(0); i++)
            {
                var s = "";
                for (int j = 0; j < input.GetLength(1); j++)
                {
                    s += input[i, j].ToString();
                }
                Console.WriteLine(s);
            }
        }
        public int FirstAnswer()
        {
            var next = NextSeatingArrangement();
            while (!SeatsEqual(next))
            {
                Copy(next);
                next = NextSeatingArrangement();
                //Print(SeatsCurrent);
                //Console.WriteLine(" ");
                Console.WriteLine($"{SeatsCurrent.Cast<int>().Sum()} : {next.Cast<int>().Sum()}");
            }
            
            return next.Cast<int>().Count(x => x == OCCUPIED);
        }
        
        public int SecondAnswer()
        {
            var next = NextFirstSeatingArrangement();
            while (!SeatsEqual(next))
            {
                Copy(next);
                next = NextFirstSeatingArrangement();
                Print(SeatsCurrent);
                Console.WriteLine(" ");
                //Console.WriteLine($"{SeatsCurrent.Cast<int>().Sum()} : {next.Cast<int>().Sum()}");
            }
            
            return next.Cast<int>().Count(x => x == OCCUPIED);
        }
    }
}