using Ryanair.Reservation.Domain.DataAccess.Repositories;
using Ryanair.Reservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ryanair.Reservation.Infrastructure.DataAccess.Repositories
{
    public class BookFlightRepository : RepositoryBase<BookFlight>, IBookFlightRepository
    {
        public override IEnumerable<BookFlight> GetAll()
        {
            var database = BookFlightDatabase.GetInstance();
            return database.BookFlightInformation;
        }
    }
}
