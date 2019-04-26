using Ryanair.Reservation.Domain.Commands;
using Ryanair.Reservation.Domain.DataAccess.Repositories;
using Ryanair.Reservation.Domain.Interfaces;
using Ryanair.Reservation.Domain.Service.Validation;
using Ryanair.Reservation.Domain.Validation;
using System.Collections.Generic;

namespace Ryanair.Reservation.Domain.Service
{
    public class ReservationFieldsValidation : IDomainValidation
    {        
        private readonly IFlightRepository _flightRepository;     
        private readonly ICreateReservationCommand _command;
        private IRulesValidation _validation;

        public ReservationFieldsValidation(           
            IFlightRepository flightRepository,            
            ICreateReservationCommand command
            )
        {
            _command = command;           
            _flightRepository = flightRepository;            
            this.SetValidationRules();
        }

        /// <summary>
        /// Define the Rules used to validate command request fields
        /// </summary>
        private void SetValidationRules()
        {
            //chain of responsabilities used to create all validations
            var bookingfields = new ValidateBookingFields(this._command);
            var bookflight = new ValidateBookFlightFields(this._command,_flightRepository);
            var ticketInformation = new ValidateTicketInformation(this._command);
            var passerngersfield = new ValidatePassengersFields(this._command);
            var passengerInformation = new ValidatePassengerInformation(this._command);
            var seatnumber = new ValidateNumericSeat(this._command);            

            bookingfields.Next = ticketInformation;
            ticketInformation.Next = bookflight;
            bookflight.Next = passengerInformation;
            passengerInformation.Next = passerngersfield;
            passerngersfield.Next = seatnumber;
            seatnumber.Next = null;            

            this._validation = bookingfields;
        }

        /// <summary>
        /// Method used to start all validation in the chain
        /// </summary>
        /// <returns><c>true</c>, if command was validated, <c>false</c> otherwise.</returns>
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
