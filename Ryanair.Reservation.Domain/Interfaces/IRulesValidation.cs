using Ryanair.Reservation.Domain.Validation;
using System.Collections.Generic;

namespace Ryanair.Reservation.Domain.Interfaces
{
    public interface IRulesValidation
    {
        void Validate(List<DomainValidationMessage> messages);
        IRulesValidation Next { get; set; }
    }
}
