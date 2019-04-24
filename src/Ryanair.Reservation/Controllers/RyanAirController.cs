using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ryanair.Reservation.Application.Mediator.Queries.Flight;
using Ryanair.Reservation.Infrastructure.Utils;
using System;
using System.Linq;

namespace Ryanair.Reservation.Controllers
{

    public class SendData
    {
        public string Nome { get; set; }
        public int Idade { get; set; }
    }


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
            SendData data = new SendData() { Nome = "nome yesye", Idade = 745 };

            var reservation = RandomGenerator.RandomReservationNumber(100, 999, 3);

            return Ok(data);
        }

        [HttpPost("Reservation")]
        public IActionResult CreateReservation([FromBody] SendData command)
        {
            if (command == null)
                return BadRequest();
            //var query = new GetAllAccountGroupsQuery();

            //var result = _mediator.Send(query).Result;

            //if (result.DomainValidationMessages?.Count() > 0)
            //    return StatusCode(422, result);

            return Ok();
        }
    }
}
