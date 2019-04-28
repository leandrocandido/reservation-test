using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Ryanair.Reservation.Domain.Entities;

namespace Ryanair.Reservation.Domain.Specifications.Flight
{
    public class FlightFlyingToSpec : SpecificationBase<Entities.Flight>
    {
        private readonly Expression<Func<Entities.Flight, bool>> _expression;
        public FlightFlyingToSpec(string destination)
        {
            _expression = c => c.Destination == destination;
        }

        public override Expression<Func<Entities.Flight, bool>> ToExpression()
        {
            return _expression;
        }
    }
}
