using System;
using System.Collections.Generic;
using System.Text;

namespace Ryanair.Reservation.Domain.Commands
{
    public class CreateBookFlightCommand : ICreateBookFlightCommand
    {
        public string Key { get; set; }
        public List<CreatePassengerCommand> Passengers { get; set; }
    }
}
