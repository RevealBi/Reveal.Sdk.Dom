using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class BoxDataSourceItemFixture
    {
        [Fact]
        public void Constructor_SetsTitleAndDataSource_WhenConstructed()
        {
            // Arrange
            string title = "Test Item";
            var dataSource = new BoxDataSource();

            // Act
            var item = new BoxDataSourceItem(title, dataSource);

            // Assert
            Assert.Equal(title, item.Title);
            Assert.Equal(dataSource, item.DataSource);
        }

        [Theory]
        [InlineData("UniqueIdentifier123")]
        [InlineData("AnotherIdentifier456")]
        [InlineData(null)]
        public void Identifier_SetsAndGetsValue_WithDifferentInputs(string identifier)
        {
            // Arrange
            var item = new BoxDataSourceItem("Test Item", new BoxDataSource());

            // Act
            item.Identifier = identifier;

            // Assert
            Assert.Equal(identifier, item.Identifier);
            Assert.Equal(identifier, item.Properties.GetValue<string>("Identifier"));
        }
    }
}