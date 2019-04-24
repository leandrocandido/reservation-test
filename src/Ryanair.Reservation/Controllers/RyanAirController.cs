using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ryanair.Reservation.Application.DTO;
using Ryanair.Reservation.Application.Mediator.Queries.Flight;
using Ryanair.Reservation.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Ryanair.Reservation.Controllers
{

    public class ResponseTeste
    {
        [XmlElement]
        public List<FlightDtoTeste> Content { get; set; }
        [XmlElement]
        public string Error { get; set; }
        public List<DomainValidationMessageTest> DomainValidationMessages { get; set; }
    }

    [Serializable]
    public class FlightDtoTeste
    {
        [XmlElement]
        public string Destination { get; set; }
        [XmlElement]
        public string Key { get; set; }
        [XmlElement]
        public string Origin { get; set; }
        [XmlElement(DataType = "date")]
        public DateTime Time { get; set; }
    }

    [Serializable]
    public class DomainValidationMessageTest
    {
        [XmlElement]
        public ValidationLevelTest Level { get; set; }
        [XmlElement]
        public string Property { get; set; }
        [XmlElement]
        public string Message { get; set; }
    }

    public enum ValidationLevelTest
    {
        [XmlEnum(Name = "Info")]
        Info,
        [XmlEnum(Name = "Warning")]
        Warning,
        [XmlEnum(Name = "Error")]
        Error
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


        //[HttpGet("Flight")]
        //public IActionResult GetFlightTeste()
        //{
        //    var query = new GetAllFlightsQuery();

        //    var result = _mediator.Send(query).Result;

        //    ResponseTeste teste = new ResponseTeste();

        //    teste.Error = "error";
        //    teste.Content = new List<FlightDtoTeste>()
        //    {
        //        new FlightDtoTeste
        //        {
        //            Destination = "asdasdas" ,
        //            Key = "fsdfsd" ,
        //            Origin = "fsdfsdf"
        //        },
        //        new FlightDtoTeste
        //        {
        //            Destination = "asdasdas",
        //        }
        //    };

        //    teste.DomainValidationMessages = new List<DomainValidationMessageTest>()
        //    {
        //        new DomainValidationMessageTest
        //        {
        //            Message = "fdsfsdfsd",
        //            Property = "dasdasdas"
        //        }
        //    };

        //    if (result.DomainValidationMessages?.Count() > 0)
        //        return StatusCode(StatusCodes.Status422UnprocessableEntity, result);

        //    return Ok(teste);
        //}

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
