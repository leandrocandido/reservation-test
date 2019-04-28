using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Ryanair.Reservation.Domain.Entities;

namespace Ryanair.Reservation.Domain.Specifications.Flight
{
    public class FlightHasFreeSeatsSpec : SpecificationBase<Entities.Flight>
    {
        private readonly Expression<Func<Entities.Flight, bool>> _expression;
        public FlightHasFreeSeatsSpec(int numberOfSeats)
        {
            _expression = c => c.Passengers.Count <= (50 - numberOfSeats);
        }

        public override Expression<Func<Entities.Flight, bool>> ToExpression()
        {
            return _expression;
        }
    }
}
