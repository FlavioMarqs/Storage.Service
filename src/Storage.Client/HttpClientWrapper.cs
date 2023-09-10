using Storage.Client.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Client
{
    public class HttpClientWrapper : IHttpClient
    {
        private readonly HttpClient _httpClient;
        public HttpClientWrapper(HttpClient httpClient) 
        { 
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return _httpClient.SendAsync(request);
        }
    }
}
