using Moq;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data
{
    public class SchemaDataSourceItemFixture
    {
        [Theory]
        [InlineData("DS Title", "DS Item Title", "DS Title", "DS Item Title")] // If Data Source has the title, when it's used to create DS Item, its title is not updated
        [InlineData(null, "DS Item Title", "DS Item Title", "DS Item Title")] // If Data Source has null title, when it's used to create DS Item, its title is updated to be the same as DS Item's title
        public void Constructor_CreateSchemaDataSourceItem_WithTitleAndDataSource(string dsTitle, string dsItemTitle, string expectedDSTitle, string expectedDSItemTitle)
        {
            // Arrange
            var dataSource = new DataSource() { Title = dsTitle };

            // Act
            var mock = new Mock<SchemaDataSourceItem>(dsItemTitle, dataSource) { CallBase = true };
            var schemaDataSourceItem = mock.Object;

            // Assert
            Assert.Equal(expectedDSItemTitle, schemaDataSourceItem.Title);
            Assert.Equal(dataSource, schemaDataSourceItem.DataSource);
            Assert.Equal(dataSource.Id, schemaDataSourceItem.DataSourceId);
            Assert.Equal(expectedDSTitle, schemaDataSourceItem.DataSource.Title);
        }

        [Fact]
        public void GetTable_ReturnSameValue_WithSetValue()
        {
            // Arrange
            var expectedTable = "Table";
            var title = "Title";
            var dataSource = new DataSource();
            var mock = new Mock<SchemaDataSourceItem>(title, dataSource) { CallBase = true };
            var schemaDataSourceItem = mock.Object;

            // Act
            schemaDataSourceItem.Table = expectedTable;

            // Assert
            Assert.Equal(expectedTable, schemaDataSourceItem.Table);
        }
    }
}
