using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class MySqlDataSourceItemFixture
    {
        [Fact]
        public void Constructor_SetsTitleAndDataSource_WhenConstructed()
        {
            // Arrange
            var title = "Test Item";
            var dataSource = new MySQLDataSource();

            // Act
            var item = new MySqlDataSourceItem(title, dataSource);

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
            var item = new MySqlDataSourceItem("Test Item", new MySQLDataSource());

            // Act
            item.ProcessDataOnServer = processDataOnServer;

            // Assert
            Assert.Equal(processDataOnServer, item.ProcessDataOnServer);
            Assert.Equal(processDataOnServer, item.Properties.GetValue<bool>("ServerAggregation"));
        }
    }
}