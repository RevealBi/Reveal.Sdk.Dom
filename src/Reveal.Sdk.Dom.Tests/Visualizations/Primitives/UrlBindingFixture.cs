using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Visualizations;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Primitives
{
    public class UrlBindingFixture
    {
        [Fact]
        public void ToJsonString_CreateCorrectJsonString_WithoutCondition()
        {
            // Arrange
            var expectedJson = """
            {
              "GlobalFilterId": "GlobalFilterId",
              "UrlExpression": "UrlExpression"
            }
            """;

            var instance = new UrlBinding()
            {
                GlobalFilterId = "GlobalFilterId",
                UrlExpression = "UrlExpression",    
            };

            // Act
            var actualJson = instance.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(actualJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }
    }
}
