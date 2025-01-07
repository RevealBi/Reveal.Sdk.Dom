using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations
{
    public class SingleGaugeVisualizationBaseFixture
    {
        [Theory]
        [InlineData("testTitle")]
        [InlineData(null)]
        public void Constructor_SetTitleAndDataSource_WhenConstructed(String title)
        {
            // Arrange
            var dataSourceItem = new DataSourceItem();

            // Act
            var visualization = new TestSingleGaugeVisualizationBase(title, dataSourceItem);

            // Assert
            Assert.Equal(title, visualization.Title);
            Assert.Equal(dataSourceItem, visualization.DataDefinition.DataSourceItem);
            Assert.IsType<SingleGaugeVisualizationDataSpec>(visualization.VisualizationDataSpec);
        }

        [Fact]
        public void GetValues_ReturnsVisualizationSpecValue_IfDataSpecIsSingleValueLabelsVisDataSpec()
        {
            // Arrange
            var dataSourceItem = new DataSourceItem();
            var visualization = new TestSingleGaugeVisualizationBase("testTitle", dataSourceItem);
            var visSpec = new SingleGaugeVisualizationDataSpec()
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

        [Fact]
        public void Label_GetSetCorrectValue_WhenUsed()
        {
            // Arrange
            var dataSourceItem = new DataSourceItem();
            var visualization = new TestSingleGaugeVisualizationBase("testTitle", dataSourceItem);
            var visSpec = new SingleGaugeVisualizationDataSpec()
            {
                Label = new DimensionColumn(),
            };
            visualization.VisualizationDataSpec = visSpec;
            var newLabel = new DimensionColumn();

            // Act 
            visualization.Label = newLabel;

            // Assert
            Assert.Same(newLabel, visualization.Label);
            Assert.Same(newLabel, visSpec.Label);
        }

        private class TestSingleGaugeVisualizationBase : SingleGaugeVisualizationBase<TestVisualizationSettings>
        {
            public TestSingleGaugeVisualizationBase(string title, DataSourceItem dataSourceItem) : base(title, dataSourceItem)
            {
            }
        }

        private class TestVisualizationSettings : VisualizationSettings
        {
        }
    }
}
