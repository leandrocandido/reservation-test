using System;

namespace Ryanair.Reservation.Domain.Entities
{
    public class Flight
    {
        public DateTime Time { get; set; }
        public string Key { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
    }
}
