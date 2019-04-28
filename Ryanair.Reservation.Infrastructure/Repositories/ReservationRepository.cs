namespace Ryanair.Reservation.Infrastructure.Repositories
{
    /// <summary>
    /// Repository of fligths.
    /// </summary>
    /// <remarks>
    /// Having separated repositories can help to build a cache layer or add some custom behavior.
    /// </remarks>
    public class ReservationRepository : RepositoryBase<Domain.Entities.Reservation>
    { }
}
