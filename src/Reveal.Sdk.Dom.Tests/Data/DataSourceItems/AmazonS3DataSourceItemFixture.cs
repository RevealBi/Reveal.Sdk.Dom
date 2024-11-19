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

            // Assert
            Assert.Equal(title, item.Title);
            Assert.Equal(dataSource, item.DataSource);
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
            var actualPropertyPath = item.Properties.GetValue<string>("Path");

            // Assert
            Assert.Equal(expectedPath, actualPath);
            Assert.Equal(expectedPath, actualPropertyPath);
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
    }
}