using System;
namespace Ryanair.Reservation.Domain.Entities.ReservationAggregate
{
    public sealed class Reservation
    {
        private Reservation() { }
        private Reservation(string email, string creditCard)
        {
            this.Email = email;
            this.CreditCard = creditCard;
            this.ReservationNumber = null;
        }

        public string CreditCard { get; set; }
        public string ReservationNumber { get; set; }
        public string Email { get; set; }

        }
}
