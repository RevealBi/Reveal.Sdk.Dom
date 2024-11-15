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

            // Act
            var actualTitle = item.Title;
            var actualDataSource = item.DataSource;

            // Assert
            Assert.Equal(title, actualTitle);
            Assert.Equal(dataSource, actualDataSource);
        }

        [Fact]
        public void Constructor_SetsTitleAndDataSource_NullDataSource()
        {
            // Arrange
            string title = "Test Item";
            DataSource dataSource = null;
            var item = new AmazonRedshiftDataSourceItem(title, dataSource);

            // Act
            var actualTitle = item.Title;
            var actualDataSource = item.DataSource;

            // Assert
            Assert.Equal(title, actualTitle);
        }

        [Fact]
        public void Constructor_SetsTitle_EmptyTitle()
        {
            // Arrange
            string title = string.Empty;
            var dataSource = new AmazonRedshiftDataSource();
            var item = new AmazonRedshiftDataSourceItem(title, dataSource);

            // Act
            var actualTitle = item.Title;

            // Assert
            Assert.Equal(title, actualTitle);
        }

        [Fact]
        public void HasAsset_ReturnsFalse_Always()
        {
            // Arrange
            var item = new AmazonRedshiftDataSourceItem("Test Item", new AmazonRedshiftDataSource());

            // Act
            var hasAsset = item.HasAsset;

            // Assert
            Assert.False(hasAsset);
        }
    }
}