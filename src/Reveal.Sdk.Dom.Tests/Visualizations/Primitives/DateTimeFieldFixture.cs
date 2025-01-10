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
    public class DateTimeFieldFixture
    {
        [Fact]
        public void Constructor_SetDefaultValues_WhenConstructedEmpty()
        {
            // Act
            var instance = new DateTimeField();

            // Assert
            Assert.Equal(string.Empty, instance.FieldName);
            Assert.Equal(DataType.DateTime, ((IFieldDataType)instance).DataType);
        }

        [Fact]
        public void Constructor_SetDefaultValues_WhenConstructedWithFieldName()
        {
            // Arrange
            var fieldName = "FieldName";
            // Act
            var instance = new DateTimeField(fieldName);

            // Assert
            Assert.Equal(fieldName, instance.FieldName);
            Assert.Equal(DataType.DateTime, ((IFieldDataType)instance).DataType);
        }

        [Fact]
        public void ToJsonString_CreateCorrectJsonString_WithoutCondition()
        {
            // Arrange
            var expectedJson = """
            {
              "FieldName": "Field name",
              "FieldLabel": "Field name",
              "UserCaption": "Field name",
              "IsCalculated": false,
              "Properties": {},
              "Sorting": "None",
              "FieldType": "DateTime"
            }
            """;

            var instance = new DateTimeField("Field name");
            

            // Act
            var actualJson = instance.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(actualJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }
    }
}
