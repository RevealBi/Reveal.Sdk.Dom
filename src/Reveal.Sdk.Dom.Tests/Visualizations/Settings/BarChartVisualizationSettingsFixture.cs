using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Settings;

public class BarChartVisualizationSettingsFixture
{
    [Fact]
    public void Constructor_FieldsHaveDefaultValues_WhenInstanceIsCreated()
    {
        // Act
        var settings = new BarChartVisualizationSettings();

        // Assert
        Assert.True(settings.AutomaticLabelRotation);
        Assert.Equal(RdashChartType.Bar, settings.ChartType);
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
              "CategoryAxisGap": 0.4,
              "CategoryAxisOverlap": -0.2,
              "ShowTotalsInTooltip" : false,
              "TrendlineType" : "None",
              "AutomaticLabelRotation" : true,
              "SyncAxisVisibleRange" : false,
              "ZoomScaleHorizontal" : 1.0,
              "ZoomScaleVertical" : 1.0,
              "LeftAxisLogarithmic" : false,
              "ShowLegends" : true,
              "ChartType" : "Bar",
              "VisualizationType" : "CHART",
              "AxisTitlesMode" : "None",
            }
            """;

        var settings = new BarChartVisualizationSettings();

        // Act
        var actualJson = settings.ToJsonString();
        var expectedJObject = JObject.Parse(expectedJson);
        var actualJObject = JObject.Parse(actualJson);

        // Assert
        Assert.Equal(expectedJObject, actualJObject);
    }
}