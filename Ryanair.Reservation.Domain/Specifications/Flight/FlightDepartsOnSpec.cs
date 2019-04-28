using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Ryanair.Reservation.Domain.Entities;

namespace Ryanair.Reservation.Domain.Specifications.Flight
{
    public class FlightDepartsOnSpec : SpecificationBase<Entities.Flight>
    {
        private readonly Expression<Func<Entities.Flight, bool>> _expression;
        public FlightDepartsOnSpec(DateTime date)
        {
            _expression = c => c.Time.Date == date.Date;
        }

        public override Expression<Func<Entities.Flight, bool>> ToExpression()
        {
            return _expression;
        }
    }
}
