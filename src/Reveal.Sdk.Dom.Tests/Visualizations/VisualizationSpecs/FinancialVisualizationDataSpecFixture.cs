using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;
using System.Collections.Generic;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.VisualizationSpecs
{
    public class FinancialVisualizationDataSpecFixture
    {
        [Fact]
        public void Constructor_FieldsHaveDefaultValue_WithoutParameters()
        {
            // Act
            var financialVSDataSpec = new FinancialVisualizationDataSpec();

            // Assert
            Assert.Equal(SchemaTypeNames.FinancialVisualizationDataSpecType, financialVSDataSpec.SchemaTypeName);
            Assert.Empty(financialVSDataSpec.Open);
            Assert.Empty(financialVSDataSpec.Close);
            Assert.Empty(financialVSDataSpec.High);
            Assert.Empty(financialVSDataSpec.Low);
        }

        [Fact]
        public void ToJsonString_CreateCorrectJsonString_WithoutCondition()
        {
            // Arrange
            var expectedJson = """
                {
                  "_type": "FinancialVisualizationDataSpecType",
                  "Open": [],
                  "High": [],
                  "Low": [],
                  "Close": [],
                  "AdHocFields": 13,
                  "FormatVersion": 5,
                  "AdHocExpandedElements": [],
                  "Rows": []
                }
                """;
            var financialVSDataSpec = new FinancialVisualizationDataSpec()
            {
                AdHocExpandedElements = new List<Dom.Visualizations.AdHocExpandedElement>(),
                AdHocFields = 13,
                FormatVersion = 5,
                Rows = new List<Dom.Visualizations.DimensionColumn>(),
                Open = new List<Dom.Visualizations.MeasureColumn>(),
                Close = new List<Dom.Visualizations.MeasureColumn>(),
                High = new List<Dom.Visualizations.MeasureColumn>(),
                Low = new List<Dom.Visualizations.MeasureColumn>()
            };

            // Act
            var actualJson = financialVSDataSpec.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(actualJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }
    }
}
