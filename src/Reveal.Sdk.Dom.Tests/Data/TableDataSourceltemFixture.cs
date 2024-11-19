using Moq;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data
{
    public class TableDataSourceItemFixture
    {
        [Theory]
        [InlineData("DS Title", "DS Item Title", "DS Title", "DS Item Title")] // If DS' title is not null, then it still have it after DSItem created.
        [InlineData(null, "DS Item Title", "DS Item Title", "DS Item Title")] // If DS' title is null, then DS Item's title will be set to it.
        public void Constructor_CreateTableDataSourceItem_WithTitleAndDataSource(string dSTitle, string dSItemTitle, string expectedDSTitle, string expectedDSItemTitle)
        {
            // Arrange
            var dataSource = new DataSource() { Title = dSTitle };

            // Act
            var tableDataSourceItem = new Mock<TableDataSourceItem>(dSItemTitle, dataSource) { CallBase = true }.Object;

            // Assert
            Assert.Equal(expectedDSItemTitle, tableDataSourceItem.Title);
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
