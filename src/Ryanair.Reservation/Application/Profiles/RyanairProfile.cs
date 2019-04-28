using System;
using AutoMapper;
using Ryanair.Reservation.Domain.Entities.FlightAggregate;
using Ryanair.Reservation.Domain.Validation;
using Ryanair.Reservation.DTO;

namespace Ryanair.Reservation.Application.Profiles
{
    public class RyanairProfile : Profile
    {
        public RyanairProfile()
        {
            CreateMap<Flight, FlightDto>().ReverseMap();
            CreateMap<Domain.Entities.ReservationAggregate.Reservation, ReservationConfirmationDto>().ReverseMap();
            CreateMap<Domain.Entities.ReservationAggregate.Reservation, ReservationDto>().ReverseMap();
            CreateMap<Domain.Entities.ReservationAggregate.ReservationFlight, ReservationFlightDto>().ReverseMap();
            CreateMap<Domain.Entities.FlightAggregate.Passenger, PassengerDto>().ReverseMap();

            CreateMap<DomainValidationException, DomainValidationExceptionDto>().ReverseMap();
            CreateMap<System.Exception, ExceptionDto>().ReverseMap();

        }
    }
}
