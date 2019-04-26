using System.Collections.Generic;
using Ryanair.Reservation.Domain.Commands;
using Ryanair.Reservation.Domain.Interfaces;
using Ryanair.Reservation.Domain.Resources;
using Ryanair.Reservation.Domain.Validation;

namespace Ryanair.Reservation.Domain.Service.Validation
{
    public class ValidateBookingFields : IRulesValidation
    {
        protected readonly ICreateReservationCommand _command;

        public ValidateBookingFields( ICreateReservationCommand command)
        {
            _command = command;
        }

        public IRulesValidation Next { get; set; }

        /// <summary>
        /// Check if booking fields is valid
        /// </summary>
        /// <param name="messages">Messages.</param>
        public void Validate(List<DomainValidationMessage> messages)
        {
            if (string.IsNullOrEmpty(this._command.Email))
                messages.Add(new DomainValidationMessage { Level = ValidationLevel.Error, Message = Language.EmailNullEmpty, Property = nameof(this._command.Email) });

            if (string.IsNullOrEmpty(this._command.CreditCard))
                messages.Add(new DomainValidationMessage { Level = ValidationLevel.Error, Message = Language.CreditCardNullEmprty, Property = nameof(this._command.CreditCard) });

            if (this.Next != null)
                this.Next.Validate(messages);
        }
    }
}
