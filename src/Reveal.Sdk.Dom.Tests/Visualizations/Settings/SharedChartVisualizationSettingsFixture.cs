using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Settings;

public class SharedChartVisualizationSettingsFixture
{
    [Fact]
    public void Constructor_FieldsHaveDefaultValue_WhenInstanceIsCreated()
    {
        // Act
        var settings = new TestSharedChartVisualizationSettings();

        // Assert
        Assert.True(settings.AutomaticLabelRotation);
        Assert.False(settings.SyncAxis);
        Assert.Equal(1.0, settings.ZoomScaleHorizontal);
        Assert.Equal(1.0, settings.ZoomScaleVertical);
        Assert.Null(settings.StartColorIndex);
        Assert.Equal("CHART", settings.VisualizationType);
        Assert.Equal("ChartVisualizationSettingsType", settings.SchemaTypeName);
        Assert.Equal(default(RdashChartType), settings.ChartType);
        Assert.False(settings.YAxisIsLogarithmic);
        Assert.Null(settings.YAxisMinValue);
        Assert.Null(settings.YAxisMaxValue);
    }

    [Theory]
    [InlineData(RdashChartType.Bar, 0.5, 0.5, 1.0)]
    [InlineData(RdashChartType.StackedBar, 0.8, 0.8, 1.0)]
    [InlineData(RdashChartType.Column, 0.5, 1.0, 0.5)]
    internal void ZoomLevel_SetZoomLevel_BasedOnChartType(
        RdashChartType chartType,
        double zoomLevelInput,
        double expectedZoomVertical,
        double expectedZoomHorizontal)
    {
        // Arrange
        var settings = new TestSharedChartVisualizationSettings
        {
            ChartType = chartType,
        };

        // Act
        settings.ZoomLevel = zoomLevelInput;

        // Assert
        Assert.Equal(expectedZoomVertical, settings.ZoomScaleVertical);
        Assert.Equal(expectedZoomHorizontal, settings.ZoomScaleHorizontal);
    }

    [Theory]
    [InlineData(1.2, 1.0)]
    [InlineData(-0.5, 0.0)]
    [InlineData(0.75, 0.75)]
    public void ZoomLevel_SetZoomLevel_CoercesValueWithinRange(
        double inputValue,
        double expectedValue)
    {
        // Arrange
        var settings = new TestSharedChartVisualizationSettings
        {
            ChartType = RdashChartType.Column
        };

        // Act
        settings.ZoomLevel = inputValue;

        // Assert
        Assert.InRange(settings.ZoomScaleHorizontal, expectedValue - 0.0001, expectedValue + 0.0001);
    }

    [Fact]
    public void ToJsonString_SerializesSettings_GeneratesCorrectJson()
    {
        // Arrange
        var expectedJson =
            """
            {
              "_type" : "ChartVisualizationSettingsType",
              "ChartType" : 0,
              "AutomaticLabelRotation" : true,
              "SyncAxisVisibleRange" : false,
              "ZoomScaleHorizontal" : 1.0,
              "ZoomScaleVertical" : 1.0,
              "LeftAxisLogarithmic" : false,
              "ShowLegends" : true,
              "VisualizationType" : "CHART"
            }
            """;

        var settings = new TestSharedChartVisualizationSettings();

        // Act
        var actualJson = settings.ToJsonString();
        var expectedJObject = JObject.Parse(expectedJson);
        var actualJObject = JObject.Parse(actualJson);

        // Assert
        Assert.Equal(expectedJObject, actualJObject);
    }

    private class TestSharedChartVisualizationSettings : SharedChartVisualizationSettings
    {
        public RdashChartType ChartType { get; set; }
    }
}