using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Core.Constants;
using Xunit;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Primitives
{
    public class TabularColumnFixture
    {
        [Fact]
        public void Constructor_SetDefaultValues_WhenConstructed()
        {

            // Act
            var column = new TabularColumn();

            // Assert
            Assert.Equal(SchemaTypeNames.TabularColumnSpecType, column.SchemaTypeName);
            Assert.Empty(column.FieldName);
            Assert.Equal(SortingType.None, column.Sorting);
        }

        [Fact]
        public void Constructor_SetProvidedFieldName_WhenConstructedWithDataField()
        {
            // Arrange
            var fieldName = "test field";

            // Act
            var column = new TabularColumn(fieldName);

            // Assert
            Assert.Equal(fieldName, column.FieldName);
        }

        [Fact]
        public void ConvertToJson_OutputAsExpected_WhenCalled()
        {
            // Arrange
            var column = new TabularColumn()
            {
                FieldName = "test field",
                Sorting = SortingType.Desc
            };

            var expectedJson = JObject.Parse("""
            {
                "_type": "TabularColumnSpecType",
                "FieldName": "test field",
                "IsHidden": false,
                "Sorting": "Desc"
            }
            """);

            // Act
            var outputJson = JsonConvert.SerializeObject(column, Formatting.Indented, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            // Assert
            Assert.Equal(expectedJson.ToString(), outputJson);
        }

        [Fact]
        public void LoadFromJson_KeepsIsHidden_WhenColumnIsHidden()
        {
            // Arrange: a grid column hidden in the Reveal editor
            var source = new DataSourceItemFactory().Create(DataSourceType.REST, "", "")
                .SetFields(new List<IField> { new TextField("City"), new NumberField("Sales") });
            var document = new RdashDocument();
            document.Visualizations.Add(new GridVisualization(source).SetColumns("City", "Sales"));
            var json = JObject.Parse(document.ToJsonString());
            json["Widgets"][0]["VisualizationDataSpec"]["Columns"].First(c => (string)c["FieldName"] == "Sales")["IsHidden"] = true;

            // Act
            var saved = JObject.Parse(RdashDocument.LoadFromJson(json.ToString()).ToJsonString());

            // Assert
            var sales = saved["Widgets"][0]["VisualizationDataSpec"]["Columns"].First(c => (string)c["FieldName"] == "Sales");
            Assert.True((bool)sales["IsHidden"]);
        }
    }
}