using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ryanair.Reservation.Application.Mediator.Commands;
using Ryanair.Reservation.Application.Mediator.Queries.Flight;
using Ryanair.Reservation.Application.Mediator.Queries.Reservation;
using System;
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

        [HttpGet("Reservation/{reservationCode}")]
        public IActionResult GetReservation(string reservationCode)
        {
            var query = new GetReservationQuery() { ReservationNumber = reservationCode };

            if ( string.IsNullOrEmpty(query.ReservationNumber))
                return BadRequest();

            var result = _mediator.Send(query).Result;

            if (result.DomainValidationMessages?.Count() > 0)
                return StatusCode(StatusCodes.Status422UnprocessableEntity, result);

            if (!result.HasContent())
                return StatusCode(StatusCodes.Status204NoContent, result);

            return Ok(result);
        }

        [HttpPost("Reservation")]
        public IActionResult CreateReservation([FromBody] CreateReservationCommand command)
        {
            if (command == null)
                return BadRequest();

            var result = _mediator.Send(command).Result;

            if (result?.DomainValidationMessages?.Count() > 0 || !string.IsNullOrEmpty(result?.Error))
                return StatusCode(422, result);          

            return Ok(result);
        }
    }
}
