using System;
using System.Linq;

namespace Day4_PassportProcessing
{
    public class Passport
    {
        public string? Byr { get; set; }

        public string? Iyr { get; set; }
        
        public string? Eyr { get; set; }
        
        public string? Hgt { get; set; }
        
        public string? Hcl { get; set; }

        public string? Ecl { get; set; }

        public string? Pid { get; set; }

        public string? Cid { get; set; }

        public bool IsValid()
        {
            if (Byr == null || Iyr == null || Eyr == null || Hgt == null || Ecl == null || Pid == null || Hcl == null)
            {
                return false;
            }

            return true;
        }

        public bool IsExtendedValid()
        {
            if(IsValid())
            {
                bool[] features = new[]
                {
                    isHeightValid(Hgt),
                    isYearValid(Byr, 1920, 2002),
                    isYearValid(Iyr, 2010, 2020),
                    isYearValid(Eyr, 2020, 2030),
                    isEyeColorValid(Ecl),
                    isHairColorValid(Hcl),
                    isPassportIdValid(Pid)
                };
                bool valid = features.All(x => x);
                return valid;
            }
            return false;
        }

        private bool isYearValid(string year, int start, int end)
        {
            if (int.Parse(year) >= start && int.Parse(year) <= end)
            {
                return true;
            }

            return false;
        }

        private bool isHeightValid(string height)
        {
            if (height.EndsWith("cm"))
            {
                var h = int.Parse(height.Replace("cm", ""));
                return h >= 150 && h <= 193;
            }

            if (height.EndsWith("in"))
            {
                var h = int.Parse(height.Replace("in", ""));
                return h >= 59 && h <= 76;
            }

            return false;
        }

        private bool isHairColorValid(string color)
        {
            char[] validChars = new[] {'a', 'b', 'c', 'd', 'e', 'f', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '#'};

            if (color.StartsWith('#') && color.Length == 7)
            {
                return color.All(x => validChars.Contains(x));
            }

            return false;

        }

        private bool isEyeColorValid(string color)
        {
            string[] validColors = new[] {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};
            return validColors.Contains(color);
        }

        private bool isPassportIdValid(string id)
        {
            if (id.Length == 9 && int.TryParse(id, out int idNumber))
            {
                return true;
            }

            return false;
        }
    }
}