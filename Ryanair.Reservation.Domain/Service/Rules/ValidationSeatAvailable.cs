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

        /// <summary>
        /// Verify is requested seats are available for required flight
        /// </summary>
        /// <param name="messages">Messages.</param>
        public void Validate(List<DomainValidationMessage> messages)
        {          
            //navigate each flight in request
            foreach (var flight in _command?.Flights)
            {
                //all seats in request
                var requiredSeats = flight.Passengers.Select(x => x.Seat).ToList();
                //all reserved seats
                var usedSeats = _reservationRepository.GetReservedSeatsPerFlight(flight.Key);
                //chec if requested seat is available
                foreach (var seatNumber in requiredSeats)
                {
                    if (usedSeats.Contains(seatNumber))
                    {
                        messages.Add(new DomainValidationMessage { Level = ValidationLevel.Error, Message = string.Format(Language.SeatInUse, seatNumber ,flight.Key), Property = nameof(flight) });
                        continue;
                    }
                }
            }

            //got to next validation
            if (this.Next != null)
                this.Next.Validate(messages);
        }
    }
}
