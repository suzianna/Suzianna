using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Suzianna.Core.Events;

namespace Suzianna.Rest.Events
{
    public class HttpRequestSentEvent : IEvent
    {
        public HttpRequestSentEvent(HttpResponseMessage response)
        {
            this.ResponseCode = response.StatusCode;
            this.ResponseContent = response.Content?.ReadAsStringAsync().Result;
        }

        public HttpStatusCode ResponseCode { get; private set; }
        public string ResponseContent { get; private set; }
    }
}
