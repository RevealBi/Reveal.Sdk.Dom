using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSources
{
    public class MicrosoftAzureSynapseAnalyticsDataSourceFixture
    {
        [Fact]
        public void Constructor_SetProviderToMicrosoftAzureSynapseAnalytics_WithoutParameters()
        {
            // Act
            var dataSource = new MicrosoftAzureSynapseAnalyticsDataSource();

            // Assert
            Assert.Equal(DataSourceProvider.MicrosoftAzureSynapseAnalytics, dataSource.Provider);
        }
    }
}
