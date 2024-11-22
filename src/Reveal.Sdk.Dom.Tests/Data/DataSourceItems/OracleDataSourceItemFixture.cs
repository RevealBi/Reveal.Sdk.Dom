using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class OracleDataSourceItemFixture
    {
        [Theory]
        [InlineData("Test Item")]
        [InlineData(null)]
        public void Constructor_SetsTitleAndDataSource_WhenCalled(string title)
        {
            // Arrange
            var dataSource = new OracleDataSource();

            // Act
            var item = new OracleDataSourceItem(title, dataSource);

            // Assert
            Assert.Equal(title, item.Title);
            Assert.Equal(dataSource, item.DataSource);
        }
    }
}