using System;
using System.Xml.Serialization;

namespace Ryanair.Reservation.Domain.DTO
{
    [Serializable]
    public class ReservationConfirmationDto
    {
        [XmlElement]
        public string ReservationNumber { get; set; }
    }
}
