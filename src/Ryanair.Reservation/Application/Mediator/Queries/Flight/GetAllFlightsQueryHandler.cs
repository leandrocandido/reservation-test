using AutoMapper;
using Ryanair.Reservation.Application.DTO;
using Ryanair.Reservation.Domain.DataAccess.Repositories;
using System.Collections.Generic;
using System.Threading;

namespace Ryanair.Reservation.Application.Mediator.Queries.Flight
{
    public class GetAllFlightsQueryHandler : AbstractRequestHandler<GetAllFlightsQuery>
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IMapper _mapper;

        public GetAllFlightsQueryHandler(IFlightRepository flightRepository, IMapper mapper)
        {
            _flightRepository = flightRepository;
            _mapper = mapper;
        }

        internal override HandleResponse HandleIt(GetAllFlightsQuery request, CancellationToken cancellationToken)
        {
            var flights = _flightRepository.GetAll();
            var converted = _mapper.Map<IEnumerable<FlightDto>>(flights);
            return new HandleResponse() { Content = converted };
        }
    }
}
