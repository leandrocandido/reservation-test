using Ryanair.Reservation.Domain.Entities;
using System.Collections.Generic;

namespace Ryanair.Reservation.Infrastructure.DataAccess
{
    public class ReservationDataTable
    {
        private static ReservationDataTable _uniqueInstance = null;

        public List<ReservationEntity> ReservationInformation { get; set; }

        private ReservationDataTable()
        {
        }

        public static ReservationDataTable GetInstance()
        {
            if (_uniqueInstance == null)
            {
                _uniqueInstance = new ReservationDataTable();
                _uniqueInstance.ReservationInformation = new List<ReservationEntity>();                
            }

            return _uniqueInstance;
        }
    }
}
