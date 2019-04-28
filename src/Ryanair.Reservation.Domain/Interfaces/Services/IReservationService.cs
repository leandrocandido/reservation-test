using System;
using Ryanair.Reservation.Domain.ValueObjects;

namespace Ryanair.Reservation.Domain.Interfaces.Services
{
    public interface IReservationService
    {
        Entities.ReservationAggregate.Reservation ConfirmReservation(ReservationData reservationData);
    }
}
