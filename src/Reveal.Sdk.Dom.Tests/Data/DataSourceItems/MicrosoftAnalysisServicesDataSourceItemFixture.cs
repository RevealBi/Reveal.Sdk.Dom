using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class MicrosoftAnalysisServicesDataSourceItemFixture
    {
        [Fact]
        public void Constructor_CreateMSAnalysisServiceDSItem_WithTitle()
        {
            // Arrange
            var dsItemTitle = "MS Analysis Service Title";

            // Act
            var dsItem = new MicrosoftAnalysisServicesDataSourceItem(dsItemTitle);

            // Assert
            Assert.Equal(dsItemTitle, dsItem.Title);
            Assert.NotNull(dsItem.DataSource);
            Assert.IsType<MicrosoftAnalysisServicesDataSource>(dsItem.DataSource);
            Assert.Equal(dsItemTitle, dsItem.DataSource.Title);
            Assert.False(dsItem.HasTabularData);
        }

        [Theory]
        [InlineData("DS Title", "DS Title", "DS Item Title", "DS Item Title")]
        [InlineData(null, "DS Item Title", "DS Item Title", "DS Item Title")]
        public void Constructor_CreateMSAnalysisServiceDSItem_WithTitleAndDataSource(string dsTitle, string expectedDSTitle, string dsItemTitle, string expectedDSItemTitle)
        {
            // Arrange
            var dataSource = new DataSource() { Title = dsTitle };

            // Act
            var dsItem = new MicrosoftAnalysisServicesDataSourceItem(dsItemTitle, dataSource);

            // Assert
            Assert.Equal(SchemaTypeNames.DataSourceItemType, dsItem.SchemaTypeName);
            Assert.Equal(expectedDSTitle, dsItem.DataSource.Title);
            Assert.Equal(expectedDSItemTitle, dsItem.Title);
            Assert.Equal(dataSource.Id, dsItem.DataSource.Id);
            Assert.Equal(dataSource.Id, dsItem.DataSourceId);
            Assert.NotSame(dataSource, dsItem.DataSource);
            Assert.IsType<MicrosoftAnalysisServicesDataSource>(dsItem.DataSource);
            Assert.False(dsItem.HasTabularData);
        }

        [Theory]
        [InlineData("DS Title", "DS Title", "DS Item Title", "DS Item Title")]
        [InlineData(null, "DS Item Title", "DS Item Title", "DS Item Title")]
        public void Constructor_CreateMSAnalysisServiceDSItem_WithTitleAndMSAnalysisServiceDataSource
            (string dsTitle, string expectedDSTitle, string dsItemTitle, string expectedDSItemTitle)
        {
            // Arrange
            var dataSource = new MicrosoftAnalysisServicesDataSource() { Title = dsTitle };

            // Act
            var dsItem = new MicrosoftAnalysisServicesDataSourceItem(dsItemTitle, dataSource);

            // Assert
            Assert.Equal(SchemaTypeNames.DataSourceItemType, dsItem.SchemaTypeName);
            Assert.Equal(expectedDSTitle, dsItem.DataSource.Title);
            Assert.Equal(expectedDSItemTitle, dsItem.Title);
            Assert.Equal(dataSource.Id, dsItem.DataSource.Id);
            Assert.Equal(dataSource.Id, dsItem.DataSourceId);
            Assert.Same(dataSource, dsItem.DataSource);
            Assert.False(dsItem.HasTabularData);
        }

        [Fact]
        public void GetCatalog_ReturnSameValue_WithSetValue()
        {
            // Arrange
            var expectedCatalog = "MSCatalog";
            var dsItem = new MicrosoftAnalysisServicesDataSourceItem("Title", new DataSource());

            // Act
            dsItem.Catalog = expectedCatalog;

            // Assert
            Assert.Equal(expectedCatalog, dsItem.Catalog);
            Assert.Equal(expectedCatalog, dsItem.Properties.GetValue<string>("Catalog"));
        }

        [Fact]
        public void GetCube_ReturnSameValue_WithSetValue()
        {
            // Arrange
            var expectedCube = "MSCube";
            var dsItem = new MicrosoftAnalysisServicesDataSourceItem("Title", new DataSource());

            // Act
            dsItem.Cube = expectedCube;

            // Assert
            Assert.Equal(expectedCube, dsItem.Cube);
            Assert.Equal(expectedCube, dsItem.Properties.GetValue<string>("Cube"));
        }
    }
}
