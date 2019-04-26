using System;
using System.Collections.Generic;
using Ryanair.Reservation.Domain.Entities.FlightAggregate;

namespace Ryanair.Reservation.Domain.Entities.ReservationAggregate
{
    public sealed class ReservationFlight
    {
        public ReservationFlight(Flight flight, IEnumerable<Passenger> passengers)
        {
            this.Flight = flight;
            this._passengers.AddRange(passengers);
        }

        public Flight Flight { get; private set; }

        // Using IReadOnlyCollection as a wrapper around a private list,
        // the only way to add passenger is through AddPassenger method, so is protected against "external updates".
        private readonly List<Passenger> _passengers = new List<Passenger>();
        public IReadOnlyCollection<Passenger> Passengers => _passengers.AsReadOnly();
    }
}
