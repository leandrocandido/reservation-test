using Ryanair.Reservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Ryanair.Reservation.Domain.Specifications.Flight
{
    public sealed class FlightDepartsFromSpec : SpecificationBase<Entities.Flight>
    {
        private string origin;
        public FlightDepartsFromSpec(string origin)
        {
            this.origin = origin;
        }

        public override Expression<Func<Entities.Flight, bool>> ToExpression()
        {
            return flightDepartsFrom => flightDepartsFrom.Origin.ToLowerInvariant() == origin.ToLowerInvariant();
        }
    }
}
