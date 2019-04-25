using Ryanair.Reservation.Domain.Commands;
using Ryanair.Reservation.Domain.Interfaces;
using Ryanair.Reservation.Domain.Resources;
using Ryanair.Reservation.Domain.Validation;
using System.Collections.Generic;
using System.Linq;

namespace Ryanair.Reservation.Domain.Service.Validation
{
    public class ValidatePassengersFields : IRulesValidation
    {
        protected readonly ICreateReservationCommand _command;
        public IRulesValidation Next { get; set; }

        public ValidatePassengersFields(ICreateReservationCommand command)
        {
            _command = command;
        }

        public void Validate(List<DomainValidationMessage> messages)
        {
            var passengers = _command.Flights.SelectMany(x => x.Passengers).ToList();            

            foreach (var item in passengers)
            {
                if (string.IsNullOrEmpty(item.Name))
                    messages.Add(new DomainValidationMessage { Level = ValidationLevel.Error, Message = Language.PassengerNameMandatory, Property = nameof(item.Name) });

                if (string.IsNullOrEmpty(item.Seat))
                    messages.Add(new DomainValidationMessage { Level = ValidationLevel.Error, Message = Language.SeatNumberMandatory , Property = nameof(item.Seat) });

                if (item.Bags > 5)
                    messages.Add(new DomainValidationMessage { Level = ValidationLevel.Error, Message = string.Format(Language.MaxBagsPerUser, item.Name), Property = nameof(item.Bags) });
            }

            if (this.Next != null)
                this.Next.Validate(messages);
        }
    }
}
