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
        #region Local variables

        private IStorageStringsClient _client;
        private Mock<IHttpClient> _httpClient;
        private readonly StorageClientOptions _options = new StorageClientOptions() 
                    { ApiServiceUrl = "http://localhost:7209" };

        #endregion

        #region SetUp / TearDown

        [SetUp]
        public void SetUp()
        {
            _httpClient = new Mock<IHttpClient>();
            //default: 404 not found
            _httpClient.Setup(d => d.SendAsync(It.IsAny<HttpRequestMessage>()))
               .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.NotFound))
               .Verifiable();

            _client = new StorageStringsClient(_options, 
                new TestsHttpClientFactory(_httpClient.Object));
        }

        [TearDown]
        public void TearDown()
        {
            _httpClient = null;
            _client = null;
        }

        #endregion

        #region DeleteStringAsync Tests

        [TestCase(1)]
        [TestCase(100)]
        [TestCase(int.MaxValue)]
        public void DeleteStringAsync_Should_Throw_NotImplementedException(int identifier)
        {
            Assert.ThrowsAsync<NotImplementedException>(async () => await _client.DeleteStringAsync(new DTOs.Requests.StringRemovalRequest { Identifier = identifier }));
        }

        #endregion

        #region GetStringByIdAsync Tests

        [TestCase(1, "fake string")]
        [TestCase(3, "oranges")]
        [TestCase(int.MaxValue, "the end")]
        [TestCase(83, "prime number")]
        public async Task GetStringByIdAsync_Should_Return_Expected_Value(int identifier, string expectedString)
        {
            var stringResponse = CreateStringResponse(identifier, expectedString, DateTime.UtcNow);

            _httpClient.Setup(d => d.SendAsync(It.Is<HttpRequestMessage>(f => f.Method == HttpMethod.Get && 
            f.RequestUri.AbsoluteUri.Equals($"{_options.StorageStringsUrl}/{identifier}", StringComparison.CurrentCultureIgnoreCase))))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK) 
                {
                    Content = new StringContent(JsonSerializer.Serialize(stringResponse)) 
                })
                .Verifiable();

            var result = await _client.GetStringByIdAsync(new DTOs.Requests.StringQueryRequest { Identifier = identifier });
            _httpClient.Verify(d => d.SendAsync(It.Is<HttpRequestMessage>(f => f.Method == HttpMethod.Get && f.RequestUri.AbsoluteUri.Equals($"{_options.StorageStringsUrl}/{identifier}"))), Times.Once);
            var resultObject = JsonSerializer.Deserialize<StringResponse>(await result.Content.ReadAsStringAsync());
            resultObject.Identifier.Should().Be(identifier);
            resultObject.StringValue.Should().Be(expectedString);
            resultObject.CreatedAt.Should().Be(stringResponse.CreatedAt);
        }

        #endregion

        #region GetAllStringsAsync Tests

        [TestCase(true)]
        [TestCase(false)]
        public async Task GetAllStringsAsync_Should_Return_ListOfObjects(bool includeDeleted)
        {
            var expectedResponse = new List<StringResponse>()
            {
                CreateStringResponse(1, "one", DateTime.Now.Subtract(TimeSpan.FromMinutes(10))),
                CreateStringResponse(2, "two", DateTime.Now.Subtract(TimeSpan.FromMinutes(20))),
                CreateStringResponse(3, "three", DateTime.Now.Subtract(TimeSpan.FromMinutes(30))),
                CreateStringResponse(4, "four", DateTime.Now.Subtract(TimeSpan.FromMinutes(40))),
                CreateStringResponse(5, "five", DateTime.Now.Subtract(TimeSpan.FromMinutes(50))),
                CreateStringResponse(6, "six", DateTime.Now.Subtract(TimeSpan.FromMinutes(60)))
            };

            _httpClient.Setup(d => d.SendAsync(It.Is<HttpRequestMessage>(f => f.Method == HttpMethod.Get &&
            f.RequestUri.AbsoluteUri.Equals($"{_options.StorageStringsUrl}/{includeDeleted}", StringComparison.CurrentCultureIgnoreCase))))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonSerializer.Serialize(expectedResponse))
                })
                .Verifiable();

            var result = await _client.GetAllStringsAsync(new DTOs.Requests.StringsQueryRequest { IncludeDeleted = includeDeleted });
            _httpClient.Verify(d => d.SendAsync(It.Is<HttpRequestMessage>(f => f.Method == HttpMethod.Get && f.RequestUri.AbsoluteUri.Equals($"{_options.StorageStringsUrl}/{includeDeleted}", StringComparison.CurrentCultureIgnoreCase))), Times.Once);
            var resultObject = JsonSerializer.Deserialize<IEnumerable<StringResponse>>(await result.Content.ReadAsStringAsync());
            
            foreach(var item in resultObject)
            {
                Assert.That(null != expectedResponse.Single(d => d.Identifier == item.Identifier &&
                                                        d.CreatedAt == item.CreatedAt &&
                                                        d.LastModifiedAt == item.LastModifiedAt));
            }    
        }
        
        #endregion

        #region Helper Methods

        private StringResponse CreateStringResponse(int identifier, 
            string stringValue, DateTime createdAt, DateTime? updatedAt = null, DateTime? deletedAt = null)
        {
            return new StringResponse
            {
                LastModifiedAt = updatedAt,
                Identifier = identifier,
                StringValue = stringValue,
                CreatedAt = createdAt
            };
        }

        #endregion
    }
}
