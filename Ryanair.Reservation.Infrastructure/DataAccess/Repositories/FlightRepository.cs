using Ryanair.Reservation.Domain.DataAccess.Repositories;
using Ryanair.Reservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ryanair.Reservation.Infrastructure.DataAccess.Repositories
{
    public class FlightRepository : RepositoryBase<Flight>, IFlightRepository
    {
        public override IEnumerable<Flight> GetAll()
        {
            var database = FlightDatabase.GetInstance();
            return database.FlightInformation;
        }
    }
}
