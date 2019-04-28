using AutoMapper;
using MediatR;
using Ryanair.Reservation.DTO;
using Ryanair.Reservation.Domain.Interfaces;
using Ryanair.Reservation.Domain.Specifications.Flight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Ryanair.Reservation.Application.Mediator.Queries.Flight
{
    public class GetFlightsByParamQueryHandler : IRequestHandler<GetFlightsByParamQuery, List<FlightDto>>
    {
        private readonly IRepository<Domain.Entities.Flight> _flightRepository;
        private readonly IMapper _mapper;

        public GetFlightsByParamQueryHandler(IRepository<Domain.Entities.Flight> flightRepository,
            IMapper mapper, IRepository<Domain.Entities.Reservation> reservationRepository)
        {
            _flightRepository = flightRepository;
            _mapper = mapper;
        }

        public Task<List<FlightDto>> Handle(GetFlightsByParamQuery request, CancellationToken cancellationToken)
        {
            var outboundFlightSpec = new FlightDepartsFromSpec(request.Origin)
                .And(new FlightHasFreeSeatsSpec(request.Passengers))
                .And(new FlightFlyingToSpec(request.Destination))
                .And(new FlightDepartsOnSpec(request.DateOut));

            Expression<Func<string, bool>> e1 = (y => y.Length > 0);
            Expression<Func<string, bool>> e2 = (z => z.Length < 10);

            var flightSearch = outboundFlightSpec;

            // If it's a round trip, we combine the spec to filter inbound flight.
            if (request.RoundTrip)
            {
                var inboundFlightSpec = new FlightDepartsFromSpec(request.Destination)
                    .And(new FlightHasFreeSeatsSpec(request.Passengers))
                    .And(new FlightFlyingToSpec(request.Origin))
                    .And(new FlightDepartsOnSpec(request.DateIn.Value));

                flightSearch = flightSearch.Or(inboundFlightSpec);
            }

            var flights = _flightRepository.List(flightSearch);

            var converted = _mapper.Map<List<FlightDto>>(flights);
            return Task.FromResult(converted.ToList());
        }
    }
}