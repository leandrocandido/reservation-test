using System;
using Ryanair.Reservation.Domain.ValueObjects;
using Xunit;


namespace Ryanair.Reservation.Tests.Domain.Entities.ReservationAggregate
{
    public class ReservationTest
    {

        [Fact]
        public void CandAddReservationOk()
        {

            var problems = Ryanair.Reservation.Domain.Entities.Reservation.CanCreateReservation(GetReservationData());

            Assert.Empty(problems);
        }

        [Fact]
        public void ReservationEmailInvalid()
        {
            var reservationData = GetReservationData();
            reservationData.Email = null;
            var problems = Ryanair.Reservation.Domain.Entities.Reservation.CanCreateReservation(reservationData);

            Assert.NotEmpty(problems);
        }

        [Fact]
        public void ReservationCreditCardInvalid()
        {

            var reservationData = GetReservationData();
            reservationData.CreditCard = null;
            var problems = Ryanair.Reservation.Domain.Entities.Reservation.CanCreateReservation(reservationData);

            Assert.NotEmpty(problems);
        }

        private ReservationData GetReservationData()
        {
            return new ReservationData
            {
                Email = "leandro@gmail.com",
                CreditCard = "123456789" 
            };

        }


    }
}
