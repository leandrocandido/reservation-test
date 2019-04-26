using Ryanair.Reservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Ryanair.Reservation.Infrastructure.DataAccess
{
    /// <summary>
    /// Singleton used to simulate a non normalized flight table
    /// </summary>
    public class FlightDatabase
    {
        private static FlightDatabase _uniqueInstance = null;

        public List<Flight> FlightInformation { get; private set; }

        private FlightDatabase()
        {           
        }

        public static FlightDatabase GetInstance()
        {
            if (_uniqueInstance == null)
            {
                _uniqueInstance = new FlightDatabase();
                _uniqueInstance.InitialLoad();
            }

            return _uniqueInstance;
        }

        private void InitialLoad()
        {
            FlightInformation = new List<Flight>()
            {
                new Flight
                {
                    Time = DateTime.ParseExact("2017-05-08T06:30:00.000Z","yyyy-MM-dd'T'HH:mm:ss.fff'Z'", null),
                    Key =  "Flight00001",
                    Origin = "DUBLIN",
                    Destination = "LONDON"
                },
                new Flight
                {
                    Time = DateTime.ParseExact("2017-05-08T12:00:00.000Z","yyyy-MM-dd'T'HH:mm:ss.fff'Z'", null),
                    Key =  "Flight00052",
                    Origin = "DUBLIN",
                    Destination = "LONDON"
                },
                new Flight
                {
                    Time = DateTime.ParseExact("2017-05-10T09:30:00.000Z","yyyy-MM-dd'T'HH:mm:ss.fff'Z'", null),
                    Key =  "Flight00103",
                    Origin = "LONDON",
                    Destination = "DUBLIN"
                },
                new Flight
                {
                    Time = DateTime.ParseExact("2017-05-09T11:30:00.000Z","yyyy-MM-dd'T'HH:mm:ss.fff'Z'", null),
                    Key =  "Flight10001",
                    Origin = "LONDON",
                    Destination = "DUBLIN"
                },
                new Flight
                {
                    Time = DateTime.ParseExact("2017-05-09T15:00:00.000Z","yyyy-MM-dd'T'HH:mm:ss.fff'Z'", null),
                    Key =  "Flight10052",
                    Origin = "LONDON",
                    Destination = "DUBLIN"
                },
                new Flight
                {
                    Time = DateTime.ParseExact("2017-05-11T12:30:00.000Z","yyyy-MM-dd'T'HH:mm:ss.fff'Z'", null),
                    Key =  "Flight10103",
                    Origin = "LONDON",
                    Destination = "DUBLIN"
                },
                new Flight
                {
                    Time = DateTime.ParseExact("2017-05-13T10:30:00.000Z","yyyy-MM-dd'T'HH:mm:ss.fff'Z'", null),
                    Key =  "Flight00021",
                    Origin = "DUBLIN",
                    Destination = "ROME"
                },
                new Flight
                {
                    Time = DateTime.ParseExact("2017-05-13T18:30:00.000Z","yyyy-MM-dd'T'HH:mm:ss.fff'Z'", null),
                    Key =  "Flight10021",
                    Origin = "ROME",
                    Destination = "DUBLIN"
                }
            };
        }


    }
}
