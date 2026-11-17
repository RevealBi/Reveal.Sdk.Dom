using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Data;
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
    public class TextFieldFixture
    {
        [Fact]
        public void Constructor_SetDefaultValues_WhenConstructed()
        {
            // Act
            var field = new TextField();

            // Assert
            Assert.Equal(DataType.String, ((IFieldDataType)field).DataType);
            Assert.Equal(string.Empty, field.FieldName);
        }

        [Fact]
        public void Constructor_SetFieldName_WhenConstructed()
        {
            // Arrange
            var fieldName = "FieldName";

            // Act
            var field = new TextField(fieldName);

            // Assert
            Assert.Equal(DataType.String, ((IFieldDataType)field).DataType);
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
                "FieldType": "String"
            }
            """;
            var fieldName = "FieldName";

            // Act
            var field = new TextField(fieldName);

            // Act
            var actualJson = field.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(actualJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }

        [Fact]
        public void LoadFromJson_KeepsTopNRule_WhenTextFieldHasNumberFilter()
        {
            // Arrange: a Top N rule on a text field, as @revealbi/dom's addDataFilter writes it
            var source = new DataSourceItemFactory().Create(DataSourceType.REST, "", "")
                .SetFields(new List<IField> { new TextField("City"), new NumberField("Sales") });
            var document = new RdashDocument();
            document.Visualizations.Add(new ColumnChartVisualization(source).SetLabel("City").SetValue("Sales"));
            var json = JObject.Parse(document.ToJsonString());
            var cityJson = json["Widgets"][0]["DataSpec"]["Fields"].First(f => (string)f["FieldName"] == "City");
            cityJson["Filter"] = JObject.Parse("""{"_type":"NumberFilterType","FilterType":"FilterByRule","SelectedValues":[],"RuleType":"TopItems","Value":10}""");

            // Act
            var loaded = RdashDocument.LoadFromJson(json.ToString());
            var savedJson = JObject.Parse(loaded.ToJsonString());

            // Assert
            var city = Assert.IsType<TextField>(loaded.Visualizations[0].DataDefinition.AsTabular().Fields.First(f => f.FieldName == "City"));
            var topN = Assert.IsType<NumberFilter>(city.Filter);
            Assert.Equal(FilterType.FilterByRule, topN.FilterType);
            Assert.Equal(NumberRuleType.TopItems, topN.RuleType);
            Assert.Equal(10, topN.Value);
            Assert.Null(city.DataFilter);

            var savedFilter = savedJson["Widgets"][0]["DataSpec"]["Fields"].First(f => (string)f["FieldName"] == "City")["Filter"];
            Assert.Equal("NumberFilterType", (string)savedFilter["_type"]);
            Assert.Equal("TopItems", (string)savedFilter["RuleType"]);
            Assert.Equal(10, (double)savedFilter["Value"]);
        }
    }
}
