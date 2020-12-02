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
            Console.WriteLine($"There are {answer1} valid passwords");
        }
    }
}