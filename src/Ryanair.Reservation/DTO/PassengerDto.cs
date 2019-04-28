using System;
using System.Xml.Serialization;

namespace Ryanair.Reservation.Domain.DTO
{
    [Serializable]
    public class PassengerDto
    {
        [XmlElement]
        public string Name { get; set; }
        [XmlElement]
        public int Bags { get; set; }
        [XmlElement]
        public string Seat { get; set; }
    }
}
