using Ryanair.Reservation.Domain.Entities;
using System.Collections.Generic;

namespace Ryanair.Reservation.Infrastructure.DataAccess
{
    public class PassengersDatabase
    {
        private static PassengersDatabase _uniqueInstance = null;

        public List<Passengers> PassengersInformation { get; set; }

        private PassengersDatabase()
        {
        }

        public static PassengersDatabase GetInstance()
        {
            if (_uniqueInstance == null)
            {
                _uniqueInstance = new PassengersDatabase();
                _uniqueInstance.PassengersInformation = new List<Passengers>();
            }

            return _uniqueInstance;
        }
    }
}
