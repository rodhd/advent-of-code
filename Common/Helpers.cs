using System;
using System.Linq;

namespace Common
{
    public static class Helpers
    {
        public static string[] ReadInput(string path)
        {
            string[] lines = System.IO.File.ReadAllLines( path);
            return lines;
        }
    }
}