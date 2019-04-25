using Ryanair.Reservation.Domain.Commands;
using Ryanair.Reservation.Domain.DataAccess.Repositories;
using Ryanair.Reservation.Domain.Interfaces;
using Ryanair.Reservation.Domain.Resources;
using Ryanair.Reservation.Domain.Validation;
using System.Collections.Generic;
using System.Linq;

namespace Ryanair.Reservation.Domain.Service.Rules
{
    public class ValidateBagsPerFlight : IRulesValidation
    {
        private readonly IReservationRepository _reservationRepository;
        protected readonly ICreateReservationCommand _command;

        public ValidateBagsPerFlight(IReservationRepository reservationRepository , ICreateReservationCommand command)
        {
            this._command = command;
            this._reservationRepository = reservationRepository;
        }
        public IRulesValidation Next { get; set; }

        public void Validate(List<DomainValidationMessage> messages)
        {
            foreach (var item in _command?.Flights)
            {
                int requestedBags = item.Passengers.Sum(x => x.Bags);
                int bags = _reservationRepository.GetBagsPerFlight(item.Key);
                int total = requestedBags + bags;
                if (total > 50)
                    messages.Add(new DomainValidationMessage { Level = ValidationLevel.Error, Message = string.Format(Language.BagsNotAllowed , requestedBags,item.Key) , Property = nameof(item.Key) });
            }

            if (this.Next != null)
                this.Next.Validate(messages);
        }
    }
}
