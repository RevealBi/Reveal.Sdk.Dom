using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Filters;
using Reveal.Sdk.Dom.Visualizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Primitives
{
    public class DataSpecBindingsFixture
    {
        [Fact]
        public void Constructor_SetDefaultValues_WhenConstructed()
        {
            // Act
            var instance = new DataSpecBindings();

            // Assert
            Assert.Empty(instance.Bindings);
        }

        [Fact]
        public void ToJsonString_CreateCorrectJsonString_WithoutCondition()
        {
            // Arrange
            var instance = new DataSpecBindings();
            instance.UrlBinding = new UrlBinding()
            {
                GlobalFilterId = "Test GlobalFilterId",
                UrlExpression = "Test UrlExpression"
            };

            instance.Bindings = new List<Binding>() {
                new TestBinding()
                {
                    Operator = BindingOperatorType.Contains,
                }
            };

            var expectedJson = """
            {
              "UrlBinding": {
                "GlobalFilterId": "Test GlobalFilterId",
                "UrlExpression": "Test UrlExpression"
              },
              "Bindings": [
                {
                  "Operator": "Contains"
                }
              ]
            }
            """;

            // Act
            var actualJson = instance.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(actualJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }

        private class TestBinding : Binding
        {
        }
    }
}
