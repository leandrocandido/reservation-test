using System.Collections.Generic;

namespace Ryanair.Reservation.Domain.DataAccess.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        public abstract IEnumerable<T> GetAll();        
    }
}
