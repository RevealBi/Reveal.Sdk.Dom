using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;
using System.Collections.Generic;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.VisualizationSpecs
{
    public class SingleValueVisualizationDataSpecFixture
    {
        [Fact]
        public void Constructor_FieldsObjHaveDefaultValue_WithoutParmeter()
        {
            // Act
            var singleValueVSDataSpec = new SingleValueVisualizationDataSpec();

            // Assert
            Assert.Equal(SchemaTypeNames.SingleValueVisualizationDataSpecType, singleValueVSDataSpec.SchemaTypeName);
            Assert.Empty(singleValueVSDataSpec.Value);
        }

        [Fact]
        public void ToJsonString_CreateCorrectJsonString_WithoutCondition()
        {
            // Arrange
            var expectedJson = """
                {
                  "_type": "Test Schema Type Name",
                  "Value": []
                }
                """;
            var singleValueVSDataSpec = new SingleValueVisualizationDataSpec()
            {
                SchemaTypeName = "Test Schema Type Name",
                Value = new List<Dom.Visualizations.MeasureColumn>()
            };

            // Act
            var actualJson = singleValueVSDataSpec.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(actualJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }
    }
}
