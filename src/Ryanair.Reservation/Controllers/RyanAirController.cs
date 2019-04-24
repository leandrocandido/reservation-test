using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ryanair.Reservation.Application.Mediator.Queries.Flight;
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

        public RyanairController(IMediator mediator)
        {
            this._mediator = mediator;
        }


        [HttpGet("Flight")]
        public IActionResult GetFlight()
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
