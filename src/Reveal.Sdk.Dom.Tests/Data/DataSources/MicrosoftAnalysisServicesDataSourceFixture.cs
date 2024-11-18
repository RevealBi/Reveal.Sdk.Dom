using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSources
{
    public class MicrosoftAnalysisServicesDataSourceFixture
    {
        [Fact]
        public void Constructor_SetProviderToMicrosoftAnalysisServices_WhenConstructed()
        {
            // Act
            var dataSource = new MicrosoftAnalysisServicesDataSource();

            // Assert
            Assert.Equal(DataSourceProvider.MicrosoftAnalysisServices, dataSource.Provider);
        }

        [Fact]
        public void Catalog_SaveValueAndProperties_WhenSet()
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
        public void Host_SaveValueAndProperties_WhenSet()
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
        public void Port_SaveValueAndProperties_WhenSet()
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
