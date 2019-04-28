using Ryanair.Reservation.Domain.Interfaces;
using Ryanair.Reservation.Domain.Specifications;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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