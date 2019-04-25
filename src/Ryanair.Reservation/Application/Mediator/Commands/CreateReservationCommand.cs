using MediatR;
using Ryanair.Reservation.Domain.Commands;
using Ryanair.Reservation.Domain.Interfaces;
using System.Collections.Generic;

namespace Ryanair.Reservation.Application.Mediator.Commands
{
    public class CreateReservationCommand : IRequest<IHandleResponse> , ICreateReservationCommand
    {
        public string Email { get; set; }
        public string CreditCard { get; set; }
        public List<CreateBookFlightCommand> Flights { get; set; }
    }
}
