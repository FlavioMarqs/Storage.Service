using NUnit.Framework;
using Storage.Client.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Client.UnitTests
{
    [TestFixture]
    public class StorageStringsClientTests
    {
        private IStorageStringsClient _client;

        [SetUp]
        public void SetUp()
        {
            var options = new StorageClientOptions { ApiServiceUrl = "mockUrl" };
            _client = new StorageStringsClient(options);
        }

        [TestCase(1)]
        [TestCase(100)]
        [TestCase(int.MaxValue)]
        public void DeleteStringAsync_Should_Throw_NotImplementedException(int identifier)
        {
            Assert.ThrowsAsync<NotImplementedException>(async () => await _client.DeleteStringAsync(new DTOs.Requests.StringRemovalRequest { Identifier = identifier }));
        }
    }
}
