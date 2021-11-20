using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Suzianna.Core.Events;

namespace Suzianna.Rest.Events
{
    public class HttpRequestSentEvent : ISelfDescriptiveEvent
    {
        public HttpRequestSentEvent(HttpStatusCode responseCode, string responseContent)
        {
            ResponseCode = responseCode;
            ResponseContent = responseContent;
        }

        public HttpStatusCode ResponseCode { get; private set; }
        public string ResponseContent { get; private set; }
        public string Describe()
        {
            return $"Http response with status code '{ResponseCode}' received.";
        }
    }
}
