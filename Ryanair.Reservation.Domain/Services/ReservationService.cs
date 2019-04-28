using Ryanair.Reservation.Domain.Entities;
using Ryanair.Reservation.Domain.Interfaces;
using Ryanair.Reservation.Domain.Interfaces.Services;
using Ryanair.Reservation.Domain.Resources;
using Ryanair.Reservation.Domain.Specifications;
using Ryanair.Reservation.Domain.Validation;
using Ryanair.Reservation.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace Ryanair.Reservation.Domain.Services
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ReservationService : IReservationService
    {
        private readonly IRepository<Flight> _flightRepository;

        public ReservationService(IRepository<Flight> flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public Entities.Reservation ConfirmReservation(ReservationData reservationData)
        {
            var problems = new List<DomainValidationMessage>();


            if ( (reservationData?.Flights == null) || ( reservationData?.Flights?.Count == 0 ) )
            {
                problems.Add(new DomainValidationMessage
                {
                    Level = ValidationLevel.Error,
                    Message = Language.NoFlightInformation,
                    Property = "Flights"
                });
                throw new DomainValidationException(problems);
            }


            // First iterate and validate the data to guarantee everithing is valid, and the passengers can be added to both flights.
            // Doing this because we skipped the persistence layer and transactions.
            // We add validated info to a dictionary so we don't need to query flights again.
            var validatedFlights = new Dictionary<Flight, IEnumerable<PassengerData>>();

            foreach (FlightData fData in reservationData.Flights)
            {
                Flight flight = _flightRepository.List(new FlightByKeySpec(fData.Key)).FirstOrDefault();

                if (flight == null)
                {
                    problems.Add(new DomainValidationMessage
                    {
                        Level = ValidationLevel.Error,
                        Message = Language.FlightNotExists,
                        Property = "Flights"
                    });
                    throw new DomainValidationException(problems);
                }

                validatedFlights.Add(flight, fData.Passengers);

                // Validate each passenger.
                foreach (var passenger in fData.Passengers)
                {
                    problems.AddRange(flight.CanAddPassenger(passenger));
                }
            }

            problems.AddRange(Entities.Reservation.CanCreateReservation(reservationData));

            // If there is any problems with data we throw an exception.
            if (problems.Any())
                throw new DomainValidationException(problems);

            var reservation = Entities.Reservation.CreateReservation(reservationData);

            // Now we have sure everything is right lets create the reservation.

            // Now we add the passengers to the flight.
            // Using the dictionary we save some roud trips to repository.
            foreach (KeyValuePair<Flight, IEnumerable<PassengerData>> pair in validatedFlights)
            {
                var flight = pair.Key;
                var passengersAdded = new List<Passenger>();

                foreach (var passengerData in pair.Value)
                {
                    var passenger = flight.AddPassenger(passengerData);
                    passengersAdded.Add(passenger);
                }

                // Add the flight to the resrevation.
                reservation.AddFlight(flight, passengersAdded);
            }

            return reservation;
        }
    }
}
