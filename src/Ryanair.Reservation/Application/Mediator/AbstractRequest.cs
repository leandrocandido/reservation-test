using MediatR;
using Ryanair.Reservation.Domain.Responses;

namespace Ryanair.Reservation.Application.Mediator
{
    public class AbstractRequest : IRequest<Response>
    {
    }
}
