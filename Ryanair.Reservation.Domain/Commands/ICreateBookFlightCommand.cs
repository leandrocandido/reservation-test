using System.Collections.Generic;

namespace Ryanair.Reservation.Domain.Commands
{
    public interface ICreateBookFlightCommand
    {
        string Key { get; set; }
        List<CreatePassengerCommand> Passengers { get; set; }
    }
}
