using Moq;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data
{
    public class ProcedureDataSourceItemFixture
    {
        [Fact]
        public void Constructor_CreateProcedureDataSourceItem_WithTitleAndDataSource()
        {
            // Arrange
            var title = "Title";
            var dataSource = new DataSource();

            // Act
            var mock = new Mock<ProcedureDataSourceItem>(title, dataSource) { CallBase = true };
            var procedureDataSourceItem = mock.Object;

            // Assert
            Assert.Equal(title, procedureDataSourceItem.Title);
            Assert.Equal(dataSource, procedureDataSourceItem.DataSource);
            Assert.Equal(dataSource.Id, procedureDataSourceItem.DataSourceId);
            Assert.Equal(title, procedureDataSourceItem.DataSource.Title);
        }
    }
}
