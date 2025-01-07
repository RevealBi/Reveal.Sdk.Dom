using Newtonsoft.Json;
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
    public class ConditionalFormattingFixture
    {
        [Fact]
        public void Constructor_SetDefaultBounds_WhenCreated()
        {
            // Arrange
            var expectedMinimum = new Bound() { ValueType = BoundValueType.LowestValue };
            var expectedMaximum = new Bound() { ValueType = BoundValueType.HighestValue };

            // Act
            var conditionalFormatting = new ConditionalFormatting();

            // Assert
            Assert.Equivalent(expectedMinimum, conditionalFormatting.Minimum);
            Assert.Equivalent(expectedMaximum, conditionalFormatting.Maximum);
        }
        [Fact]
        public void ToJsonString_CreateCorrectJsonString_WithoutCondition()
        {
            // Arrange
            var expectedJson = """
            {
                "Minimum": {
                    "ValueType": "LowestValue"
                },
                "Maximum": {
                    "ValueType": "HighestValue"
                },
                "Bands": [
                    {
                        "_type": "ConditionalFormattingBandType",
                        "Shape": "Circle",
                        "Type": "Percentage",
                        "Color": "Green",
                        "Value": 80.0
                    },
                    {
                        "_type": "ConditionalFormattingBandType",
                        "Shape": "Circle",
                        "Type": "Percentage",
                        "Color": "Yellow",
                        "Value": 50.0
                    },
                    {
                        "_type": "ConditionalFormattingBandType",
                        "Shape": "Circle",
                        "Type": "Percentage",
                        "Color": "Red"
                    }
                ]
            }
            """;
            var pivotVSDataSpec = new ConditionalFormatting();

            // Act
            var actualJson = pivotVSDataSpec.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(actualJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }
    }  
}
