using Ryanair.Reservation.Domain.Constants;
using Ryanair.Reservation.Domain.Interfaces;
using Ryanair.Reservation.Domain.Validation;
using Ryanair.Reservation.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ryanair.Reservation.Domain.Entities
{
    public class Flight : IAggregateRoot
    {
        public DateTime Time { get; private set; }
        public string Key { get; private set; }
        public string Origin { get; private set; }
        public string Destination { get; private set; }
        
        // Using IReadOnlyCollection as a wrapper around a private list,
        // the only way to add passenger is through AddPassenger method, so is protected against "external updates".
        private readonly List<Passenger> _passengers = new List<Passenger>();
        public IReadOnlyCollection<Passenger> Passengers => _passengers.AsReadOnly();

        /// <summary>
        /// Adds a passenger to the flight.
        /// </summary>
        /// <param name="passengerData">The passenger's data.</param>
        /// <returns>The new passenger instance added to the list.</returns>
        public Passenger AddPassenger(PassengerData passengerData)
        {
            var errors = CanAddPassenger(passengerData);


            var passenger = new Passenger(passengerData.Name, passengerData.Bags, passengerData.Seat, this);

            this._passengers.Add(passenger);

            return passenger;
        }

        /// <summary>
        /// Check if passenger data is valid.
        /// </summary>
        /// <param name="passengerData">An <see cref="Passenger"/> object to validate.</param>
        /// <returns>An <see cref="IEnumerable{DomainValidationMessage}"/> containing the found errors.</returns>
        public IEnumerable<DomainValidationMessage> CanAddPassenger(PassengerData passengerData)
        {
            var errors = new List<DomainValidationMessage>();
            if (string.IsNullOrEmpty(passengerData.Name))
                errors.Add(new DomainValidationMessage { Level = ValidationLevel.Error, Message = Resources.Language.PassengerNameRequired });

            if (string.IsNullOrEmpty(passengerData.Seat))
                errors.Add(new DomainValidationMessage { Level = ValidationLevel.Error, Message = Resources.Language.SeatNumberMandatory });

            int.TryParse(passengerData.Seat, out int seatNumber);
            if (seatNumber < RyanairConstants.INITIAL_SEAT || seatNumber > RyanairConstants.FINAL_SEAT)
                errors.Add(new DomainValidationMessage
                {
                    Level = ValidationLevel.Error,
                    Message = string.Format(Resources.Language.InvalidSeatNumber, passengerData.Seat)
                });

            if (this.IsSeatFree(passengerData.Seat))
                errors.Add(new DomainValidationMessage
                {
                    Level = ValidationLevel.Error,
                    Message = string.Format(Resources.Language.SeatInUse, passengerData.Seat, this.Key)
                });

            if (passengerData.Bags > RyanairConstants.MAX_BAGS_PASSENGER)
                errors.Add(new DomainValidationMessage
                {
                    Level = ValidationLevel.Error,
                    Message = string.Format(Resources.Language.MaxBagsPerUser, passengerData.Name)
                });

            if (this.HasBaggageSpace(passengerData.Bags))
                errors.Add(new DomainValidationMessage
                {
                    Level = ValidationLevel.Error,
                    Message = string.Format(Resources.Language.ThereIsNoSpaceForBags, passengerData.Bags, this.Key)
                });

            return errors;
        }

        /// <summary>
        /// Check if a seat number is available.
        /// </summary>
        /// <param name="seat"></param>
        /// <returns></returns>
        public bool IsSeatFree(string seat)
        {
            return this._passengers.Select(p => p.Seat).Any(s => s == seat);
        }

        /// <summary>
        /// Check if flight has room for a number of bags.
        /// </summary>
        /// <param name="quantity">The number of bags space needed.</param>
        /// <returns>True if there is the required space, otherwise false.</returns>
        public bool HasBaggageSpace(int quantity)
        {
            return this.Passengers.Sum(c => c.Bags) <= (RyanairConstants.MAX_BAGS - quantity);
        }
    }
}
