using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class MicrosoftAzureSynapseAnalyticsDataSourceItemFixture
    {
        [Fact]
        public void Constructor_CreateAzureSynapseDSItem_WithTitle()
        {
            // Arrange
            var expectedDSItemTitle = "Azure Synapse DSI Title";

            // Act
            var dataSourceItem = new MicrosoftAzureSynapseAnalyticsDataSourceItem(expectedDSItemTitle);

            // Assert
            Assert.Equal(expectedDSItemTitle, dataSourceItem.Title);
            Assert.NotNull(dataSourceItem.DataSource);
            Assert.Equal(expectedDSItemTitle, dataSourceItem.DataSource.Title);
            Assert.IsType<MicrosoftAzureSynapseAnalyticsDataSource>(dataSourceItem.DataSource);
        }

        [Theory]
        [InlineData("DS Title", "DS Item Title", "DS Title", "DS Item Title")]
        [InlineData(null, "DS Item Title", "DS Item Title", "DS Item Title")]
        public void Constructor_CreateDataSource_WithTitleAndDataSource(string dSTitle, string dSItemTitle, string expectedDSTitle, string expectedDSItemTitle)
        {
            // Arrange
            var dataSource = new DataSource() { Title = dSTitle };

            // Act
            var dataSourceItem = new MicrosoftAzureSynapseAnalyticsDataSourceItem(dSItemTitle, dataSource);

            // Assert
            Assert.Equal(expectedDSItemTitle, dataSourceItem.Title);
            Assert.Equal(dataSource, dataSourceItem.DataSource);
            Assert.Equal(dataSource.Id, dataSourceItem.DataSourceId);
            Assert.Equal(expectedDSTitle, dataSourceItem.DataSource.Title);
            Assert.NotSame(dataSource, dataSourceItem.DataSource);
            Assert.IsType<MicrosoftAzureSynapseAnalyticsDataSource>(dataSourceItem.DataSource);
        }

        [Theory]
        [InlineData("DS Title", "DS Item Title", "DS Title", "DS Item Title")]
        [InlineData(null, "DS Item Title", "DS Item Title", "DS Item Title")]
        public void Constructor_CreateDataSource_WithTitleAndAzureSynapseDataSource(string dSTitle, string dSItemTitle, string expectedDSTitle, string expectedDSItemTitle)
        {
            // Arrange
            var dataSource = new MicrosoftAzureSynapseAnalyticsDataSource() { Title = dSTitle };

            // Act
            var dataSourceItem = new MicrosoftAzureSynapseAnalyticsDataSourceItem(dSItemTitle, dataSource);

            // Assert
            Assert.Equal(expectedDSItemTitle, dataSourceItem.Title);
            Assert.Equal(dataSource, dataSourceItem.DataSource);
            Assert.Equal(dataSource.Id, dataSourceItem.DataSourceId);
            Assert.Equal(expectedDSTitle, dataSourceItem.DataSource.Title);
            Assert.Same(dataSource, dataSourceItem.DataSource);
        }
    }
}
