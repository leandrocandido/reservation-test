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

        public void Validate(List<DomainValidationMessage> messages)
        {
            var seats = _command.Flights
                .SelectMany(x => x.Passengers)
                .Select(x => x.Seat)
                .ToList().Select(int.Parse)
                .ToList();

            foreach (var item in seats)
            {
                if (!Enumerable.Range(1, 50).Contains(item))
                {
                    messages.Add(new DomainValidationMessage { Level = ValidationLevel.Error, Message = string.Format(Language.SeatNumberRange, item), Property = nameof(item) });
                    continue;
                }
            }

            if (this.Next != null)
                this.Next.Validate(messages);
        }
    }
}
