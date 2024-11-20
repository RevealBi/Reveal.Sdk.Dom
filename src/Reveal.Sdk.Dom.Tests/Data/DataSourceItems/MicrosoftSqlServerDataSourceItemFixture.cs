using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class MicrosoftSqlServerDataSourceItemFixture
    {

        [Fact]
        public void Constructor_SetsTitleAndDataSource_AsProvided()
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

        [Theory]
        [InlineData("DS Title", "DS Item Title", "DS Title", "DS Item Title")]
        [InlineData(null, "DS Item Title", "DS Item Title", "DS Item Title")]
        public void Constructor_SetsTitleTableAndDataSource_AsProvided(string dSTitle, string dSItemTitle, string expectedDSTitle, string expectedDSItemTitle)
        {
            // Arrange
            string table = "Test Table";
            var dataSource = new MicrosoftSqlServerDataSource() { Title = dSTitle };

            // Act
            var item = new MicrosoftSqlServerDataSourceItem(dSItemTitle, table, dataSource);

            // Assert
            Assert.Equal(table, item.Table);
            Assert.Equal(expectedDSItemTitle, item.Title);
            Assert.Equal(dataSource, item.DataSource);
            Assert.Equal(dataSource.Id, item.DataSourceId);
            Assert.Equal(expectedDSTitle, item.DataSource.Title);
            Assert.Equal(dataSource, item.DataSource);
            Assert.Equal(dataSource.Id, item.DataSourceId);
        }

        [Fact]
        public void Constructor_CreatesSqlServerDataSource_WithNonSqlServerDataSource()
        {
            // Arrange
            var dataSource = new DataSource();
            var item = new MicrosoftSqlServerDataSourceItem("Test Item", dataSource);

            // Assert
            Assert.IsType<MicrosoftSqlServerDataSource>(item.DataSource);
        }

        [Fact]
        public void Constructor_DoesNotCreateNewDataSource_WithSqlServerDataSource()
        {
            // Arrange
            var dataSource = new MicrosoftSqlServerDataSource();
            var item = new MicrosoftSqlServerDataSourceItem("Test Item", dataSource);

            // Assert
            Assert.Equal(dataSource, item.DataSource);
        }


        [Fact]
        public void HasTabularData_BeTrue_ByDefault()
        {
            // Arrange
            var item = new MicrosoftSqlServerDataSourceItem("Test Item", new MicrosoftSqlServerDataSource());

            // Assert
            Assert.True(item.HasTabularData);
        }

        [Fact]
        public void HasAsset_BeFalse_ByDefault()
        {
            // Arrange
            var item = new MicrosoftSqlServerDataSourceItem("Test Item", new MicrosoftSqlServerDataSource());

            // Assert
            Assert.False(item.HasAsset);
        }

        [Fact]
        public void Table_ReturnsCorrectValue_WhenSet()
        {
            // Arrange
            string table = "Test Table";
            var item = new MicrosoftSqlServerDataSourceItem("Test Item", new MicrosoftSqlServerDataSource());

            // Act
            item.Table = table;

            // Assert
            Assert.Equal(table, item.Table);
        }
    }
}
