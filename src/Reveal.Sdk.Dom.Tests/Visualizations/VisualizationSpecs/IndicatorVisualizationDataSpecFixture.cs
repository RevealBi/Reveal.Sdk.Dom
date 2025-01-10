using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;
using System.Collections.Generic;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.VisualizationSpecs
{
    public class IndicatorVisualizationDataSpecFixture
    {
        [Fact]
        public void Constructor_FieldsHaveDefaultValue_WithoutParameters()
        {
            // Act
            var indicatorVSDataSpec = new IndicatorVisualizationDataSpec();

            // Assert
            Assert.Equal(IndicatorVisualizationType.YearToDatePreviousYear, indicatorVSDataSpec.IndicatorType);
            Assert.Equal(SchemaTypeNames.IndicatorVisualizationDataSpecType, indicatorVSDataSpec.SchemaTypeName);
        }

        [Fact]
        public void ToJsonString_CreateCorrectJsonString_WithoutCondition()
        {
            // Arrange
            var expectedJson = """
                {
                  "_type": "AssetVisualizationSettingsType",
                  "IndicatorType": "QuarterToDatePreviousQuarter",
                  "Date": {
                    "_type": "DimensionColumnSpecType"
                  },
                  "Value": [],
                  "AdHocFields": 6,
                  "FormatVersion": 9,
                  "AdHocExpandedElements": [],
                  "Rows": []
                }
                """;
            var indicatorVSDataSpec = new IndicatorVisualizationDataSpec()
            {
                SchemaTypeName = SchemaTypeNames.AssetVisualizationSettingsType,
                Value = new List<MeasureColumn>(),
                AdHocExpandedElements = new List<AdHocExpandedElement>(),
                AdHocFields = 6,
                Date = new DimensionColumn(),
                FormatVersion = 9,
                IndicatorType = IndicatorVisualizationType.QuarterToDatePreviousQuarter,
                Rows = new List<DimensionColumn>()
            };

            // Act
            var actualJson = indicatorVSDataSpec.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(actualJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }
    }
}
