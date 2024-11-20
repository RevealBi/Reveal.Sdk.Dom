using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class MicrosoftSqlServerDataSourceItemFixture
    {
        [Fact]
        public void HasTabularData_ShouldBeTrue_WhenConstructed()
        {
            // Arrange
            var item = new MicrosoftSqlServerDataSourceItem("Test Item", new MicrosoftSqlServerDataSource());

            // Assert
            Assert.True(item.HasTabularData);
        }

        [Fact]
        public void HasAsset_ShouldBeFalse_WhenConstructed()
        {
            // Arrange
            var item = new MicrosoftSqlServerDataSourceItem("Test Item", new MicrosoftSqlServerDataSource());

            // Assert
            Assert.False(item.HasAsset);
        }

        [Theory]
        [InlineData("Test Table")]
        [InlineData(null)]
        public void Table_SetsAndGetsValue_WithDifferentInputs(string table)
        {
            // Arrange
            var item = new MicrosoftSqlServerDataSourceItem("Test Item", new MicrosoftSqlServerDataSource());

            // Act
            item.Table = table;

            // Assert
            Assert.Equal(table, item.Table);
        }

        [Fact]
        public void Constructor_SetsTitleAndDataSource_WhenConstructedWithDataSource()
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
        public void Constructor_SetsTitleTableAndDataSource_WhenConstructedWithTable()
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
        public void Constructor_CreatesSqlServerDataSource_WhenConstructedWithNonSqlServerDataSource()
        {
            // Arrange
            var dataSource = new DataSource();

            // Act
            var item = new MicrosoftSqlServerDataSourceItem("Test Item", dataSource);

            // Assert
            Assert.IsType<MicrosoftSqlServerDataSource>(item.DataSource);
        }

        [Fact]
        public void Constructor_DoesNotCreateNewDataSource_WhenConstructedWithSqlServerDataSource()
        {
            // Arrange
            var dataSource = new MicrosoftSqlServerDataSource();

            // Act
            var item = new MicrosoftSqlServerDataSourceItem("Test Item", dataSource);

            // Assert
            Assert.Equal(dataSource, item.DataSource);
        }
    }
}
