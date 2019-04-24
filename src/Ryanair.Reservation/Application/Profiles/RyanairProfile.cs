using AutoMapper;
using Ryanair.Reservation.Application.DTO;
using Ryanair.Reservation.Domain.Entities;

namespace Ryanair.Reservation.Application.Profiles
{
    public class RyanairProfile : Profile
    {
        public RyanairProfile()
        {
            CreateMap<Flight, FlightDto>().ReverseMap();
        }
    }
}
