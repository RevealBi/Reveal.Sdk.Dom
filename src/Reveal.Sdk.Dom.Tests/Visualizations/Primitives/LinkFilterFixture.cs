using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Visualizations;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Primitives
{
    public class LinkFilterFixture
    {
        [Fact]
        public void Constructor_SetValues_WithoutParams()
        {
            // Act
            var instance = new LinkFilter();

            // Assert
            Assert.Equal(LinkFilterType.Column, instance.Type);
        }

        [Fact]
        public void Constructor_SetValues_WithParams()
        {
            // Act
            var instance = new LinkFilter("Name", "TargetId", "Value", LinkFilterType.Literal);

            // Assert
            Assert.Equal("Name", instance.Name);
            Assert.Equal("TargetId", instance.TargetFilterId);
            Assert.Equal("Value", instance.Value);
            Assert.Equal(LinkFilterType.Literal, instance.Type);
        }

        [Fact]
        public void ToJsonString_CreateCorrectJsonString_WithoutCondition()
        {
            // Arrange
            var instance = new LinkFilter("Name", "TargetId", "Value", LinkFilterType.Literal);

            var expectedJson = """
            {
              "Name": "Name",
              "Namespace": "TargetId",
              "Type": "Literal",
              "Value": "Value"
            }
            """;

            // Act
            var actualJson = instance.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(actualJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }
    }
}
