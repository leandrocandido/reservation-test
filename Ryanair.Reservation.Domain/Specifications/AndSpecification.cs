using Ryanair.Reservation.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Ryanair.Reservation.Domain.Specifications
{
    /// <summary>
    /// Combines two specifications using && (And) operator.
    /// </summary>
    /// <typeparam name="T">An implementation of <see cref="IAggregateRoot"/>.</typeparam>
    public class AndSpecification<T> : SpecificationBase<T> where T : IAggregateRoot
    {
        private readonly ISpecification<T> _left;
        private readonly ISpecification<T> _right;

        public AndSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            _right = right;
            _left = left;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> leftExpression = _left.ToExpression();
            Expression<Func<T, bool>> rightExpression = _right.ToExpression();

            BinaryExpression andExpression = Expression.AndAlso(leftExpression.Body, rightExpression.Body);

            return Expression.Lambda<Func<T, bool>>(andExpression, leftExpression.Parameters.Single());
        }

    }


}
