using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;
using System.Collections.Generic;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.VisualizationSpecs
{
    public class SingleValueCategoryVisualizationDataSpecFixture
    {
        [Fact]
        public void Constructor_FieldsHaveDefaultValue_WithoutParameters()
        {
            // Act
            var singleValueCategoryVSDataSpec = new SingleValueCategoryVisualizationDataSpec();

            // Assert
            Assert.Equal(SchemaTypeNames.SingleValueCategoryVisualizationDataSpecType, singleValueCategoryVSDataSpec.SchemaTypeName);
            Assert.Empty(singleValueCategoryVSDataSpec.Value);
        }

        [Fact]
        public void ToJsonString_CreateCorrectJsonString_WithoutCondition()
        {
            // Arrange
            var expectedJson = """
                {
                  "_type": "SingleValueCategoryVisualizationDataSpecType",
                  "Value": [],
                  "Category": {
                    "_type": "DimensionColumnSpecType"
                  },
                  "AdHocFields": 12,
                  "FormatVersion": 2,
                  "AdHocExpandedElements": [],
                  "Rows": []
                }
                """;
            var singleValueCategoryVSDataSpec = new SingleValueCategoryVisualizationDataSpec()
            {
                AdHocExpandedElements = new List<Dom.Visualizations.AdHocExpandedElement>(),
                AdHocFields = 12,
                Category = new Dom.Visualizations.DimensionColumn(),
                FormatVersion = 2,
                Rows = new List<Dom.Visualizations.DimensionColumn>(),
                Value = new List<Dom.Visualizations.MeasureColumn>()
            };

            // Act
            var actualJson = singleValueCategoryVSDataSpec.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(actualJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }
    }
}
