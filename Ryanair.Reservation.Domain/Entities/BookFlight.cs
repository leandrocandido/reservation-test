using System.Collections.Generic;

namespace Ryanair.Reservation.Domain.Entities
{
    public class BookFlight
    {

        public string Key { get; set; }
        public List<Passengers> Passengers { get; set; }
    }
}
