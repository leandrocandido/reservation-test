using Ryanair.Reservation.Domain.Interfaces;
using Ryanair.Reservation.Domain.Utils;
using Ryanair.Reservation.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ryanair.Reservation.Domain.Entities
{
    public sealed class Reservation : IAggregateRoot
    {
        public Reservation(string email, string creditCard)
        {
            this.Email = email;
            this.CreditCard = creditCard;
            this.ReservationNumber = this.RandomReservationNumber();
        }

        public string CreditCard { get; private set; }
        public string ReservationNumber { get; set; }
        public string Email { get; set; }

        // Using IReadOnlyCollection as a wrapper around a private list, so is protected against "external updates".
        private readonly List<ReservationFlight> _flights = new List<ReservationFlight>();
        public IReadOnlyCollection<ReservationFlight> Flights => _flights.AsReadOnly();

        public void AddFlight(Flight flight, IEnumerable<Passenger> passengers)
        {
            this._flights.Add(new ReservationFlight(flight, passengers));
        }

        /// <summary>
        /// Generates and return a random number for reservation.
        /// </summary>
        /// <returns>A random string in the format GHT002</returns>
        private string RandomReservationNumber()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomGenerator.RandomString(3, false));
            builder.Append(RandomGenerator.RandomNumber(100, 999));

            return builder.ToString();
        }
    }
}