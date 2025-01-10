using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;
using System.Collections.Generic;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.VisualizationSpecs
{
    public class BubbleVisualizationDataSpecFixture
    {
        [Fact]
        public void Constructor_FieldsHaveDefaultValue()
        {
            // Act
            var bubbleVSDataSpec = new BubbleVisualizationDataSpec();

            // Assert
            Assert.Equal(SchemaTypeNames.BubbleVisualizationDataSpecType, bubbleVSDataSpec.SchemaTypeName);
            Assert.Empty(bubbleVSDataSpec.Radius);
        }

        [Fact]
        public void ToJsonString_CreateCorrectJsonString_WithoutCondition()
        {
            // Arrange
            var expectedJson = """
                {
                  "_type": "BubbleVisualizationDataSpecType",
                  "Radius": [],
                  "Category": {
                    "_type": "DimensionColumnSpecType"
                  },
                  "XAxis": [],
                  "YAxis": [],
                  "AdHocFields": 13,
                  "FormatVersion": 5,
                  "AdHocExpandedElements": [],
                  "Rows": []
                }
                """;
            var bubbleVSDataSpec = new BubbleVisualizationDataSpec()
            {
                AdHocExpandedElements = new List<Dom.Visualizations.AdHocExpandedElement>(),
                AdHocFields = 13,
                FormatVersion = 5,
                Rows = new List<Dom.Visualizations.DimensionColumn>(),
                Category = new Dom.Visualizations.DimensionColumn(),
                Radius = new List<Dom.Visualizations.MeasureColumn>(),
                XAxis = new List<Dom.Visualizations.MeasureColumn>(),
                YAxis = new List<Dom.Visualizations.MeasureColumn>()
            };

            // Act
            var actualJson = bubbleVSDataSpec.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(actualJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }
    }
}
