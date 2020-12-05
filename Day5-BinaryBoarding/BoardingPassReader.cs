using System;
using System.Linq;

namespace Day5_BinaryBoarding
{
    public static class BoardingPassReader
    {
        public static int GetMaxId(string[] input)
        {
            var seats = input.Select(x => new Seat(x)).ToList();
            var maxId = seats.Select(x => x.Id).Max();
            return maxId;
        }

        public static int GetFreeSeat(string[] input)
        {
            var seats = input.Select(x => new Seat(x)).ToList();
            var occupiedIds = seats.Select(x => x.Id).ToList();
            var possibleIds = Enumerable.Range(occupiedIds.Min(), occupiedIds.Max());
            var freeIds = possibleIds.Except(occupiedIds);

            foreach (var id in freeIds)
            {
                if (occupiedIds.Contains(id - 1) && occupiedIds.Contains(id + 1))
                {
                    return id;
                }
            }
            return 0;
        }
    }
}