using System;
using System.Collections.Generic;
using System.Text;

namespace Ryanair.Reservation.Domain.ValueObjects
{
    public class FlightData
    {
        public string Key { get; set; }

        public IEnumerable<PassengerData> Passengers { get; set; }
    }
}
