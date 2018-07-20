using System;
using System.Collections.Generic;

namespace Ryanair.Reservation.Bonus
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("> Review the class FlightList below. What's wrong and what could be fixed?");
            Console.WriteLine("> Do any changes you consider that should be made and add, if you have some, comments to README_Candidate.md file.");
            Console.ReadKey();
        }
    }

    public class FlightList
    {
        private List<string> _Destinations;
        private List<string> _DepartureTimes;

        public void Add(string destination, string departure)
        {
            _Destinations.Add(destination);
            _DepartureTimes.Add(departure);
        }

        public void Remove(string destination)
        {
            int i = _Destinations.IndexOf(destination);
            _DepartureTimes.RemoveAt(i);
            _Destinations.RemoveAt(i);
        }
    }
}
