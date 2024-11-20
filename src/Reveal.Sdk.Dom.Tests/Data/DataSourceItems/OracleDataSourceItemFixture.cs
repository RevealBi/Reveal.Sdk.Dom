using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class OracleDataSourceItemFixture
    {
        [Fact]
        public void Constructor_SetsTitleAndDataSource_WhenConstructed()
        {
            // Arrange
            var title = "Test Item";
            var dataSource = new OracleDataSource();

            // Act
            var item = new OracleDataSourceItem(title, dataSource);

            // Assert
            Assert.Equal(title, item.Title);
            Assert.Equal(dataSource, item.DataSource);
        }
    }
}