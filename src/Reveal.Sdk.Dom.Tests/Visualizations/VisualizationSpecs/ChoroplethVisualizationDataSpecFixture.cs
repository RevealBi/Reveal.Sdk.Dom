using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;
using System.Collections.Generic;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.VisualizationSpecs
{
    public class ChoroplethVisualizationDataSpecFixture
    {
        [Fact]
        public void Constructor_FieldsHaveDefaultValue()
        {
            // Act
            var choroplethVSDataSpec = new ChoroplethVisualizationDataSpec();

            // Assert
            Assert.Equal(SchemaTypeNames.ChoroplethMapVisualizationDataSpecType, choroplethVSDataSpec.SchemaTypeName);
        }

        [Fact]
        public void ToJsonString_CreateCorrectJsonString_WithoutCondition()
        {
            // Arrange
            var expectedJson = """
                {
                  "_type": "ChoroplethMapVisualizationDataSpecType",
                  "MapColor": {
                    "_type": "DimensionColumnSpecType"
                  },
                  "Value": [],
                  "AdHocFields": 13,
                  "FormatVersion": 5,
                  "AdHocExpandedElements": [],
                  "Rows": []
                }
                """;
            var choroplethVSDataSpec = new ChoroplethVisualizationDataSpec()
            {
                AdHocExpandedElements = new List<Dom.Visualizations.AdHocExpandedElement>(),
                AdHocFields = 13,
                FormatVersion = 5,
                MapColor = new Dom.Visualizations.DimensionColumn(),
                Rows = new List<Dom.Visualizations.DimensionColumn>(),
                Value = new List<Dom.Visualizations.MeasureColumn>(),
            };

            // Act
            var actualJson = choroplethVSDataSpec.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(actualJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }
    }
}
