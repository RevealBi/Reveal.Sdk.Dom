using System.Collections.Generic;
using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSources
{
    public class RestDataSourceFixture
    {
        [Fact]
        public void Constructor_SetsProviderToREST_Always()
        {
            // Arrange & Act
            var dataSource = new RestDataSource();

            // Assert
            Assert.Equal(DataSourceProvider.REST, dataSource.Provider);
        }

        [Fact]
        public void Body_SetsAndGetsValue_ValidInput()
        {
            // Arrange
            var dataSource = new RestDataSource();
            var expectedBody = "Test Body Content";

            // Act
            dataSource.Body = expectedBody;

            // Assert
            Assert.Equal(expectedBody, dataSource.Body);
            Assert.Equal(expectedBody, dataSource.Properties.GetValue<string>("Body"));
        }

        [Fact]
        public void ContentType_SetsAndGetsValue_ValidInput()
        {
            // Arrange
            var dataSource = new RestDataSource();
            var expectedContentType = "application/json";

            // Act
            dataSource.ContentType = expectedContentType;

            // Assert
            Assert.Equal(expectedContentType, dataSource.ContentType);
            Assert.Equal(expectedContentType, dataSource.Properties.GetValue<string>("ContentType"));
        }

        [Fact]
        public void Headers_SetsAndGetsValue_ValidInput()
        {
            // Arrange
            var dataSource = new RestDataSource();
            var expectedHeaders = new List<string> { "Authorization: Bearer token", "Content-Type: application/json" };

            // Act
            dataSource.Headers = expectedHeaders;

            // Assert
            Assert.Equal(expectedHeaders, dataSource.Headers);
            Assert.Equal(expectedHeaders, dataSource.Properties.GetValue<List<string>>("Headers"));
        }

        [Fact]
        public void Method_SetsAndGetsValue_ValidInput()
        {
            // Arrange
            var dataSource = new RestDataSource();
            var expectedMethod = "POST";

            // Act
            dataSource.Method = expectedMethod;

            // Assert
            Assert.Equal(expectedMethod, dataSource.Method);
            Assert.Equal(expectedMethod, dataSource.Properties.GetValue<string>("Method"));
        }

        [Fact]
        public void Body_SetsValueToNull_HandlesGracefully()
        {
            // Arrange
            var dataSource = new RestDataSource();

            // Act
            dataSource.Body = null;

            // Assert
            Assert.Null(dataSource.Body);
            Assert.Null(dataSource.Properties.GetValue<string>("Body"));
        }

        [Fact]
        public void Headers_SetsValueToNull_HandlesGracefully()
        {
            // Arrange
            var dataSource = new RestDataSource();

            // Act
            dataSource.Headers = null;

            // Assert
            Assert.Null(dataSource.Headers);
            Assert.Null(dataSource.Properties.GetValue<List<string>>("Headers"));
        }

        [Fact]
        public void ContentType_SetsValueToNull_HandlesGracefully()
        {
            // Arrange
            var dataSource = new RestDataSource();

            // Act
            dataSource.ContentType = null;

            // Assert
            Assert.Null(dataSource.ContentType);
            Assert.Null(dataSource.Properties.GetValue<string>("ContentType"));
        }

        [Fact]
        public void Method_SetsValueToNull_HandlesGracefully()
        {
            // Arrange
            var dataSource = new RestDataSource();

            // Act
            dataSource.Method = null;

            // Assert
            Assert.Null(dataSource.Method);
            Assert.Null(dataSource.Properties.GetValue<string>("Method"));
        }

        [Fact]
        public void Headers_AddsValueToList_ValidInput()
        {
            // Arrange
            var dataSource = new RestDataSource();
            var initialHeaders = new List<string> { "Authorization: Bearer token" };
            var newHeader = "Content-Type: application/json";

            // Act
            dataSource.Headers = initialHeaders;
            dataSource.Headers.Add(newHeader);

            // Assert
            Assert.Contains(newHeader, dataSource.Headers);
        }
    }
}