using MediatR;
using Ryanair.Reservation.Domain.DTO;
using Ryanair.Reservation.Domain.Interfaces;

namespace Ryanair.Reservation.Application.Mediator.Queries.Reservation
{
    public class GetReservationQuery : IRequest<ReservationDto>
    {
        public string ReservationNumber { get; set; }
    }
}
