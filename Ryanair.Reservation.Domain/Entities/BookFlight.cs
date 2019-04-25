using Ryanair.Reservation.Domain.Commands;
using Ryanair.Reservation.Domain.DataAccess.Repositories;
using Ryanair.Reservation.Domain.Resources;
using Ryanair.Reservation.Domain.Validation;
using System.Collections.Generic;

namespace Ryanair.Reservation.Domain.Entities
{
    public class BookFlight : EntityDomainValidation
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IBookFlightRepository _bookFlightRepository;
        private readonly IFlightRepository _flightRepository;
        private readonly IPassengerRepository _passengeringRepository;
        private readonly ICreateBookFlightCommand _command;

        private BookFlight() { }

        public BookFlight(
           IBookingRepository bookingRepository,
           IBookFlightRepository bookFlightRepository,
           IFlightRepository flightRepository,
           IPassengerRepository passengeringRepository,
           ICreateBookFlightCommand command
       )
        {
            _command = command;
            _bookingRepository = bookingRepository;
            _bookFlightRepository = bookFlightRepository;
            _flightRepository = flightRepository;
            _passengeringRepository = passengeringRepository;
            this.ProcessDomainEntity();
        }

        public string Key { get; set; }
        public List<Passengers> Passengers { get; set; }

        protected override void DomainValidation(List<DomainValidationMessage> messages)
        {
            if (string.IsNullOrEmpty(this._command.Key))
                messages.Add(new DomainValidationMessage { Level = ValidationLevel.Error, Message = string.Format(Language.FlightNullEmpty, _command.Key), Property = nameof(this.Key) });

            if (!_flightRepository.FlightExists(this._command.Key) && !string.IsNullOrEmpty(this._command.Key))
                messages.Add(new DomainValidationMessage { Level = ValidationLevel.Error, Message = string.Format( Language.FlightNotExists, _command.Key ), Property = nameof(this.Key) });
        }

        protected override void AfterValidation()
        {
            this.Key = _command.Key;
        }
    }
}
