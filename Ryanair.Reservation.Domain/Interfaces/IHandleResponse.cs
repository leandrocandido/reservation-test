using Ryanair.Reservation.Domain.Validation;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Ryanair.Reservation.Domain.Interfaces
{
    public interface IHandleResponse
    {
        [XmlElement]
        string Error { get; set; }
        [XmlElement]
        List<DomainValidationMessage> DomainValidationMessages { get; set; }        
    }
}
