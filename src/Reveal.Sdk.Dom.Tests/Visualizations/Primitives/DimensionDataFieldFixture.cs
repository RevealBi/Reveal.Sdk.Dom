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
    public class DimensionDataFieldFixture
    {
        [Fact]
        public void Constructor_SetDefaultValues_WhenConstructed()
        {
            // Arrange
            var fieldName = "FieldName";
            // Act
            var instance = new TestDimensionDataField(fieldName);

            // Assert
            Assert.Equal(fieldName, instance.FieldName);
            Assert.Empty(instance.DrillDownElements);
            Assert.Empty(instance.ExpandedItems);
        }

        [Fact]
        public void ToJsonString_CreateCorrectJsonString_WithoutCondition()
        {
            // Arrange
            var expectedJson = """
            {
              "DrillDownElements": [
                "DrillDownElement1",
                "DrillDownElement2"
              ],
              "ExpandedItems": [
                "ExpandedItem1",
                "ExpandedItem2"
              ],
              "SortByField": "Test SortByField",
              "FieldName": "Field name"
            }
            """;

            var instance = new TestDimensionDataField("Field name");
            instance.DrillDownElements = new List<string>()
            {
                "DrillDownElement1",
                "DrillDownElement2"
            };

            instance.ExpandedItems = new List<string>()
            {
                "ExpandedItem1",
                "ExpandedItem2"
            };

            instance.SortByField = "Test SortByField";

            // Act
            var actualJson = instance.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(actualJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }

        private class TestDimensionDataField : DimensionDataField
        {
            public TestDimensionDataField(string fieldName) : base(fieldName)
            {
            }
        }
    }
}
