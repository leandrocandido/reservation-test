using MediatR;
using Ryanair.Reservation.Domain.Interfaces;

namespace Ryanair.Reservation.Application.Mediator.Queries.Flight
{
    public class GetAllFlightsQuery : IRequest<IHandleResponse>
    {
    }
}
