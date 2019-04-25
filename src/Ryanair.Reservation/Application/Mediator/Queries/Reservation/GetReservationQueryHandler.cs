using System;
using System.Threading;
using AutoMapper;
using Ryanair.Reservation.Application.Extensions;
using Ryanair.Reservation.Domain.DataAccess.Repositories;
using Ryanair.Reservation.Domain.Interfaces;
using Ryanair.Reservation.Domain.Responses;

namespace Ryanair.Reservation.Application.Mediator.Queries.Reservation
{
    public class GetReservationQueryHandler : AbstractRequestHandler<GetReservationQuery>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        public GetReservationQueryHandler(IReservationRepository reservationRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }

        internal override IHandleResponse HandleIt(GetReservationQuery request, CancellationToken cancellationToken)
        {
            var reseration = _reservationRepository.GetByReservationNumber(request.ReservationNumber);
            var result = reseration.ConvertReservation();
            return new ReservationByCodeResponse() { Content = result };
        }
    }
}
