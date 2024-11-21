using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class SnowflakeDataSourceItemFixture
    {
        [Theory]
        [InlineData("Test Item")]
        [InlineData(null)]
        public void Constructor_SetsTitleAndDataSource_WhenCalled(string title)
        {
            // Arrange
            var dataSource = new SnowflakeDataSource();

            // Act
            var item = new SnowflakeDataSourceItem(title, dataSource);

            // Assert
            Assert.Equal(title, item.Title);
            Assert.Equal(dataSource, item.DataSource);
        }

        [Theory]
        [InlineData("Test Item")]
        [InlineData(null)]
        public void Constructor_SetsTitleAndDataSource_WhenConstructedWithGenericDataSource(string title)
        {
            // Arrange
            var dataSource = new DataSource();

            // Act
            var item = new SnowflakeDataSourceItem(title, dataSource);

            // Assert
            Assert.Equal(title, item.Title);
            Assert.IsType<SnowflakeDataSource>(item.DataSource);
        }

        [Fact]
        public void ProcessDataOnServer_ShouldSetAndGetValue_WithInputs()
        {
            // Arrange
            var item = new SnowflakeDataSourceItem("Test Item", new SnowflakeDataSource());

            // Act
            item.ProcessDataOnServer = true;

            // Assert
            Assert.True(item.ProcessDataOnServer);
            Assert.True(item.Properties.GetValue<bool>("ServerAggregation"));
        }
    }
}
