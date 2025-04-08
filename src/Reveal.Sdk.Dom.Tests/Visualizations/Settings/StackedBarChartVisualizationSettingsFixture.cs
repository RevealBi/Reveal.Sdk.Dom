using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Settings;

public class StackedBarChartVisualizationSettingsFixture
{
    [Fact]
    public void Constructor_FieldsHaveDefaultValues_WhenInstanceIsCreated()
    {
        // Act
        var settings = new StackedBarChartVisualizationSettings();

        // Assert
        Assert.Equal(RdashChartType.StackedBar, settings.ChartType);
    }

    [Fact]
    public void ToJsonString_GeneratesCorrectJson_WhenSerialized()
    {
        // Arrange
        var expectedJson =
            """
            {
              "_type" : "ChartVisualizationSettingsType",
              "IsPercentageDistributed" : true,
              "ShowTotalsInTooltip" : true,
              "AutomaticLabelRotation" : true,
              "SyncAxisVisibleRange" : false,
              "ZoomScaleHorizontal" : 1.0,
              "ZoomScaleVertical" : 1.0,
              "LeftAxisLogarithmic" : false,
              "ShowLegends" : true,
              "ChartType" : "StackedBar",
              "VisualizationType" : "CHART",
              "AxisTitlesMode" : "None",
            }
            """;

        var settings = new StackedBarChartVisualizationSettings
        {
            IsPercentageDistributed = true,
            ShowTotalsInTooltip = true
        };

        // Act
        var actualJson = settings.ToJsonString();
        var expectedJObject = JObject.Parse(expectedJson);
        var actualJObject = JObject.Parse(actualJson);

        // Assert
        Assert.Equal(expectedJObject, actualJObject);
    }
}