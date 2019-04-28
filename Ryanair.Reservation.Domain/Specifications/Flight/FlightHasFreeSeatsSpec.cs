using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Ryanair.Reservation.Domain.Entities;

namespace Ryanair.Reservation.Domain.Specifications.Flight
{
    public class FlightHasFreeSeatsSpec : SpecificationBase<Entities.Flight>
    {
        private int _numberOfSeats;
        public FlightHasFreeSeatsSpec(int numberOfSeats)
        {
            _numberOfSeats = numberOfSeats;
        }

        public override Expression<Func<Entities.Flight, bool>> ToExpression()
        {
            return (flightHasFreeSeats => flightHasFreeSeats.Passengers.Count <= (50 - _numberOfSeats));
        }
    }
}
