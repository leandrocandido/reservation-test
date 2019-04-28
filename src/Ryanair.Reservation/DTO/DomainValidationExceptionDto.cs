using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Ryanair.Reservation.Domain.Validation;

namespace Ryanair.Reservation.DTO
{
    [Serializable]
    public class DomainValidationExceptionDto : ExceptionDto
    {
        [XmlElement]
        public List<DomainValidationMessage> ValidationMessages { get; set; }
    }
}
