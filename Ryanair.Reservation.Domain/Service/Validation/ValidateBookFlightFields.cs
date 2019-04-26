using Ryanair.Reservation.Domain.Commands;
using Ryanair.Reservation.Domain.DataAccess.Repositories;
using Ryanair.Reservation.Domain.Interfaces;
using Ryanair.Reservation.Domain.Resources;
using Ryanair.Reservation.Domain.Validation;
using System.Collections.Generic;
using System.Linq;

namespace Ryanair.Reservation.Domain.Service.Validation
{
    class ValidateBookFlightFields : IRulesValidation
    {
        private readonly IFlightRepository _flightRepository;
        protected readonly ICreateReservationCommand _command;

        public IRulesValidation Next { get; set; }

        public ValidateBookFlightFields(ICreateReservationCommand command, IFlightRepository flightRepository)
        {
            _command = command;
            _flightRepository = flightRepository;
        }

        /// <summary>
        /// Check if flight information is consistent
        /// </summary>
        /// <param name="messages">Messages.</param>
        public void Validate(List<DomainValidationMessage> messages)
        {

            foreach (var item in _command?.Flights)
            {
                if (string.IsNullOrEmpty(item.Key))
                    messages.Add(new DomainValidationMessage { Level = ValidationLevel.Error, Message = string.Format(Language.FlightNullEmpty, item.Key), Property = nameof(item.Key) });

                if (!_flightRepository.FlightExists(item.Key) && !string.IsNullOrEmpty(item.Key))
                    messages.Add(new DomainValidationMessage { Level = ValidationLevel.Error, Message = string.Format(Language.FlightNotExists, item.Key), Property = nameof(item.Key) });
            }
            //go to next validation
            if (this.Next != null)
                this.Next.Validate(messages);
        }

    }
}
