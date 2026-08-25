using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Settings;

public class SparklineVisualizationSettingsFixture
{
    [Fact]
    public void Constructor_FieldsHaveDefaultValues_WhenInstanceIsCreated()
    {
        // Act
        var settings = new SparklineVisualizationSettings();

        // Assert
        Assert.Equal(SchemaTypeNames.SparklineVisualizationSettingsType, settings.SchemaTypeName);
        Assert.Equal(VisualizationTypes.SPARKLINE, settings.VisualizationType);
        Assert.Empty(settings.ColumnSettings);
        Assert.Equal(SparklineChartType.Line, settings.ChartType);
        Assert.True(settings.ShowLastTwoValues);
        Assert.True(settings.ShowDifference);
        Assert.False(settings.PositiveIsRed);
    }

    [Fact]
    public void ToJsonString_GeneratesCorrectJson_WhenSerialized()
    {
        // Arrange
        var expectedJson =
            """
            {
              "_type" : "SparklineVisualizationSettingsType",
              "ChartType" : "Line",
              "PositiveIsRed" : true,
              "ShowLastTwoValues" : false,
              "ShowDifference" : false,
              "FontSize" : "Medium",
              "VisualizationColumns" : [],
              "Style" : {
                "FixedLeftColumns" : false,
                "TextAlignment" : "Inherit",
                "NumericAlignment" : "Inherit",
                "DateAlignment" : "Inherit"
              },
              "VisualizationType" : "SPARKLINE"
            }
            """;

        var settings = new SparklineVisualizationSettings
        {
            _visualizationDataSpec = new SparklineVisualizationDataSpec()
            {
                IndicatorType = IndicatorVisualizationType.MonthToDatePreviousMonth
            },
            ChartType = SparklineChartType.Line,
            ShowLastTwoValues = false,
            ShowDifference = false,
            PositiveIsRed = true,
            AggregationType = SparklineAggregationType.Days,
            FontSize = FontSize.Medium,
            NumberOfPeriods = 5,
        };

        // Act
        var actualJson = settings.ToJsonString();
        var expectedJObject = JObject.Parse(expectedJson);
        var actualJObject = JObject.Parse(actualJson);

        // Assert
        Assert.Equal(expectedJObject, actualJObject);
    }
    
    [Theory]
    [InlineData(IndicatorVisualizationType.LastDays, SparklineAggregationType.Days)]
    [InlineData(IndicatorVisualizationType.LastMonths, SparklineAggregationType.Months)]
    [InlineData(IndicatorVisualizationType.LastYears, SparklineAggregationType.Years)]
    internal void AggregationType_SetAndGet_ConvertsValuesCorrectly(
        IndicatorVisualizationType indicatorType,
        SparklineAggregationType expectedAggregationType)
    {
        // Arrange
        var settings = new SparklineVisualizationSettings
        {
            _visualizationDataSpec = new SparklineVisualizationDataSpec
            {
                IndicatorType = indicatorType
            }
        };

        // Act
        var aggregationType = settings.AggregationType;

        // Assert
        Assert.Equal(expectedAggregationType, aggregationType);
    }

    [Theory]
    [InlineData(SparklineAggregationType.Days, IndicatorVisualizationType.LastDays)]
    [InlineData(SparklineAggregationType.Months, IndicatorVisualizationType.LastMonths)]
    [InlineData(SparklineAggregationType.Years, IndicatorVisualizationType.LastYears)]
    internal void AggregationType_SetAndGet_UpdatesVisualizationDataSpec(
        SparklineAggregationType aggregationType,
        IndicatorVisualizationType expectedIndicatorType)
    {
        // Arrange
        var settings = new SparklineVisualizationSettings
        {
            _visualizationDataSpec = new SparklineVisualizationDataSpec()
        };

        // Act
        settings.AggregationType = aggregationType;

        // Assert
        Assert.Equal(expectedIndicatorType, settings._visualizationDataSpec.IndicatorType);
    }

    [Fact]
    public void NumberOfPeriods_SetAndGet_UpdatesVisualizationDataSpec()
    {
        // Arrange
        var settings = new SparklineVisualizationSettings
        {
            _visualizationDataSpec = new SparklineVisualizationDataSpec()
        };

        // Act
        settings.NumberOfPeriods = 10;

        // Assert
        Assert.Equal(10, settings.NumberOfPeriods);
        Assert.Equal(10, settings._visualizationDataSpec.NumberOfPeriods);
    }
    
    [Theory]
    [InlineData(IndicatorVisualizationType.LastDays, SparklineAggregationType.Days)]
    [InlineData(IndicatorVisualizationType.LastMonths, SparklineAggregationType.Months)]
    [InlineData(IndicatorVisualizationType.LastYears, SparklineAggregationType.Years)]
    [InlineData((IndicatorVisualizationType)99, SparklineAggregationType.Years)]
    internal void ConvertIndicatorVisualizationTypeToSparklineAggregationType_MapsCorrectly_WhenCalled(
        IndicatorVisualizationType inputType, 
        SparklineAggregationType expectedOutputType)
    {
        // Arrange
        var settings = new SparklineVisualizationSettings
        {
            _visualizationDataSpec = new SparklineVisualizationDataSpec()
        };

        // Act
        var result = settings.ConvertIndicatorVisualizationTypeToSparklineAggregationType(inputType);

        // Assert
        Assert.Equal(expectedOutputType, result);
    }

    [Theory]
    [InlineData(SparklineAggregationType.Days, IndicatorVisualizationType.LastDays)]
    [InlineData(SparklineAggregationType.Months, IndicatorVisualizationType.LastMonths)]
    [InlineData(SparklineAggregationType.Years, IndicatorVisualizationType.LastYears)]
    [InlineData((SparklineAggregationType)99, IndicatorVisualizationType.LastYears)]
    internal void ConvertSparklineAggregationTypeToIndicatorVisualizationType_MapsCorrectly_WhenCalled(
        SparklineAggregationType inputType, 
        IndicatorVisualizationType expectedOutputType)
    {
        // Arrange
        var settings = new SparklineVisualizationSettings
        {
            _visualizationDataSpec = new SparklineVisualizationDataSpec()
        };

        // Act
        var result = settings.ConvertSparklineAggregationTypeToIndicatorVisualizationType(inputType);

        // Assert
        Assert.Equal(expectedOutputType, result);
    }
}
