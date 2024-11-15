using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class AmazonRedshiftDataSourceItemFixture
    {
        [Fact]
        public void Constructor_SetsTitleAndDataSource_ValidDataSource()
        {
            // Arrange
            string title = "Test Item";
            var dataSource = new AmazonRedshiftDataSource();
            var item = new AmazonRedshiftDataSourceItem(title, dataSource);

            // Assert
            Assert.Equal(title, item.Title);
            Assert.Equal(dataSource, item.DataSource);
        }
    }
}