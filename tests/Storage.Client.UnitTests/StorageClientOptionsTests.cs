using NUnit.Framework;
using FluentAssertions;

namespace Storage.Client.UnitTests
{
    [TestFixture]
    public class StorageClientOptionsTests
    {
        [TestCase("localhost:8100/")]
        [TestCase("localhost:8100")]
        [TestCase("localhost:7109")]
        public void Properties_Should_Return_ExpectedValues(string url)
        {
            var options = new StorageClientOptions { ApiServiceUrl = url };

            options.ApiServiceUrl.Should().Be(url);
            options.ApiServiceUrlWithSlash.Should().Be($"{url.TrimEnd('/')}/");
            options.StorageStringsUrl.Should().Be($"{options.ApiServiceUrlWithSlash}StorageStrings");
        }
    }
}