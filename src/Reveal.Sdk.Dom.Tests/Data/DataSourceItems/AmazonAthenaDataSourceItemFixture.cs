using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class AmazonAthenaDataSourceItemFixture
    {
        [Fact]
        public void Constructor_WithDataSource_SetsTitleAndDataSource()
        {
            // Arrange
            string title = "Test Item";
            var dataSource = new AmazonAthenaDataSource();

            // Act
            var item = new AmazonAthenaDataSourceItem(title, dataSource);

            // Assert
            Assert.Equal(title, item.Title);
            Assert.Equal(dataSource, item.DataSource);
        }

        [Fact]
        public void Constructor_WithNullDataSource_ShouldHandleNullGracefully()
        {
            // Arrange
            string title = "Test Item";

            // Act
            var item = new AmazonAthenaDataSourceItem(title, null);

            // Assert
            Assert.Equal(title, item.Title);
        }

        [Fact]
        public void Constructor_WithEmptyTitle_ShouldHandleEmptyTitleGracefully()
        {
            // Arrange
            string title = string.Empty;
            var dataSource = new AmazonAthenaDataSource();

            // Act
            var item = new AmazonAthenaDataSourceItem(title, dataSource);

            // Assert
            Assert.Equal(title, item.Title);
            Assert.Equal(dataSource, item.DataSource);
        }

        [Fact]
        public void HasTabularData_Should_Be_True()
        {
            // Arrange
            var item = new AmazonAthenaDataSourceItem("Test Item", new AmazonAthenaDataSource());

            // Assert
            Assert.True(item.HasTabularData);
        }

        [Fact]
        public void HasAsset_Should_Be_False()
        {
            // Arrange
            var item = new AmazonAthenaDataSourceItem("Test Item", new AmazonAthenaDataSource());

            // Assert
            Assert.False(item.HasAsset);
        }

        [Fact]
        public void Table_GetAndSetProperties_ReturnsCorrectValue()
        {
            // Arrange
            string table = "Test Table";
            var item = new AmazonAthenaDataSourceItem("Test Item", new AmazonAthenaDataSource());

            // Act
            item.Table = table;

            // Assert
            Assert.Equal(table, item.Table);
        }
    }
}