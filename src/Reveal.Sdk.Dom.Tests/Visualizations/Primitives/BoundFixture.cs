using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Visualizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Primitives
{
    public class BoundFixture
    {
        [Fact]
        public void Constructor_FieldsHaveDefaultValue_WithoutParameters()
        {
            // Act
            var bound = new Bound();

            // Assert
            Assert.Equal(BoundValueType.NumberValue, bound.ValueType);
            Assert.Null(bound.Value);
        }

        [Fact]
        public void ToJsonString_CreateExpectedJson_WithoutCondition()
        {
            // Arrange
            var expectedJson = """
                {
                  "ValueType": "HighestValue",
                  "Value": 12.05
                }
            """;

            var bound = new Bound()
            {
                ValueType = BoundValueType.HighestValue,
                Value = 12.05
            };

            // Act
            var json = bound.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(json);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);

        }
    }
}
