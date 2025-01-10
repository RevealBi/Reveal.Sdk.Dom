using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Settings;

public class RoundChartVisualizationSettingsBaseFixture
{
    [Fact]
    public void Constructor_FieldsHaveDefaultValues_WhenInstanceIsCreated()
    {
        // Act
        var settings = new TestRoundChartVisualizationSettingsBase();

        // Assert
        Assert.True(settings.ShowLegend);
        Assert.Equal(default(LabelDisplayMode), settings.SliceLabelDisplay);
        Assert.Null(settings.StartColorIndex);
        Assert.Equal(3.0, settings.OthersSliceThreshold);
        Assert.Equal("CHART", settings.VisualizationType);
        Assert.Equal("ChartVisualizationSettingsType", settings.SchemaTypeName);
        Assert.Equal(default(RdashChartType), settings.ChartType);
        Assert.Null(settings.StartPosition);
        Assert.False(settings.ShowZeroValuesInLegend);
    }

    [Fact]
    public void ToJsonString_GeneratesCorrectJson_WhenSerialized()
    {
        // Arrange
        var expectedJson =
            """
            {
              "_type" : "ChartVisualizationSettingsType",
              "PieStartPosition" : 90,
              "ShowZeroValuesInLegend" : true,
              "ShowLegends" : true,
              "LabelDisplayMode" : "ValueAndPercentage",
              "BrushOffsetIndex" : 2,
              "OthersSliceThreshold" : 3.0,
              "ChartType" : "Column",
              "VisualizationType" : "CHART"
            }
            """;

        var settings = new TestRoundChartVisualizationSettingsBase
        {
            ShowLegend = true,
            SliceLabelDisplay = LabelDisplayMode.ValueAndPercentage,
            StartColorIndex = 2,
            OthersSliceThreshold = 3.0,
            StartPosition = 90,
            ShowZeroValuesInLegend = true
        };

        // Act
        var actualJson = settings.ToJsonString();
        var expectedJObject = JObject.Parse(expectedJson);
        var actualJObject = JObject.Parse(actualJson);

        // Assert
        Assert.Equal(expectedJObject, actualJObject);
    }
    
    private class TestRoundChartVisualizationSettingsBase : RoundChartVisualizationSettingsBase
    {
    }
}