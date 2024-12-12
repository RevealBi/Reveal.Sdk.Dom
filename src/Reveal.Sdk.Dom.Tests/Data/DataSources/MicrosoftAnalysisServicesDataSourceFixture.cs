using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSources
{
    public class MicrosoftAnalysisServicesDataSourceFixture
    {
        [Fact]
        public void Constructor_CreateMSAnalysisDS_WithoutParameters()
        {
            // Act
            var dataSource = new MicrosoftAnalysisServicesDataSource();

            // Assert
            Assert.Equal(DataSourceProvider.MicrosoftAnalysisServices, dataSource.Provider);
        }

        [Fact]
        public void GetCatalog_ReturnSameValue_WithSetValue()
        {
            // Arrange
            var dataSource = new MicrosoftAnalysisServicesDataSource();
            var catalog = "TestCatalog";

            // Act
            dataSource.Catalog = catalog;

            // Assert
            Assert.Equal(catalog, dataSource.Catalog);
            Assert.Equal(catalog, dataSource.Properties.GetValue<string>("Catalog"));
        }

        [Fact]
        public void GetHost_ReturnSameValue_WithSetValue()
        {
            // Arrange
            var dataSource = new MicrosoftAnalysisServicesDataSource();
            var host = "TestHost";

            // Act
            dataSource.Host = host;

            // Assert
            Assert.Equal(host, dataSource.Host);
            Assert.Equal(host, dataSource.Properties.GetValue<string>("Host"));
        }

        [Fact]
        public void GetPort_ReturnSameValue_WithSetValue()
        {
            // Arrange
            var dataSource = new MicrosoftAnalysisServicesDataSource();
            var port = 8123;

            // Act
            dataSource.Port = port;

            // Assert
            Assert.Equal(port, dataSource.Port);
            Assert.Equal(port, dataSource.Properties.GetValue<int>("Port"));
        }
    }
}
