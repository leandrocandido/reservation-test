using Ryanair.Reservation.Domain.Interfaces;
using Ryanair.Reservation.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ryanair.Reservation.Infrastructure.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : IAggregateRoot
    {
        protected List<T> collection = new List<T>();

        public void Delete(T entity) => collection.Remove(entity);

        public void Insert(T entity) => collection.Add(entity);

        public IEnumerable<T> List() => collection;

        public IEnumerable<T> List(ISpecification<T> specification) => collection.AsQueryable().Where(specification.ToExpression());

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
