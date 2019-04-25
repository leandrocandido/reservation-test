namespace Ryanair.Reservation.Domain.Commands
{
    public interface ICreatePassengerCommand
    {
        string Name { get; set; }
        int Bags { get; set; }
        string Seat { get; set; }
    }
}
