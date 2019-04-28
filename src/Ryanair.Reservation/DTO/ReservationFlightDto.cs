using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Ryanair.Reservation.DTO
{
    [Serializable]
    public class ReservationFlightDto
    {
        [XmlElement]
        public string Key { get; set; }
        [XmlElement]
        public List<PassengerDto> Passengers { get; set; }
    }
}
