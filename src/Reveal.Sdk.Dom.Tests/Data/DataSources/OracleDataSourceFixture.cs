using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSources
{
    public class OracleDataSourceFixture
    {
        [Fact]
        public void Constructor_SetsProviderToOracle_WhenConstructed()
        {
            // Act
            var dataSource = new OracleDataSource();

            // Assert
            Assert.Equal(DataSourceProvider.Oracle, dataSource.Provider);
        }

        [Theory]
        [InlineData("TestService")]
        [InlineData(null)]
        public void Service_SetsAndGetsValue_WithDifferentInputs(string service)
        {
            // Arrange
            var dataSource = new OracleDataSource();

            // Act
            dataSource.Service = service;

            // Assert
            Assert.Equal(service, dataSource.Service);
            Assert.Equal(service, dataSource.Properties.GetValue<string>("Service"));
        }

        [Theory]
        [InlineData("TestSID")]
        [InlineData(null)]
        public void SID_SetsAndGetsValue_WithDifferentInputs(string sid)
        {
            // Arrange
            var dataSource = new OracleDataSource();

            // Act
            dataSource.SID = sid;

            // Assert
            Assert.Equal(sid, dataSource.SID);
            Assert.Equal(sid, dataSource.Properties.GetValue<string>("SID"));
        }
    }
}