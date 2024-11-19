using Moq;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data
{
    public class TableDataSourceItemFixture
    {
        [Theory]
        [InlineData("DS Title", "DS Title")]
        [InlineData(null, "Expected DS Item Title")]
        public void Constructor_CreateTableDataSourceItem_WithTitleAndDataSource(string dataSourceTitle, string expectedDSTitle)
        {
            // Arrange
            var title = "Expected DS Item Title";
            var dataSource = new DataSource() { Title = dataSourceTitle };

            // Act
            var mock = new Mock<TableDataSourceItem>(title, dataSource) { CallBase = true };
            var tableDataSourceItem = mock.Object;

            // Assert
            Assert.Equal(title, tableDataSourceItem.Title);
            Assert.Equal(dataSource, tableDataSourceItem.DataSource);
            Assert.Equal(dataSource.Id, tableDataSourceItem.DataSourceId);
            Assert.Equal(expectedDSTitle, tableDataSourceItem.DataSource.Title);
        }

        [Fact]
        public void GetTable_ReturnSameValue_WithSetValue()
        {
            // Arrange
            var title = "Title";
            var dataSource = new DataSource();
            var mock = new Mock<TableDataSourceItem>(title, dataSource) { CallBase = true };
            var tableDataSourceItem = mock.Object;
            var expectedTable = "Test Table";

            // Act
            tableDataSourceItem.Table = expectedTable;

            // Assert
            Assert.Equal(expectedTable, tableDataSourceItem.Table);
        }
    }
}
