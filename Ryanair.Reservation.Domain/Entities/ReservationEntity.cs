using Ryanair.Reservation.Domain.Commands;
using Ryanair.Reservation.Domain.DataAccess.Repositories;
using Ryanair.Reservation.Domain.Interfaces;
using Ryanair.Reservation.Domain.Service;
using Ryanair.Reservation.Domain.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Ryanair.Reservation.Domain.Entities
{
    public class ReservationEntity
    {
        protected readonly IReservationRepository _reservationRepository;
        protected readonly ICreateReservationCommand _command;
        protected readonly IDomainValidation _rulesValidation;
        protected readonly IDomainValidation _fildsValidation;
        protected readonly IFlightRepository _flightRepository;

        public ReservationEntity() { }

        public ReservationEntity(ICreateReservationCommand command, IReservationRepository reservationRepository , IFlightRepository flightRepository)
        {
            _reservationRepository = reservationRepository;
            _flightRepository = flightRepository;
            _command = command;
            _rulesValidation = new ReservationRulesValidation(_reservationRepository, command);
            _fildsValidation = new ReservationFieldsValidation(_flightRepository,command);
            this.ValidateEntity();
            this.FillEntity();
        }              

        protected void ValidateEntity()
        {
            //do fields Validation
            _fildsValidation.ValidateCommand();

            //do rules validation
            _rulesValidation.ValidateCommand();           
        }

        void FillEntity()
        {
            this.ReservationNumber = GenerateReservationNumber();
            this.Email = this._command.Email;
            this. CreditCard = this._command.CreditCard;

            List<ReservationEntity> res = new List<ReservationEntity>();            

            //creates a list of reservation for each client in flight
            foreach (var flights in _command.Flights)
            {
                foreach (var pass in flights.Passengers)
                {
                    ReservationEntity entity = new ReservationEntity();
                    entity.ReservationNumber = this.ReservationNumber;
                    entity.Email = this.Email;
                    entity.CreditCard = this.CreditCard;
                    entity.Key = flights.Key;
                    entity.Bags = pass.Bags;
                    entity.Seat = pass.Seat;
                    entity.Name = pass.Name;

                    res.Add(entity);
                }
            }
            //save on database
            _reservationRepository.Save(res);
        }

        /// <summary>
        /// Generates Reservation Number ex AVR102
        /// </summary>
        /// <returns>The reservation number.</returns>
        protected string GenerateReservationNumber()
        {
            var reservation = RandomGenerator.RandomReservationNumber(100, 999, 3);

            //verify if rando reservation number is already in use
            while(_reservationRepository.ReservationNumberExists(reservation))
            {
                reservation = RandomGenerator.RandomReservationNumber(100, 999, 3); ;
            }

            return reservation;
        }

        public string ReservationNumber { get; set; }
        public string Email { get; set; }
        public string CreditCard { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public int Bags { get; set; }
        public string Seat { get; set; }      
    }
}
