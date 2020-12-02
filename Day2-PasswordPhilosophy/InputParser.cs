using System;
using System.Linq;

namespace Day2_PasswordPhilosophy
{
    public static class InputParser
    {
        public static PasswordEntry ParseInput(string input)
        {
            var minFrequency = Int32.Parse(input.Split(" ").First().Split("-").First());
            var maxFrequency = Int32.Parse(input.Split(" ").First().Split("-")[1]);
            var character = input.Split(" ")[1].Replace(":","")[0];
            var password = input.Split(" ")[2];
            return new PasswordEntry(minFrequency, maxFrequency, character, password);
        }
    }
}