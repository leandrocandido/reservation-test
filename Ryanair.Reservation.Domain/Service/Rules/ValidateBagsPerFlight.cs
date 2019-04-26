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


        /// <summary>
        /// Validade the max number allowed per flight
        /// </summary>
        /// <param name="messages">Messages.</param>
        public void Validate(List<DomainValidationMessage> messages)
        {
            foreach (var item in _command?.Flights)
            {
                //all bags in the request(per flight)
                int requestedBags = item.Passengers.Sum(x => x.Bags);
                //all bags already reserved in flight
                int bags = _reservationRepository.GetBagsPerFlight(item.Key);
                int total = requestedBags + bags;
                //max number of bags allowed per flight.
                if (total > 50)
                    messages.Add(new DomainValidationMessage { Level = ValidationLevel.Error, Message = string.Format(Language.BagsNotAllowed , requestedBags,item.Key) , Property = nameof(item.Key) });
            }

            //got to the next validation
            if (this.Next != null)
                this.Next.Validate(messages);
        }
    }
}
