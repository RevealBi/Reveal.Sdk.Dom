using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class AmazonAthenaDataSourceItemFixture
    {
        [Fact]
        public void Constructor_SetsTitleAndDataSource_ValidDataSource()
        {
            // Arrange
            string title = "Test Item";
            var dataSource = new AmazonAthenaDataSource();
            var item = new AmazonAthenaDataSourceItem(title, dataSource);

            // Act
            var actualTitle = item.Title;
            var actualDataSource = item.DataSource;

            // Assert
            Assert.Equal(title, actualTitle);
            Assert.Equal(dataSource, actualDataSource);
        }
    }
}