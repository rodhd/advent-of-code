using System;
using System.Linq;

namespace Common
{
    public static class Helpers
    {
        public static string[] ReadInput(string path)
        {
            var inputsDir = "/Users/user/Projects/personal/advent-of-code/Solver/Inputs/";
            string[] lines = System.IO.File.ReadAllLines(inputsDir + path);
            return lines;
        }
    }
}