using System;
using System.Threading;
using AutoMapper;
using Ryanair.Reservation.Domain.DataAccess.Repositories;
using Ryanair.Reservation.Domain.DTO;
using Ryanair.Reservation.Domain.Entities;
using Ryanair.Reservation.Domain.Interfaces;
using Ryanair.Reservation.Domain.Responses;

namespace Ryanair.Reservation.Application.Mediator.Commands
{
    public class CreateReservationCommandHandler : AbstractRequestHandler<CreateReservationCommand>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IBookFlightRepository _bookFlightRepository;
        private readonly IFlightRepository _flightRepository;
        private readonly IPassengerRepository _passengeringRepository;
        private readonly IMapper _mapper;

        public CreateReservationCommandHandler(
            IBookingRepository bookingRepository,
            IBookFlightRepository bookFlightRepository,
            IFlightRepository flightRepository,
            IPassengerRepository passengeringRepository,
            IMapper mapper
        )
        {
            _bookingRepository = bookingRepository;
            _bookFlightRepository = bookFlightRepository;
            _flightRepository = flightRepository;
            _passengeringRepository = passengeringRepository;
            _mapper = mapper;
        }

        internal override IHandleResponse HandleIt(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var book = new Booking(_bookingRepository, _bookFlightRepository, _flightRepository, _passengeringRepository, request);
            var converted = _mapper.Map<ReservationInfoDto>(book);
            return new ReservationCreationResponse() { Content = converted };
            
        }
    }
}
