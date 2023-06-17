using System.Net;
using Suzianna.Core.Screenplay;
using Suzianna.Core.Screenplay.Actors;
using Suzianna.Rest.Screenplay.Interactions;
using Suzianna.Rest.Screenplay.Questions;

namespace Suzianna.Rest.OAuth
{
    public class RopcFlowTask : ITask
    {
        private string endpointUrl;
        private string password;
        private string scope;
        private string username;

        internal RopcFlowTask()
        {
        }

        public void PerformAs<T>(T actor) where T : Actor
        {
            var requestBody = GetRequestBody();
            actor.AttemptsTo(Post.Data(requestBody).To(endpointUrl));
            if (actor.AsksFor(LastResponse.StatusCode()) == HttpStatusCode.OK)
            {
                var response = actor.AsksFor(LastResponse.Content<OAuthToken>());
                actor.Remember(TokenConstants.TokenKey, response);
            }
        }

        private string GetRequestBody()
        {
            var body = $"grant_type=password&username={username}&password={password}";
            if (!string.IsNullOrEmpty(scope))
                body += $"&scope={scope}";
            return body;
        }

        public RopcFlowTask WithCredentials(string username, string password)
        {
            this.username = username;
            this.password = password;
            return this;
        }

        public RopcFlowTask FromEndpoint(string endpoint)
        {
            endpointUrl = endpoint;
            return this;
        }

        public RopcFlowTask WithScope(string scope)
        {
            this.scope = scope;
            return this;
        }
    }
}