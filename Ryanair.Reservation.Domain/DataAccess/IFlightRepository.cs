using System.Collections.Generic;

namespace Ryanair.Reservation.Domain.DataAccess
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
    }
}
