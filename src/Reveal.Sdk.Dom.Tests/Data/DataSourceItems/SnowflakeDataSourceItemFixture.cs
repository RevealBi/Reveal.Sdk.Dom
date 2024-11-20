using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class SnowflakeDataSourceItemFixture
    {
        [Fact]
        public void Constructor_ShouldSetTitleAndDataSource_WhenConstructedWithSnowflakeDataSource()
        {
            // Arrange
            var title = "Test Item";
            var dataSource = new SnowflakeDataSource();

            // Act
            var item = new SnowflakeDataSourceItem(title, dataSource);

            // Assert
            Assert.Equal(title, item.Title);
            Assert.Equal(dataSource, item.DataSource);
        }

        [Fact]
        public void Constructor_ShouldSetTitleAndDataSource_WhenConstructedWithGenericDataSource()
        {
            // Arrange
            var title = "Test Item";
            var dataSource = new DataSource();

            // Act
            var item = new SnowflakeDataSourceItem(title, dataSource);

            // Assert
            Assert.Equal(title, item.Title);
            Assert.IsType<SnowflakeDataSource>(item.DataSource);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ProcessDataOnServer_ShouldSetAndGetValue_WithDifferentInputs(bool processDataOnServer)
        {
            // Arrange
            var item = new SnowflakeDataSourceItem("Test Item", new SnowflakeDataSource());

            // Act
            item.ProcessDataOnServer = processDataOnServer;

            // Assert
            Assert.Equal(processDataOnServer, item.ProcessDataOnServer);
            Assert.Equal(processDataOnServer, item.Properties.GetValue<bool>("ServerAggregation"));
        }

        [Fact]
        public void CreateDataSourceInstance_ShouldReturnSnowflakeDataSourceInstance()
        {
            // Arrange
            var dataSource = new DataSource();
            var item = new TestSnowflakeDataSourceItem("Test Item", dataSource);

            // Act
            var createdDataSource = item.TestCreateDataSourceInstance(dataSource);

            // Assert
            Assert.IsType<SnowflakeDataSource>(createdDataSource);
            Assert.Equal(dataSource.Id, createdDataSource.Id);
            Assert.Equal(dataSource.Title, createdDataSource.Title);
        }

    }
    
    internal class TestSnowflakeDataSourceItem : SnowflakeDataSourceItem
    {
        public TestSnowflakeDataSourceItem(string title, DataSource dataSource) 
            : base(title, dataSource)
        {
        }

        public DataSource TestCreateDataSourceInstance(DataSource dataSource)
        {
            return CreateDataSourceInstance(dataSource);
        }
    }
}
