using System.Collections.Generic;

namespace Ryanair.Reservation.Domain.Entities
{
    public class Booking
    {      
        public string Email { get; set; }
        public string CreditCard { get; set; }
        public List<BookFlight> Flights { get; set; }
        public string ReservationNumber { get; set; }
    }
}
