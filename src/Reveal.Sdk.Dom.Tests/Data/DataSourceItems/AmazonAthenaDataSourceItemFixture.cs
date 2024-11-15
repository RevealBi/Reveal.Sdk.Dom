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

        [Fact]
        public void Constructor_SetsTitleAndDataSource_NullDataSource()
        {
            // Arrange
            string title = "Test Item";
            DataSource dataSource = null;
            var item = new AmazonAthenaDataSourceItem(title, dataSource);

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
            var dataSource = new AmazonAthenaDataSource();
            var item = new AmazonAthenaDataSourceItem(title, dataSource);

            // Act
            var actualTitle = item.Title;

            // Assert
            Assert.Equal(title, actualTitle);
        }

        [Fact]
        public void HasTabularData_ReturnsTrue_Always()
        {
            // Arrange
            var item = new AmazonAthenaDataSourceItem("Test Item", new AmazonAthenaDataSource());

            // Act
            var hasTabularData = item.HasTabularData;

            // Assert
            Assert.True(hasTabularData);
        }

        [Fact]
        public void HasAsset_ReturnsFalse_Always()
        {
            // Arrange
            var item = new AmazonAthenaDataSourceItem("Test Item", new AmazonAthenaDataSource());

            // Act
            var hasAsset = item.HasAsset;

            // Assert
            Assert.False(hasAsset);
        }

        [Fact]
        public void Table_SetsAndGetsValue_ValidTableName()
        {
            // Arrange
            string table = "Test Table";
            var item = new AmazonAthenaDataSourceItem("Test Item", new AmazonAthenaDataSource());

            // Act
            item.Table = table;
            var actualTable = item.Table;

            // Assert
            Assert.Equal(table, actualTable);
        }
    }
}