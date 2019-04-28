using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Ryanair.Reservation.DTO
{
    [Serializable]
    public class ReservationDto
    {
        [XmlElement]
        public string Email { get; set; }
        [XmlElement]
        public string ReservationNumber { get; set; }
        [XmlElement]
        public List<ReservationFlightDto> Flights { get; set; }
    }
}
