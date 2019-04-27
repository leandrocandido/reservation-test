using System;
using System.Collections.Generic;

namespace Ryanair.Reservation.Domain.Interfaces
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IEnumerable<T> List();
        IEnumerable<T> List(ISpecification<T> specification);

        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
