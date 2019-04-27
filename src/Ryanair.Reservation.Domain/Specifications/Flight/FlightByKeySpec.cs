using System;
using System.Linq.Expressions;

namespace Ryanair.Reservation.Domain.Specifications.Flight
{
    public sealed class FlightByKeySpec : SpecificationBase<Entities.FlightAggregate.Flight>
    {
        private readonly Expression<Func<Entities.FlightAggregate.Flight, bool>> _expression;
        public FlightByKeySpec(string key)
        {
            _expression = flightByKey => flightByKey.Key == key;
        }

        public override Expression<Func<Entities.FlightAggregate.Flight, bool>> ToExpression()
        {
            return _expression;
        }
    }
}
