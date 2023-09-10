using Storage.Client.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Client.UnitTests
{
    public class TestsHttpClientFactory : IHttpClientFactory
    {
        private readonly IHttpClient _httpClient;
        public TestsHttpClientFactory(IHttpClient client) {
            _httpClient = client;
        }
        public IHttpClient Create()
        {
            return _httpClient;
        }
    }
}
