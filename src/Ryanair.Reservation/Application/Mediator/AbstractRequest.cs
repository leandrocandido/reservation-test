using MediatR;
using Ryanair.Reservation.Domain.Entities;

namespace Ryanair.Reservation.Application.Mediator
{
    public class AbstractRequest : IRequest<Response>
    {
    }
}
