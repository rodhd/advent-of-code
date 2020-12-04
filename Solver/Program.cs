using System;
using Common;
using Day4_PassportProcessing;

namespace Solver
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Helpers.ReadInput("Day4.txt");
            var result = PassportValidation.Validate(input);
            Console.WriteLine($"Answer 1: There are {result} valid passports");

            var result2 = PassportValidation.ExtraValidate(input);
            Console.WriteLine($"Answer 2: There are {result2} valid passports");
        }
    }
}