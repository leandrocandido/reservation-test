using System;
using System.Collections.Generic;
using System.Text;

namespace Ryanair.Reservation.Domain.Commands
{
    public class CreatePassengerCommand : ICreatePassengerCommand
    {
        public string Name { get; set; }
        public int Bags { get; set; }
        public string Seat { get; set; }        
    }
}
