using System;
using System.Collections.Generic;
using System.Linq;
using Suzianna.Core.Utilities;

namespace Suzianna.Rest
{
    internal class UriFormatter
    {
        private string _baseUrl;
        private readonly Dictionary<string, string> _queryParameters = new();
        private string _resourceName;

        public void SetBaseUrl(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public void SetResourceName(string resourceName)
        {
            var queryParameters = QueryStringUtil.ExtractParameters(resourceName);
            queryParameters.ForEach(a => AddQueryParameter(a.Key, a.Value));

            if (resourceName.Contains("?"))
                resourceName = resourceName.Substring(0, resourceName.IndexOf("?", StringComparison.Ordinal));

            _resourceName = resourceName;
        }

        public void AddQueryParameter(string key, string value)
        {
            _queryParameters.AddOrUpdate(key, value);
        }

        public Uri ToUri()
        {
            var baseUri = new Uri(_baseUrl, UriKind.Absolute);
            var url = _resourceName;
            if (_queryParameters.Any())
            {
                var query = string.Join("&", _queryParameters.Select(a => a.Key + "=" + a.Value).ToList());
                url += "?" + query;
            }

            return new Uri(baseUri, url);
        }
    }
}