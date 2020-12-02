using System;

namespace Day2_PasswordPhilosophy
{
    class Program
    {
        static void Main(string[] args)
        {
            var answer1 =
                PasswordChecker.CheckPasswords(
                    "/Users/user/Projects/personal/advent-of-code/Day2-PasswordPhilosophy/Input.txt");
            Console.WriteLine($"Answer 1: There are {answer1} valid passwords");

            var answer2 =
                PasswordChecker.CheckPasswordsAlternative(
                    "/Users/user/Projects/personal/advent-of-code/Day2-PasswordPhilosophy/Input.txt");
            Console.WriteLine($"Answer 2: There are {answer2} valid passwords");
        }
    }
}