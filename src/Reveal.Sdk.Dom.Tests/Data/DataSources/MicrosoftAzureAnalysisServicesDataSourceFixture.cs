using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSources
{
    public class MicrosoftAzureAnalysisServicesDataSourceFixture
    {
        [Fact]
        public void Constructor_SetProviderToMicrosoftAzureAnalysisServices_WhenConstructed()
        {
            // Act
            var dataSource = new MicrosoftAzureAnalysisServicesDataSource();

            // Assert
            Assert.Equal(DataSourceProvider.MicrosoftAzureAnalysisServices, dataSource.Provider);
        }

        [Fact]
        public void ServerUrl_SaveValueAndProperties_WhenSet()
        {
            // Arrange
            var dataSource = new MicrosoftAzureAnalysisServicesDataSource();
            var serverUrl = "TestServerUrl";

            // Act
            dataSource.ServerUrl = serverUrl;

            // Assert
            Assert.Equal(serverUrl, dataSource.ServerUrl);
            Assert.Equal(serverUrl, dataSource.Properties.GetValue<string>("ServerUrl"));
        }

    }
}
