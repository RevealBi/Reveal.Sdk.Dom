using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Visualizations;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Primitives
{
    public class GaugeBandFixture
    {
        [Fact]
        public void Constructor_SetDefaultSchemaTypeName_WithoutParameters()
        {
            // Act
            var gaugeBand = new GaugeBand();

            // Assert
            Assert.Equal(SchemaTypeNames.GaugeBandType, gaugeBand.SchemaTypeName);
        }

        [Fact]
        public void ToJsonString_CreateCorrectJson_WithoutCondition()
        {
            // Arrange
            var expectedJson = """
                {
                  "_type": "Test Schema Type Name",
                  "Type": "NumberValue",
                  "Color": "Red",
                  "Value": 12.5
                }
                """;
            var gaugeBand = new GaugeBand()
            {
                Color = BandColor.Red,
                SchemaTypeName = "Test Schema Type Name",
                Value = 12.5,
                ValueComparisonType = ValueComparisonType.Number
            };

            // Act
            var actualJson = gaugeBand.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(actualJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }
    }
}
