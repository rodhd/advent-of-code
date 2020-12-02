using System;
using System.Linq;

namespace Day2_PasswordPhilosophy
{
    public class PasswordEntry
    {
        public int MinFrequency { get; set; }

        public int MaxFrequency { get; set; }

        public char Character { get; set; }

        public string Password { get; set; }

        public PasswordEntry(int minFrequency, int maxFrequency, char character, string password)
        {
            MinFrequency = minFrequency;
            MaxFrequency = maxFrequency;
            Character = character;
            Password = password;
        }

        public bool IsPasswordValid()
        {
            var charFrequency = Password.Count(x => x == Character);
            if (charFrequency >= MinFrequency && charFrequency <= MaxFrequency)
            {
                return true;
            }
            return false;
        }

        public bool IsPasswordValidAlternative()
        {
            var first = Password[MinFrequency - 1] == Character ? 1 : 0;
            var second = Password[MaxFrequency - 1] == Character ? 1 : 0;

            var ocurrences = first + second;

            if (ocurrences == 1)
            {
                return true;
            }

            return false;
        }
    }
}