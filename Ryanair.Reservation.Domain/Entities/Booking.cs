using Ryanair.Reservation.Domain.Commands;
using Ryanair.Reservation.Domain.DataAccess.Repositories;
using Ryanair.Reservation.Domain.Resources;
using Ryanair.Reservation.Domain.Utils;
using Ryanair.Reservation.Domain.Validation;
using System.Collections.Generic;

namespace Ryanair.Reservation.Domain.Entities
{
    public class Booking
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
            this.ProcessBook();
        }

        public void ProcessBook()
        {
            var msgs = this.Validate(_command);

            if (msgs.Count > 0)
                throw new DomainValidationException(msgs);

            this.Email = _command.Email;
            this.CreditCard = _command.CreditCard;
            this.ReservationNumber = RandomGenerator.RandomReservationNumber(100, 999, 3);            

            foreach (var item in _command.Flights)
            {

            }
        }

        private List<DomainValidationMessage> Validate(ICreateReservationCommand command)
        {
            List<DomainValidationMessage> messages = new List<DomainValidationMessage>();

            if (string.IsNullOrEmpty(command.Email))
                messages.Add(new DomainValidationMessage { Level = ValidationLevel.Error, Message = Language.EmailNullEmpty, Property = nameof(this.Email) });

            if (string.IsNullOrEmpty(command.CreditCard))
                messages.Add(new DomainValidationMessage { Level = ValidationLevel.Error, Message = Language.CreditCardNullEmprty, Property = nameof(this.CreditCard) });

            return messages;
        }


        public string Email { get; set; }
        public string CreditCard { get; set; }
        public List<BookFlight> Flights { get; set; }
        public string ReservationNumber { get; set; }
    }
}
