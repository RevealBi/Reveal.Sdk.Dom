using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Settings;

public class CategoryChartVisualizationSettingsFixture
{
    [Fact]
    public void Constructor_FieldsHaveDefaultValue_WhenInitializing()
    {
        // Act
        var settings = new TestCategoryChartVisualizationSettings();

        // Assert
        Assert.False(settings.ShowTotalsInTooltip);
        Assert.Equal(default(TrendlineType), settings.Trendline);
        Assert.True(settings.AutomaticLabelRotation);
        Assert.Equal(RdashChartType.Column, settings.ChartType);
        Assert.Equal("ChartVisualizationSettingsType", settings.SchemaTypeName);
        Assert.True(settings.ShowLegend);
        Assert.Null(settings.StartColorIndex);
        Assert.False(settings.SyncAxis);
        Assert.Equal("CHART", settings.VisualizationType);
        Assert.False(settings.YAxisIsLogarithmic);
        Assert.Null(settings.YAxisMaxValue);
        Assert.Null(settings.YAxisMinValue);
        Assert.Equal(1, settings.ZoomLevel);
        Assert.Equal(1, settings.ZoomScaleHorizontal);
        Assert.Equal(1, settings.ZoomScaleVertical);
    }
    
    [Fact]
    public void ToJsonString_GeneratesCorrectJson_WhenSerialized()
    {
        // Arrange
        var expectedJson =
            """
            {
              "_type" : "ChartVisualizationSettingsType",
              "ShowTotalsInTooltip" : true,
              "TrendlineType" : "LinearFit",
              "AutomaticLabelRotation" : true,
              "SyncAxisVisibleRange" : false,
              "ZoomScaleHorizontal" : 1.0,
              "ZoomScaleVertical" : 1.0,
              "LeftAxisLogarithmic" : false,
              "ShowLegends" : true,
              "ChartType" : "Column",
              "VisualizationType" : "CHART",
              "AxisTitlesMode" : "None",
            }
            """;

        var settings = new TestCategoryChartVisualizationSettings
        {
            ShowTotalsInTooltip = true,
            Trendline = TrendlineType.LinearFit
        };

        // Act
        var actualJson = settings.ToJsonString();
        var expectedJObject = JObject.Parse(expectedJson);
        var actualJObject = JObject.Parse(actualJson);

        // Assert
        Assert.Equal(expectedJObject, actualJObject);
    }
    
    private class TestCategoryChartVisualizationSettings : CategoryChartVisualizationSettings
    {
        public TestCategoryChartVisualizationSettings() { }
    }
}