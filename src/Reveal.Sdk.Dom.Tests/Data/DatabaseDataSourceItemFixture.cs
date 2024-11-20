using Moq;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data
{
    public class DatabaseDataSourceItemFixture
    {
        [Theory]
        [InlineData("DS Title", "DS Item Title", "DS Title", "DS Item Title")] // If Data Source has the title, when it's used to create DS Item, its title is not updated
        [InlineData(null, "DS Item Title", "DS Item Title", "DS Item Title")] // If Data Source has null title, when it's used to create DS Item, its title is updated to be the same as DS Item's title
        public void Constructor_CreateDatabaseDSItem_WithTitleAndDataSource(string dsTitle, string dsItemTitle, string expectedDSTitle, string expectedDSItemTitle)
        {
            // Arrange
            var dataSource = new DataSource() { Title = dsTitle};

            // Act
            var mock = new Mock<DatabaseDataSourceItem>(dsItemTitle, dataSource) { CallBase = true };
            var dbDataSourceItem = mock.Object;

            // Assert
            Assert.Equal(expectedDSItemTitle, dbDataSourceItem.Title);
            Assert.Equal(dataSource, dbDataSourceItem.DataSource);
            Assert.Equal(dataSource.Id, dbDataSourceItem.DataSourceId);
            Assert.Equal(expectedDSTitle, dbDataSourceItem.DataSource.Title);
        }

        [Fact]
        public void GetDatabase_ReturnsSameValue_WithSetValue()
        {
            // Arrange
            var mock = new Mock<DatabaseDataSourceItem>("Title", new DataSource()) { CallBase = true };
            var expectedDatabaseName = "database";
            var dbDataSourceItem = mock.Object;

            // Act
            dbDataSourceItem.Database = expectedDatabaseName;

            // Assert
            Assert.Equal(expectedDatabaseName, dbDataSourceItem.Database);
        }
    }
}
