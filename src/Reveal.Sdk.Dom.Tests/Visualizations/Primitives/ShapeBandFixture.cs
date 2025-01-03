using Moq;
using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Visualizations;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Primitives
{
    public class ShapeBandFixture
    {
        [Fact]
        public void Constructor_ShapeTypeHaveDefaultValue_WithoutParameters()
        {
            // Act
            var shapeBand = new MockShapeBand();

            // Assert
            Assert.Equal(ShapeType.Circle, shapeBand.Shape);
        }

        [Fact]
        public void ToJsonString_CreateCorrectJsonData_WithoutCondition()
        {
            // Arrange
            var expectedJson = """
                {
                  "Shape": "ArrowUp",
                  "Type": "NumberValue",
                  "Color": "Red",
                  "Value": 10.5
                }
                """;
            var shapeBand = new MockShapeBand();
            shapeBand.Color = BandColor.Red;
            shapeBand.ValueComparisonType = ValueComparisonType.Number;
            shapeBand.Value = 10.5;
            shapeBand.Shape = ShapeType.ArrowUp;

            // Act
            var actualJson = shapeBand.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(actualJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }

        private class MockShapeBand : ShapeBand { }
    }
}
