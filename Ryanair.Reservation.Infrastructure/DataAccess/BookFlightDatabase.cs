using Ryanair.Reservation.Domain.Entities;
using System.Collections.Generic;

namespace Ryanair.Reservation.Infrastructure.DataAccess
{
    public class BookFlightDatabase
    {
        private static BookFlightDatabase _uniqueInstance = null;

        public List<BookFlight> BookFlightInformation { get; set; }

        private BookFlightDatabase()
        {
        }

        public static BookFlightDatabase GetInstance()
        {
            if (_uniqueInstance == null)
            {
                _uniqueInstance = new BookFlightDatabase();
                _uniqueInstance.BookFlightInformation = new List<BookFlight>();
            }

            return _uniqueInstance;
        }
    }
}
