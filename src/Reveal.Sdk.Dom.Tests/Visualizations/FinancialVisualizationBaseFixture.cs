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
    public class FinancialVisualizationBaseFixture
    {
        [Theory]
        [InlineData("testTitle")]
        [InlineData(null)]
        public void Constructor_SetTitleAndDataSource_WhenConstructed(String title)
        {
            // Arrange
            var dataSourceItem = new DataSourceItem();

            // Act
            var visualization = new TestFinancialVisualizationBase(title, dataSourceItem);

            // Assert
            Assert.Equal(title, visualization.Title);
            Assert.Equal(dataSourceItem, visualization.DataDefinition.DataSourceItem);
            Assert.IsType<FinancialVisualizationDataSpec>(visualization.VisualizationDataSpec);
        }

        [Fact]
        public void GetLabels_ReturnsVisualizationDataSpecRows_WhenUsed()
        {
            // Arrange
            var dataSourceItem = new DataSourceItem();
            var visualization = new TestFinancialVisualizationBase("testTitle", dataSourceItem);
            var visSpec = new FinancialVisualizationDataSpec()
            {
                Rows = new List<DimensionColumn>
                {
                    new(),
                    new(),
                }
            };
            visualization.VisualizationDataSpec = visSpec;

            // Act 
            var label = visualization.Labels;

            // Assert
            Assert.NotNull(label);
            Assert.Same(visSpec.Rows, label);
        }

        [Fact]
        public void GetOpens_ReturnsVisualizationDataSpecOpen_WhenUsed()
        {
            // Arrange
            var dataSourceItem = new DataSourceItem();
            var visualization = new TestFinancialVisualizationBase("testTitle", dataSourceItem);
            var visSpec = new FinancialVisualizationDataSpec()
            {
                Open = new List<MeasureColumn>
                {
                    new(),
                    new(),
                }
            };
            visualization.VisualizationDataSpec = visSpec;

            // Act 
            var opens = visualization.Opens;

            // Assert
            Assert.NotNull(opens);
            Assert.Same(visSpec.Open, opens);
        }

        [Fact]
        public void GetHighs_ReturnsVisualizationDataSpecHigh_WhenUsed()
        {
            // Arrange
            var dataSourceItem = new DataSourceItem();
            var visualization = new TestFinancialVisualizationBase("testTitle", dataSourceItem);
            var visSpec = new FinancialVisualizationDataSpec()
            {
                High = new List<MeasureColumn>
                {
                    new(),
                    new(),
                }
            };
            visualization.VisualizationDataSpec = visSpec;

            // Act 
            var values = visualization.Highs;

            // Assert
            Assert.NotNull(values);
            Assert.Same(visSpec.High, values);
        }

        [Fact]
        public void GetLows_ReturnsVisualizationDataSpecRows_WhenUsed()
        {
            // Arrange
            var dataSourceItem = new DataSourceItem();
            var visualization = new TestFinancialVisualizationBase("testTitle", dataSourceItem);
            var visSpec = new FinancialVisualizationDataSpec()
            {
                Low = new List<MeasureColumn>
                {
                    new(),
                    new(),
                }
            };
            visualization.VisualizationDataSpec = visSpec;

            // Act 
            var values = visualization.Lows;

            // Assert
            Assert.NotNull(values);
            Assert.Same(visSpec.Low, values);
        }

        [Fact]
        public void GetCloses_ReturnsVisualizationDataSpecRows_WhenUsed()
        {
            // Arrange
            var dataSourceItem = new DataSourceItem();
            var visualization = new TestFinancialVisualizationBase("testTitle", dataSourceItem);
            var visSpec = new FinancialVisualizationDataSpec()
            {
                Close = new List<MeasureColumn>
                {
                    new(),
                    new(),
                }
            };
            visualization.VisualizationDataSpec = visSpec;

            // Act 
            var values = visualization.Lows;

            // Assert
            Assert.NotNull(values);
            Assert.Same(visSpec.Low, values);
        }

        private class TestFinancialVisualizationBase : FinancialVisualizationBase<TestFinancialVisualizationSettingsBase>
        {
            public TestFinancialVisualizationBase(string title, DataSourceItem dataSourceItem) : base(title, dataSourceItem)
            {
            }
        }

        private class TestFinancialVisualizationSettingsBase : FinancialVisualizationSettingsBase
        {
        }
    }
}