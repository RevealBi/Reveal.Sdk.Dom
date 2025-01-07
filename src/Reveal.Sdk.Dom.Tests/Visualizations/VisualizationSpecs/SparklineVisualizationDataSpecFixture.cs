using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;
using System.Collections.Generic;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.VisualizationSpecs
{
    public class SparklineVisualizationDataSpecFixture
    {
        [Fact]
        public void Constructor_FieldsHaveDefaultValue()
        {
            // Act
            var sparklineVSDataSpec = new SparklineVisualizationDataSpec();

            // Assert
            Assert.Equal(SchemaTypeNames.SparklineVisualizationDataSpecType, sparklineVSDataSpec.SchemaTypeName);
            Assert.Equal(IndicatorVisualizationType.LastMonths, sparklineVSDataSpec.IndicatorType);
        }

        [Fact]
        public void ToJsonString_CreateCorrectJsonString_WithoutCondition()
        {
            // Arrange
            var expectedJson = """
                {
                  "_type": "BubbleVisualizationDataSpecType",
                  "NumberOfPeriods": 3,
                  "ShowIndicator": false,
                  "IndicatorType": "YearToDatePreviousYear",
                  "Date": {
                    "_type": "DimensionColumnSpecType"
                  },
                  "Value": [],
                  "AdHocFields": 12,
                  "FormatVersion": 2,
                  "AdHocExpandedElements": [],
                  "Rows": []
                }
                """;
            var sparklineVSDataSpec = new SparklineVisualizationDataSpec()
            {
                AdHocExpandedElements = new List<AdHocExpandedElement>(),
                AdHocFields = 12,
                Date = new DimensionColumn(),
                FormatVersion = 2,
                IndicatorType = IndicatorVisualizationType.YearToDatePreviousYear,
                NumberOfPeriods =3,
                Rows = new List<DimensionColumn>(),
                SchemaTypeName = SchemaTypeNames.BubbleVisualizationDataSpecType,
                ShowIndicator = false,
                Value = new List<MeasureColumn>()
            };

            // Act
            var actualJson = sparklineVSDataSpec.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(actualJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }
    }
}
