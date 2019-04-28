using Ryanair.Reservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Ryanair.Reservation.Domain.Specifications.Flight
{
    public sealed class FlightDepartsFromSpec : SpecificationBase<Entities.Flight>
    {
        private readonly Expression<Func<Entities.Flight, bool>> _expression;
        public FlightDepartsFromSpec(string origin)
        {
            _expression = c => c.Origin == origin;
        }

        public override Expression<Func<Entities.Flight, bool>> ToExpression()
        {
            return _expression;
        }
    }
}
