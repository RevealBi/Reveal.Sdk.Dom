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
    public class DateFieldFixture
    {

        [Fact]
        public void Constructor_SetDefaultValues_WithoutFieldName()
        {
            // Act
            var instance = new DateField();

            // Assert
            Assert.Equal(string.Empty, instance.FieldName);
            Assert.Equal(DataType.Date, ((IFieldDataType)instance).DataType);
        }

        [Fact]
        public void Constructor_SetDefaultValues_WithFieldName()
        {
            // Arrange
            var fieldName = "FieldName";

            // Act
            var instance = new DateField(fieldName);

            // Assert
            Assert.Equal(fieldName, instance.FieldName);
            Assert.Equal(DataType.Date, ((IFieldDataType)instance).DataType);
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
              "FieldType": "Date"
            }
            """;

            var instance = new DateField("Field name");
            

            // Act
            var actualJson = instance.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(actualJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }
    }
}
