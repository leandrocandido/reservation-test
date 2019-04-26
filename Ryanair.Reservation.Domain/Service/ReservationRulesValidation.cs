using System.Collections.Generic;
using Ryanair.Reservation.Domain.Commands;
using Ryanair.Reservation.Domain.DataAccess.Repositories;
using Ryanair.Reservation.Domain.Interfaces;
using Ryanair.Reservation.Domain.Service.Rules;
using Ryanair.Reservation.Domain.Validation;

namespace Ryanair.Reservation.Domain.Service
{
    public class ReservationRulesValidation : IDomainValidation
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ICreateReservationCommand _command;
        private IRulesValidation _validation;

        public ReservationRulesValidation(
            IReservationRepository reservationRepository,
            ICreateReservationCommand command
            )
        {
            _reservationRepository = reservationRepository;
            _command = command;            
            this.SetValidationRules();
        }

        /// <summary>
        /// Define the Rules used to validate command request fields
        /// </summary>
        private void SetValidationRules()
        {            
            var bags = new ValidateBagsPerFlight(_reservationRepository, _command);
            var seatsRange = new ValidateSeatRange(_command);
            bags.Next = seatsRange;
            seatsRange.Next = null;
            _validation = bags;
        }

        public bool ValidateCommand()
        {
            var result = false;
            var messages = new List<DomainValidationMessage>();
            _validation.Validate(messages);
            if (messages.Count > 0)
                throw new DomainValidationException(messages);
            else
                result = true;

            return result;            
        }
    }
}
