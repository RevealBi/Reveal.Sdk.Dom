using Moq;
using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Visualizations;
using System.Collections.Generic;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Primitives
{
    public class SummarizationSpecFixture
    {
        [Fact]
        public void Constructor_FieldsHaveDefaultValue_WithoutParameters()
        {
            // Act
            var summarizationSpec = new SummarizationSpec();

            // Assert
            Assert.Null(summarizationSpec.AdHocFields);
            Assert.Empty(summarizationSpec.Rows);
            Assert.Empty(summarizationSpec.Columns);
            Assert.Empty(summarizationSpec.Values);
            Assert.Empty(summarizationSpec.AdHocExpandedElements);
        }

        [Fact]
        public void ToJsonString_CreateCorrectJsonString_WithoutCondition()
        {
            // Arrange
            var expectedJson = """
                {
                  "HideGrandTotalRow": true,
                  "HideGrandTotalCol": true,
                  "AdHocFields": 12,
                  "Rows": [],
                  "Columns": [],
                  "Values": [],
                  "AdHocExpandedElements": []
                }
                """;
            var summarizationSpec = new SummarizationSpec()
            {
                HideGrandTotalRow = true,
                HideGrandTotalCol = true,
                AdHocFields = 12,
                Rows = new List<IDimensionDataField>(),
                Columns = new List<IDimensionDataField>(),
                AdHocExpandedElements = new List<AdHocExpandedElement>(),
                Values = new List<NumberDataField>(),
            };

            // Act
            var actualJson = summarizationSpec.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(actualJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }

    }
}
