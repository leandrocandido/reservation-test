using Ryanair.Reservation.Domain.Commands;
using Ryanair.Reservation.Domain.Interfaces;
using Ryanair.Reservation.Domain.Resources;
using Ryanair.Reservation.Domain.Validation;
using System.Collections.Generic;
using System.Linq;

namespace Ryanair.Reservation.Domain.Service.Validation
{
    class ValidateTicketInformation : IRulesValidation
    {
        protected readonly ICreateReservationCommand _command;

        public IRulesValidation Next { get; set; }

        public ValidateTicketInformation(ICreateReservationCommand command)
        {
            _command = command;
        }

        /// <summary>
        /// check if request has a valid ticket information
        /// </summary>
        /// <param name="messages">Messages.</param>
        public void Validate(List<DomainValidationMessage> messages)
        {
            if (_command?.Flights != null && _command?.Flights?.Any() == false)
            {
                messages.Add(new DomainValidationMessage { Level = ValidationLevel.Error, Message = Language.NoFlightInformation, Property = "Flight" });
            }
            else if (_command?.Flights.Count > 2)
            {
                messages.Add(new DomainValidationMessage { Level = ValidationLevel.Error, Message = Language.MoreThenTwoFlights, Property = "Flight" });
            }

            if (this.Next != null)
                this.Next.Validate(messages);
        }
    }
}
