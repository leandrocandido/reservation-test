using System;
using System.Collections.Generic;

namespace Ryanair.Reservation.Domain.Validation
{
    public class DomainValidationException : Exception
    {
        public DomainValidationException(IEnumerable<DomainValidationMessage> messages) : base()
        {
            this.ValidationError = messages;
        }

        public IEnumerable<DomainValidationMessage> ValidationError { get; }
    }
}
