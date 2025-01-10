using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;
using System.Collections.Generic;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.VisualizationSpecs
{
    public class SingleValueLabelsVisualizationDataSpecFixture
    {
        [Fact]
        public void Constructor_FieldsHaveDefaultValue_WithoutParameters()
        {
            // Act
            var singleValueLabelVSDataSpec = new SingleValueLabelsVisualizationDataSpec();

            // Assert
            Assert.Equal(SchemaTypeNames.SingleValueLabelsVisualizationDataSpecType, singleValueLabelVSDataSpec.SchemaTypeName);
            Assert.Empty(singleValueLabelVSDataSpec.Value);
        }

        [Fact]
        public void ToJsonString_CreateCorrectJsonString_WithoutCondition()
        {
            // Arrange
            var expectedJson = """
                {
                  "_type": "SingleValueLabelsVisualizationDataSpecType",
                  "Value": [],
                  "AdHocFields": 15,
                  "FormatVersion": 5,
                  "AdHocExpandedElements": [],
                  "Rows": []
                }
                """;
            var singleValueLabelVSDataSpec = new SingleValueLabelsVisualizationDataSpec()
            {
                AdHocExpandedElements = new List<Dom.Visualizations.AdHocExpandedElement>(),
                AdHocFields = 15,
                FormatVersion = 5,
                Rows = new List<Dom.Visualizations.DimensionColumn>(),
                Value = new List<Dom.Visualizations.MeasureColumn>()
            };

            // Act
            var actualJson = singleValueLabelVSDataSpec.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(actualJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }
    }
}
