using AutoMapper;
using Ryanair.Reservation.Domain.Entities;
using Ryanair.Reservation.Domain.Validation;
using Ryanair.Reservation.DTO;

namespace Ryanair.Reservation.Application.Profiles
{
    public class RyanairProfile : Profile
    {
        public RyanairProfile()
        {
            CreateMap<Flight, FlightDto>().ReverseMap();
            CreateMap<Domain.Entities.Reservation, ReservationConfirmationDto>().ReverseMap();
            CreateMap<Domain.Entities.Reservation, ReservationDto>().ReverseMap();
            CreateMap<Domain.Entities.ReservationFlight, ReservationFlightDto>().ReverseMap();
            CreateMap<Domain.Entities.Passenger, PassengerDto>().ReverseMap();

            CreateMap<DomainValidationException, DomainValidationExceptionDto>().ReverseMap();
            CreateMap<System.Exception, ExceptionDto>().ReverseMap();

        }
    }
}
