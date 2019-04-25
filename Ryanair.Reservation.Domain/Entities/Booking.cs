using Ryanair.Reservation.Domain.Commands;
using Ryanair.Reservation.Domain.DataAccess.Repositories;
using Ryanair.Reservation.Domain.Resources;
using Ryanair.Reservation.Domain.Utils;
using Ryanair.Reservation.Domain.Validation;
using System.Collections.Generic;

namespace Ryanair.Reservation.Domain.Entities
{
    public class Booking : EntityDomainValidation
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IBookFlightRepository _bookFlightRepository;
        private readonly IFlightRepository _flightRepository;
        private readonly IPassengerRepository _passengeringRepository;
        private readonly ICreateReservationCommand _command;

        private Booking() { }

        public Booking(
            IBookingRepository bookingRepository,
            IBookFlightRepository bookFlightRepository,
            IFlightRepository flightRepository,
            IPassengerRepository passengeringRepository,
            ICreateReservationCommand command
        )
        {
            _command = command;
            _bookingRepository = bookingRepository;
            _bookFlightRepository = bookFlightRepository;
            _flightRepository = flightRepository;
            _passengeringRepository = passengeringRepository;
            this.ProcessDomainEntity();
        }       

        protected override void DomainValidation(List<DomainValidationMessage> messages)
        {
            if (string.IsNullOrEmpty(this._command.Email))
                messages.Add(new DomainValidationMessage { Level = ValidationLevel.Error, Message = Language.EmailNullEmpty, Property = nameof(this.Email) });

            if (string.IsNullOrEmpty(this._command.CreditCard))
                messages.Add(new DomainValidationMessage { Level = ValidationLevel.Error, Message = Language.CreditCardNullEmprty, Property = nameof(this.CreditCard) });
        }

        protected override void AfterValidation()
        {
            this.Email = _command.Email;
            this.CreditCard = _command.CreditCard;
            this.ReservationNumber = RandomGenerator.RandomReservationNumber(100, 999, 3);

            this.Flights = new List<BookFlight>();

            foreach (var item in _command.Flights)
            {
                var bookflight = new BookFlight(_bookingRepository, _bookFlightRepository, _flightRepository, _passengeringRepository, item);
                this.Flights.Add(bookflight);
            }
        }

        public string Email { get; set; }
        public string CreditCard { get; set; }
        public List<BookFlight> Flights { get; set; }
        public string ReservationNumber { get; set; }
    }
}
