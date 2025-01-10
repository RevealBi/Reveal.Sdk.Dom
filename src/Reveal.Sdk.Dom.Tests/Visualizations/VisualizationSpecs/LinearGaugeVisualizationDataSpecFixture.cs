using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;
using System.Collections.Generic;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.VisualizationSpecs
{
    public class LinearGaugeVisualizationDataSpecFixture
    {
        [Fact]
        public void Constructor_FieldsHaveDefaultValue_WithoutParameters()
        {
            // Act
            var linearGaugeVSDataSpec = new LinearGaugeVisualizationDataSpec();

            // Assert
            Assert.Equal(SchemaTypeNames.LinearGaugeVisualizationDataSpecType, linearGaugeVSDataSpec.SchemaTypeName);
            Assert.Empty(linearGaugeVSDataSpec.Target);
            Assert.Empty(linearGaugeVSDataSpec.Value);
        }

        [Fact]
        public void ToJsonString_CreateCorrectJsonString_WithoutCondition()
        {
            // Arrange
            var expectedJson = """
                {
                  "_type": "LinearGaugeVisualizationDataSpecType",
                  "Value": [],
                  "Target": [],
                  "AdHocFields": 6,
                  "FormatVersion": 12,
                  "AdHocExpandedElements": [],
                  "Rows": []
                }
                """;
            var linearGaugeVSDataSpec = new LinearGaugeVisualizationDataSpec()
            {
                AdHocExpandedElements = new List<Dom.Visualizations.AdHocExpandedElement>(),
                AdHocFields = 6,
                FormatVersion = 12,
                Rows = new List<Dom.Visualizations.DimensionColumn>(),
                Target = new List<Dom.Visualizations.MeasureColumn>(),
                Value = new List<Dom.Visualizations.MeasureColumn>(),
            };

            // Act
            var actualJson = linearGaugeVSDataSpec.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(actualJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }
    }
}
