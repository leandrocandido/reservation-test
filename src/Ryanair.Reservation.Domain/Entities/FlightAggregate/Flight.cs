using System;
using System.Collections.Generic;
using Ryanair.Reservation.Domain.Interfaces;
using Ryanair.Reservation.Domain.Validation;

namespace Ryanair.Reservation.Domain.Entities.FlightAggregate
{
    public class Flight : IAggregateRoot
    {
        public DateTime Time { get; set; }
        public string Key { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }

    }
}
