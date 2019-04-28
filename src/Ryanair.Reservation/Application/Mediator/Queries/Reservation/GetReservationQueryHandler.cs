using AutoMapper;
using MediatR;
using Ryanair.Reservation.Domain.DTO;
using Ryanair.Reservation.Domain.Interfaces;
using Ryanair.Reservation.Domain.Specifications.Reservation;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ryanair.Reservation.Application.Mediator.Queries.Reservation
{
    public class GetReservationQueryHandler : IRequestHandler<GetReservationQuery, ReservationDto>
    {
        private readonly IRepository<Domain.Entities.Reservation> _reservationRepository;
        private readonly IMapper _mapper;

        public GetReservationQueryHandler(IRepository<Domain.Entities.Reservation> reservationRepository,
            IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }

        public Task<ReservationDto> Handle(GetReservationQuery request, CancellationToken cancellationToken)
        {
            var reservation = _reservationRepository.List(new ReservationByNumberSpec(request.ReservationNumber)).FirstOrDefault();

            var converted = _mapper.Map<ReservationDto>(reservation);

            return Task.FromResult(converted);
        }
    }
}
