using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;
using System.Collections.Generic;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.VisualizationSpecs
{
    public class IndicatorTargetVisualizationDataSpecFixture
    {
        [Fact]
        public void Constructor_FieldsHaveDefaultValue_WithoutParameters()
        {
            // Act
            var indicatorTargetVSDataSpec = new IndicatorTargetVisualizationDataSpec();

            // Assert
            Assert.Empty(indicatorTargetVSDataSpec.Target);
            Assert.Equal(KpiGoalPeriod.YearToDate, indicatorTargetVSDataSpec.DateFilterType);
            Assert.Equal(SchemaTypeNames.IndicatorTargetVisualizationDataSpecType, indicatorTargetVSDataSpec.SchemaTypeName);
        }

        [Fact]
        public void ToJsonString_CreateCorrectJsonString_WithoutCondition()
        {
            // Arrange
            var expectedJson = """
                {
                  "_type": "AssetVisualizationSettingsType",
                  "Target": [],
                  "DateFilterType": "PreviousMonth",
                  "CustomDateRange": {},
                  "Date": {
                    "_type": "DimensionColumnSpecType"
                  },
                  "Value": [],
                  "AdHocFields": 11,
                  "FormatVersion": 2,
                  "AdHocExpandedElements": [],
                  "Rows": []
                }
                """;
            var indicatorTargetVSDataSpec = new IndicatorTargetVisualizationDataSpec()
            {
                SchemaTypeName = SchemaTypeNames.AssetVisualizationSettingsType,
                Value = new List<MeasureColumn>(),
                AdHocExpandedElements = new List<AdHocExpandedElement>(),
                AdHocFields = 11,
                CustomDateRange = new DateRange(),
                Date = new DimensionColumn(),
                DateFilterType = KpiGoalPeriod.PreviousMonth,
                FormatVersion = 2,
                Rows = new List<DimensionColumn>(),
                Target = new List<MeasureColumn>(),
            };

            // Act
            var actualJson = indicatorTargetVSDataSpec.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(actualJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }
    }
}
