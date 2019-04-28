using Ryanair.Reservation.Application.Mediator.Commands;
using Ryanair.Reservation.Domain.Validation;
using Ryanair.Reservation.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Ryanair.Reservation.Tests
{
    public class ReservationFieldsValidationTest
    {
        [Fact]
        public void CreatBookingFailed_Test()
        {
            FlightRepository _flightRepository = new FlightRepository();            

            var command = this.GetEntityValidEntity();

            var validation = new ReservationFieldsValidation(_flightRepository, command);
            var expected = true;
            Assert.Equal(validation.ValidateCommand(), expected);

            command = this.GetEntityInvalidValidEmal();
            validation = new ReservationFieldsValidation(_flightRepository, command);

            var ex = Assert.Throws<DomainValidationException>(() => validation.ValidateCommand());
            Assert.Equal("Email could not be empty.", ex.ValidationError.FirstOrDefault().Message);
            Assert.Equal("Email", ex.ValidationError.FirstOrDefault().Property);
            Assert.Equal(ValidationLevel.Error, ex.ValidationError.FirstOrDefault().Level);

            command = this.GetEntityInvalidFlightTicket();
            validation = new ReservationFieldsValidation(_flightRepository, command);
            ex = Assert.Throws<DomainValidationException>(() => validation.ValidateCommand());
            Assert.Equal("Flight key could not be empty.", ex.ValidationError.FirstOrDefault().Message);
            Assert.Equal("Key", ex.ValidationError.FirstOrDefault().Property);
            Assert.Equal(ValidationLevel.Error, ex.ValidationError.FirstOrDefault().Level);


            command = this.GetEntityInvalidSeat();
            validation = new ReservationFieldsValidation(_flightRepository, command);
            ex = Assert.Throws<DomainValidationException>(() => validation.ValidateCommand());
            Assert.Equal("Could not convert 28K to number.", ex.ValidationError.FirstOrDefault().Message);            
            Assert.Equal(ValidationLevel.Error, ex.ValidationError.FirstOrDefault().Level);

        }

        [Fact]
        public void RulesValidationBagsPerUserFailed_Test()
        {
            IFlightRepository _flightRepository = new FlightRepository();
            var command = this.GetEntityValidBagsMax();
            var validation = new ReservationFieldsValidation(_flightRepository, command);

            var ex = Assert.Throws<DomainValidationException>(() => validation.ValidateCommand());
            Assert.Equal("The user Robert Plant cannot carry more then 5 bags.", ex.ValidationError.FirstOrDefault().Message);
            Assert.Equal("Bags", ex.ValidationError.FirstOrDefault().Property);
            Assert.Equal(ValidationLevel.Error, ex.ValidationError.FirstOrDefault().Level);
        }      

        private CreateReservationCommand GetEntityValidBagsMax()
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
                                Bags = 7,
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

        private CreateReservationCommand GetEntityInvalidValidEmal()
        {
            return new CreateReservationCommand()
            {
                Email = "",
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

        private CreateReservationCommand GetEntityInvalidFlightTicket()
        {
            return new CreateReservationCommand()
            {
                Email = "contact@contact.com",
                CreditCard = "0123456789012345",
                Flights = new List<CreateBookFlightCommand>()
                {
                    new CreateBookFlightCommand
                    {
                        Key = "",
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

        private CreateReservationCommand GetEntityInvalidSeat()
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
                                Seat = "28K"
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
    }
}
