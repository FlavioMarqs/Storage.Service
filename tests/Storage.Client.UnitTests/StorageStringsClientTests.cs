using FluentAssertions;
using Moq;
using NUnit.Framework;
using Storage.Client.Interfaces;
using Storage.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Storage.Client.UnitTests
{
    [TestFixture]
    public class StorageStringsClientTests
    {
        private IStorageStringsClient _client;
        private Mock<IHttpClient> _httpClient;
        private readonly StorageClientOptions _options = new StorageClientOptions() 
                    { ApiServiceUrl = "http://localhost:7209" };

        [SetUp]
        public void SetUp()
        {
            _httpClient = new Mock<IHttpClient>();
            _client = new StorageStringsClient(_options, new TestsHttpClientFactory(_httpClient.Object));
        }

        [TestCase(1)]
        [TestCase(100)]
        [TestCase(int.MaxValue)]
        public void DeleteStringAsync_Should_Throw_NotImplementedException(int identifier)
        {
            Assert.ThrowsAsync<NotImplementedException>(async () => await _client.DeleteStringAsync(new DTOs.Requests.StringRemovalRequest { Identifier = identifier }));
        }

        [TestCase(1, "fake string")]
        [TestCase(3, "oranges")]
        [TestCase(int.MaxValue, "the end")]
        [TestCase(83, "prime number")]
        public async Task GetById_Should_Return_Expected_Value(int identifier, string expectedString)
        {
            var stringResponse = new StringResponse() 
                { 
                Identifier = identifier, 
                StringValue = expectedString,
                CreatedAt = DateTime.UtcNow
            };

            _httpClient.Setup(d => d.SendAsync(It.IsAny<HttpRequestMessage>()))
               .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.NotFound))
               .Verifiable(); 
            
            _httpClient.Setup(d => d.SendAsync(It.Is<HttpRequestMessage>(f => f.Method == HttpMethod.Get && 
            f.RequestUri.AbsoluteUri.Equals($"{_options.StorageStringsUrl}/{identifier}", StringComparison.CurrentCultureIgnoreCase))))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK) 
                {
                    Content = new StringContent(JsonSerializer.Serialize(stringResponse)) 
                })
                .Verifiable();

            var result = await _client.GetStringByIdAsync(new DTOs.Requests.StringQueryRequest { Identifier = identifier });
            _httpClient.Verify(d => d.SendAsync(It.Is<HttpRequestMessage>(f => f.Method == HttpMethod.Get && f.RequestUri.AbsoluteUri.Equals($"http://localhost:7209/Storage/Strings/{identifier}"))), Times.Once);
            var resultObject = JsonSerializer.Deserialize<StringResponse>(await result.Content.ReadAsStringAsync());
            resultObject.Identifier.Should().Be(identifier);
            resultObject.StringValue.Should().Be(expectedString);
            resultObject.CreatedAt.Should().Be(stringResponse.CreatedAt);
        }
    }
}
