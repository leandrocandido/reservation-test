using System;
using System.Collections.Generic;
using System.Text;

namespace Ryanair.Reservation.Domain.ValueObjects
{
    public class ReservationData
    {
        public string Email { get; set; }
        public string CreditCard { get; set; }
        public List<FlightData> Flights { get; set; }
    }
}
