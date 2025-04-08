using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Settings;

public class ScatterVisualizationSettingsFixture
{
    [Fact]
    public void Constructor_FieldsHaveDefaultValues_WhenInstanceIsCreated()
    {
        // Act
        var settings = new ScatterVisualizationSettings();

        // Assert
        Assert.Equal(RdashChartType.Scatter, settings.ChartType);
        Assert.False(settings.XAxisIsLogarithmic);
        Assert.Null(settings.XAxisMinValue);
        Assert.Null(settings.XAxisMaxValue);
    }

    [Fact]
    public void ToJsonString_GeneratesCorrectJson_WhenSerialized()
    {
        // Arrange
        var expectedJson =
            """
            {
              "_type": "Testing Schema Type Name",
              "RightAxisLogarithmic": true,
              "RightAxisMinValue": 1.0,
              "RightAxisMaxValue": 200.0,
              "ShowAxisX": false,
              "ShowAxisY": true,
              "LeftAxisLogarithmic": true,
              "LeftAxisMinValue": 100.0,
              "LeftAxisMaxValue": 9000.0,
              "AxisTitlesMode" : "None",
              "ShowLegends": true,
              "BrushOffsetIndex": 1,
              "ChartType": "Scatter",
              "VisualizationType": "CHART",
            }
            """;

        var settings = new ScatterVisualizationSettings()
        {
            XAxisIsLogarithmic = true,
            XAxisMinValue = 1,
            XAxisMaxValue = 200,
            YAxisMinValue = 100,
            YAxisMaxValue = 9000,
            AxisDisplayMode = AxisDisplayMode.YAxis,
            ShowLegend = true,
            YAxisIsLogarithmic = true,
            StartColorIndex = 1,
            SchemaTypeName = "Testing Schema Type Name",
        };

        // Act
        var actualJson = settings.ToJsonString();
        var expectedJObject = JObject.Parse(expectedJson);
        var actualJObject = JObject.Parse(actualJson);

        // Assert
        Assert.Equal(expectedJObject, actualJObject);
    }
}
