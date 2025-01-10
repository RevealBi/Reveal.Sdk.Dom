using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;
using System.Collections.Generic;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.VisualizationSpecs
{
    public class TimeSeriesVisualizationDataSpecFixture
    {
        [Fact]
        public void Constructor_FieldsHaveDefaultValue_WithoutParameters()
        {
            // Act
            var timeSeriesVSDataSpec = new TimeSeriesVisualizationDataSpec();

            // Assert
            Assert.Equal(SchemaTypeNames.TimeSeriesVisualizationDataSpecType, timeSeriesVSDataSpec.SchemaTypeName);
        }

        [Fact]
        public void ToJsonString_CreateCorrectJsonString_WithoutCondition()
        {
            // Arrange
            var expectedJson = """
                {
                  "_type": "TimeSeriesVisualizationDataSpecType",
                  "Category": {
                    "_type": "DimensionColumnSpecType"
                  },
                  "Values": [],
                  "AdHocFields": 13,
                  "FormatVersion": 5,
                  "AdHocExpandedElements": [],
                  "Rows": []
                }
                """;
            var timeSeriesVSDataSpec = new TimeSeriesVisualizationDataSpec()
            {
                AdHocExpandedElements = new List<Dom.Visualizations.AdHocExpandedElement>(),
                AdHocFields = 13,
                FormatVersion = 5,
                Rows = new List<Dom.Visualizations.DimensionColumn>(),
                Category = new Dom.Visualizations.DimensionColumn(),
                Values = new List<Dom.Visualizations.MeasureColumn>()
            };

            // Act
            var actualJson = timeSeriesVSDataSpec.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(actualJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }
    }
}
