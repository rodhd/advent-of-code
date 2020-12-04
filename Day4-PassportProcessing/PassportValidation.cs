using System;
using System.Collections.Generic;
using System.Linq;

namespace Day4_PassportProcessing
{
    public static class PassportValidation
    {
        public static int Validate(string[] input)
        {
            var passports = ParseMany(input);
            var validPassports = passports.Count(x => x.IsValid());
            return validPassports;
        }
        
        public static int ExtraValidate(string[] input)
        {
            var passports = ParseMany(input);
            var validPassports = passports.Count(x => x.IsExtendedValid());
            return validPassports;
        }

        public static List<Passport> ParseMany(string[] data)
        {
            List<Passport> result = new List<Passport>();
            List<string> temp = new List<string>();
            foreach (var l in data)
            {
                if (!String.IsNullOrWhiteSpace(l))
                {
                    string[] split = l.Split(" ");
                    temp.AddRange(split);
                }
                else
                {
                    result.Add(Parse(temp));
                    temp.Clear();
                }
            }

            if (temp.Any())
            {
                result.Add(Parse(temp));
            }

            return result;
        }

        public static Passport Parse(List<string> data)
        {
            return new Passport()
            {
                Byr = GetProperty(data, "byr"),
                Iyr = GetProperty(data, "iyr"),
                Cid = GetProperty(data, "cid"),
                Ecl = GetProperty(data, "ecl"),
                Eyr = GetProperty(data, "eyr"),
                Hgt = GetProperty(data, "hgt"),
                Pid = GetProperty(data, "pid"),
                Hcl = GetProperty(data, "hcl")
            };
        }

        public static string? GetProperty(List<string> text, string prop)
        {
            if (string.IsNullOrWhiteSpace(text.FirstOrDefault(x => x.StartsWith(prop))))
            {
                return null;
            }

            return text.FirstOrDefault(x => x.StartsWith(prop)).Split(":")[1];
        }
    }
}