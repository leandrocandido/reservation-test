using System;
using System.Collections.Generic;
using System.Threading;
using AutoMapper;
using Ryanair.Reservation.Application.Mediator.Commands;
using Ryanair.Reservation.Application.Profiles;
using Ryanair.Reservation.Domain.Entities;
using Ryanair.Reservation.Domain.Interfaces;
using Ryanair.Reservation.Domain.Interfaces.Services;
using Ryanair.Reservation.Domain.Services;
using Ryanair.Reservation.Domain.Validation;
using Ryanair.Reservation.Domain.ValueObjects;
using Ryanair.Reservation.Infrastructure.Repositories;
using Xunit;

namespace Ryanair.Reservation.Tests.Application.Mediator.Commands
{
    public class CreateReservationCommandHandlerTest
    {
        [Fact]
        public void CreateReservationCommandOk()
        {
            IRepository<Ryanair.Reservation.Domain.Entities.Reservation> reservationRepository = new ReservationRepository();
            IRepository<Flight> flightRepository = new FlightRepository();
            IReservationService service = new ReservationService(flightRepository);

            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new RyanairProfile());
            });
            var mapper = mockMapper.CreateMapper();

            var token = new CancellationToken();

            var handler = new CreateReservationCommandHandler(service, reservationRepository, mapper);

            var result  = handler.Handle(GetReservationData(),token).Result;

            Assert.NotNull(result);
        }

        [Fact]
        public void CreateReservationCommandFail()
        {
            IRepository<Ryanair.Reservation.Domain.Entities.Reservation> reservationRepository = new ReservationRepository();
            IRepository<Flight> flightRepository = new FlightRepository();
            IReservationService service = new ReservationService(flightRepository);

            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new RyanairProfile());
            });
            var mapper = mockMapper.CreateMapper();

            var token = new CancellationToken();

            var handler = new CreateReservationCommandHandler(service, reservationRepository, mapper);
            var command = GetReservationData();
            command.Email = null;

            Assert.Throws<DomainValidationException>(() => handler.Handle(command, token).Result);
        }

        private CreateReservationCommand GetReservationData()
        {
            return new CreateReservationCommand
            {
                Email = "leandro@gmail.com",
                CreditCard = "123456789",
                Flights = new List<FlightData>()
                {
                    new FlightData
                    {
                        Key = "Flight00052",
                        Passengers = new List<PassengerData>()
                        {
                            new PassengerData
                            {
                                Bags = 2,
                                Name = "leandro candido",
                                Seat = "02"
                            },
                            new PassengerData
                            {
                                Bags = 2,
                                Name = "Kaqueline Fernandes",
                                Seat = "03"
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
                                Bags = 2,
                                Name = "Jose Dias",
                                Seat = "10"
                            },
                            new PassengerData
                            {
                                Bags = 2,
                                Name = "Joao ribeiro",
                                Seat = "11"
                            }
                        }
                    }
                }

            };

        }

    }
}
