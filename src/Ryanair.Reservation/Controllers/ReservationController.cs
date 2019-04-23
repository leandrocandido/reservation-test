using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ryanair.Reservation.Controllers
{
    [Route("api/[controller]")]    
    public class ReservationController : Controller
    {
        private readonly IMediator _mediator;

        public ReservationController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        public IActionResult Get()
        {
            //var query = new GetAllAccountGroupsQuery();

            //var result = _mediator.Send(query).Result;

            //if (result.DomainValidationMessages?.Count() > 0)
            //    return StatusCode(422, result);

            return Ok();
        }
    }
}
