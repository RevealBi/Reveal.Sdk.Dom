using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using System;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data
{
    public class HostDataSourceFixture
    {
        [Fact]
        public void GetHost_ReturnSameValue_WithSetValue()
        {
            // Arrange
            var hostDataSource = new HostDataSource();
            var expectedHost = "test-host";

            // Act
            hostDataSource.Host = expectedHost;

            // Assert
            Assert.Equal(expectedHost, hostDataSource.Host);
            Assert.Equal(expectedHost, hostDataSource.Properties.GetValue<string>("Host"));
        }

        [Fact]
        public void GetPort_ReturnSameValue_WithSetPort()
        {
            // Arrange
            var hostDataSource = new HostDataSource();
            var expectedPort = "8080";

            // Act
            hostDataSource.Port = expectedPort;

            // Assert
            Assert.Equal(expectedPort, hostDataSource.Port);
            Assert.Equal(expectedPort, hostDataSource.Properties.GetValue<string>("Port"));
        }
    }
}
