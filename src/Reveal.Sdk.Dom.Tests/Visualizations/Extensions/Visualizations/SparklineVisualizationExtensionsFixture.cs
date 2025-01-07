using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;
using System.Collections.Generic;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Extensions.Visualizations
{
    public class SparklineVisualizationExtensionsFixture
    {
        [Fact]
        public void ConfigureSettings_UpdateSpartlineVSSettings_WithoutConditions()
        {
            // Arrange
            var visualization = new SparklineVisualization();
            var expectedSettings = new SparklineVisualizationSettings()
            {
                ChartType = SparklineChartType.Line,
                DateFieldAlignment = Alignment.Center,
                FontSize = FontSize.Large,
                NumericFieldAlignment = Alignment.Left,
                PositiveIsRed = false,
                SchemaTypeName = "Sparkline VS Type Name",
                ShowDifference = false,
                ShowLastTwoValues = true,
                Style = new GridVisualizationStyle()
                {
                    DateAlignment = Alignment.Left,
                    FixedLeftColumns = true,
                    NumericAlignment = Alignment.Right,
                    TextAlignment = Alignment.Inherit
                },
                TextFieldAlignment = Alignment.Right,
                VisualizationType = "Sparkline VS Type",
                _visualizationDataSpec = new SparklineVisualizationDataSpec()
                {
                    AdHocExpandedElements = new List<AdHocExpandedElement>(),
                    AdHocFields = 12,
                    Value = new List<MeasureColumn>()
                    {
                        new MeasureColumn(){ DataField = new NumberDataField("Test Number Field") }
                    },
                    Date = new DimensionColumn() { DataField = new DateDataField("Test Date Field") },
                    FormatVersion = 2,
                    IndicatorType = IndicatorVisualizationType.QuarterToDatePreviousYear,
                    NumberOfPeriods = 3,
                    Rows = new List<DimensionColumn>()
                    {
                        new DimensionColumn() { DataField = new DateDataField("Date field") }
                    },
                    SchemaTypeName = "Schema Type Name",
                    ShowIndicator = false
                }
            };
            expectedSettings.AggregationType = SparklineAggregationType.Years;
            expectedSettings.NumberOfPeriods = 2;
            //expectedSettings.
            var action = (SparklineVisualizationSettings settings) =>
            {
                settings._visualizationDataSpec = expectedSettings._visualizationDataSpec;
                settings.AggregationType = expectedSettings.AggregationType;
                settings.ChartType = expectedSettings.ChartType;
                settings.DateFieldAlignment = expectedSettings.DateFieldAlignment;
                settings.FontSize = expectedSettings.FontSize;
                settings.NumberOfPeriods = expectedSettings.NumberOfPeriods;
                settings.NumericFieldAlignment = expectedSettings.NumericFieldAlignment;
                settings.PositiveIsRed = expectedSettings.PositiveIsRed;
                settings.SchemaTypeName = expectedSettings.SchemaTypeName;
                settings.ShowDifference = expectedSettings.ShowDifference;
                settings.ShowLastTwoValues = expectedSettings.ShowLastTwoValues;
                settings.Style = expectedSettings.Style;
                settings.TextFieldAlignment = expectedSettings.TextFieldAlignment;
                settings.VisualizationType = expectedSettings.VisualizationType;
            };

            // Act
            visualization.ConfigureSettings(action);

            // Assert
            Assert.Equivalent(expectedSettings, visualization.Settings);
        }
    }
}
