using Ryanair.Reservation.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Ryanair.Reservation.Domain.Specifications
{
    /// <summary>
    /// Base class for specs just to enforce constructor with the creteria. It can be used to hold logic to sort, skip, take, etc.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SpecificationBase<T> : ISpecification<T> where T : IAggregateRoot
    {

        public ISpecification<T> And(ISpecification<T> specification)
        {
            return new AndSpecification<T>(this, specification);
        }

        public ISpecification<T> Or(ISpecification<T> specification)
        {
            return new OrSpecification<T>(this, specification);
        }

        public abstract Expression<Func<T, bool>> ToExpression();
    }
}
