using Ryanair.Reservation.Domain.Commands;
using Ryanair.Reservation.Domain.Interfaces;
using Ryanair.Reservation.Domain.Resources;
using Ryanair.Reservation.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ryanair.Reservation.Domain.Service.Rules
{
    public class ValidateSeatRange : IRulesValidation
    {
        protected readonly ICreateReservationCommand _command;

        public IRulesValidation Next { get; set; }

        public ValidateSeatRange(ICreateReservationCommand command)
        {
            _command = command;
        }

        /// <summary>
        /// Check if seats informed in request are in the range (1-50)
        /// </summary>
        /// <param name="messages">Messages.</param>
        public void Validate(List<DomainValidationMessage> messages)
        {
            //creates a list of seats in request
            var seats = _command.Flights
                .SelectMany(x => x.Passengers)
                .Select(x => x.Seat)
                .ToList().Select(int.Parse)
                .ToList();

            //seat verification
            foreach (var item in seats)
            {
                if (!Enumerable.Range(1, 50).Contains(item))
                {
                    messages.Add(new DomainValidationMessage { Level = ValidationLevel.Error, Message = string.Format(Language.SeatNumberRange, item), Property = nameof(item) });
                    continue;
                }
            }
            //got to the next validation
            if (this.Next != null)
                this.Next.Validate(messages);
        }
    }
}
