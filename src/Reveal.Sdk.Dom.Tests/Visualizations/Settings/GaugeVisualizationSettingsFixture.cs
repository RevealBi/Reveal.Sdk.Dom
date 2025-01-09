using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Settings;

public class GaugeVisualizationSettingsFixture
{
    [Fact]
    public void Constructor_FieldsHaveDefaultValues_WhenInstanceIsCreated()
    {
        // Act
        var settings = new GaugeVisualizationSettings();

        // Assert
        Assert.NotNull(settings);
        Assert.Null(settings.Minimum);
        Assert.Null(settings.Maximum);
        Assert.NotNull(settings.GaugeBands);
        Assert.Equal(3, settings.GaugeBands.Count);
        Assert.NotNull(settings.UpperBand);
        Assert.Equal(BandColor.Green, settings.UpperBand.Color);
        Assert.Equal(80.0, settings.UpperBand.Value);
        Assert.NotNull(settings.MiddleBand);
        Assert.Equal(BandColor.Yellow, settings.MiddleBand.Color);
        Assert.Equal(50.0, settings.MiddleBand.Value);
        Assert.NotNull(settings.LowerBand);
        Assert.Equal(BandColor.Red, settings.LowerBand.Color);
        Assert.Null(settings.LowerBand.Value);
    }

    [Theory]
    [InlineData(ValueComparisonType.Percentage)]
    [InlineData(ValueComparisonType.Number)]
    public void ValueComparisonType_SetAndGet_UpdatesGaugeBands(ValueComparisonType valueComparisonType)
    {
        // Arrange
        var settings = new GaugeVisualizationSettings();

        // Act
        settings.ValueComparisonType = valueComparisonType;

        // Assert
        Assert.Equal(valueComparisonType, settings.ValueComparisonType);

        Assert.All(settings.GaugeBands, band =>
        {
            Assert.NotNull(band);
            Assert.Equal(valueComparisonType, band.ValueComparisonType);
        });
        Assert.Equal(valueComparisonType, settings.UpperBand.ValueComparisonType);
        Assert.Equal(valueComparisonType, settings.MiddleBand.ValueComparisonType);
        Assert.Equal(valueComparisonType, settings.LowerBand.ValueComparisonType);
    }
    
    [Fact]
    public void ToJsonString_GeneratesCorrectJson_WhenSerialized()
    {
        // Arrange
        var expectedJson = 
            """
            {
              "_type" : "GaugeVisualizationSettingsType",
              "Minimum" : {
                "ValueType" : "NumberValue",
                "Value" : 0.0
              },
              "Maximum" : {
                "ValueType" : "NumberValue",
                "Value" : 100.0
              },
              "ViewType" : "Circular",
              "GaugeBands" : [ {
                "_type" : "GaugeBandType",
                "Type" : "Percentage",
                "Color" : "Green",
                "Value" : 90.0
              }, {
                "_type" : "GaugeBandType",
                "Type" : "Percentage",
                "Color" : "Yellow",
                "Value" : 60.0
              }, {
                "_type" : "GaugeBandType",
                "Type" : "Percentage",
                "Color" : "Red",
                "Value" : 30.0
              } ],
              "VisualizationType" : "GAUGE"
            }
            """;

        var settings = new GaugeVisualizationSettings
        {
            ViewType = GaugeViewType.Circular,
            Minimum = new Bound { Value = 0.0 },
            Maximum = new Bound { Value = 100.0 }
        };

        settings.GaugeBands[0].Value = 90.0;
        settings.GaugeBands[0].ValueComparisonType = ValueComparisonType.Percentage;

        settings.GaugeBands[1].Value = 60.0;
        settings.GaugeBands[1].ValueComparisonType = ValueComparisonType.Percentage;

        settings.GaugeBands[2].Value = 30.0;
        settings.GaugeBands[2].ValueComparisonType = ValueComparisonType.Percentage;

        // Act
        var actualJson = settings.ToJsonString();
        var expectedJObject = JObject.Parse(expectedJson);
        var actualJObject = JObject.Parse(actualJson);

        // Assert
        Assert.Equal(expectedJObject, actualJObject);
    }
}