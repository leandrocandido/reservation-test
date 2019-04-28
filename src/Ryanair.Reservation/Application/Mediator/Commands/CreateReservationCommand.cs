using MediatR;
using Ryanair.Reservation.DTO;
using Ryanair.Reservation.Domain.Interfaces;
using Ryanair.Reservation.Domain.ValueObjects;
using System.Collections.Generic;

namespace Ryanair.Reservation.Application.Mediator.Commands
{
    public class CreateReservationCommand : ReservationData, IRequest<ReservationConfirmationDto>
    {
    }
}
