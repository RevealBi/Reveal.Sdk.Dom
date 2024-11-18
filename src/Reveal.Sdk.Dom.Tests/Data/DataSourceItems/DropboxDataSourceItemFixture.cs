using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class DropboxDataSourceItemFixture
    {
        [Fact]
        public void Constructor_SetsTitleAndDataSource_ValidDataSource()
        {
            // Arrange
            string title = "Test Item";
            var dataSource = new DropboxDataSource();
            var item = new DropboxDataSourceItem(title, dataSource);

            // Act
            var actualTitle = item.Title;
            var actualDataSource = item.DataSource;

            // Assert
            Assert.Equal(title, actualTitle);
            Assert.Equal(dataSource, actualDataSource);
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