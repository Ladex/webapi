using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi2Book.Web.Common.ErrorHandling
{
    public class SimpleErrorResult:IHttpActionResult
    {
        private readonly HttpRequestMessage _requestMessage;
        private readonly HttpStatusCode _statusCode;
        private readonly string _errorMessage;

        public SimpleErrorResult(HttpRequestMessage requestMessage,HttpStatusCode statusCode,string errorMessage)
        {
            _requestMessage = requestMessage;
            _statusCode = statusCode;
            _errorMessage = errorMessage;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_requestMessage.CreateResponse(_statusCode, _errorMessage));
        }
    }
}