using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ryanair.Reservation.Application.Mediator.Queries.Flight;
using System.Linq;

namespace Ryanair.Reservation.Controllers
{
    [Route("api/[controller]")]    
    public class RyanairController : Controller
    {
        private readonly IMediator _mediator;

        public RyanairController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet("Flight")]
        //[Produces("application/json", "application/xml")]
        //[Produces("text/xml")]
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
            //var query = new GetAllAccountGroupsQuery();

            //var result = _mediator.Send(query).Result;

            //if (result.DomainValidationMessages?.Count() > 0)
            //    return StatusCode(422, result);

            return Ok();
        }

        [HttpPost("Reservation")]
        public IActionResult CreateReservation()
        {
            //var query = new GetAllAccountGroupsQuery();

            //var result = _mediator.Send(query).Result;

            //if (result.DomainValidationMessages?.Count() > 0)
            //    return StatusCode(422, result);

            return Ok();
        }
    }
}
