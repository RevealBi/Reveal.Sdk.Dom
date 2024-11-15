using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class AmazonS3DataSourceItemFixture
    {
        [Fact]
        public void Constructor_SetsTitleAndDataSource_ValidParameters()
        {
            // Arrange
            string title = "Test Item";
            var dataSource = new AmazonS3DataSource();
            var item = new AmazonS3DataSourceItem(title, dataSource);

            // Act
            var actualTitle = item.Title;
            var actualDataSource = item.DataSource;

            // Assert
            Assert.Equal(title, actualTitle);
            Assert.Equal(dataSource, actualDataSource);
        }

        [Fact]
        public void Constructor_SetsTitleAndDataSource_NullDataSource()
        {
            // Arrange
            string title = "Test Item";
            DataSource dataSource = null;
            var item = new AmazonS3DataSourceItem(title, dataSource);

            // Act
            var actualTitle = item.Title;
            var actualDataSource = item.DataSource;

            // Assert
            Assert.Equal(title, actualTitle);
        }

        [Fact]
        public void Constructor_SetsTitle_EmptyTitle()
        {
            // Arrange
            string title = string.Empty;
            var dataSource = new AmazonS3DataSource();
            var item = new AmazonS3DataSourceItem(title, dataSource);

            // Act
            var actualTitle = item.Title;
            var actualDataSource = item.DataSource;

            // Assert
            Assert.Equal(title, actualTitle);
            Assert.Equal(dataSource, actualDataSource);
        }

        [Fact]
        public void HasAsset_ReturnsFalse_Always()
        {
            // Arrange
            var item = new AmazonS3DataSourceItem("Test Item", new AmazonS3DataSource());

            // Act
            var hasAsset = item.HasAsset;

            // Assert
            Assert.False(hasAsset);
        }

        [Fact]
        public void Path_SetsAndGetsValue_ValidValue()
        {
            // Arrange
            var item = new AmazonS3DataSourceItem("Test Item", new AmazonS3DataSource());
            var expectedPath = "s3://bucket-name/path/to/resource";

            // Act
            item.Path = expectedPath;
            var actualPath = item.Path;

            // Assert
            Assert.Equal(expectedPath, actualPath);
        }

        [Fact]
        public void Path_SetsValue_NullValue()
        {
            // Arrange
            var item = new AmazonS3DataSourceItem("Test Item", new AmazonS3DataSource());

            // Act
            item.Path = null;
            var actualPath = item.Path;
            var actualPropertyPath = item.Properties.GetValue<string>("Path");

            // Assert
            Assert.Null(actualPath);
            Assert.Null(actualPropertyPath);
        }

        [Fact]
        public void Properties_StoresPathValueCorrectly_ValidPath()
        {
            // Arrange
            var item = new AmazonS3DataSourceItem("Test Item", new AmazonS3DataSource());
            var expectedPath = "s3://bucket-name/path/to/resource";

            // Act
            item.Path = expectedPath;
            var actualPropertyPath = item.Properties.GetValue<string>("Path");

            // Assert
            Assert.Equal(expectedPath, actualPropertyPath);
        }
    }
}