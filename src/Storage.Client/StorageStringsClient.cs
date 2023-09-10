using Storage.Client.Interfaces;
using Storage.DTOs.Requests;
using Storage.DTOs.Responses;
using System.Text.Json;

namespace Storage.Client
{
    public class StorageStringsClient : IStorageStringsClient
    {
        private readonly StorageClientOptions _options;
        private readonly IHttpClientFactory _httpClientFactory;
        public StorageStringsClient(StorageClientOptions options, IHttpClientFactory httpClientFactory)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public Task<HttpResponseMessage> DeleteStringAsync(StringRemovalRequest request)
            => throw new NotImplementedException();

        public async Task<HttpResponseMessage> GetAllStringsAsync(StringsQueryRequest request)
            => await SendAsync(HttpMethod.Get, $"{_options.StorageStringsUrl}/all/{request.IncludeDeleted}");

        public async Task<HttpResponseMessage> GetStringByIdAsync(StringQueryRequest request)
            => await SendAsync(HttpMethod.Get, $"{_options.StorageStringsUrl}/{request.Identifier}");


        public async Task<HttpResponseMessage> PutStringAsync(StringCreationRequest request)
            => await SendAsync(HttpMethod.Put, 
                $"{_options.StorageStringsUrl}", 
                JsonSerializer.Serialize(request, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase}));

        private async Task<HttpResponseMessage> SendAsync(HttpMethod httpMethod, string url, string payload = null)
        {
            using (var client = _httpClientFactory.Create())
            {
                var request = new HttpRequestMessage(httpMethod, url);
                request.Content = string.IsNullOrWhiteSpace(payload) ? null : new StringContent(payload);
                var response = await client.SendAsync(request);
                return response;
            }
        }
    }
}
