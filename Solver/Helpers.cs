using System;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;

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

        public static string[] ReadInputRegex(string path, string exp)
        {
            var inputsDir = "/Users/user/Projects/personal/advent-of-code/Solver/Inputs/";
            string text = System.IO.File.ReadAllText(inputsDir + path);

            Regex r = new Regex(exp);

            var matches = Regex.Matches(text, exp);

            var result = matches.Select(x => x.ToString()).ToArray();

            return result;
        }
        
        public static string[] ReadParagraphs(string path)
        {
            var inputsDir = "/Users/user/Projects/personal/advent-of-code/Solver/Inputs/";
            Regex r = new Regex(@"(([a-z]+|\n[a-z]+))+\n");
            string text = System.IO.File.ReadAllText(inputsDir + path);

            var matches = r.Matches(text);

            var result = matches.Select(x => x.ToString()).ToArray();

            return result;
        }
    }
}