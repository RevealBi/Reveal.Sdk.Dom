using System.Collections.Generic;
using System.Linq;
using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSources
{
    public class RestDataSourceFixture
    {
        public static IEnumerable<object[]> HeadersTestData =>
            new List<object[]>
            {
                new object[] { new List<string> { "Authorization: Bearer token", "Content-Type: application/json" } },
                new object[] { null }
            };
        
        [Fact]
        public void Constructor_SetsProviderToREST_WhenConstructed()
        {
            // Arrange & Act
            var dataSource = new RestDataSource();

            // Assert
            Assert.Equal(DataSourceProvider.REST, dataSource.Provider);
        }

        [Theory]
        [InlineData("https://example.com/api/data")]
        [InlineData(null)]
        public void Url_SetsAndGetsValue_WithDifferentInputs(string url)
        {
            // Arrange
            var dataSource = new RestDataSource();

            // Act
            dataSource.Url = url;

            // Assert
            Assert.Equal(url, dataSource.Url);
            Assert.Equal(url, dataSource.Properties.GetValue<string>("URL"));
        }

        [Theory]
        [InlineData("application/json")]
        [InlineData("text/xml")]
        [InlineData(null)]
        public void ContentType_SetsAndGetsValue_WithDifferentInputs(string contentType)
        {
            // Arrange
            var dataSource = new RestDataSource();

            // Act
            dataSource.ContentType = contentType;

            // Assert
            Assert.Equal(contentType, dataSource.ContentType);
            Assert.Equal(contentType, dataSource.Properties.GetValue<string>("ContentType"));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void UseAnonymousAuthentication_SetsAndGetsValue_WithDifferentInputs(bool value)
        {
            // Arrange
            var dataSource = new RestDataSource();

            // Act
            dataSource.UseAnonymousAuthentication = value;

            // Assert
            Assert.Equal(value, dataSource.UseAnonymousAuthentication);
            Assert.Equal(value, dataSource.Properties.GetValue<bool>("UseAnonymousAuthentication"));
        }

        [Theory]
        [MemberData(nameof(HeadersTestData))]
        public void Headers_SetsAndGetsValue_WithDifferentInputs(string[] headers)
        {
            // Arrange
            var dataSource = new RestDataSource();
            var expectedHeaders = headers?.ToList();

            // Act
            dataSource.Headers = expectedHeaders;

            // Assert
            Assert.Equal(expectedHeaders, dataSource.Headers);
            Assert.Equal(expectedHeaders, dataSource.Properties.GetValue<List<string>>("Headers"));
        }

        [Theory]
        [InlineData("GET")]
        [InlineData("POST")]
        [InlineData(null)]
        public void Method_SetsAndGetsValue_WithDifferentInputs(string method)
        {
            // Arrange
            var dataSource = new RestDataSource();

            // Act
            dataSource.Method = method;

            // Assert
            Assert.Equal(method, dataSource.Method);
            Assert.Equal(method, dataSource.Properties.GetValue<string>("Method"));
        }
    }
}