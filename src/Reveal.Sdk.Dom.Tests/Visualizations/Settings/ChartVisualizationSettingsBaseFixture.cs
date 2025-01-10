using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Settings;

public class ChartVisualizationSettingsBaseFixture
{
    [Fact]
    public void Constructor_FieldsHaveDefaultValue()
    {
        // Act
        var settings = new TestChartVisualizationSettings();

        // Assert
        Assert.Equal(SchemaTypeNames.ChartVisualizationSettingsType, settings.SchemaTypeName);
        Assert.Equal(VisualizationTypes.CHART, settings.VisualizationType);
        Assert.Equal(default(RdashChartType), settings.ChartType);
    }

    [Fact]
    public void ToJsonString_GeneratesCorrectJson_WhenSerialized()
    {
        // Arrange
        var expectedJson =
            """
            {
              "SchemaTypeName": "ChartVisualizationSettingsType",
              "VisualizationType": "CHART",
              "ChartType": "Bar"
            }
            """;
        var settings = new TestChartVisualizationSettings
        {
            ChartType = RdashChartType.Bar
        };

        // Act
        var actualJson = settings.ToJsonString();
        var expectedJObject = JObject.Parse(expectedJson);
        var actualJObject = JObject.Parse(actualJson);

        // Assert
        Assert.Equal(expectedJObject, actualJObject);
    }

    private class TestChartVisualizationSettings : ChartVisualizationSettingsBase
    {
        public TestChartVisualizationSettings() { }
    }
}