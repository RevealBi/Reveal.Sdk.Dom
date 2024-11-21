using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class MongoDbDataSourceItemFixture
    {
        [Fact]
        public void Constructor_SetsTitleAndDataSource_WhenCalled()
        {
            // Arrange
            var title = "Test Item";
            var dataSource = new MongoDBDataSource();

            // Act
            var item = new MongoDbDataSourceItem(title, dataSource);

            // Assert
            Assert.Equal(title, item.Title);
            Assert.Equal(dataSource, item.DataSource);
        }

        [Theory]
        [InlineData("TestCollection")]
        [InlineData(null)]
        public void Collection_SetsAndGetsValue_WithDifferentInputs(string collectionName)
        {
            // Arrange
            var item = new MongoDbDataSourceItem("Test Item", new MongoDBDataSource());

            // Act
            item.Collection = collectionName;

            // Assert
            Assert.Equal(collectionName, item.Collection);
            Assert.Equal(collectionName, item.Properties.GetValue<string>("Collection"));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ProcessDataOnServer_SetsAndGetsValue_WithDifferentInputs(bool processDataOnServer)
        {
            // Arrange
            var item = new MongoDbDataSourceItem("Test Item", new MongoDBDataSource());

            // Act
            item.ProcessDataOnServer = processDataOnServer;

            // Assert
            Assert.Equal(processDataOnServer, item.ProcessDataOnServer);
            Assert.Equal(processDataOnServer, item.Properties.GetValue<bool>("ServerAggregation"));
        }
    }
}