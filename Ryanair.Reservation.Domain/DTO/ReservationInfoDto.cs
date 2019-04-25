using System;
using System.Xml.Serialization;

namespace Ryanair.Reservation.Domain.DTO
{
    [Serializable]
    public class ReservationInfoDto
    {
        [XmlElement]
        public string ReservationNumber { get; set; }
    }
}
