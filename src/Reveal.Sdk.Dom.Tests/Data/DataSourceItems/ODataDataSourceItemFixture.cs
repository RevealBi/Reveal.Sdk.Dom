using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class ODataDataSourceItemFixture
    {
        [Fact]
        public void Constructor_SetsTitleAndDataSource_WhenConstructed()
        {
            // Arrange
            string title = "Test Item";
            var dataSource = new ODataDataSource();

            // Act
            var item = new ODataDataSourceItem(title, dataSource);

            // Assert
            Assert.Equal(title, item.Title);
            Assert.Equal(dataSource, item.DataSource);
        }

        [Theory]
        [InlineData("SampleEntity")]
        [InlineData(null)]
        public void EntityType_SetsAndGetsValue_WithDifferentInputs(string entityType)
        {
            // Arrange
            var item = new ODataDataSourceItem("Test Item", new ODataDataSource());

            // Act
            item.EntityType = entityType;

            // Assert
            Assert.Equal(entityType, item.EntityType);
            Assert.Equal(entityType, item.Properties.GetValue<string>("EntityType"));
        }

        [Theory]
        [InlineData("Namespace.FunctionName")]
        [InlineData(null)]
        public void FunctionQName_SetsAndGetsValue_WithDifferentInputs(string functionQName)
        {
            // Arrange
            var item = new ODataDataSourceItem("Test Item", new ODataDataSource());

            // Act
            item.FunctionQName = functionQName;

            // Assert
            Assert.Equal(functionQName, item.FunctionQName);
            Assert.Equal(functionQName, item.Properties.GetValue<string>("FunctionQName"));
        }

        [Theory]
        [InlineData("https://example.com/api/entity")]
        [InlineData(null)]
        public void Url_SetsAndGetsValue_WithDifferentInputs(string url)
        {
            // Arrange
            var item = new ODataDataSourceItem("Test Item", new ODataDataSource());

            // Act
            item.Url = url;

            // Assert
            Assert.Equal(url, item.Url);
            Assert.Equal(url, item.Properties.GetValue<string>("Url"));
        }
    }
}