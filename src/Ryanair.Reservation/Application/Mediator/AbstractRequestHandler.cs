using MediatR;
using Ryanair.Reservation.Domain.Interfaces;
using Ryanair.Reservation.Domain.Responses;
using Ryanair.Reservation.Domain.Validation;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ryanair.Reservation.Application.Mediator
{
    public abstract class AbstractRequestHandler<T> : IRequestHandler<T, IHandleResponse> where T : IRequest<IHandleResponse>
    {
        internal abstract IHandleResponse HandleIt(T request, CancellationToken cancellationToken);

        public Task<IHandleResponse> Handle(T request, CancellationToken cancellationToken)
        {            
            IHandleResponse result = new SingleHandleResponse();

            if (request == null)
                return Task.FromResult(result);

            try
            {
                result = HandleIt(request, cancellationToken);                

            }
            catch (DomainValidationException ex)
            {             
                result.DomainValidationMessages = ex.ValidationError.ToList();
            }
            catch (Exception ex)
            {             
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                var file = Path.GetFileName(frame.GetFileName());
                result.Error = $"Exception error: {ex.Message} file name: {file} - line {line}";
            }

            return Task.FromResult(result);
        }
    }
}
