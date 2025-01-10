using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Settings;

public class VisualizationSettingsFixture
{
    [Fact]
    public void Constructor_FieldsHaveDefaultValue()
    {
        // Act
        var settings = new TestVisualizationSettings();

        // Assert
        Assert.Null(settings.VisualizationType);
        Assert.Null(settings.SchemaTypeName);
    }
    
    [Fact]
    public void ToJsonString_GeneratesCorrectJson_WhenSerialized()
    {
        // Arrange
        var expectedJson = 
            """
            {
              "VisualizationType" : "BubbleVisualizationDataSpecType"
            }
            """;
        var settings = new TestVisualizationSettings()
        {
            VisualizationType = "BubbleVisualizationDataSpecType",
        };

        // Act
        var actualJson = settings.ToJsonString();
        var expectedJObject = JObject.Parse(expectedJson);
        var actualJObject = JObject.Parse(actualJson);

        // Assert
        Assert.Equal(expectedJObject, actualJObject);
    }
    
    private class TestVisualizationSettings : VisualizationSettings
    {
        public TestVisualizationSettings() { }
    }
}