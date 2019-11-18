using Microsoft.Rest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace CloudBacktesting.ApiGateWay.Business.UserService
{
    public class ClientServiceProvider : ServiceClient<ClientServiceProvider>, IClientServiceProvider
    {
        public Uri BaseUri { get; set; }

        public JsonSerializerSettings SerializationSettings { get; set; }

        public JsonSerializerSettings DeserializationSettings  { get; set; }
        private HttpRestClientServicesProvider(params DelegatingHandler[] handlers) : base(handlers)
        {
            this.Initialize();
        }

        private HttpRestClientServicesProvider(HttpClientHandler rootHandler, params DelegatingHandler[] handlers) : base(rootHandler, handlers)
        {
            this.Initialize();
        }
    }
}
