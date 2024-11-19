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
            var item = new DropboxDataSourceItem(title, dataSource);

            // Assert
            Assert.Equal(title, item.Title);
            Assert.Equal(dataSource, item.DataSource);
        }

        [Fact]
        public void Path_SetsAndGetsValue_ValidPath()
        {
            // Arrange
            var item = new DropboxDataSourceItem("Test Item", new DropboxDataSource());
            var expectedPath = "/test/path/to/file";

            // Act
            item.Path = expectedPath;
            var actualPath = item.Path;

            // Assert
            Assert.Equal(expectedPath, actualPath);
        }

        [Fact]
        public void Path_SetsValue_NullPath()
        {
            // Arrange
            var item = new DropboxDataSourceItem("Test Item", new DropboxDataSource());

            // Act
            item.Path = null;
            var actualPath = item.Path;
            var actualPropertyPath = item.Properties.GetValue<string>("Path");

            // Assert
            Assert.Null(actualPath);
            Assert.Null(actualPropertyPath);
        }
    }
}