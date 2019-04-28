using Ryanair.Reservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Ryanair.Reservation.Domain.Specifications
{
    public sealed class FlightByKeySpec : SpecificationBase<Entities.Flight>
    {
        private readonly Expression<Func<Entities.Flight, bool>> _expression;
        public FlightByKeySpec(string key)
        {
            _expression = flightByKey => flightByKey.Key == key;
        }

        public override Expression<Func<Entities.Flight, bool>> ToExpression()
        {
            return _expression;
        }
    }
}
