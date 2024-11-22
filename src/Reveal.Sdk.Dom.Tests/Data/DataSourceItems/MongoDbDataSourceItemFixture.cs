using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class MongoDbDataSourceItemFixture
    {
        [Theory]
        [InlineData("Test Item")]
        [InlineData(null)]
        public void Constructor_SetsTitleAndDataSource_WhenCalled(string title)
        {
            // Arrange
            var dataSource = new MongoDBDataSource();

            // Act
            var item = new MongoDbDataSourceItem(title, dataSource);

            // Assert
            Assert.Equal(title, item.Title);
            Assert.Equal(dataSource, item.DataSource);
        }

        [Fact]
        public void Collection_SetsAndGetsValue_WithInputs()
        {
            // Arrange
            var item = new MongoDbDataSourceItem("Test Item", new MongoDBDataSource());

            // Act
            item.Collection = "TestCollection";

            // Assert
            Assert.Equal("TestCollection", item.Collection);
            Assert.Equal("TestCollection", item.Properties.GetValue<string>("Collection"));
        }

        [Fact]
        public void ProcessDataOnServer_SetsAndGetsValue_WithInputs()
        {
            // Arrange
            var item = new MongoDbDataSourceItem("Test Item", new MongoDBDataSource());

            // Act
            item.ProcessDataOnServer = true;

            // Assert
            Assert.True(item.ProcessDataOnServer);
            Assert.True(item.Properties.GetValue<bool>("ServerAggregation"));
        }
    }
}