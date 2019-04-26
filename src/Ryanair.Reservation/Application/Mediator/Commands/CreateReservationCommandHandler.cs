using System;
using System.Threading;
using AutoMapper;
using Ryanair.Reservation.Domain.DataAccess.Repositories;
using Ryanair.Reservation.Domain.DTO;
using Ryanair.Reservation.Domain.Entities;
using Ryanair.Reservation.Domain.Interfaces;
using Ryanair.Reservation.Domain.Responses;
using Ryanair.Reservation.Domain.Service;

namespace Ryanair.Reservation.Application.Mediator.Commands
{
    public class CreateReservationCommandHandler : AbstractRequestHandler<CreateReservationCommand>
    {       
        private readonly IFlightRepository _flightRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;        

        public CreateReservationCommandHandler(IFlightRepository flightRepository,IReservationRepository reservationRepository, IMapper mapper)
        {           
            _flightRepository = flightRepository;            
            _reservationRepository = reservationRepository;
            _mapper = mapper;            
        }

        internal override IHandleResponse HandleIt(CreateReservationCommand request, CancellationToken cancellationToken)
        {   
            //create and save reservation information.
            var reserv = new ReservationEntity(request, _reservationRepository, _flightRepository);
            //auto mapper.
            var converted = _mapper.Map<ReservationInfoDto>(reserv);
            //format to reservation creation response.
            return new ReservationCreationResponse() { Content = converted };                     
        }
    }
}
