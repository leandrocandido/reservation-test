using Ryanair.Reservation.Domain.DTO;
using Ryanair.Reservation.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Ryanair.Reservation.Application.Extensions
{
    public static class ReservationExtension
    {
        public static BookingDto ConvertReservation(this List<ReservationEntity> reservations )
        {
            return reservations.GroupBy(x => new { x.ReservationNumber, x.Email, x.Key })
                .Select(x =>
                   new BookingDto
                   {
                       Email = x.Key.Email,
                       ReservationNumber = x.Key.ReservationNumber,
                       Flights = reservations.Where(f => f.Key == x.Key.Key)
                                   .GroupBy(f => new { f.Name, f.Seat, f.Bags, f.Key })
                                   .Select(f =>
                                       new BookFlightDto()
                                       {
                                           Key = f.Key.Key,
                                           Passengers = reservations.Where(p => p.ReservationNumber == x.Key.ReservationNumber && p.Key == f.Key.Key)
                                                            .GroupBy(p => new { p.Name, p.Bags, p.Seat })
                                                            .Select(p =>
                                                           new PassengersDto()
                                                           {
                                                               Bags = p.Key.Bags,
                                                               Seat = p.Key.Seat,
                                                               Name = p.Key.Name
                                                           }).Distinct().ToList()
                                       }).Distinct().ToList()
                   }).Distinct().FirstOrDefault();           
        }
    }
}
