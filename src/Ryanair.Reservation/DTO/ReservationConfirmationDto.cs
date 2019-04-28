using System;
using System.Xml.Serialization;

namespace Ryanair.Reservation.DTO
{
    [Serializable]
    public class ReservationConfirmationDto
    {
        [XmlElement]
        public string ReservationNumber { get; set; }
    }
}
