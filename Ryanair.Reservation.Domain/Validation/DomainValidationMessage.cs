using System.Runtime.Serialization;

namespace Ryanair.Reservation.Domain.Validation
{
    [DataContract]
    public class DomainValidationMessage
    {        
        public ValidationLevel Level { get; set; }        
        public string Property { get; set; }        
        public string Message { get; set; }

        public DomainValidationMessage(ValidationLevel level, string message, params object[] messageParams)
            : this(level, null, message, messageParams) { }

        public DomainValidationMessage(ValidationLevel level, string property, string message, params object[] messageParams)
        {
            if (messageParams.Length > 0)
                message = string.Format(message, messageParams);

            this.Message = message;
            this.Level = level;
            this.Property = property;

        }
    }
}
