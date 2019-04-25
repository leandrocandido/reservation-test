using Ryanair.Reservation.Domain.Commands;
using Ryanair.Reservation.Domain.DataAccess.Repositories;
using Ryanair.Reservation.Domain.Entities;
using Ryanair.Reservation.Domain.Validation;
using Ryanair.Reservation.Infrastructure.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Ryanair.Reservation.Tests
{
    public class BookFlightTest
    {
        [Fact]
        public void CreatBookingFailed_Test()
        {
            IBookingRepository _bookingRepository = new BookingRepository();
            IBookFlightRepository _bookFlightRepository = new BookFlightRepository();
            IFlightRepository _flightRepository = new FlightRepository();
            IPassengerRepository _passengeringRepository = new PassengerRepository();

            var command = new CreateBookFlightCommandTest
            {
                Key = null,                
            }; 

            var ex = Assert.Throws<DomainValidationException>(() => new BookFlight(_bookingRepository, _bookFlightRepository, _flightRepository, _passengeringRepository, command));
            Assert.Equal("Flight key could not be empty.", ex.ValidationError.FirstOrDefault().Message);
            Assert.Equal("Key", ex.ValidationError.FirstOrDefault().Property);
            Assert.Equal(ValidationLevel.Error, ex.ValidationError.FirstOrDefault().Level);

        }
    }

    internal class CreateBookFlightCommandTest : ICreateBookFlightCommand
    {
        public string Key { get; set; }        
        public List<CreatePassengerCommand> Passengers { get; set; }
    }

}
