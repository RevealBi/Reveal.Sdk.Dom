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

        [Theory]
        [InlineData("https://example.com/api")]
        [InlineData(null)]
        public void Url_SetsAndGetsValue_WithDifferentInputs(string url)
        {
            // Arrange
            var item = new WebServiceDataSourceItem("Test Item", new WebServiceDataSource());

            // Act
            item.Url = url;

            // Assert
            Assert.Equal(url, item.Url);
            Assert.Equal(url, item.Properties.GetValue<string>("Url"));
        }
    }
}