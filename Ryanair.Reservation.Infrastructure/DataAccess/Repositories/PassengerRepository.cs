using Ryanair.Reservation.Domain.DataAccess.Repositories;
using Ryanair.Reservation.Domain.Entities;
using System.Collections.Generic;

namespace Ryanair.Reservation.Infrastructure.DataAccess.Repositories
{
    public class PassengerRepository : RepositoryBase<Passengers>, IPassengerRepository
    {
        public override IEnumerable<Passengers> GetAll()
        {
            var database = PassengersDatabase.GetInstance();
            return database.PassengersInformation;
        }
    }
}
