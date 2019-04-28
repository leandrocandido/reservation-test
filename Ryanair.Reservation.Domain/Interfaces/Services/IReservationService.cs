using Ryanair.Reservation.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ryanair.Reservation.Domain.Interfaces.Services
{
    public interface IReservationService
    {
        Entities.Reservation ConfirmReservation(ReservationData reservationData);
    }
}
