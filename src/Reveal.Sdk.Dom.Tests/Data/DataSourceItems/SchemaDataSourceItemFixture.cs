using Moq;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class SchemaDataSourceItemFixture
    {
        [Fact]
        public void Constructor_CreateSchemaDataSourceItem_WithTitleAndDataSource()
        {
            // Arrange
            var expectedTitle = "Title";
            var dataSource = new DataSource();

            // Act
            var mock = new Mock<SchemaDataSourceItem>(expectedTitle, dataSource) { CallBase = true };
            var schemaDataSourceItem = mock.Object;

            // Assert
            Assert.Equal(expectedTitle, schemaDataSourceItem.Title);
            Assert.Equal(dataSource, schemaDataSourceItem.DataSource);
            Assert.Equal(dataSource.Id, schemaDataSourceItem.DataSourceId);
            Assert.Equal(expectedTitle, schemaDataSourceItem.DataSource.Title);
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
