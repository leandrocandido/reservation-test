using MediatR;
using Ryanair.Reservation.Domain.Entities;

namespace Ryanair.Reservation.Application.Mediator.Queries.Flight
{
    public class GetAllFlightsQuery : IRequest<Response>
    {
    }
}
