using System;
using Ryanair.Reservation.Domain.ValueObjects;

namespace Ryanair.Reservation.Domain.Interfaces.Services
{
    public interface IReservationService
    {
        Entities.Reservation ConfirmReservation(ReservationData reservationData);
    }
}
