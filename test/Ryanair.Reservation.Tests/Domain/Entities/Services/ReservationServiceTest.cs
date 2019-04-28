using System;
using System.Collections.Generic;
using Ryanair.Reservation.Domain.Entities;
using Ryanair.Reservation.Domain.Interfaces;
using Ryanair.Reservation.Domain.Interfaces.Services;
using Ryanair.Reservation.Domain.Services;
using Ryanair.Reservation.Domain.Validation;
using Ryanair.Reservation.Domain.ValueObjects;
using Ryanair.Reservation.Infrastructure.Repositories;
using Xunit;

namespace Ryanair.Reservation.Tests.Domain.Entities.Services
{
    public class ReservationServiceTest
    {
        [Fact]
        public void CreatingReservationOk()
        {
            IRepository<Flight> _flightRepository = new FlightRepository();
            IReservationService service = new ReservationService(_flightRepository);
            var result = service.ConfirmReservation(GetEntityValidEntity());
            Assert.NotNull(result);
        }

        [Fact]
        public void CreatingReservationNoFlightKeyInformationError()
        {
            IRepository<Flight> _flightRepository = new FlightRepository();
            IReservationService service = new ReservationService(_flightRepository);

            var data = GetEntityValidEntity();
            data.Flights[0].Key = "";

            Assert.Throws<DomainValidationException>(() => service.ConfirmReservation(data));
        }

        [Fact]
        public void CreatingReservationNoFlightInformationError()
        {
            IRepository<Flight> _flightRepository = new FlightRepository();
            IReservationService service = new ReservationService(_flightRepository);

            var data = GetEntityValidEntity();
            data.Flights = null;

            Assert.Throws<DomainValidationException>(() => service.ConfirmReservation(data));
        }


        private ReservationData GetEntityValidEntity()
        {
            return new ReservationData()
            {
                Email = "contact@contact.com",
                CreditCard = "0123456789012345",
                Flights = new List<FlightData>()
                {
                    new FlightData
                    {
                        Key = "Flight00052",
                        Passengers = new List<PassengerData>()
                        {
                            new PassengerData
                            {
                                Name = "Robert Plant",
                                Bags = 2,
                                Seat = "27"
                            },
                            new PassengerData
                            {
                                Name = "Ozzy Osbourne",
                                Bags = 0,
                                Seat = "28"
                            }
                        }
                    },
                    new FlightData
                    {
                        Key = "Flight00103",
                        Passengers = new List<PassengerData>()
                        {
                            new PassengerData
                            {
                                Name = "Robert Plant",
                                Bags = 2,
                                Seat = "41"
                            },
                            new PassengerData
                            {
                                Name = "Ozzy Osbourne",
                                Seat = "40"
                            }
                        }
                    }
                }
            };
        }
    }
}
