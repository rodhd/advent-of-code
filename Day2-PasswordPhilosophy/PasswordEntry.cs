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
    }
}