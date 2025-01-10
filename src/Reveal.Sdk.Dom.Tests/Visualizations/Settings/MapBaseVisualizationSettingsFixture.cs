using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Settings;

public class MapBaseVisualizationSettingsFixture
{
    [Fact]
    public void Constructor_FieldsHaveDefaultValue_WhenInitializing()
    {
        // Act
        var settings = new MapBaseVisualizationSettings();

        // Assert
        Assert.Equal(DashboardMapVisualizationType.Standard, settings.MapType);
        Assert.Null(settings.LatitudeColumnName);
        Assert.Null(settings.LongitudeColumnName);
        Assert.Null(settings.LatitudeLongitudeColumnName);
        Assert.Null(settings.DisplayColumnName);
        Assert.Null(settings.DisplayValueColumnName);
        Assert.Null(settings.DisplayValueColor);
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
              "MapType" : 1,
              "LatitudeColumnName" : "Latitude",
              "LongitudeColumnName" : "Longitude",
              "LatitudeLongitudeColumnName" : "LatLong",
              "DisplayColumnName" : "Country",
              "DisplayValueColumnName" : "Population",
              "DisplayValueColor" : "Red",
              "VisualizationType" : null
            }
            """;

        var settings = new MapBaseVisualizationSettings
        {
            MapType = DashboardMapVisualizationType.Satellite,
            LatitudeColumnName = "Latitude",
            LongitudeColumnName = "Longitude",
            LatitudeLongitudeColumnName = "LatLong",
            DisplayColumnName = "Country",
            DisplayValueColumnName = "Population",
            DisplayValueColor = "Red"
        };

        // Act
        var actualJson = JsonConvert.SerializeObject(settings);
        var expectedJObject = JObject.Parse(expectedJson);
        var actualJObject = JObject.Parse(actualJson);

        // Assert
        Assert.Equal(expectedJObject, actualJObject);
    }
}