using Ryanair.Reservation.Domain.Interfaces;
using Ryanair.Reservation.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Ryanair.Reservation.Domain.Responses
{        
    public class SingleHandleResponse : IHandleResponse
    {
        [XmlElement]
        public string Error { get; set; }
        [XmlElement]
        public List<DomainValidationMessage> DomainValidationMessages { get; set; }

        public virtual bool HasContent()
        {
            throw new NotImplementedException();
        }
    }
}
