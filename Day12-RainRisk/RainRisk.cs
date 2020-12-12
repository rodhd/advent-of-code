using System;
using System.Threading;

namespace Day12_RainRisk
{
    public class RainRisk
    {
        public int Latitude { get; set; }
        public int Longitude { get; set; }
        
        public int Direction { get; set; }
        
        public int WaypointLongitude { get; set; }
        
        public int WaypointLatitude { get; set; }
        public string[] Instructions { get; set; }

        public RainRisk(string[] instructions)
        {
            Instructions = instructions;
            Latitude = 0;
            Longitude = 0;
            Direction = 0;
            WaypointLatitude = 1;
            WaypointLongitude = 10;
        }

        private void Move(string instruction)
        {
            if (instruction.StartsWith("N"))
            {
                Latitude += int.Parse(instruction.Replace("N", ""));
            }
            if (instruction.StartsWith("S"))
            {
                Latitude += -1 * int.Parse(instruction.Replace("S", ""));
            }
            if (instruction.StartsWith("E"))
            {
                Longitude += int.Parse(instruction.Replace("E", ""));
            }
            if (instruction.StartsWith("W"))
            {
                Longitude += -1 * int.Parse(instruction.Replace("W", ""));
            }
            if (instruction.StartsWith("R"))
            {
                Direction += -1 * int.Parse(instruction.Replace("R", ""));
            }
            if (instruction.StartsWith("L"))
            {
                Direction += int.Parse(instruction.Replace("L", ""));
            }
            if (instruction.StartsWith("F"))
            {
                int dist = int.Parse(instruction.Replace("F", ""));
                Longitude += dist * Convert.ToInt32(Math.Cos(Math.PI * Direction / 180));
                Latitude += dist * Convert.ToInt32(Math.Sin(Math.PI * Direction / 180));
            }
        }

        public double FirstAnswer()
        {
            foreach (var i in Instructions)
            {
                Move(i);
            }

            return Math.Abs(Latitude) + Math.Abs(Longitude);
        }

        public void MoveWaypoint(string instruction)
        {
            if (instruction.StartsWith("N"))
            {
                WaypointLatitude += int.Parse(instruction.Replace("N", ""));
            }
            if (instruction.StartsWith("S"))
            {
                WaypointLatitude += -1 * int.Parse(instruction.Replace("S", ""));
            }
            if (instruction.StartsWith("E"))
            {
                WaypointLongitude += int.Parse(instruction.Replace("E", ""));
            }
            if (instruction.StartsWith("W"))
            {
                WaypointLongitude += -1 * int.Parse(instruction.Replace("W", ""));
            }
            if (instruction.StartsWith("R"))
            {
                int degrees = int.Parse(instruction.Replace("R", ""));
                Rotate(-1, degrees);
                
            }
            if (instruction.StartsWith("L"))
            {
                int degrees = int.Parse(instruction.Replace("L", ""));
                Rotate(1, degrees);
            }
            if (instruction.StartsWith("F"))
            {
                int dist = int.Parse(instruction.Replace("F", ""));
                Longitude += dist * WaypointLongitude;
                Latitude += dist * WaypointLatitude;
            } 
        }

        private void Rotate(int dir, int degrees)
        {
            int temp = 0;
            switch (degrees)
            {
                case 90:
                    temp = WaypointLongitude;
                    WaypointLongitude = -1 * WaypointLatitude * dir;
                    WaypointLatitude = temp * dir;
                    break;
                case 180:
                    WaypointLatitude *= -1;
                    WaypointLongitude *= -1;
                    break;
                case 270:
                    temp = WaypointLongitude;
                    WaypointLongitude = WaypointLatitude * dir;
                    WaypointLatitude = temp * dir * -1;
                    break;
            }
        }
        
        public double SecondAnswer()
        {
            foreach (var i in Instructions)
            {
                MoveWaypoint(i);
            }

            return Math.Abs(Latitude) + Math.Abs(Longitude);
        }
        
    }
}