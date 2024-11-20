using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class MicrosoftSqlServerDataSourceItemFixture
    {
        [Fact]
        public void HasTabularData_Should_Be_True()
        {
            // Arrange
            var item = new MicrosoftSqlServerDataSourceItem("Test Item", new MicrosoftSqlServerDataSource());

            // Assert
            Assert.True(item.HasTabularData);
        }

        [Fact]
        public void HasAsset_Should_Be_False()
        {
            // Arrange
            var item = new MicrosoftSqlServerDataSourceItem("Test Item", new MicrosoftSqlServerDataSource());

            // Assert
            Assert.False(item.HasAsset);
        }

        [Fact]
        public void Table_GetAndSetProperties_ReturnsCorrectValue()
        {
            // Arrange
            string table = "Test Table";
            var item = new MicrosoftSqlServerDataSourceItem("Test Item", new MicrosoftSqlServerDataSource());

            // Act
            item.Table = table;

            // Assert
            Assert.Equal(table, item.Table);
        }

        [Fact]
        public void Constructor_WithDataSource_SetsTitleAndDataSource()
        {
            // Arrange
            string title = "Test Item";
            var dataSource = new MicrosoftSqlServerDataSource();

            // Act
            var item = new MicrosoftSqlServerDataSourceItem(title, dataSource);

            // Assert
            Assert.Equal(title, item.Title);
            Assert.Equal(dataSource, item.DataSource);
        }

        [Fact]
        public void Constructor_WithTable_SetsTitleTableAndDataSource()
        {
            // Arrange
            string title = "Test Item";
            string table = "Test Table";
            var dataSource = new MicrosoftSqlServerDataSource();

            // Act
            var item = new MicrosoftSqlServerDataSourceItem(title, table, dataSource);

            // Assert
            Assert.Equal(title, item.Title);
            Assert.Equal(table, item.Table);
            Assert.Equal(dataSource, item.DataSource);
        }

        [Fact]
        public void Constructor_WithNonSqlServerDataSource_CreatesSqlServerDataSource()
        {
            // Arrange
            var dataSource = new DataSource();
            var item = new MicrosoftSqlServerDataSourceItem("Test Item", dataSource);

            // Assert
            Assert.IsType<MicrosoftSqlServerDataSource>(item.DataSource);
        }

        [Fact]
        public void Constructor_WithSqlServerDataSource_DoesNotCreateNewDataSource()
        {
            // Arrange
            var dataSource = new MicrosoftSqlServerDataSource();
            var item = new MicrosoftSqlServerDataSourceItem("Test Item", dataSource);

            // Assert
            Assert.Equal(dataSource, item.DataSource);
        }
    }
}
