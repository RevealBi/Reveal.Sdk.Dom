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
              "IsHidden": false,
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

        [Fact]
        public void LoadFromJson_KeepsFormatting_WhenDateFieldHasACustomFormat()
        {
            // Arrange: a date format set on the field in the Reveal editor
            var source = new DataSourceItemFactory().Create(DataSourceType.REST, "", "")
                .SetFields(new List<IField> { new DateField("Date"), new NumberField("Sales") });
            var document = new RdashDocument();
            document.Visualizations.Add(new GridVisualization(source).SetColumns("Date", "Sales"));
            var json = JObject.Parse(document.ToJsonString());
            json["Widgets"][0]["DataSpec"]["Fields"].First(f => (string)f["FieldName"] == "Date")["Formatting"] =
                JObject.Parse("""{"_type":"DateFormattingSpecType","OverrideDefaultSettings":false,"DateFormat":"dd-MMM-yyyy"}""");

            // Act
            var saved = JObject.Parse(RdashDocument.LoadFromJson(json.ToString()).ToJsonString());

            // Assert
            var date = saved["Widgets"][0]["DataSpec"]["Fields"].First(f => (string)f["FieldName"] == "Date");
            Assert.Equal("dd-MMM-yyyy", (string)date["Formatting"]["DateFormat"]);
        }
    }
}
