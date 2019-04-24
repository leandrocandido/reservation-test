using System.Xml.Serialization;

namespace Ryanair.Reservation.Domain.Validation
{
    public class DomainValidationMessage
    {
        [XmlElement]
        public ValidationLevel Level { get; set; }
        [XmlElement]
        public string Property { get; set; }
        [XmlElement]
        public string Message { get; set; }      
    }
}
