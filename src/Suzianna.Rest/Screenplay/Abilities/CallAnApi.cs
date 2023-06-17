using System.Collections.Generic;
using System.Net.Http;
using Suzianna.Core.Screenplay;
using Suzianna.Rest.Interception;

namespace Suzianna.Rest.Screenplay.Abilities
{
    public class CallAnApi : IAbility
    {
        private readonly List<IHttpInterceptor> _interceptors;
        private IHttpRequestSender _sender;

        private CallAnApi(string baseUrl)
        {
            BaseUrl = baseUrl;
            _interceptors = new List<IHttpInterceptor>();
        }

        public string BaseUrl { get; }
        public HttpResponseMessage LastResponse { get; private set; }

        public static CallAnApi At(string baseUrl)
        {
            var ability = new CallAnApi(baseUrl);
            return ability.With(new DefaultHttpRequestSender());
        }

        public CallAnApi With(IHttpRequestSender sender)
        {
            _sender = sender;
            return this;
        }

        public CallAnApi WhichRequestsInterceptedBy(IHttpInterceptor interceptor)
        {
            _interceptors.Add(interceptor);
            return this;
        }

        public void SendRequest(HttpRequestMessage message)
        {
            foreach (var interceptor in _interceptors) message = interceptor.Intercept(message);
            LastResponse = _sender.Send(message);
        }
    }
}