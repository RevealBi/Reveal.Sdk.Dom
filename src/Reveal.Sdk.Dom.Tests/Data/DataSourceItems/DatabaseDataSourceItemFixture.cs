using Moq;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class DatabaseDataSourceItemFixture
    {
        [Fact]
        public void Constructor_CreateDatabaseDSItem_WithTitleAndDataSource()
        {
            // Arrange
            var title = "Title";
            var dataSource = new DataSource();

            // Act
            var mock = new Mock<DatabaseDataSourceItem>(title, dataSource) { CallBase = true };
            var dbDataSourceItem = mock.Object;

            // Assert
            Assert.Equal(title, dbDataSourceItem.Title);
            Assert.Equal(dataSource, dbDataSourceItem.DataSource);
            Assert.Equal(dataSource.Id, dbDataSourceItem.DataSourceId);
            Assert.Equal(title, dbDataSourceItem.DataSource.Title);
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
