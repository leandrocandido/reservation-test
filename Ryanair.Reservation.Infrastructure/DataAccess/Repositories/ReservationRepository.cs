using Ryanair.Reservation.Domain.DataAccess.Repositories;
using Ryanair.Reservation.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Ryanair.Reservation.Infrastructure.DataAccess.Repositories
{
    public class ReservationRepository : RepositoryBase<ReservationEntity>, IReservationRepository
    {
        public override IEnumerable<ReservationEntity> GetAll()
        {
            var reservation = ReservationDataTable.GetInstance();
            return reservation.ReservationInformation;
        }

        public int GetBagsPerFlight(string flightkey)
        {
            var reservation = ReservationDataTable.GetInstance();
            return reservation.ReservationInformation
                .Where(x => x.Key == flightkey)
                .Sum(x => x.Bags);
        }

        public List<string> GetReservedSeatsPerFlight(string flightkey)
        {
            var reservation = ReservationDataTable.GetInstance();
            return reservation.ReservationInformation
                .Where(x => x.Key == flightkey)
                .Select(x => x.Seat).ToList();
        }

        public bool ReservationNumberExists(string reservationNumber)
        {
            var reservation = ReservationDataTable.GetInstance();
            return reservation.ReservationInformation.Where(x => x.ReservationNumber == reservationNumber).Any();
        }

        public void Save(ReservationEntity entity)
        {
            var reservation = ReservationDataTable.GetInstance();
            reservation.ReservationInformation.Add(entity);
        }

        public void Save(List<ReservationEntity> entities)
        {
            var reservation = ReservationDataTable.GetInstance();
            reservation.ReservationInformation.AddRange(entities);
        }

    }
}
