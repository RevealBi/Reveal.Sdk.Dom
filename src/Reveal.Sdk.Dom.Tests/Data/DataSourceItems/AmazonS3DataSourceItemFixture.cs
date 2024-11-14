using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class AmazonS3DataSourceItemFixture
    {
        [Fact]
        public void Constructor_WithDataSource_SetsTitleAndDataSource()
        {
            // Arrange
            string title = "Test Item";
            var dataSource = new AmazonS3DataSource();

            // Act
            var item = new AmazonS3DataSourceItem(title, dataSource);

            // Assert
            Assert.Equal(title, item.Title);
            Assert.Equal(dataSource, item.DataSource);
        }

        [Fact]
        public void Constructor_WithNullDataSource_ShouldHandleNullGracefully()
        {
            // Arrange
            string title = "Test Item";

            // Act
            var item = new AmazonS3DataSourceItem(title, null);

            // Assert
            Assert.Equal(title, item.Title);
        }

        [Fact]
        public void Constructor_WithEmptyTitle_ShouldHandleEmptyTitleGracefully()
        {
            // Arrange
            string title = string.Empty;
            var dataSource = new AmazonS3DataSource();

            // Act
            var item = new AmazonS3DataSourceItem(title, dataSource);

            // Assert
            Assert.Equal(title, item.Title);
            Assert.Equal(dataSource, item.DataSource);
        }

        [Fact]
        public void HasAsset_Should_BeFalse()
        {
            // Arrange
            var item = new AmazonS3DataSourceItem("Test Item", new AmazonS3DataSource());

            // Assert
            Assert.False(item.HasAsset);
        }

        [Fact]
        public void Path_Should_GetAndSetPathProperty()
        {
            // Arrange
            var item = new AmazonS3DataSourceItem("Test Item", new AmazonS3DataSource());
            var expectedPath = "s3://bucket-name/path/to/resource";

            // Act
            item.Path = expectedPath;
            var result = item.Path;

            // Assert
            Assert.Equal(expectedPath, result);
        }

        [Fact]
        public void Path_SetToNull_ShouldHandleNullGracefully()
        {
            // Arrange
            var item = new AmazonS3DataSourceItem("Test Item", new AmazonS3DataSource());

            // Act
            item.Path = null;

            // Assert
            Assert.Null(item.Path);
            Assert.Null(item.Properties.GetValue<string>("Path"));
        }

        [Fact]
        public void Properties_Should_StorePathValueCorrectly()
        {
            // Arrange
            var item = new AmazonS3DataSourceItem("Test Item", new AmazonS3DataSource());
            var expectedPath = "s3://bucket-name/path/to/resource";

            // Act
            item.Path = expectedPath;

            // Assert
            Assert.Equal(expectedPath, item.Properties.GetValue<string>("Path"));
        }
    }
}