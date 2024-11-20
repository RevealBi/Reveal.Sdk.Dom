using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class PostgreSqlDataSourceItemFixture
    {
        [Fact]
        public void Constructor_SetsTitleAndDataSource_WhenConstructed()
        {
            // Arrange
            var title = "Test Item";
            var dataSource = new PostgreSQLDataSource();

            // Act
            var item = new PostgreSqlDataSourceItem(title, dataSource);

            // Assert
            Assert.Equal(title, item.Title);
            Assert.Equal(dataSource, item.DataSource);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ProcessDataOnServer_SetsAndGetsValue_WithDifferentInputs(bool processDataOnServer)
        {
            // Arrange
            var item = new PostgreSqlDataSourceItem("Test Item", new PostgreSQLDataSource());

            // Act
            item.ProcessDataOnServer = processDataOnServer;

            // Assert
            Assert.Equal(processDataOnServer, item.ProcessDataOnServer);
            Assert.Equal(processDataOnServer, item.Properties.GetValue<bool>("ServerAggregation"));
        }
    }
}