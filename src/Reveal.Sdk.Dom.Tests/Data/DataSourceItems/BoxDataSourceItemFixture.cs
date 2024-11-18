using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class BoxDataSourceItemFixture
    {
        [Fact]
        public void Constructor_SetsTitleAndDataSource_ValidDataSource()
        {
            // Arrange
            string title = "Test Item";
            var dataSource = new BoxDataSource();
            var item = new BoxDataSourceItem(title, dataSource);

            // Act
            var actualTitle = item.Title;
            var actualDataSource = item.DataSource;

            // Assert
            Assert.Equal(title, actualTitle);
            Assert.Equal(dataSource, actualDataSource);
        }

        [Fact]
        public void Identifier_SetsAndGetsValue_ValidIdentifier()
        {
            // Arrange
            var item = new BoxDataSourceItem("Test Item", new BoxDataSource());
            var expectedIdentifier = "UniqueIdentifier123";

            // Act
            item.Identifier = expectedIdentifier;
            var actualIdentifier = item.Identifier;

            // Assert
            Assert.Equal(expectedIdentifier, actualIdentifier);
        }

        [Fact]
        public void Identifier_SetsValue_NullIdentifier()
        {
            // Arrange
            var item = new BoxDataSourceItem("Test Item", new BoxDataSource());

            // Act
            item.Identifier = null;
            var actualIdentifier = item.Identifier;
            var actualPropertyIdentifier = item.Properties.GetValue<string>("Identifier");

            // Assert
            Assert.Null(actualIdentifier);
            Assert.Null(actualPropertyIdentifier);
        }
    }
}