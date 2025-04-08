using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Settings;

public class BubbleVisualizationSettingsFixture
{
    [Fact]
    public void Constructor_FieldsHaveDefaultValues_WhenInstanceIsCreated()
    {
        // Act
        var settings = new BubbleVisualizationSettings();

        // Assert
        Assert.Equal(RdashChartType.Bubble, settings.ChartType);
    }
    
    [Fact]
    public void ToJsonString_GeneratesCorrectJson_WhenSerialized()
    {
        // Arrange
        var expectedJson =
            """
            {
              "_type": "Test Bubble VS",
              "RightAxisLogarithmic": true,
              "RightAxisMinValue": 15.1,
              "RightAxisMaxValue": 250.5,
              "ShowAxisX": true,
              "ShowAxisY": false,
              "LeftAxisLogarithmic": true,
              "LeftAxisMinValue": 16.2,
              "LeftAxisMaxValue": 150.6,
              "AxisTitlesMode" : "None",
              "ShowLegends": true,
              "BrushOffsetIndex": 3,
              "ChartType": "Bubble",
              "VisualizationType": "CHART"
            }
            """;

        var settings = new BubbleVisualizationSettings() { 
            AxisDisplayMode = AxisDisplayMode.XAxis,
            ChartType = RdashChartType.Bubble,
            SchemaTypeName = "Test Bubble VS",
            ShowLegend = true,
            StartColorIndex = 3,
            XAxisIsLogarithmic = true,
            XAxisMinValue = 15.1,
            XAxisMaxValue = 250.5,
            YAxisMinValue = 16.2,
            YAxisMaxValue = 150.6,
            YAxisIsLogarithmic = true,
        };

        // Act
        var actualJson = settings.ToJsonString();
        var expectedJObject = JObject.Parse(expectedJson);
        var actualJObject = JObject.Parse(actualJson);

        // Assert
        Assert.Equal(expectedJObject, actualJObject);
    }

}