using Storage.Client.Interfaces;

namespace Storage.Client
{
    public class HttpClientFactory : IHttpClientFactory
    {
        public IHttpClient Create()
        {
            return new HttpClientWrapper(new HttpClient());
        }
    }
}
