using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class MySqlDataSourceItemFixture
    {
        [Theory]
        [InlineData("Test Item")]
        [InlineData(null)]
        public void Constructor_SetsTitleAndDataSource_WhenCalled(string title)
        {
            // Arrange
            var dataSource = new MySQLDataSource();

            // Act
            var item = new MySqlDataSourceItem(title, dataSource);

            // Assert
            Assert.Equal(title, item.Title);
            Assert.Equal(dataSource, item.DataSource);
        }


        [Fact]
        public void ProcessDataOnServer_SetsAndGetsValue_WithInputs()
        {
            // Arrange
            var item = new MySqlDataSourceItem("Test Item", new MySQLDataSource());

            // Act
            item.ProcessDataOnServer = true;

            // Assert
            Assert.True(item.ProcessDataOnServer);
            Assert.True(item.Properties.GetValue<bool>("ServerAggregation"));
        }
    }
}