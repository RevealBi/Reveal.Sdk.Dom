using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class DropboxDataSourceItemFixture
    {
        [Fact]
        public void Constructor_SetsTitleAndDataSource_WhenConstructed()
        {
            // Arrange
            string title = "Test Item";
            var dataSource = new DropboxDataSource();
            
            //Act
            var item = new DropboxDataSourceItem(title, dataSource);

            // Assert
            Assert.Equal(title, item.Title);
            Assert.Equal(dataSource, item.DataSource);
        }

        [Theory]
        [InlineData("/test/path/to/file")]
        [InlineData("/another/path")]
        [InlineData(null)]
        public void Path_SetsAndGetsValue_WithDifferentInputs(string path)
        {
            // Arrange
            var item = new DropboxDataSourceItem("Test Item", new DropboxDataSource());

            // Act
            item.Path = path;

            // Assert
            Assert.Equal(path, item.Path);
            Assert.Equal(path, item.Properties.GetValue<string>("Path"));
        }
    }
}