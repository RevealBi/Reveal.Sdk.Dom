using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Visualizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Primitives
{
    public class DateDataFieldFixture
    {
        [Fact]
        public void Constructor_SetDefaultValues_WithoutFieldName()
        {
            // Act
            var instance = new DateDataField();

            // Assert
            Assert.Equal(string.Empty, instance.FieldName);
            Assert.Equal(SchemaTypeNames.SummarizationDateFieldType, instance.SchemaTypeName);
            Assert.Equal(DateAggregationType.Year, instance.AggregationType);
        }

        [Fact]
        public void Constructor_SetDefaultValues_WithFieldName()
        {
            // Arrange
            var fieldName = "FieldName";

            // Act
            var instance = new DateDataField(fieldName);

            // Assert
            Assert.Equal(fieldName, instance.FieldName);
            Assert.Equal(SchemaTypeNames.SummarizationDateFieldType, instance.SchemaTypeName);
            Assert.Equal(DateAggregationType.Year, instance.AggregationType);
        }

        [Fact]
        public void ToJsonString_CreateCorrectJsonString_WithoutCondition()
        {
            // Arrange
            var expectedJson = """
            {
              "_type": "SummarizationDateFieldType",
              "DateAggregationType": "Quarter",
              "DateFormatting": {
                "_type": "DateFormattingSpecType",
                "DateFormat": "yyyy-MM-dd",
                "OverrideDefaultSettings": false
              },
              "DrillDownElements": [],
              "ExpandedItems": [],
              "FieldName": "Field name"
            }
            """;

            var instance = new DateDataField("Field name")
            {
                Formatting = new DateFormatting("yyyy-MM-dd"),
                AggregationType = DateAggregationType.Quarter
            };

            // Act
            var actualJson = instance.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(actualJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }
    }
}
