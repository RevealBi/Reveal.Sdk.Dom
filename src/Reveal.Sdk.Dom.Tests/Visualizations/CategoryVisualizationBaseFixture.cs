using Moq;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;
using System;
using System.Collections.Generic;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations
{
    public class CategoryVisualizationBaseFixture
    {
        [Fact]
        public void Constructor_SetNullTitleAndDataSourceItem_WithOnlyDataSourceItem()
        {
            // Arrange
            var dataSourceItem = new DataSourceItem();

            // Act
            var visualization = new TestCategoryVisualization(dataSourceItem);

            // Assert
            Assert.Null(visualization.Title);
            Assert.Equal(dataSourceItem, visualization.DataDefinition.DataSourceItem);
            Assert.IsType<CategoryVisualizationDataSpec>(visualization.VisualizationDataSpec);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("TestTitle")]    
        public void Constructor_SetTitleAndDataSourceItem_WithTitleAndDatasource(string testTitle)
        {
            // Arrange
            var dataSourceItem = new DataSourceItem();

            // Act
            var visualization = new TestCategoryVisualization(testTitle, dataSourceItem);

            // Assert
            Assert.Equal(testTitle, visualization.Title);
            Assert.Equal(dataSourceItem, visualization.DataDefinition.DataSourceItem);
            Assert.IsType<CategoryVisualizationDataSpec>(visualization.VisualizationDataSpec);
        }

        [Fact]
        public void VisualizationDataSpec_GetSetAsExpected_WhenUse()
        {
            // Arrange
            var dataSourceItem = new DataSourceItem();
            var visualization = new TestCategoryVisualization("testTitle", dataSourceItem);
            CategoryVisualizationDataSpec dataSpec = new CategoryVisualizationDataSpec();
            // Act
            visualization.VisualizationDataSpec = dataSpec;

            // Assert
            Assert.Equal(dataSpec, visualization.VisualizationDataSpec);
        }

        [Fact]
        public void GetLabels_ReturnsVisualizationSpecRows_WhenUse()
        {
            // Arrange
            var dataSourceItem = new DataSourceItem();
            var visualization = new TestCategoryVisualization("testTitle", dataSourceItem);
            var visSpec = new CategoryVisualizationDataSpec()
            {
                Rows = new List<DimensionColumn>
                {
                    new(),
                    new(),
                }
            };
            visualization.VisualizationDataSpec = visSpec;

            // Act
            var values = visualization.Labels;

            // Assert
            Assert.NotNull(values);
            Assert.Equal(2, values.Count);
            Assert.Equal(visSpec.Rows, values);
        }

        [Fact]
        public void GetValues_ReturnsVisualizationSpecValues_WhenUse()
        {
            // Arrange
            var dataSourceItem = new DataSourceItem();
            var visualization = new TestCategoryVisualization("testTitle", dataSourceItem);
            var visSpec = new CategoryVisualizationDataSpec()
            {
                Values = new List<MeasureColumn>
                {
                    new(),
                    new(),
                }
            };
            visualization.VisualizationDataSpec = visSpec;

            // Act
            var values = visualization.Values;

            // Assert
            Assert.NotNull(values);
            Assert.Equal(2, values.Count);
            Assert.Equal(visSpec.Values, values);
        }

        [Fact]
        public void GetSetCategory_ReturnsVisualizationSpecCategory_WhenUse()
        {
            // Arrange
            var dataSourceItem = new DataSourceItem();
            var visualization = new TestCategoryVisualization("testTitle", dataSourceItem);
            var visSpec = new CategoryVisualizationDataSpec()
            {
                Category = new DimensionColumn()
            };
            visualization.VisualizationDataSpec = visSpec;
            
            var newCategory = new DimensionColumn();

            // Act
            var category = visualization.Category;

            // Assert
            Assert.NotNull(category);
            Assert.Same(visSpec.Category, category);

            // Act
            visualization.Category = newCategory;

            // Assert
            Assert.Same(newCategory, visualization.Category);
            Assert.Same(newCategory, visualization.VisualizationDataSpec.Category);
        }


        private class TestCategoryVisualization : CategoryVisualizationBase<TestChartVisualizationSettings>
        {
            public TestCategoryVisualization(string title, DataSourceItem dataSourceItem) : base(title, dataSourceItem)
            {
            }

            public TestCategoryVisualization(DataSourceItem dataSourceItem) : base(dataSourceItem)
            {
            }
        }

        private class TestChartVisualizationSettings : ChartVisualizationSettingsBase
        {
        }
    }
}
