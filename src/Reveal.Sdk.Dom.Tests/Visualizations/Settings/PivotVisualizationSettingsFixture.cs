using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Settings;

public class PivotVisualizationSettingsFixture
{
    [Fact]
    public void Constructor_FieldsHaveDefaultValues_WhenInstanceIsCreated()
    {
        // Act
        var settings = new PivotVisualizationSettings();

        // Assert
        Assert.Equal(SchemaTypeNames.PivotVisualizationSettingsType, settings.SchemaTypeName);
        Assert.Equal(VisualizationTypes.PIVOT, settings.VisualizationType);
        Assert.Empty(settings.ColumnSettings);
        Assert.Null(settings._visualizationDataSpec);
    }

    [Fact]
    public void ToJsonString_GeneratesCorrectJson_WhenSerialized()
    {
        // Arrange
        var expectedJson =
            """
            {
              "_type" : "PivotVisualizationSettingsType",
              "FontSize" : "Large",
              "VisualizationColumns" : [],
              "Style" : {
                "FixedLeftColumns" : false,
                "TextAlignment" : "Center",
                "NumericAlignment" : "Right",
                "DateAlignment" : "Left"
              },
              "VisualizationType" : "PIVOT"
            }
            """;

        var settings = new PivotVisualizationSettings
        {
            _visualizationDataSpec = new PivotVisualizationDataSpec(),
            FontSize = FontSize.Large,
            Style = new GridVisualizationStyle
            {
                FixedLeftColumns = false,
                TextAlignment = Alignment.Center,
                NumericAlignment = Alignment.Right,
                DateAlignment = Alignment.Left
            },
            ShowGrandTotals = true,
        };

        // Act
        var actualJson = settings.ToJsonString();
        var expectedJObject = JObject.Parse(expectedJson);
        var actualJObject = JObject.Parse(actualJson);

        // Assert
        Assert.Equal(expectedJObject, actualJObject);
    }

    [Fact]
    public void ShowGrandTotals_SetAndGet_UpdatesVisualizationDataSpec()
    {
        // Arrange
        var settings = new PivotVisualizationSettings();
        settings._visualizationDataSpec = new PivotVisualizationDataSpec();

        // Act
        settings.ShowGrandTotals = true;

        // Assert
        Assert.True(settings.ShowGrandTotals);
        Assert.True(settings._visualizationDataSpec.ShowGrandTotals);
    }
}
