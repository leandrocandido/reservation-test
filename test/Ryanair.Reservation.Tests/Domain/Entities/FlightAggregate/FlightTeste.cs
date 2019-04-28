using System;
using System.Collections.Generic;
using System.Linq;
using Ryanair.Reservation.Domain.Entities;
using Ryanair.Reservation.Domain.Interfaces;
using Ryanair.Reservation.Domain.Specifications;
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
            IRepository<Flight> _flightRepository = new FlightRepository();
            Flight flight = _flightRepository.List(new FlightByKeySpec("Flight00052")).FirstOrDefault();

            var result = flight.CanAddPassenger(GetPassengerData());

            Assert.Empty(result);
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

        private PassengerData GetPassengerData()
        {
            return new PassengerData
            {
                Name = "Robert Plant",
                Bags = 2,
                Seat = "27"
            };
        }


        //private ReservationData GetEntityValidEntity()
        //{
        //    return new ReservationData()
        //    {
        //        Email = "contact@contact.com",
        //        CreditCard = "0123456789012345",
        //        Flights = new List<FlightData>()
        //        {
        //            new FlightData
        //            {
        //                Key = "Flight00052",
        //                Passengers = new List<PassengerData>()
        //                {
        //                    new PassengerData
        //                    {
        //                        Name = "Robert Plant",
        //                        Bags = 2,
        //                        Seat = "27"
        //                    },
        //                    new PassengerData
        //                    {
        //                        Name = "Ozzy Osbourne",
        //                        Bags = 0,
        //                        Seat = "28"
        //                    }
        //                }
        //            },
        //            new FlightData
        //            {
        //                Key = "Flight00103",
        //                Passengers = new List<PassengerData>()
        //                {
        //                    new PassengerData
        //                    {
        //                        Name = "Robert Plant",
        //                        Bags = 2,
        //                        Seat = "41"
        //                    },
        //                    new PassengerData
        //                    {
        //                        Name = "Ozzy Osbourne",
        //                        Seat = "40"
        //                    }
        //                }
        //            }
        //        }
        //    };
        }
}
