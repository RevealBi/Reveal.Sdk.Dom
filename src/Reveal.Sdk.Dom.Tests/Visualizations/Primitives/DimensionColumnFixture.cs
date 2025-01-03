using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Core.Constants;
using Xunit;
using Newtonsoft.Json;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Primitives
{
    public class DimensionColumnFixture
    {
        [Fact]
        public void Constructor_SchemaTypeIsDimensionColSpecType_WhenConstructed()
        {

            // Act
            var column = new DimensionColumn();

            // Assert
            Assert.Equal(SchemaTypeNames.DimensionColumnSpecType, column.SchemaTypeName);
        }

        [Fact]
        public void Constructor_SchemaTypeIsDimensionColSpecType_WhenConstructedWithDataField()
        {
            // Arrange
            var dataField = new TextDataField("test field");
            // Act
            var column = new DimensionColumn(dataField);

            // Assert
            Assert.Equal(dataField, column.DataField);
        }

        [Fact]
        public void ConvertToJson_UseExpectedFields_WhenCalled()
        {
            // Arrange
            var dataField = new TextDataField("test field");
            var column = new DimensionColumn(dataField);
            column.DataField = new TextDataField("test field");
            column.XmlaElement = new TestXmlaDimensionElement();

            var expectedJson = JObject.Parse("""
            {
              "_type": "DimensionColumnSpecType",
              "SummarizationField": {
                "_type": "SummarizationRegularFieldType",
                "DrillDownElements": [],
                "ExpandedItems": [],
                "FieldName": "test field"
              },
              "XmlaElement": {
                "DimensionType": "Regular",
                "DrillDownElements": [],
                "Sorting": "None",
                "FieldSortingByLabel": false,
                "FullyExpandedLevels": 0,
                "ExpandedItems": [],
                "DateAggregationType": "Year",
                "DateFiscalYearStartMonth": 0,
                "DrillDownMembers": []
              }
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

        private class TestXmlaDimensionElement : XmlaDimensionElement
        {
        }
    }
}