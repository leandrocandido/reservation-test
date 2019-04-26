using Ryanair.Reservation.Domain.DTO;

namespace Ryanair.Reservation.Domain.Responses
{
    public class ReservationByCodeResponse : SingleHandleResponse
    {
        public BookingDto Content { get; set; }

        public override bool HasContent()
        {
            return this.Content != null;
        }
    }
}
