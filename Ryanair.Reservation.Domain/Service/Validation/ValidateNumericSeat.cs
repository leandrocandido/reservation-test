using Ryanair.Reservation.Domain.Commands;
using Ryanair.Reservation.Domain.Interfaces;
using Ryanair.Reservation.Domain.Resources;
using Ryanair.Reservation.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ryanair.Reservation.Domain.Service.Validation
{
    public class ValidateNumericSeat : IRulesValidation
    {
        protected readonly ICreateReservationCommand _command;
        public IRulesValidation Next { get; set; }

        public ValidateNumericSeat(ICreateReservationCommand command)
        {
            _command = command;
        }

        /// <summary>
        /// Check if value in request is a numeric value
        /// </summary>
        /// <param name="messages">Messages.</param>
        public void Validate(List<DomainValidationMessage> messages)
        {
            var seats = _command.Flights.SelectMany(x => x.Passengers).Select(x => x.Seat).ToList();

            foreach (var item in seats)
            {
                if ( !int.TryParse(item, out int n))
                {                    
                    messages.Add(new DomainValidationMessage { Level = ValidationLevel.Error, Message = string.Format(Language.SeatNumberError, item), Property = nameof(item) });
                    continue;
                }
            }

            if (this.Next != null)
                this.Next.Validate(messages);
        }
    }
}
