using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Core.Constants;
using Xunit;
using Newtonsoft.Json;

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
    }
}