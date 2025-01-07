using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Visualizations;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Primitives
{
    public class BandFixture
    {
        [Fact]
        public void Constructor_FieldsHaveDefaultValue_WithoutParameters()
        {
            // Act
            var band = new MockBand();

            // Assert
            Assert.Equal(ValueComparisonType.Percentage, band.ValueComparisonType);
            Assert.Equal(BandColor.Green, band.Color);
            Assert.Null(band.Value);
        }

        [Fact]
        public void ToJsonString_CreateCorrectJsonString_WithoutCondition()
        {
            // Arrange
            var expectedJson = """
                {
                  "_type": "TestSchemaTypeName",
                  "Type": "NumberValue",
                  "Color": "Green",
                  "Value": 12.5
                }
                """;
            var band = new MockBand()
            {
                Color = BandColor.Green,
                SchemaTypeName = "TestSchemaTypeName",
                Value = 12.5,
                ValueComparisonType = ValueComparisonType.Number
            };

            // Act
            var actualJson = band.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(actualJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }

        private class MockBand : Band { }
    }
}
