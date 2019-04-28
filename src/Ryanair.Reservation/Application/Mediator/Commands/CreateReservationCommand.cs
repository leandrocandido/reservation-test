using System;
using MediatR;
using Ryanair.Reservation.Domain.ValueObjects;
using Ryanair.Reservation.DTO;

namespace Ryanair.Reservation.Application.Mediator.Commands
{
    public class CreateReservationCommand : ReservationData, IRequest<ReservationConfirmationDto>
    {
    }
}
