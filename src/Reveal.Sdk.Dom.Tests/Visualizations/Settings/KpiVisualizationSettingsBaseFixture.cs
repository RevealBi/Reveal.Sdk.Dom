using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Settings;

public class KpiVisualizationSettingsBaseFixture
{
    [Fact]
    public void Constructor_FieldsHaveDefaultValues_WhenInitializing()
    {
        // Act
        var settings = new TestKpiVisualizationSettingsBase();

        // Assert
        Assert.Equal(IndicatorDifferenceMode.Percentage, settings.DifferenceMode);
        Assert.False(settings.PositiveIsRed);
        Assert.True(settings.IncludeToday);
        Assert.Null(settings.SchemaTypeName);
        Assert.Null(settings.VisualizationType);
    }

    [Fact]
    public void ToJsonString_GeneratesCorrectJson_WhenSerialized()
    {
        // Arrange
        var expectedJson =
            """
            {
              "_type" : null,
              "DifferenceMode" : "Value",
              "PositiveIsRed" : true,
              "IncludeToday" : false,
              "VisualizationType" : null
            }
            """;

        var settings = new TestKpiVisualizationSettingsBase
        {
            DifferenceMode = IndicatorDifferenceMode.Value,
            PositiveIsRed = true,
            IncludeToday = false
        };

        // Act
        var actualJson = JsonConvert.SerializeObject(settings, Formatting.Indented, new JsonSerializerSettings
        {
            Converters = { new StringEnumConverter() }
        });

        var expectedJObject = JObject.Parse(expectedJson);
        var actualJObject = JObject.Parse(actualJson);

        // Assert
        Assert.Equal(expectedJObject, actualJObject);
    }

    private class TestKpiVisualizationSettingsBase : KpiVisualizationSettingsBase
    {
    }
}