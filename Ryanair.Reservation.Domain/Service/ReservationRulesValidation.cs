using Ryanair.Reservation.Domain.Commands;
using Ryanair.Reservation.Domain.DataAccess.Repositories;
using Ryanair.Reservation.Domain.Interfaces;
using Ryanair.Reservation.Domain.Service.Rules;
using Ryanair.Reservation.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ryanair.Reservation.Domain.Service
{
    public class ReservationRulesValidation : IReservationRulesValidation
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

    public interface IReservationRulesValidation : IDomainValidation
    { }
}
