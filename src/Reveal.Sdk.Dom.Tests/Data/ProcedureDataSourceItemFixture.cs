using Moq;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data
{
    public class ProcedureDataSourceItemFixture
    {
        [Theory]
        [InlineData("DS Title", "DS Item Title", "DS Title", "DS Item Title")] // If Data Source has the title, when it's used to create DS Item, its title is not updated
        [InlineData(null, "DS Item Title", "DS Item Title", "DS Item Title")] // If Data Source has null title, when it's used to create DS Item, its title is updated to be the same as DS Item's title
        public void Constructor_CreateProcedureDataSourceItem_WithTitleAndDataSource(string dsTitle, string dsItemTitle, string expectedDSTitle, string expectedDSItemTitle)
        {
            // Arrange
            var dataSource = new DataSource() { Title = dsTitle };

            // Act
            var mock = new Mock<ProcedureDataSourceItem>(dsItemTitle, dataSource) { CallBase = true };
            var procedureDataSourceItem = mock.Object;

            // Assert
            Assert.Equal(expectedDSItemTitle, procedureDataSourceItem.Title);
            Assert.Equal(dataSource, procedureDataSourceItem.DataSource);
            Assert.Equal(dataSource.Id, procedureDataSourceItem.DataSourceId);
            Assert.Equal(expectedDSTitle, procedureDataSourceItem.DataSource.Title);
        }
    }
}
