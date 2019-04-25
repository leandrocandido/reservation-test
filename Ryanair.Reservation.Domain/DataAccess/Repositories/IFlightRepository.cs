using Ryanair.Reservation.Domain.Entities;

namespace Ryanair.Reservation.Domain.DataAccess.Repositories
{
    public interface IFlightRepository : IRepository<Flight>
    {
        bool FlightExists(string flight);
    }
}
