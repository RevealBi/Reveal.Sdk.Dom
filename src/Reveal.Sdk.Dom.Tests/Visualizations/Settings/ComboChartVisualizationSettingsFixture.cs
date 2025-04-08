using System;
using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Settings;

public class ComboChartVisualizationSettingsFixture
{
    [Fact]
    public void Constructor_FieldsHaveDefaultValues_WhenInstanceIsCreated()
    {
        // Act
        var settings = new ComboChartVisualizationSettings();

        // Assert
        Assert.Equal(AxisDisplayMode.Both, settings.AxisDisplayMode);
        Assert.Equal(ComboChartType.Column, settings.Chart1Type);
        Assert.False(settings.Chart2OnTop);
        Assert.Equal(ComboChartType.Line, settings.Chart2Type);
        Assert.Equal(RdashChartType.Composite, settings.ChartType);
        Assert.False(settings.RightAxisIsLogarithmic);
        Assert.Null(settings.RightAxisMaxValue);
        Assert.Null(settings.RightAxisMinValue);
        Assert.True(settings.ShowAxisX);
        Assert.True(settings.ShowAxisY);
        Assert.True(settings.ShowRightAxis);
    }

    [Theory]
    [InlineData(true, true, AxisDisplayMode.Both)]
    [InlineData(false, false, AxisDisplayMode.None)]
    [InlineData(true, false, AxisDisplayMode.XAxis)]
    [InlineData(false, true, AxisDisplayMode.YAxis)]
    public void AxisDisplayMode_Get_WhenAxisVisibilityChanges(bool showAxisX, bool showAxisY, AxisDisplayMode expectedMode)
    {
        // Arrange
        var settings = new ComboChartVisualizationSettings
        {
            ShowAxisX = showAxisX,
            ShowAxisY = showAxisY
        };

        // Act
        var actualMode = settings.AxisDisplayMode;

        // Assert
        Assert.Equal(expectedMode, actualMode);
    }

    [Theory]
    [InlineData(AxisDisplayMode.Both, true, true)]
    [InlineData(AxisDisplayMode.None, false, false)]
    [InlineData(AxisDisplayMode.XAxis, true, false)]
    [InlineData(AxisDisplayMode.YAxis, false, true)]
    public void AxisDisplayMode_Set_WhenUpdateAxisVisibility(AxisDisplayMode mode, bool expectedShowAxisX, bool expectedShowAxisY)
    {
        // Arrange
        var settings = new ComboChartVisualizationSettings();

        // Act
        settings.AxisDisplayMode = mode;

        // Assert
        Assert.Equal(expectedShowAxisX, settings.ShowAxisX);
        Assert.Equal(expectedShowAxisY, settings.ShowAxisY);
        Assert.Equal(mode, settings.AxisDisplayMode);
    }
    
    [Fact]
    public void AxisDisplayMode_Set_ThrowsArgumentOutOfRangeException_WhenInvalidValueAssigned()
    {
        // Arrange
        var settings = new ComboChartVisualizationSettings();

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            settings.AxisDisplayMode = (AxisDisplayMode)999;
        });
    }
    
    [Theory]
    [InlineData(true, false)]
    [InlineData(false, true)]
    public void ShowRightAxis_Set_WhenUpdatesSingleAxisMode(bool showRightAxis, bool expectedSingleAxisMode)
    {
        // Arrange
        var settings = new ComboChartVisualizationSettings();

        // Act
        settings.ShowRightAxis = showRightAxis;

        // Assert
        Assert.Equal(showRightAxis, settings.ShowRightAxis);
        Assert.Equal(expectedSingleAxisMode, settings.SingleAxisMode);
    }

    [Fact]
    public void ToJsonString_GeneratesCorrectJson_WhenSerialized()
    {
        // Arrange
        var expectedJson =
            """
            {
              "_type" : "ChartVisualizationSettingsType",
              "CompositeChartTypesSwapped" : false,
              "CompositeChartType1" : "Column",
              "CompositeChartType2" : "Line",
              "RightAxisLogarithmic" : false,
              "SingleAxisMode" : false,
              "ShowAxisX" : true,
              "ShowAxisY" : true,
              "LeftAxisLogarithmic" : false,
              "ShowLegends" : true,
              "ChartType" : "Composite",
              "VisualizationType" : "CHART",
              "AxisTitlesMode" : "None",
            }
            """;

        var settings = new ComboChartVisualizationSettings
        {
            RightAxisIsLogarithmic = false,
            Chart1Type = ComboChartType.Column,
            Chart2Type = ComboChartType.Line,
            Chart2OnTop = false,
            ShowAxisX = true,
            ShowAxisY = true,
            SingleAxisMode = false,
            RightAxisMinValue = null,
            RightAxisMaxValue = null,
            ShowLegend = true,
            ShowRightAxis = true,
        };

        // Act
        var actualJson = settings.ToJsonString();
        var expectedJObject = JObject.Parse(expectedJson);
        var actualJObject = JObject.Parse(actualJson);

        // Assert
        Assert.Equal(expectedJObject, actualJObject);
    }

}