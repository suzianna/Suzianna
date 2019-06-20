using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Suzianna.Core.Utilities;
using Suzianna.Rest.Serialization;

namespace Suzianna.Rest
{
    internal class HttpRequestBuilder
    {
        private HttpMethod _method;
        private object _content;
        private ContentType _contentType;
        private readonly Dictionary<string, string> _requestHeaders;
        private readonly UriFormatter _uriFormatter;
        public HttpRequestBuilder()
        {
            this._requestHeaders = new Dictionary<string, string>();
            this._uriFormatter = new UriFormatter();
        }
        public HttpRequestBuilder WithPostVerb()
        {
            this._method = HttpMethod.Post;
            return this;
        }
        public HttpRequestBuilder WithPatchVerb()
        {
            this._method = new HttpMethod("PATCH");
            return this;
        }
        public HttpRequestBuilder WithGetVerb()
        {
            this._method = HttpMethod.Get;
            return this;
        }
        public HttpRequestBuilder WithPutVerb()
        {
            this._method = HttpMethod.Put;
            return this;
        }
        public HttpRequestBuilder WithDeleteVerb()
        {
            this._method = HttpMethod.Delete;
            return this;
        }
        public HttpRequestBuilder WithBaseUrl(string url)
        {
            this._uriFormatter.SetBaseUrl(url);
            return this;
        }
        public HttpRequestBuilder WithResourceName(string resourceName)
        {
            this._uriFormatter.SetResourceName(resourceName);
            return this;
        }

        public HttpRequestBuilder WithQueryParameter(string key, string value)
        {
            this._uriFormatter.AddQueryParameter(key,value);
            return this;
        }
        public HttpRequestBuilder WithToken(string tokenValue, TokenTypes tokenTypes)
        {
            var authorizationHeader = AuthorizationHeaderFactory.Create(tokenValue, tokenTypes);
            this._requestHeaders.AddOrUpdate(authorizationHeader);
            return this;
        }
        public HttpRequestBuilder WithHeader(string key, string value)
        {
            this._requestHeaders.AddOrUpdate(key, value);
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
            this._content = content;
            this._contentType = contentType;
            return this;
        }
        public HttpRequestMessage Build()
        {
            var httpRequest = new HttpRequestMessage
            {
                Method = this._method,
                RequestUri = this._uriFormatter.ToUri()
            };
            if (HasContent())
            {
                httpRequest.Content = MakeContent();
                httpRequest.Content.Headers.ContentType = new MediaTypeHeaderValue(this._contentType.ToMediaType());
            }
            return AddHeadersToRequestHeaders(httpRequest);
        }
        private bool HasContent()
        {
            return this._content != null;
        }
        private HttpContent MakeContent()
        {
            return new StringContent(SerializeContent());
        }
        private string SerializeContent()
        {
            var serializer = SerializerFactory.Create(_contentType);
            return serializer.Serialize(this._content);
        }
        private HttpRequestMessage AddHeadersToRequestHeaders(HttpRequestMessage request)
        {
            foreach (var header in this._requestHeaders)
            {
                request.Headers.TryAddWithoutValidation(header.Key, header.Value);
                request.Content?.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }
            return request;
        }

      
    }
}
