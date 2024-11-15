using Reveal.Sdk.Dom.Data;
using System;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSources
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
        }

        [Fact]  
        public void GetPort_ReturnSameValue_WithSetPort()
        {
            // Arrange
            var hostDataSource = new HostDataSource();
            var expectedPort = "port";

            // Act
            hostDataSource.Port = expectedPort;

            // Assert
            Assert.Equal(expectedPort, hostDataSource.Port);
        }
    }
}
