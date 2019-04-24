using Ryanair.Reservation.Domain.DTO;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Ryanair.Reservation.Domain.Responses
{
    public class ResponseFlight : SingleHandleResponse
    {
        [XmlElement]
        public List<FlightDto> Content { get; set; }     
    }   
}
