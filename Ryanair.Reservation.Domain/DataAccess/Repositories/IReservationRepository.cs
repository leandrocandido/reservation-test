using Ryanair.Reservation.Domain.Entities;
using System.Collections.Generic;

namespace Ryanair.Reservation.Domain.DataAccess.Repositories
{
    public interface IReservationRepository : IRepository<ReservationEntity>
    {
        int GetBagsPerFlight(string flightkey);
        List<string> GetReservedSeatsPerFlight(string flightkey);
        bool ReservationNumberExists(string reservationNumber);
        void Save(ReservationEntity entity);
        void Save(List<ReservationEntity> entities);
    }
}
