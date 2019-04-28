using System;
using System.Collections.Generic;
using MediatR;
using Ryanair.Reservation.DTO;

namespace Ryanair.Reservation.Application.Mediator.Queries.Flight
{
    public class GetFlightsByParamQuery : IRequest<List<FlightDto>>
    {
        public int Passengers { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime DateOut { get; set; }
        public DateTime? DateIn { get; set; }
        public bool RoundTrip { get; set; }
    }
}
