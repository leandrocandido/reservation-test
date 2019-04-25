using System.Collections.Generic;

namespace Ryanair.Reservation.Domain.Commands
{
    public interface ICreateReservationCommand
    {
        string Email { get; set; }
        string CreditCard { get; set; }
        List<CreateBookFlightCommand> Flights { get; set; }
    }
}
