using System.Net.Http;
using Suzianna.Core.Events;
using Suzianna.Core.Screenplay.Actors;

namespace Suzianna.Rest.Events
{
    public class StartSendingHttpRequestEvent : ISelfDescriptiveEvent
    {
        public StartSendingHttpRequestEvent(Actor actor, HttpRequestMessage message)
        {
            Method = message.Method;
            Url = message.RequestUri.AbsoluteUri;
            ActorName = actor.Name;
        }

        public string Url { get; }
        public HttpMethod Method { get; }
        public string ActorName { get; set; }

        public string Describe()
        {
            return $"{ActorName} is going to send a '{Method}' request to '{Url}'.";
        }
    }
}