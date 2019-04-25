using Ryanair.Reservation.Domain.Commands;
using Ryanair.Reservation.Domain.DataAccess.Repositories;
using Ryanair.Reservation.Domain.Entities;
using Ryanair.Reservation.Domain.Validation;
using Ryanair.Reservation.Infrastructure.DataAccess.Repositories;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Ryanair.Reservation.Tests
{
    public class BookingTests
    {
        [Fact]
        public void CreatBookingFailed_Test()
        {
            //not necessary mock repositories, our "database" already is in memory
            IBookingRepository _bookingRepository = new BookingRepository();
            IBookFlightRepository _bookFlightRepository = new BookFlightRepository();
            IFlightRepository _flightRepository = new FlightRepository();
            IPassengerRepository _passengeringRepository = new PassengerRepository();

            var command = new CreateReservationCommandTest
            {
                Email = null,
                CreditCard = "25445585446658",
            };

            var ex = Assert.Throws<DomainValidationException>(() => new Booking(_bookingRepository, _bookFlightRepository, _flightRepository, _passengeringRepository, command));
            Assert.Equal("Email could not be empty.", ex.ValidationError.FirstOrDefault().Message);
            Assert.Equal("Email", ex.ValidationError.FirstOrDefault().Property);
            Assert.Equal(ValidationLevel.Error, ex.ValidationError.FirstOrDefault().Level);            
        }
    }


    internal class CreateReservationCommandTest : ICreateReservationCommand
    {
        public string Email { get; set; }
        public string CreditCard { get; set; }
        public List<CreateBookFlightCommand> Flights { get; set; }
    }

}
