using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Settings;

public class ScatterMapVisualizationSettingsFixture
{
    [Fact]
    public void Constructor_FieldsHaveDefaultValues_WhenInstanceIsCreated()
    {
        // Act
        var settings = new ScatterMapVisualizationSettings();

        // Assert
        Assert.Equal(SchemaTypeNames.ScatterMapVisualizationSettingsType, settings.SchemaTypeName);
        Assert.Equal(VisualizationTypes.SCATTER_MAP, settings.VisualizationType);
        Assert.NotNull(settings.ConditionalFormatting);
        Assert.Equal(MapColorMode.RangeOfValues, settings.ColorMode);
        Assert.Equal(3, settings.ImageTileZoomLevel);
        Assert.True(settings.ShowImageTiles);
        Assert.False(settings.UseDifferentMarkers);
        Assert.NotNull(settings.Zoom);
    }

    [Fact]
    public void ToJsonString_GeneratesCorrectJson_WhenSerialized()
    {
        // Arrange
        var expectedJson =
            """
            {
              "_type" : "ScatterMapVisualizationSettingsType",
              "ConditionalFormatting" : {
                "Bands" : [ {
                  "_type" : "ConditionalFormattingBandType",
                  "Type" : "Percentage",
                  "Color" : "Green",
                  "Value" : 80.0
                }, {
                  "_type" : "ConditionalFormattingBandType",
                  "Type" : "Percentage",
                  "Color" : "Yellow",
                  "Value" : 50.0
                }, {
                  "_type" : "ConditionalFormattingBandType",
                  "Type" : "Percentage",
                  "Color" : "Red"
                } ]
              },
              "ColorizationMode" : "Range",
              "ZoomThreshold" : 3,
              "ShowTileSource" : true,
              "UseDifferentMarkers" : false,
              "ShowLegends" : true,
              "ColorIndex" : -1,
              "VisualizationType" : "SCATTER_MAP"
            }
            """;

        var settings = new ScatterMapVisualizationSettings
        {
            ColorMode = MapColorMode.RangeOfValues,
            ImageTileZoomLevel = 3,
            ShowImageTiles = true
        };

        // Act
        var actualJson = settings.ToJsonString();
        var expectedJObject = JObject.Parse(expectedJson);
        var actualJObject = JObject.Parse(actualJson);

        // Assert
        Assert.Equal(expectedJObject, actualJObject);
    }

    [Fact]
    public void ToJsonString_OmitsZoomRectangle_WhenZoomIsNotSet()
    {
        // Arrange
        var document = new RdashDocument("Maps");
        document.Visualizations.Add(CreateScatterMap());

        // Act
        var settings = JObject.Parse(document.ToJsonString())["Widgets"][0]["VisualizationSettings"];

        // Assert
        Assert.Null(settings["ZoomRectangle"]);
    }

    [Fact]
    public void ToJsonString_WritesZoomRectangle_WhenZoomIsSet()
    {
        // Arrange
        var document = new RdashDocument("Maps");
        document.Visualizations.Add(CreateScatterMap().ConfigureSettings(settings =>
        {
            settings.Zoom.Longitude = -180;
            settings.Zoom.Latitude = -60;
            settings.Zoom.DegreesLongitude = 360;
            settings.Zoom.DegreesLatitude = 150;
        }));
        var expected = JObject.Parse("""{ "X" : -180.0, "Y" : -60.0, "Width" : 360.0, "Height" : 150.0 }""");

        // Act
        var json = document.ToJsonString();
        var roundTripJson = RdashDocument.LoadFromJson(json).ToJsonString();

        // Assert
        Assert.Equal(expected, JObject.Parse(json)["Widgets"][0]["VisualizationSettings"]["ZoomRectangle"]);
        Assert.Equal(expected, JObject.Parse(roundTripJson)["Widgets"][0]["VisualizationSettings"]["ZoomRectangle"]);
    }

    [Fact]
    public void ToJsonString_OmitsZoomRectangle_WhenLoadedZoomIsEmpty()
    {
        // Arrange
        var document = new RdashDocument("Maps");
        document.Visualizations.Add(CreateScatterMap());
        var json = JObject.Parse(document.ToJsonString());
        json["Widgets"][0]["VisualizationSettings"]["ZoomRectangle"] = JObject.Parse("""{ "X" : 0.0, "Y" : 0.0, "Width" : 0.0, "Height" : 0.0 }""");

        // Act
        var roundTripJson = RdashDocument.LoadFromJson(json.ToString()).ToJsonString();

        // Assert
        Assert.Null(JObject.Parse(roundTripJson)["Widgets"][0]["VisualizationSettings"]["ZoomRectangle"]);
    }

    [Fact]
    public void ToJsonString_OmitsZoomRectangle_WhenLoadedZoomIsNull()
    {
        // Arrange
        var document = new RdashDocument("Maps");
        document.Visualizations.Add(CreateScatterMap());
        var json = JObject.Parse(document.ToJsonString());
        json["Widgets"][0]["VisualizationSettings"]["ZoomRectangle"] = JValue.CreateNull();

        // Act
        var roundTripJson = RdashDocument.LoadFromJson(json.ToString()).ToJsonString();

        // Assert
        Assert.Null(JObject.Parse(roundTripJson)["Widgets"][0]["VisualizationSettings"]["ZoomRectangle"]);
    }

    private static ScatterMapVisualization CreateScatterMap()
    {
        var dataSourceItem = new DataSourceItem("Cities", new DataSource())
        {
            Fields = new List<IField> { new TextField("City"), new NumberField("Lat"), new NumberField("Lon") }
        };
        return new ScatterMapVisualization("Cities", dataSourceItem).SetLatitude("Lat").SetLongitude("Lon").SetLabel("City");
    }
}