using System;
using System.Collections.Generic;
using System.Linq;

namespace Day17_ConwayCubes
{
    public class ConwayCubes
    {
        private Matrix3D Cubes { get; set; }
        private Matrix4D Cubes4d { get; set; }

        public ConwayCubes(string[] input)
        {
            Cubes = new Matrix3D(0, input.Length, 0, input.First().Length, 0, 1);
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    bool active = input[i][j] == '#';
                    Cubes[i, j, 0] = active;
                }
            }

            Cubes4d = new Matrix4D(0, input.Length, 0, input.First().Length, 0, 1, 0, 1);
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    bool active = input[i][j] == '#';
                    Cubes4d[i, j, 0, 0] = active;
                }
            }
        }

        public void FirstAnswer()
        {
            int total = 0;
            for (int i = 0; i < 6; i++)
            {
                Cubes = Cubes.Cycle();
                total = Cubes.TotalActive();
                Console.WriteLine($"Active: {total}");
            }
            
            
            Console.WriteLine($"Active: {total}");
        }

        public void SecondAnswer()
        {
            int total = 0;
            for (int i = 0; i < 6; i++)
            {
                Cubes4d = Cubes4d.Cycle();
                total = Cubes4d.TotalActive();
                Console.WriteLine($"Active: {total}");
            }
            
            
            Console.WriteLine($"Active: {total}");
        }
    }
}