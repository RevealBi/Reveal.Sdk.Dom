using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Settings;

public class FinancialVisualizationSettingsBaseFixture
{
    [Fact]
    public void Constructor_FieldsHaveDefaultValue_WhenInitializing()
    {
        // Act
        var settings = new TestFinancialVisualizationSettingsBase();

        // Assert
        Assert.False(settings.LeftAxisIsLogarithmic);
        Assert.Null(settings.LeftAxisMinValue);
        Assert.Null(settings.LeftAxisMaxValue);
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
              "_type" : "ChartVisualizationSettingsType",
              "LeftAxisLogarithmic" : true,
              "LeftAxisMinValue" : 10.5,
              "LeftAxisMaxValue" : 100.75,
              "AxisTitlesMode" : "None",
              "ChartType" : "Column",
              "VisualizationType" : "CHART"
            }
            """;

        var settings = new TestFinancialVisualizationSettingsBase
        {
            LeftAxisIsLogarithmic = true,
            LeftAxisMinValue = 10.5,
            LeftAxisMaxValue = 100.75
        };

        // Act
        var actualJson = settings.ToJsonString();
        var expectedJObject = JObject.Parse(expectedJson);
        var actualJObject = JObject.Parse(actualJson);

        // Assert
        Assert.Equal(expectedJObject, actualJObject);
    }

    private class TestFinancialVisualizationSettingsBase : FinancialVisualizationSettingsBase
    {
        public TestFinancialVisualizationSettingsBase() { }
    }
}