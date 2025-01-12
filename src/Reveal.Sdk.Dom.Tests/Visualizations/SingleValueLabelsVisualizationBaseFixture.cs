﻿using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;
using System;
using System.Collections.Generic;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations
{
    public class SingleValueLabelsVisualizationBaseFixture
    {
        [Theory]
        [InlineData("testTitle")]
        [InlineData(null)]
        public void Constructor_SetTitleAndDataSourceItem_WhenConstructed(String title)
        {
            // Arrange
            var dataSourceItem = new DataSourceItem();

            // Act
            var visualization = new TestSingleValueLabelsVisualization(title, dataSourceItem);

            // Assert
            Assert.Equal(title, visualization.Title);
            Assert.Equal(dataSourceItem, visualization.DataDefinition.DataSourceItem);
            Assert.IsType<SingleValueLabelsVisualizationDataSpec>(visualization.VisualizationDataSpec);
        }

        [Fact]
        public void GetLabels_ReturnsVisualizationSpecRows_IfDataSpecIsSingleValueLabelsVisDataSpec()
        {
            // Arrange
            var dataSourceItem = new DataSourceItem();
            var visualization = new TestSingleValueLabelsVisualization("testTitle", dataSourceItem);
            var visSpec = new SingleValueLabelsVisualizationDataSpec()
            {
                Rows = new List<DimensionColumn>
                {
                    new(),
                    new(),
                }
            };
            visualization.VisualizationDataSpec = visSpec;

            // Act
            var labels = visualization.Labels;

            // Assert
            Assert.Equal(visSpec.Rows, labels);
        }

        [Fact]
        public void GetLabels_ReturnsEmptyList_IfDataSpecIsNotSingleValueLabelsVisDataSpec()
        {
            // Arrange
            var dataSourceItem = new DataSourceItem();
            var visualization = new TestSingleValueLabelsVisualization("testTitle", dataSourceItem);
            var visSpec = new CategoryVisualizationDataSpec();
            visualization.VisualizationDataSpec = visSpec;

            // Act
            var labels = visualization.Labels;

            // Assert
            Assert.Empty(labels);
        }

        [Fact]
        public void GetValues_ReturnsVisualizationSpecValue_IfDataSpecIsSingleValueLabelsVisDataSpec()
        {
            // Arrange
            var dataSourceItem = new DataSourceItem();
            var visualization = new TestSingleValueLabelsVisualization("testTitle", dataSourceItem);
            var visSpec = new SingleValueLabelsVisualizationDataSpec()
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
            Assert.Equal(visSpec.Value, values);
        }

        [Fact]
        public void GetValues_ReturnsEmptyList_IfDataSpecIsNotSingleValueLabelsVisDataSpec()
        {
            // Arrange
            var dataSourceItem = new DataSourceItem();
            var visualization = new TestSingleValueLabelsVisualization("testTitle", dataSourceItem);
            var visSpec = new CategoryVisualizationDataSpec();
            visualization.VisualizationDataSpec = visSpec;

            // Act
            var values = visualization.Values;

            // Assert
            Assert.Empty(values);
        }


        private class TestSingleValueLabelsVisualization : SingleValueLabelsVisualizationBase<TestVisualizationSettings>
        {
            public TestSingleValueLabelsVisualization(string title, DataSourceItem dataSourceItem) : base(title, dataSourceItem)
            {
            }
        }

        private class TestVisualizationSettings: VisualizationSettings
        {
        }
    }
}
