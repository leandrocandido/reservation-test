using System;

namespace Ryanair.Reservation.Domain.Entities
{
    public class Passengers
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Bags { get; set; }
        public string Seat { get; set; }
    }
}
