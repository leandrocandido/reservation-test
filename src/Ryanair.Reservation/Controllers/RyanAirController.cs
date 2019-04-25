using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ryanair.Reservation.Application.Mediator.Commands;
using Ryanair.Reservation.Application.Mediator.Queries.Flight;
using Ryanair.Reservation.Domain.Commands;
using Ryanair.Reservation.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ryanair.Reservation.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json", "application/xml")]   
    public class RyanairController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public RyanairController(IMediator mediator, ILogger<RyanairController> logger)
        {
            this._mediator = mediator;
            this._logger = logger;
        }

        [HttpGet("FlightAll")]
        public IActionResult GetFlightAll()
        {
            _logger.LogInformation("Getting all flights");
            var query = new GetAllFlightsQuery();

            var result = _mediator.Send(query).Result;

            if (result.DomainValidationMessages?.Count() > 0)
                return StatusCode(StatusCodes.Status422UnprocessableEntity, result);

            return Ok(result);
        }


        [HttpGet("Flight")]
        public IActionResult GetFlight(int passengers , string origin , string destination , DateTime dateOut , DateTime dateIn , bool roundTrip)
        {
            var query = new GetAllFlightsQuery();

            var result = _mediator.Send(query).Result;

            if (result.DomainValidationMessages?.Count() > 0)
                return StatusCode(StatusCodes.Status422UnprocessableEntity, result);          

            return Ok(result);
        }

        [HttpGet("Reservation")]
        public IActionResult GetReservation()
        {
            //SendData data = new SendData() { Nome = "nome yesye", Idade = 745 };
            BookingDto data = new BookingDto()
            {
                Email = "contact@contact.com",
                ReservationNumber = "0123456789012345",
                Flights = new List<BookFlightDto>()
                {
                    new BookFlightDto
                    {
                        Key = "Flight00052",
                        Passengers = new List<PassengersDto>()
                        {
                            new PassengersDto
                            {
                                Name = "Robert Plant",
                                Bags = 3,
                                Seat = "27"
                            },
                            new PassengersDto
                            {
                                Name = "Ozzy Osbourne",
                                Bags = 0,
                                Seat = "28"
                            }
                        }
                    },
                    new BookFlightDto
                    {
                        Key = "Flight00103",
                        Passengers = new List<PassengersDto>()
                        {
                            new PassengersDto
                            {
                                Name = "Robert Plant",
                                Bags = 2,
                                Seat = "41"
                            },
                            new PassengersDto
                            {
                                Name = "Ozzy Osbourne",                                
                                Seat = "40"
                            }
                        }
                    }
                }
            };
            
            return Ok(data);
        }

        [HttpPost("Reservation")]
        public IActionResult CreateReservation([FromBody] CreateReservationCommand command)
        {
            if (command == null)
                return BadRequest();

            var result = _mediator.Send(command).Result;

            if (result?.DomainValidationMessages?.Count() > 0 || !string.IsNullOrEmpty(result?.Error))
                return StatusCode(422, result);

            //check if content is valid?

            //if (string.IsNullOrEmpty(result?.Error))
            //    return NotFound();

            return Ok(result);
        }
    }
}
