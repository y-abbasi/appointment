using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Suzianna.Core.Screenplay.Questions;

namespace Suzianna.Rest.Screenplay.Questions
{
    public static class LastResponse
    {
        public static IQuestion<HttpStatusCode> StatusCode()
        {
            return new LastResponseStatusCode();
        }

        public static IQuestion<T> Content<T>()
        {
            return new LastResponseTypedContent<T>();
        }

        public static IQuestion<string> Header(string key)
        {
            return new LastResponseHeader(key);
        }

        public static IQuestion<HttpResponseHeaders> Headers()
        {
            return new LastResponseHeaders();
        }

        public static IQuestion<HttpResponseMessage> Raw()
        {
            return new LastResponseRaw();
        }
    }
}