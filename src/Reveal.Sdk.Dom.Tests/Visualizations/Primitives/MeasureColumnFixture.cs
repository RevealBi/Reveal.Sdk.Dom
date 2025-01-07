using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Core.Constants;
using Xunit;
using Newtonsoft.Json;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Primitives
{
    public class MeasureColumnFixture
    {
        [Fact]
        public void Constructor_SchemaTypeIsDimensionColSpecType_WhenConstructed()
        {

            // Act
            var column = new MeasureColumn();

            // Assert
            Assert.Equal(SchemaTypeNames.MeasureColumnSpecType, column.SchemaTypeName);
        }

        [Fact]
        public void Constructor_SchemaTypeIsDimensionColSpecType_WhenConstructedWithDataField()
        {
            // Arrange
            var dataField = new NumberDataField("test field");

            // Act
            var column = new MeasureColumn(dataField);

            // Assert
            Assert.Equal(dataField, column.DataField);
        }

        [Fact]
        public void ConvertToJson_UseExpectedFields_WhenCalled()
        {
            // Arrange
            var column = new MeasureColumn();
            column.DataField = new NumberDataField("test field");
            column.XmlaMeasure = new XmlaMeasure();

            var expectedJson =  JObject.Parse("""
            {
              "_type": "MeasureColumnSpecType",
              "SummarizationField": {
                "_type": "SummarizationValueFieldType",
                "FieldLabel": "test field",
                "UserCaption": "test field",
                "IsHidden": false,
                "AggregationType": "Sum",
                "Sorting": "None",
                "IsCalculated": false,
                "FieldName": "test field"
              },
              "XmlaMeasure": {
                "IsHidden": false,
                "IsCalculated": false,
                "Sorting": "None",
                "Metadata": {}
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

    }
}