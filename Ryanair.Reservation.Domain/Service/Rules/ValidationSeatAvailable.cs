using System.Collections.Generic;
using System.Linq;
using Ryanair.Reservation.Domain.Commands;
using Ryanair.Reservation.Domain.DataAccess.Repositories;
using Ryanair.Reservation.Domain.Interfaces;
using Ryanair.Reservation.Domain.Resources;
using Ryanair.Reservation.Domain.Validation;

namespace Ryanair.Reservation.Domain.Service.Rules
{
    class ValidationSeatAvailable : IRulesValidation
    {
        public IRulesValidation Next { get; set; }

        private readonly IReservationRepository _reservationRepository;
        protected readonly ICreateReservationCommand _command;

        public ValidationSeatAvailable(IReservationRepository reservationRepository, ICreateReservationCommand command)
        {
            this._command = command;
            this._reservationRepository = reservationRepository;
        }

        public void Validate(List<DomainValidationMessage> messages)
        {          
            foreach (var flight in _command?.Flights)
            {
                var requiredSeats = flight.Passengers.Select(x => x.Seat).ToList();
                var usedSeats = _reservationRepository.GetReservedSeatsPerFlight(flight.Key);

                foreach (var seatNumber in requiredSeats)
                {
                    if (usedSeats.Contains(seatNumber))
                    {
                        messages.Add(new DomainValidationMessage { Level = ValidationLevel.Error, Message = string.Format(Language.SeatInUse, seatNumber ,flight.Key), Property = nameof(flight) });
                        continue;
                    }
                }
            }

            if (this.Next != null)
                this.Next.Validate(messages);
        }
    }
}
