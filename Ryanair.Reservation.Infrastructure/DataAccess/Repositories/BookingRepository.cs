using Ryanair.Reservation.Domain.DataAccess.Repositories;
using Ryanair.Reservation.Domain.Entities;
using System.Collections.Generic;

namespace Ryanair.Reservation.Infrastructure.DataAccess.Repositories
{
    public class BookingRepository : RepositoryBase<Booking>, IBookingRepository
    {
        public override IEnumerable<Booking> GetAll()
        {
            var database = BookingDatabase.GetInstance();
            return database.BookingInformation;
        }
    }
}
