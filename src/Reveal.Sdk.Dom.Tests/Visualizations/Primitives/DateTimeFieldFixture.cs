using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Data;
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

        [Fact]
        public void LoadFromJson_KeepsFormatting_WhenDateTimeFieldHasACustomFormat()
        {
            // Arrange: a date and time format set on the field in the Reveal editor
            var source = new DataSourceItemFactory().Create(DataSourceType.REST, "", "")
                .SetFields(new List<IField> { new DateTimeField("Stamp"), new NumberField("Sales") });
            var document = new RdashDocument();
            document.Visualizations.Add(new GridVisualization(source).SetColumns("Stamp", "Sales"));
            var json = JObject.Parse(document.ToJsonString());
            json["Widgets"][0]["DataSpec"]["Fields"].First(f => (string)f["FieldName"] == "Stamp")["Formatting"] =
                JObject.Parse("""{"_type":"DateFormattingSpecType","OverrideDefaultSettings":false,"DateFormat":"dd-MMM-yyyy HH:mm"}""");

            // Act
            var saved = JObject.Parse(RdashDocument.LoadFromJson(json.ToString()).ToJsonString());

            // Assert
            var stamp = saved["Widgets"][0]["DataSpec"]["Fields"].First(f => (string)f["FieldName"] == "Stamp");
            Assert.Equal("dd-MMM-yyyy HH:mm", (string)stamp["Formatting"]["DateFormat"]);
        }
    }
}
