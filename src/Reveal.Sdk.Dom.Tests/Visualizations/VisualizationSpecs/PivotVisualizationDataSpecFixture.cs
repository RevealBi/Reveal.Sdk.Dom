using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;
using System.Collections.Generic;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.VisualizationSpecs
{
    public class PivotVisualizationDataSpecFixture
    {
        [Fact]
        public void Constructor_FieldsHaveDefaultValue_WithoutParameters()
        {
            // Act
            var pivotVSDataSpec = new PivotVisualizationDataSpec();

            // Assert
            Assert.Equal(SchemaTypeNames.PivotVisualizationDataSpecType, pivotVSDataSpec.SchemaTypeName);
            Assert.Empty(pivotVSDataSpec.Columns);
            Assert.Empty(pivotVSDataSpec.Values);
        }

        [Fact]
        public void ToJsonString_CreateCorrectJsonString_WithoutCondition()
        {
            // Arrange
            var expectedJson = """
                {
                  "_type": "Test Schema Type Name",
                  "Columns": [],
                  "Values": [],
                  "ShowGrandTotals": true,
                  "AdHocFields": 12,
                  "FormatVersion": 2,
                  "AdHocExpandedElements": [],
                  "Rows": []
                }
                """;
            var pivotVSDataSpec = new PivotVisualizationDataSpec()
            {
                AdHocExpandedElements = new List<Dom.Visualizations.AdHocExpandedElement>(),
                SchemaTypeName = "Test Schema Type Name",
                AdHocFields = 12,
                Columns = new List<Dom.Visualizations.DimensionColumn>(),
                FormatVersion = 2,
                Rows = new List<Dom.Visualizations.DimensionColumn>(),
                ShowGrandTotals = true,
                Values = new List<Dom.Visualizations.MeasureColumn>()
            };

            // Act
            var actualJson = pivotVSDataSpec.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(actualJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }
    }
}
