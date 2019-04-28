using System;
using System.Xml.Serialization;

namespace Ryanair.Reservation.DTO
{
    [Serializable]
    public class FlightDto
    {
        [XmlElement]
        public string Destination { get; set; }
        [XmlElement]
        public string Key { get; set; }
        [XmlElement]
        public string Origin { get; set; }
        [XmlElement(DataType = "date")]
        public DateTime Time { get; set; }
    }
}
