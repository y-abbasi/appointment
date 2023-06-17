using System.Net;
using System.Net.Http;
using Suzianna.Core.Events;

namespace Suzianna.Rest.Events
{
    public class HttpRequestSentEvent : ISelfDescriptiveEvent
    {
        public HttpRequestSentEvent(HttpResponseMessage response)
        {
            ResponseCode = response.StatusCode;
            ResponseContent = response.Content?.ReadAsStringAsync().Result;
        }

        public HttpStatusCode ResponseCode { get; }
        public string ResponseContent { get; }

        public string Describe()
        {
            return $"Http response with status code '{ResponseCode}' received.";
        }
    }
}