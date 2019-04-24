using Ryanair.Reservation.Application.DTO;
using Ryanair.Reservation.Domain.Validation;
using System.Collections.Generic;

namespace Ryanair.Reservation.Application.Mediator
{
    public class Response
    {
        public object Content { get; set; }
        public string Error { get; set; }
        public IEnumerable<DomainValidationMessage> DomainValidationMessages { get; set; }
    }
}
