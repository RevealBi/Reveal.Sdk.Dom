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
    public class FormatingBaseFixture
    {
        [Fact]
        public void ToJsonString_CreateCorrectJsonString_WithoutCondition()
        {
            // Arrange
            var expectedJson = """
            {
              "OverrideDefaultSettings": false
            }
            """;

            var field = new TestFormattingBase();

            // Act
            var actualJson = field.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(actualJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }

        [Fact]
        public void LoadFromJson_KeepsLocale_WhenFormattingHasOne()
        {
            // Arrange: date and number formats with a locale, as the Reveal editor writes them
            var source = new DataSourceItemFactory().Create(DataSourceType.REST, "", "")
                .SetFields(new List<IField> { new DateField("Date"), new NumberField("Sales") });
            var document = new RdashDocument();
            document.Visualizations.Add(new GridVisualization(source).SetColumns("Date", "Sales"));
            var json = JObject.Parse(document.ToJsonString());
            var fields = json["Widgets"][0]["DataSpec"]["Fields"];
            fields.First(f => (string)f["FieldName"] == "Date")["Formatting"] =
                JObject.Parse("""{"_type":"DateFormattingSpecType","OverrideDefaultSettings":true,"Locale":"de-DE","DateFormat":"dd-MMM-yyyy"}""");
            fields.First(f => (string)f["FieldName"] == "Sales")["Formatting"] =
                JObject.Parse("""{"_type":"NumberFormattingSpecType","OverrideDefaultSettings":true,"Locale":"de-DE"}""");

            // Act
            var saved = JObject.Parse(RdashDocument.LoadFromJson(json.ToString()).ToJsonString());

            // Assert
            var savedFields = saved["Widgets"][0]["DataSpec"]["Fields"];
            Assert.Equal("de-DE", (string)savedFields.First(f => (string)f["FieldName"] == "Date")["Formatting"]["Locale"]);
            Assert.Equal("de-DE", (string)savedFields.First(f => (string)f["FieldName"] == "Sales")["Formatting"]["Locale"]);
        }

        private class TestFormattingBase : FormattingBase
        {
        }
    }
}
