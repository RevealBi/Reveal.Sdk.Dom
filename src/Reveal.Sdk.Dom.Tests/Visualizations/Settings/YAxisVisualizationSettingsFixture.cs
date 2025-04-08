using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Settings;

public class YAxisVisualizationSettingsFixture
{
    [Fact]
    public void Constructor_FieldsHaveDefaultValues_WhenInstanceIsCreated()
    {
        // Act
        var settings = new TestYAxisVisualizationSettings();

        // Assert
        Assert.False(settings.YAxisIsLogarithmic);
        Assert.Null(settings.YAxisMinValue);
        Assert.Null(settings.YAxisMaxValue);
        Assert.True(settings.ShowLegend);
        Assert.Equal(default(int?), settings.StartColorIndex);
        Assert.Equal("CHART", settings.VisualizationType);
        Assert.Equal("ChartVisualizationSettingsType", settings.SchemaTypeName);
    }

    [Fact]
    public void ToJsonString_GeneratesCorrectJson_WhenSerialized()
    {
        // Arrange
        var expectedJson =
            """
            {
              "_type" : "ChartVisualizationSettingsType",
              "LeftAxisLogarithmic" : true,
              "LeftAxisMinValue" : 10.0,
              "LeftAxisMaxValue" : 100.0,
              "AxisTitlesMode" : "None",
              "ShowLegends" : true,
              "ChartType" : "Column",
              "VisualizationType" : "CHART"
            }
            """;

        var settings = new TestYAxisVisualizationSettings
        {
            YAxisIsLogarithmic = true,
            YAxisMinValue = 10.0,
            YAxisMaxValue = 100.0,
        };

        // Act
        var actualJson = settings.ToJsonString();
        var expectedJObject = JObject.Parse(expectedJson);
        var actualJObject = JObject.Parse(actualJson);

        // Assert
        Assert.Equal(expectedJObject, actualJObject);
    }

    private class TestYAxisVisualizationSettings : YAxisVisualizationSettings
    {
        public TestYAxisVisualizationSettings() { }
    }
}