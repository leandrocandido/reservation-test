using MediatR;
using Ryanair.Reservation.DTO;
using Ryanair.Reservation.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
