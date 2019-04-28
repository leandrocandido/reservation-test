using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ryanair.Reservation.Application.Mediator.Commands;
using Ryanair.Reservation.Application.Mediator.Queries.Flight;
using Ryanair.Reservation.Application.Mediator.Queries.Reservation;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ryanair.Reservation.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json", "application/xml")]
    public class FlightsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public FlightsController(IMediator mediator, ILogger<FlightsController> logger)
        {
            this._mediator = mediator;
            this._logger = logger;
        }

        [HttpGet()]
        public async Task<IActionResult> GetByParamsAsync(GetFlightsByParamQuery requestParams)
        {
            var result = await _mediator.Send(requestParams);

            return Ok(result);
        }

    }
}
