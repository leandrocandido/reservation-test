using Ryanair.Reservation.Domain.Entities;
using System.Collections.Generic;

namespace Ryanair.Reservation.Infrastructure.DataAccess
{
    public class BookingDatabase
    {
        private static BookingDatabase _uniqueInstance = null;

        public List<Booking> BookingInformation { get; set; }

        private BookingDatabase()
        {
        }

        public static BookingDatabase GetInstance()
        {
            if (_uniqueInstance == null)
            {
                _uniqueInstance = new BookingDatabase();
                _uniqueInstance.BookingInformation = new List<Booking>();
            }

            return _uniqueInstance;
        }
    }
}
