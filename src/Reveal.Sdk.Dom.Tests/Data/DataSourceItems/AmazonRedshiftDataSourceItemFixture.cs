using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class AmazonRedshiftDataSourceItemFixture
    {
        [Fact]
        public void Constructor_WithDataSource_SetsTitleAndDataSource()
        {
            // Arrange
            string title = "Test Item";
            var dataSource = new AmazonRedshiftDataSource();

            // Act
            var item = new AmazonRedshiftDataSourceItem(title, dataSource);

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
            var item = new AmazonRedshiftDataSourceItem(title, null);

            // Assert
            Assert.Equal(title, item.Title);
        }

        [Fact]
        public void Constructor_WithEmptyTitle_ShouldHandleEmptyTitleGracefully()
        {
            // Arrange
            string title = string.Empty;
            var dataSource = new AmazonRedshiftDataSource();

            // Act
            var item = new AmazonRedshiftDataSourceItem(title, dataSource);

            // Assert
            Assert.Equal(title, item.Title);
        }

        [Fact]
        public void HasAsset_Should_Be_False()
        {
            // Arrange
            var item = new AmazonRedshiftDataSourceItem("Test Item", new AmazonRedshiftDataSource());

            // Assert
            Assert.False(item.HasAsset);
        }
    }
}