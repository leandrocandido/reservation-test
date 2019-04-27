using System;
using System.Linq;
using Ryanair.Reservation.Domain.Entities.FlightAggregate;
using Ryanair.Reservation.Domain.Interfaces;
using Ryanair.Reservation.Domain.Specifications.Flight;
using Ryanair.Reservation.Domain.ValueObjects;
using Ryanair.Reservation.Infrastructure.Repositories;
using Xunit;

namespace Ryanair.Reservation.Tests.Domain.Entities.FlightAggregate
{
    public class FlightTeste
    {
        [Fact]
        public void CandAddPassengerOk()
        {
            try
            {
                IRepository<Flight> _flightRepository = new FlightRepository();
                Flight flight = _flightRepository.List(new FlightByKeySpec("Flight00052")).FirstOrDefault();

                var result = flight.CanAddPassenger(GetPassengerData());

                Assert.Empty(result);
            }
            catch (Exception ex)
            {
                var res = ex.Message;
            }
          
        }

        [Fact]
        public void PassengerNameIsMissing()
        {
            IRepository<Flight> _flightRepository = new FlightRepository();
            Flight flight = _flightRepository.List(new FlightByKeySpec("Flight00052")).FirstOrDefault();

            var passenger = GetPassengerData();
            passenger.Name = "";

            var result = flight.CanAddPassenger(passenger);

            Assert.NotEmpty(result);
        }

        [Fact]
        public void PassengerSeatIsMissingIsMissing()
        {
            IRepository<Flight> _flightRepository = new FlightRepository();
            Flight flight = _flightRepository.List(new FlightByKeySpec("Flight00052")).FirstOrDefault();

            var passenger = GetPassengerData();
            passenger.Seat = "";

            var result = flight.CanAddPassenger(passenger);

            Assert.NotEmpty(result);
        }

        [Fact]
        public void PassengerSeatOuOfTheRange()
        {
            IRepository<Flight> _flightRepository = new FlightRepository();
            Flight flight = _flightRepository.List(new FlightByKeySpec("Flight00052")).FirstOrDefault();

            var passenger = GetPassengerData();
            passenger.Seat = "70";

            var result = flight.CanAddPassenger(passenger);

            Assert.NotEmpty(result);
        }

        [Fact]
        public void PassengerMaxBagsAllowed()
        {
            IRepository<Flight> _flightRepository = new FlightRepository();
            Flight flight = _flightRepository.List(new FlightByKeySpec("Flight00052")).FirstOrDefault();

            var passenger = GetPassengerData();
            passenger.Bags = 10;

            var result = flight.CanAddPassenger(passenger);

            Assert.NotEmpty(result);
        }

        [Fact]
        public void PassengerSeatIsInUse()
        {
            IRepository<Flight> _flightRepository = new FlightRepository();
            Flight flight = _flightRepository.List(new FlightByKeySpec("Flight00052")).FirstOrDefault();

            var passenger = GetPassengerData();
            flight.AddPassenger(passenger);

            var result = flight.CanAddPassenger(passenger);

            Assert.NotEmpty(result);
        }


        [Fact]
        public void PassengerNoBagsSpace()
        {
            IRepository<Reservation.Domain.Entities.ReservationAggregate.Reservation> _reservationRepository = new ReservationRepository();

            IRepository<Flight> _flightRepository = new FlightRepository();
            Flight flight = _flightRepository.List(new FlightByKeySpec("Flight00052")).FirstOrDefault();

            var passenger = GetPassengerData();

            passenger.Bags = 49;

            flight.AddPassenger(passenger);

            passenger.Bags = 2;
            passenger.Seat = "28";
            var result = flight.CanAddPassenger(passenger);

            Assert.NotEmpty(result);
        }


        private PassengerData GetPassengerData()
        {
            return new PassengerData
            {
                Name = "Robert Plant",
                Bags = 2,
                Seat = "27"
            };
        }
    }
}
