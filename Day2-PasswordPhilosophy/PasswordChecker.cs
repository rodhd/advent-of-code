using System.Linq;

namespace Day2_PasswordPhilosophy
{
    public static class PasswordChecker
    {
        public static int CheckPasswords(string path)
        {
            var input = ReadInput(path);
            var passwords = input.Select(x => InputParser.ParseInput(x)).ToList();
            var validPasswords = passwords.Select(x => x.IsPasswordValid()).Count(x => x);
            return validPasswords;
        }
        
        public static int CheckPasswordsAlternative(string path)
        {
            var input = ReadInput(path);
            var passwords = input.Select(x => InputParser.ParseInput(x)).ToList();
            var validPasswords = passwords.Select(x => x.IsPasswordValidAlternative()).Count(x => x);
            return validPasswords;
        }
        
        public static string[] ReadInput(string path)
        {
            string[] lines = System.IO.File.ReadAllLines( path);
            return lines;
        }
    }
}