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
    public class TimeFieldFixture
    {
        [Fact]
        public void Constructor_SetDefaultValues_WhenConstructed()
        {
            // Act
            var field = new TimeField();

            // Assert
            Assert.Equal(DataType.Time, ((IFieldDataType)field).DataType);
            Assert.Equal(string.Empty, field.FieldName);
        }

        [Fact]
        public void Constructor_SetFieldName_WhenConstructedWithFieldName()
        {
            // Arrange
            var fieldName = "FieldName";

            // Act
            var field = new TimeField(fieldName);

            // Assert
            Assert.Equal(DataType.Time, ((IFieldDataType)field).DataType);
            Assert.Equal(fieldName, field.FieldName);
        }

        [Fact]
        public void ToJsonString_CreateCorrectJsonString_WithoutCondition()
        {
            // Arrange
            var expectedJson = """
            {
                "FieldName": "FieldName",
                "FieldLabel": "FieldName",
                "UserCaption": "FieldName",
                "IsCalculated": false,
                "Properties": {},
                "Sorting": "None",
                "FieldType": "Time"
            }
            """;
            var fieldName = "FieldName";

            // Act
            var field = new TimeField(fieldName);

            // Act
            var actualJson = field.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(actualJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }

        [Fact]
        public void LoadFromJson_KeepsFormatting_WhenTimeFieldHasACustomFormat()
        {
            // Arrange: a time format set on the field in the Reveal editor
            var source = new DataSourceItemFactory().Create(DataSourceType.REST, "", "")
                .SetFields(new List<IField> { new TimeField("Clock"), new NumberField("Sales") });
            var document = new RdashDocument();
            document.Visualizations.Add(new GridVisualization(source).SetColumns("Clock", "Sales"));
            var json = JObject.Parse(document.ToJsonString());
            json["Widgets"][0]["DataSpec"]["Fields"].First(f => (string)f["FieldName"] == "Clock")["Formatting"] =
                JObject.Parse("""{"_type":"DateFormattingSpecType","OverrideDefaultSettings":false,"DateFormat":"HH:mm:ss"}""");

            // Act
            var saved = JObject.Parse(RdashDocument.LoadFromJson(json.ToString()).ToJsonString());

            // Assert
            var clock = saved["Widgets"][0]["DataSpec"]["Fields"].First(f => (string)f["FieldName"] == "Clock");
            Assert.Equal("HH:mm:ss", (string)clock["Formatting"]["DateFormat"]);
        }
    }
}

