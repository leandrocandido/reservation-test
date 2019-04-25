using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Ryanair.Reservation.Domain.DTO
{
    [Serializable]
    public class BookingDto
    {
        [XmlElement]
        public string Email { get; set; }
        [XmlElement]
        public string ReservationNumber { get; set; }
        [XmlElement]
        public List<BookFlightDto> Flights { get; set; }
    }
}
