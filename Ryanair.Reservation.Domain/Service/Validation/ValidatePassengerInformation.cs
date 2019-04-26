using Ryanair.Reservation.Domain.Commands;
using Ryanair.Reservation.Domain.Interfaces;
using Ryanair.Reservation.Domain.Resources;
using Ryanair.Reservation.Domain.Validation;
using System.Collections.Generic;
using System.Linq;

namespace Ryanair.Reservation.Domain.Service.Validation
{
    class ValidatePassengerInformation : IRulesValidation
    {
        protected readonly ICreateReservationCommand _command;
        public IRulesValidation Next { get; set; }

        public ValidatePassengerInformation(ICreateReservationCommand command)
        {
            _command = command;
        }

        /// <summary>
        /// Check if request has passenger information
        /// </summary>
        /// <param name="messages">Messages.</param>
        public void Validate(List<DomainValidationMessage> messages)
        {
            if (_command.Flights.SelectMany(x => x.Passengers).Count() == 0)
                messages.Add(new DomainValidationMessage { Level = ValidationLevel.Error, Message = Language.PaasengerInformationMissing, Property = "Passenger" });

            if (this.Next != null)
                this.Next.Validate(messages);

        }
    }
}
