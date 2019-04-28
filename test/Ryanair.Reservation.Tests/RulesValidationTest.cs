using Ryanair.Reservation.Application.Mediator.Commands;
using Ryanair.Reservation.Domain.
using Ryanair.Reservation.Domain.DataAccess.Repositories;
using Ryanair.Reservation.Domain.Entities;
using Ryanair.Reservation.Domain.Service;
using Ryanair.Reservation.Domain.Validation;
using Ryanair.Reservation.Infrastructure.DataAccess;
using Ryanair.Reservation.Infrastructure.DataAccess.Repositories;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Ryanair.Reservation.Tests
{
    public class RulesValidationTest
    {
        [Fact]
        public void RulesValidationOk_Test()
        {
            IReservationRepository _reservationRepository = new ReservationRepository();            
            var command = GetEntityValidEntity();
            LoadDataBase();

            var validation = new ReservationRulesValidation(_reservationRepository, command);           
            var expected = true;
            Assert.Equal(validation.ValidateCommand(), expected);
        }

        [Fact]
        public void RulesValidationMaxNumberBags_Test()
        {
            IReservationRepository _reservationRepository = new ReservationRepository();
            var command = GetEntityMaxBag();
            LoadDataBase();
            
            var validation = new ReservationRulesValidation(_reservationRepository, command);
            var ex = Assert.Throws<DomainValidationException>(() => validation.ValidateCommand());
            Assert.Equal("There isn`t enought space for 3 bags in Flight00052 flight.", ex.ValidationError.FirstOrDefault().Message);
            Assert.Equal("Key", ex.ValidationError.FirstOrDefault().Property);
            Assert.Equal(ValidationLevel.Error, ex.ValidationError.FirstOrDefault().Level);            
        }

        [Fact]
        public void RulesValidationSeatRangeNumberBags_Test()
        {

            IReservationRepository _reservationRepository = new ReservationRepository();
            var command = GetEntityOuofRangeSeat();
            LoadDataBase();
            
            var validation = new ReservationRulesValidation(_reservationRepository, command);
            var ex = Assert.Throws<DomainValidationException>(() => validation.ValidateCommand());
            Assert.Equal("Seat Number 55 is out of the range.", ex.ValidationError.FirstOrDefault().Message);
            Assert.Equal(ValidationLevel.Error, ex.ValidationError.FirstOrDefault().Level);
        }

        private CreateReservationCommand GetEntityValidEntity()
        {
            return new CreateReservationCommand()
            {
                Email = "contact@contact.com",
                CreditCard = "0123456789012345",
                Flights = new List<CreateBookFlightCommand>()
                {
                    new CreateBookFlightCommand
                    {
                        Key = "Flight00052",
                        Passengers = new List<CreatePassengerCommand>()
                        {
                            new CreatePassengerCommand
                            {
                                Name = "Robert Plant",
                                Bags = 2,
                                Seat = "27"
                            },
                            new CreatePassengerCommand
                            {
                                Name = "Ozzy Osbourne",
                                Bags = 0,
                                Seat = "28"
                            }
                        }
                    },
                    new CreateBookFlightCommand
                    {
                        Key = "Flight00103",
                        Passengers = new List<CreatePassengerCommand>()
                        {
                            new CreatePassengerCommand
                            {
                                Name = "Robert Plant",
                                Bags = 2,
                                Seat = "41"
                            },
                            new CreatePassengerCommand
                            {
                                Name = "Ozzy Osbourne",
                                Seat = "40"
                            }
                        }
                    }
                }
            };
        }

        private CreateReservationCommand GetEntityMaxBag()
        {
            return new CreateReservationCommand()
            {
                Email = "contact@contact.com",
                CreditCard = "0123456789012345",
                Flights = new List<CreateBookFlightCommand>()
                {
                    new CreateBookFlightCommand
                    {
                        Key = "Flight00052",
                        Passengers = new List<CreatePassengerCommand>()
                        {
                            new CreatePassengerCommand
                            {
                                Name = "Robert Plant",
                                Bags = 3,
                                Seat = "27"
                            },
                            new CreatePassengerCommand
                            {
                                Name = "Ozzy Osbourne",
                                Bags = 0,
                                Seat = "28"
                            }
                        }
                    },
                    new CreateBookFlightCommand
                    {
                        Key = "Flight00103",
                        Passengers = new List<CreatePassengerCommand>()
                        {
                            new CreatePassengerCommand
                            {
                                Name = "Robert Plant",
                                Bags = 2,
                                Seat = "41"
                            },
                            new CreatePassengerCommand
                            {
                                Name = "Ozzy Osbourne",
                                Seat = "40"
                            }
                        }
                    }
                }
            };
        }

        private CreateReservationCommand GetEntityOuofRangeSeat()
        {
            return new CreateReservationCommand()
            {
                Email = "contact@contact.com",
                CreditCard = "0123456789012345",
                Flights = new List<CreateBookFlightCommand>()
                {
                    new CreateBookFlightCommand
                    {
                        Key = "Flight00052",
                        Passengers = new List<CreatePassengerCommand>()
                        {
                            new CreatePassengerCommand
                            {
                                Name = "Robert Plant",
                                Bags = 2,
                                Seat = "55"
                            },
                            new CreatePassengerCommand
                            {
                                Name = "Ozzy Osbourne",
                                Bags = 0,
                                Seat = "28"
                            }
                        }
                    },
                    new CreateBookFlightCommand
                    {
                        Key = "Flight00103",
                        Passengers = new List<CreatePassengerCommand>()
                        {
                            new CreatePassengerCommand
                            {
                                Name = "Robert Plant",
                                Bags = 2,
                                Seat = "41"
                            },
                            new CreatePassengerCommand
                            {
                                Name = "Ozzy Osbourne",
                                Seat = "40"
                            }
                        }
                    }
                }
            };
        }


        private void LoadDataBase()
        {
            var table = ReservationDataTable.GetInstance();
            table.ReservationInformation = InitalLoad();
        }

        private List<ReservationEntity> InitalLoad()
        {
            return new List<ReservationEntity>()
            {
                new ReservationEntity
                {
                    ReservationNumber = "ABC123",
                    Email = "contact@contact.com",
                    CreditCard = "0123456789012345",
                    Key = "Flight00052",
                    Name = "LEANDRO",
                    Bags = 5,
                    Seat = "01",
                },
                new ReservationEntity
                {
                    ReservationNumber = "ABC123",
                    Email = "contact@contact.com",
                    CreditCard = "0123456789012345",
                    Key = "Flight00052",
                    Name = "LEANDRO",
                    Bags = 5,
                    Seat = "01",
                },
                new ReservationEntity
                {
                    ReservationNumber = "ABC123",
                    Email = "contact@contact.com",
                    CreditCard = "0123456789012345",
                    Key = "Flight00052",
                    Name = "LEANDRO",
                    Bags = 5,
                    Seat = "01",
                },
                new ReservationEntity
                {
                    ReservationNumber = "ABC123",
                    Email = "contact@contact.com",
                    CreditCard = "0123456789012345",
                    Key = "Flight00052",
                    Name = "LEANDRO",
                    Bags = 5,
                    Seat = "01",
                },
                new ReservationEntity
                {
                    ReservationNumber = "ABC123",
                    Email = "contact@contact.com",
                    CreditCard = "0123456789012345",
                    Key = "Flight00052",
                    Name = "LEANDRO",
                    Bags = 5,
                    Seat = "01",
                },
                new ReservationEntity
                {
                    ReservationNumber = "ABC123",
                    Email = "contact@contact.com",
                    CreditCard = "0123456789012345",
                    Key = "Flight00052",
                    Name = "LEANDRO",
                    Bags = 5,
                    Seat = "01",
                },
                new ReservationEntity
                {
                    ReservationNumber = "ABC123",
                    Email = "contact@contact.com",
                    CreditCard = "0123456789012345",
                    Key = "Flight00052",
                    Name = "LEANDRO",
                    Bags = 5,
                    Seat = "01",
                },new ReservationEntity
                {
                    ReservationNumber = "ABC123",
                    Email = "contact@contact.com",
                    CreditCard = "0123456789012345",
                    Key = "Flight00052",
                    Name = "LEANDRO",
                    Bags = 5,
                    Seat = "01",
                },new ReservationEntity
                {
                    ReservationNumber = "ABC123",
                    Email = "contact@contact.com",
                    CreditCard = "0123456789012345",
                    Key = "Flight00052",
                    Name = "LEANDRO",
                    Bags = 5,
                    Seat = "01",
                }
                ,new ReservationEntity
                {
                    ReservationNumber = "ABC123",
                    Email = "contact@contact.com",
                    CreditCard = "0123456789012345",
                    Key = "Flight00052",
                    Name = "LEANDRO",
                    Bags = 3,
                    Seat = "01",
                }
                ,new ReservationEntity
                {
                    ReservationNumber = "ABC123",
                    Email = "contact@contact.com",
                    CreditCard = "0123456789012345",
                    Key = "Flight00103",
                    Name = "LEANDRO",
                    Bags = 5,
                    Seat = "01",
                }
                ,new ReservationEntity
                {
                    ReservationNumber = "ABC123",
                    Email = "contact@contact.com",
                    CreditCard = "0123456789012345",
                    Key = "Flight00103",
                    Name = "LEANDRO",
                    Bags = 5,
                    Seat = "01",
                }
            };
        }
    }
}
