using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ryanair.Reservation.Application.Mediator.Commands;

namespace Ryanair.Reservation.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json", "application/xml")]
    public class ReservationsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public ReservationsController(IMediator mediator, ILogger<ReservationsController> logger)
        {
            this._mediator = mediator;
            this._logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationCommand command)
        {
            if (command == null)
                return BadRequest();

            var result = await _mediator.Send(command);

            return Created(Url.RouteUrl(result), result);
        }
    }

}
