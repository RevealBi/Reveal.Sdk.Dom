using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Settings;

public class GridVisualizationSettingsBaseFixture
{
    [Fact]
    public void Constructor_FieldsHaveDefaultValues_WhenInitializing()
    {
        // Act
        var settings = new TestGridVisualizationSettings();

        // Assert
        Assert.Equal(FontSize.Small, settings.FontSize);
        Assert.NotNull(settings.Style);
        Assert.Equal(default(Alignment), settings.DateFieldAlignment);
        Assert.Equal(default(Alignment), settings.NumericFieldAlignment);
        Assert.Equal(default(Alignment), settings.TextFieldAlignment);
        Assert.Null(settings.SchemaTypeName);
        Assert.Null(settings.VisualizationType);
    }
    
    [Fact]
    public void DateFieldAlignment_GetAndSet_UpdatesStyleDateAlignment()
    {
        // Arrange
        var settings = new TestGridVisualizationSettings();
        var expectedAlignment = Alignment.Center;

        // Act
        settings.DateFieldAlignment = expectedAlignment;

        // Assert
        Assert.Equal(expectedAlignment, settings.DateFieldAlignment);
        Assert.Equal(expectedAlignment, settings.Style.DateAlignment);
    }

    [Fact]
    public void NumericFieldAlignment_GetAndSet_UpdatesStyleNumericAlignment()
    {
        // Arrange
        var settings = new TestGridVisualizationSettings();
        var expectedAlignment = Alignment.Right;

        // Act
        settings.NumericFieldAlignment = expectedAlignment;

        // Assert
        Assert.Equal(expectedAlignment, settings.NumericFieldAlignment);
        Assert.Equal(expectedAlignment, settings.Style.NumericAlignment);
    }

    [Fact]
    public void TextFieldAlignment_GetAndSet_UpdatesStyleTextAlignment()
    {
        // Arrange
        var settings = new TestGridVisualizationSettings();
        var expectedAlignment = Alignment.Left;

        // Act
        settings.TextFieldAlignment = expectedAlignment;

        // Assert
        Assert.Equal(expectedAlignment, settings.TextFieldAlignment);
        Assert.Equal(expectedAlignment, settings.Style.TextAlignment);
    }

    [Fact]
    public void ToJsonString_GeneratesCorrectJson_WhenSerialized()
    {
        // Arrange
        var expectedJson =
            """
            {
              "_type" : null,
              "FontSize" : "Large",
              "VisualizationColumns": null,
              "Style" : {
                "FixedLeftColumns" : false,
                "TextAlignment" : "Left",
                "NumericAlignment" : "Right",
                "DateAlignment" : "Center"
              },
              "VisualizationType" : null
            }
            """;

        var settings = new TestGridVisualizationSettings
        {
            FontSize = FontSize.Large,
            DateFieldAlignment = Alignment.Center,
            NumericFieldAlignment = Alignment.Right,
            TextFieldAlignment = Alignment.Left
        };

        // Act
        var actualJson = JsonConvert.SerializeObject(settings);
        var expectedJObject = JObject.Parse(expectedJson);
        var actualJObject = JObject.Parse(actualJson);

        // Assert
        Assert.Equal(expectedJObject, actualJObject);
    }
    
    private class TestGridVisualizationSettings : GridVisualizationSettingsBase
    {
        public TestGridVisualizationSettings() : base() { }
    }
}