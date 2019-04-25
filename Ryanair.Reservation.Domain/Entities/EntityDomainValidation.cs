using Ryanair.Reservation.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ryanair.Reservation.Domain.Entities
{
    public abstract class EntityDomainValidation
    {        
        protected abstract void DomainValidation(List<DomainValidationMessage> messages);

        protected abstract void AfterValidation();

        protected List<DomainValidationMessage> Validate()
        {
            List<DomainValidationMessage> messages = new List<DomainValidationMessage>();
            this.DomainValidation(messages);
            return messages;
        }

        protected void ProcessDomainEntity()
        {
            var msgs = this.Validate();

            if (msgs.Count > 0)
                throw new DomainValidationException(msgs);

            this.AfterValidation();
        }
    }
}
