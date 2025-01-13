using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Settings;

public class CustomVisualizationSettingsFixture
{
    [Fact]
    public void Constructor_FieldsHaveDefaultValues_WhenInstanceIsCreated()
    {
        // Act
        var settings = new CustomVisualizationSettings();

        // Assert
        Assert.Equal(SchemaTypeNames.DiyVisualizationSettingsType, settings.SchemaTypeName);
        Assert.Equal(VisualizationTypes.JS_EXTENSION, settings.VisualizationType);
        Assert.Null(settings.Title);
        Assert.Null(settings.Url);
    }

    [Fact]
    public void Title_SetAndGet_UpdatesCorrectly()
    {
        // Arrange
        var settings = new CustomVisualizationSettings();
        var expectedTitle = "Custom Title";

        // Act
        settings.Title = expectedTitle;

        // Assert
        Assert.Equal(expectedTitle, settings.Title);
    }

    [Fact]
    public void ToJsonString_GeneratesCorrectJson_WhenSerialized()
    {
        // Arrange
        var expectedJson =
            """
            {
              "_type": "DiyVisualizationSettingsType",
              "VisualizationType": "JS_EXTENSION",
              "Title": "Custom Visualization",
              "Url": "http://dl.infragistics.com/reportplus/reveal/samples/Samples.xlsx"
            }
            """;

        var settings = new CustomVisualizationSettings
        {
            Title = "Custom Visualization",
            Url = "http://dl.infragistics.com/reportplus/reveal/samples/Samples.xlsx"
        };

        // Act
        var actualJson = settings.ToJsonString();
        var expectedJObject = JObject.Parse(expectedJson);
        var actualJObject = JObject.Parse(actualJson);

        // Assert
        Assert.Equal(expectedJObject, actualJObject);
    }
}