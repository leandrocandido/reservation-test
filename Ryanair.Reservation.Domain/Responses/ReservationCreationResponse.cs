using Ryanair.Reservation.Domain.DTO;

namespace Ryanair.Reservation.Domain.Responses
{
    public class ReservationCreationResponse : SingleHandleResponse
    {
        public ReservationInfoDto Content { get; set; }
    }
}
