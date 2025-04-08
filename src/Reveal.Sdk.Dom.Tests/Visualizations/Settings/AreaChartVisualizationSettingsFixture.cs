using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Settings;

public class AreaChartVisualizationSettingsFixture
{
    [Fact]
    public void Constructor_FieldsHaveDefaultValues_WhenInstanceIsCreated()
    {
        // Act
        var settings = new AreaChartVisualizationSettings();

        // Assert
        Assert.NotNull(settings);
        Assert.Equal(RdashChartType.Area, settings.ChartType);
        Assert.Equal(SchemaTypeNames.ChartVisualizationSettingsType, settings.SchemaTypeName);
        Assert.True(settings.ShowLegend);
        Assert.False(settings.ShowTotalsInTooltip);
        Assert.Null(settings.StartColorIndex);
        Assert.False(settings.SyncAxis);
        Assert.Equal(default(TrendlineType), settings.Trendline);
        Assert.Equal(VisualizationTypes.CHART, settings.VisualizationType);
        Assert.False(settings.YAxisIsLogarithmic);
        Assert.Null(settings.YAxisMaxValue);
        Assert.Null(settings.YAxisMinValue);
        Assert.Equal(1.0, settings.ZoomLevel);
        Assert.Equal(1.0, settings.ZoomScaleHorizontal);
        Assert.Equal(1.0, settings.ZoomScaleVertical);
    }

    [Fact]
    public void ToJsonString_GeneratesCorrectJson_WhenSerialized()
    {
        // Arrange
        var expectedJson =
            """
            {
              "_type" : "ChartVisualizationSettingsType",
              "ShowTotalsInTooltip" : false,
              "TrendlineType" : "None",
              "AutomaticLabelRotation" : true,
              "SyncAxisVisibleRange" : false,
              "ZoomScaleHorizontal" : 1.0,
              "ZoomScaleVertical" : 1.0,
              "LeftAxisLogarithmic" : false,
              "LeftAxisMinValue" : null,
              "LeftAxisMaxValue" : null,
              "AxisTitlesMode" : "None",
              "ShowLegends" : true,
              "BrushOffsetIndex" : null,
              "ChartType" : "Area",
              "VisualizationType" : "CHART"
            }
            """;

        var settings = new AreaChartVisualizationSettings();

        // Act
        var actualJson = JsonConvert.SerializeObject(settings);
        var expectedJObject = JObject.Parse(expectedJson);
        var actualJObject = JObject.Parse(actualJson);

        // Assert
        Assert.Equal(expectedJObject, actualJObject);
    }
}