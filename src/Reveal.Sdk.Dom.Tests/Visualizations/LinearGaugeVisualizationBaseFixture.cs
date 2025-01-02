using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;
using System;
using System.Collections.Generic;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations
{
    public class LinearGaugeVisualizationBaseFixture
    {

        [Theory]
        [InlineData("testTitle")]
        [InlineData(null)]
        public void Constructor_SetTitleAndDataSource_WhenConstructed(String title)
        {
            // Arrange
            var dataSourceItem = new DataSourceItem();

            // Act
            var visualization = new TestLinearGaugeVisualizationBase(title, dataSourceItem);

            // Assert
            Assert.Equal(title, visualization.Title);
            Assert.Equal(dataSourceItem, visualization.DataDefinition.DataSourceItem);
            Assert.IsType<LinearGaugeVisualizationDataSpec>(visualization.VisualizationDataSpec);
        }

        [Fact]
        public void GetLabels_ReturnsVisualizationSpecRows_IfDataSpecIsSingleValueLabelsVisDataSpec()
        {
            // Arrange
            var dataSourceItem = new DataSourceItem();
            var visualization = new TestLinearGaugeVisualizationBase("testTitle", dataSourceItem);
            var visSpec = new LinearGaugeVisualizationDataSpec()
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
        public void GetValues_ReturnsVisualizationSpecValue_IfDataSpecIsSingleValueLabelsVisDataSpec()
        {
            // Arrange
            var dataSourceItem = new DataSourceItem();
            var visualization = new TestLinearGaugeVisualizationBase("testTitle", dataSourceItem);
            var visSpec = new LinearGaugeVisualizationDataSpec()
            {
                Value = new List<MeasureColumn>
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
            Assert.Equal(visSpec.Value, values);
        }

        private class TestLinearGaugeVisualizationBase : LinearGaugeVisualizationBase<TestGaugeVisualizationSettingss>
        {
            public TestLinearGaugeVisualizationBase(string title, DataSourceItem dataSourceItem) : base(title, dataSourceItem)
            {
            }
        }

        private class TestGaugeVisualizationSettingss : GaugeVisualizationSettings
        {
        }
    }
}
