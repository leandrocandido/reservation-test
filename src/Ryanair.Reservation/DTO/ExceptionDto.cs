using System;
using System.Xml.Serialization;

namespace Ryanair.Reservation.DTO
{
    [Serializable]
    public class ExceptionDto
    {
        [XmlElement]
        public string Message { get; set; }
        [XmlElement]
        public string StackTrace { get; set; }
    }
}
