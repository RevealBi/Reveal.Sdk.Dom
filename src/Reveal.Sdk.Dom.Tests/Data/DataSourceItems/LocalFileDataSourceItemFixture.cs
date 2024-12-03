using Moq;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class LocalFileDataSourceItemFixture
    {
        [Fact]
        public void Constructor_CreateDSItemWithDefault_WithTitle()
        {
            // Arrange
            var dsItemTitle = "Local file ds Item Title";

            // Act
            var mock = new Mock<LocalFileDataSourceItem>(dsItemTitle) { CallBase = true };
            var localFileDataSourceItem = mock.Object;

            // Assert
            Assert.Equal(dsItemTitle, localFileDataSourceItem.Title);
            Assert.Equal(DataSourceIds.LOCALFILE, localFileDataSourceItem.DataSourceId);
            Assert.Equal(DataSourceIds.LOCALFILE, localFileDataSourceItem.DataSource.Id);
            Assert.Equal(DataSourceIds.LOCALFILE, localFileDataSourceItem.ResourceItemDataSource.Id);
            Assert.Equal(DataSourceIds.LOCALFILE, localFileDataSourceItem.ResourceItem.DataSource.Id);
            Assert.Equal(DataSourceIds.LOCALFILE, localFileDataSourceItem.ResourceItem.DataSourceId);
            Assert.Equal(dsItemTitle, localFileDataSourceItem.ResourceItem.Title);
        }

        [Fact]
        public void Constructor_GenerateExpectedTitleAndPath_WithTitleAndPath()
        {
            // Arrange
            var dsItemTitle = "Local file data source item title";
            var path = "samples.xlsx";
            var expectedPath = $"local:{path}";

            // Act
            var mock = new Mock<LocalFileDataSourceItem>(dsItemTitle, path) { CallBase = true };
            var localFileDataSourceItem = mock.Object;

            // Assert
            Assert.Equal(dsItemTitle, localFileDataSourceItem.Title);
            Assert.Equal(DataSourceIds.LOCALFILE, localFileDataSourceItem.DataSourceId);
            Assert.Equal(DataSourceIds.LOCALFILE, localFileDataSourceItem.DataSource.Id);
            Assert.Equal(expectedPath, localFileDataSourceItem.Path);
            Assert.Equal(expectedPath, localFileDataSourceItem.ResourceItem.Properties.GetValue<string>("URI"));
            Assert.Equal(DataSourceIds.LOCALFILE, localFileDataSourceItem.ResourceItemDataSource.Id);
            Assert.Equal(DataSourceIds.LOCALFILE, localFileDataSourceItem.ResourceItem.DataSource.Id);
            Assert.Equal(DataSourceIds.LOCALFILE, localFileDataSourceItem.ResourceItem.DataSourceId);
            Assert.Equal(dsItemTitle, localFileDataSourceItem.ResourceItem.Title);
        }

        [Fact]
        public void Constructor_CreateExpectedLocalFileDSItem_WithCustomTitleAndLocalFileDataSource()
        {
            // Arrange
            var dsItemTitle = "Local file data source item title";
            var dataSource = new LocalFileDataSource();

            // Act
            var mock = new Mock<LocalFileDataSourceItem>(dsItemTitle, dataSource) { CallBase = true };
            var localFileDataSourceItem = mock.Object;

            // Assert
            Assert.Equal(dsItemTitle, localFileDataSourceItem.Title);
            Assert.Equal(dataSource, localFileDataSourceItem.DataSource);
            Assert.Equal(dataSource.Id, localFileDataSourceItem.DataSourceId);
            Assert.Equal(DataSourceIds.LOCALFILE, localFileDataSourceItem.ResourceItemDataSource.Id);
            Assert.Equal(DataSourceIds.LOCALFILE, localFileDataSourceItem.ResourceItem.DataSource.Id);
            Assert.Equal(DataSourceIds.LOCALFILE, localFileDataSourceItem.ResourceItem.DataSourceId);
            Assert.Equal(dsItemTitle, localFileDataSourceItem.ResourceItem.Title);
        }

        [Fact]
        public void Constructor_CreateExpectedLocalFileDSItem_WithCustomTitleAndDataSource()
        {
            // Arrange
            var dsItemTitle = "Local file data source item title";
            var dataSource = new PostgreSQLDataSource();

            // Act
            var mock = new Mock<LocalFileDataSourceItem>(dsItemTitle, dataSource) { CallBase = true };
            var localFileDataSourceItem = mock.Object;

            // Assert
            Assert.Equal(dsItemTitle, localFileDataSourceItem.Title);
            Assert.Equal(DataSourceIds.LOCALFILE, localFileDataSourceItem.DataSourceId);
            Assert.Equal(DataSourceIds.LOCALFILE, localFileDataSourceItem.DataSource.Id);
            Assert.Equal(DataSourceIds.LOCALFILE, localFileDataSourceItem.ResourceItemDataSource.Id);
            Assert.Equal(DataSourceIds.LOCALFILE, localFileDataSourceItem.ResourceItem.DataSource.Id);
            Assert.Equal(DataSourceIds.LOCALFILE, localFileDataSourceItem.ResourceItem.DataSourceId);
            Assert.Equal(dsItemTitle, localFileDataSourceItem.ResourceItem.Title);
        }

        [Fact]
        public void GetPath_ReturnSameValue_WithSetValue()
        {
            // Arrange
            var mock = new Mock<LocalFileDataSourceItem>("Title") { CallBase = true };
            var localFileDataSourceItem = mock.Object;
            var path = "samples.xlsx";
            var expectedPath = $"local:{path}";

            // Act
            localFileDataSourceItem.Path = path;

            // Assert
            Assert.Equal(expectedPath, localFileDataSourceItem.Path);
            Assert.Equal(expectedPath, localFileDataSourceItem.ResourceItem.Properties.GetValue<string>("URI"));
        }
    }
}
