using Ryanair.Reservation.Domain.DataAccess.Repositories;
using Ryanair.Reservation.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Ryanair.Reservation.Infrastructure.DataAccess.Repositories
{
    public class FlightRepository : RepositoryBase<Flight>, IFlightRepository
    {

        public bool FlightExists(string flight)
        {
            var database = FlightDatabase.GetInstance();
            var res = database.FlightInformation.Any(x => x.Key == flight);
            return res;
        }

        public override IEnumerable<Flight> GetAll()
        {
            var database = FlightDatabase.GetInstance();
            return database.FlightInformation;
        }    
    }
}
