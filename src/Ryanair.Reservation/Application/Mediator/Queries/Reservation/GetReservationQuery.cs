using MediatR;
using Ryanair.Reservation.Domain.Interfaces;

namespace Ryanair.Reservation.Application.Mediator.Queries.Reservation
{
    public class GetReservationQuery : IRequest<IHandleResponse>
    {
        public string ReservationNumber { get; set; }
    }
}
