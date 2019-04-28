using System;
using MediatR;
using Ryanair.Reservation.DTO;

namespace Ryanair.Reservation.Application.Mediator.Queries.Reservation
{
    public class GetReservationQuery : IRequest<ReservationDto>
    {
        public string ReservationNumber { get; set; }
    }
}
