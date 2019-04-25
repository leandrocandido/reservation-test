using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Ryanair.Reservation.Domain.DTO
{
    [Serializable]
    public class BookFlightDto
    {
        [XmlElement]
        public string Key { get; set; }
        [XmlElement]
        public List<PassengersDto> Passengers { get; set; }
    }
}
