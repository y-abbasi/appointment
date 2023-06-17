using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Suzianna.Core.Utilities;
using Suzianna.Rest.Serialization;

namespace Suzianna.Rest
{
    internal class HttpRequestBuilder
    {
        private readonly Dictionary<string, string> _requestHeaders;
        private readonly UriFormatter _uriFormatter;
        private object _content;
        private ContentType _contentType;
        private HttpMethod _method;

        private bool flag;

        public HttpRequestBuilder()
        {
            _requestHeaders = new Dictionary<string, string>();
            _uriFormatter = new UriFormatter();
        }

        public HttpRequestBuilder WithPostVerb()
        {
            _method = HttpMethod.Post;
            return this;
        }

        public HttpRequestBuilder WithPatchVerb()
        {
            _method = new HttpMethod("PATCH");
            return this;
        }

        public HttpRequestBuilder WithGetVerb()
        {
            _method = HttpMethod.Get;
            return this;
        }

        public HttpRequestBuilder WithPutVerb()
        {
            _method = HttpMethod.Put;
            return this;
        }

        public HttpRequestBuilder WithDeleteVerb()
        {
            _method = HttpMethod.Delete;
            return this;
        }

        public HttpRequestBuilder WithBaseUrl(string url)
        {
            if (flag == false)
            {
                flag = true;
                _uriFormatter.SetBaseUrl(url);
            }

            return this;
        }

        public HttpRequestBuilder WithResourceName(string resourceName)
        {
            _uriFormatter.SetResourceName(resourceName);
            return this;
        }

        public HttpRequestBuilder WithQueryParameter(string key, string value)
        {
            _uriFormatter.AddQueryParameter(key, value);
            return this;
        }

        public HttpRequestBuilder WithToken(string tokenValue, TokenTypes tokenTypes)
        {
            var authorizationHeader = AuthorizationHeaderFactory.Create(tokenValue, tokenTypes);
            _requestHeaders.AddOrUpdate(authorizationHeader);
            return this;
        }

        public HttpRequestBuilder WithHeader(string key, string value)
        {
            _requestHeaders.AddOrUpdate(key, value);
            return this;
        }

        public HttpRequestBuilder WithContentAsJson(object content)
        {
            return WithContent(content, ContentType.Json);
        }

        public HttpRequestBuilder WithContentAsXml(object content)
        {
            return WithContent(content, ContentType.Xml);
        }

        public HttpRequestBuilder WithContentAsPlainText(string content)
        {
            return WithContent(content, ContentType.PlainText);
        }

        private HttpRequestBuilder WithContent(object content, ContentType contentType)
        {
            _content = content;
            _contentType = contentType;
            return this;
        }

        public HttpRequestMessage Build()
        {
            var httpRequest = new HttpRequestMessage
            {
                Method = _method,
                RequestUri = _uriFormatter.ToUri()
            };
            if (HasContent())
            {
                httpRequest.Content = MakeContent();
                httpRequest.Content.Headers.ContentType = new MediaTypeHeaderValue(_contentType.ToMediaType());
            }

            return AddHeadersToRequestHeaders(httpRequest);
        }

        private bool HasContent()
        {
            return _content != null;
        }

        private HttpContent MakeContent()
        {
            return new StringContent(SerializeContent());
        }

        private string SerializeContent()
        {
            var serializer = SerializerFactory.Create(_contentType);
            return serializer.Serialize(_content);
        }

        private HttpRequestMessage AddHeadersToRequestHeaders(HttpRequestMessage request)
        {
            foreach (var header in _requestHeaders)
            {
                request.Headers.TryAddWithoutValidation(header.Key, header.Value);
                request.Content?.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            return request;
        }
    }
}