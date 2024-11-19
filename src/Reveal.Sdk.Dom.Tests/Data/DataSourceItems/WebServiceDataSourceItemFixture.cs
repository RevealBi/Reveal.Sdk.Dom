using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class WebServiceDataSourceItemFixture
    {
        [Fact]
        public void Constructor_SetsTitleAndDataSource_WhenConstructed()
        {
            // Arrange
            string title = "Test Item";
            var dataSource = new WebServiceDataSource();
            var item = new WebServiceDataSourceItem(title, dataSource);

            // Assert
            Assert.Equal(title, item.Title);
            Assert.Equal(dataSource, item.DataSource);
        }

        [Fact]
        public void Url_SetsAndGetsValue_WhenSetWithValidInput()
        {
            // Arrange
            var item = new WebServiceDataSourceItem("Test Item", new WebServiceDataSource());
            var expectedUrl = "https://example.com/api";

            // Act
            item.Url = expectedUrl;

            // Assert
            Assert.Equal(expectedUrl, item.Url);
            Assert.Equal(expectedUrl, item.Properties.GetValue<string>("Url"));
        }

        [Fact]
        public void Url_SetsValueToNull_WhenSetToNull()
        {
            // Arrange
            var item = new WebServiceDataSourceItem("Test Item", new WebServiceDataSource());

            // Act
            item.Url = null;

            // Assert
            Assert.Null(item.Url);
            Assert.Null(item.Properties.GetValue<string>("Url"));
        }

        [Fact]
        public void HasAsset_ReturnsFalse_Always()
        {
            // Arrange
            var item = new WebServiceDataSourceItem("Test Item", new WebServiceDataSource());

            // Assert
            Assert.False(item.HasAsset);
        }
    }
}