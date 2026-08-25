using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Settings;

public class GridColumnSettingsFixture
{
    [Fact]
    public void Constructor_UsesExpectedDefaults_WhenColumnNameIsProvided()
    {
        // Act
        var settings = new GridColumnSettings("Amount");

        // Assert
        Assert.Equal("Amount", settings.ColumnName);
        Assert.Null(settings.Width);
        Assert.Equal(Alignment.Inherit, settings.TextAlignment);
        Assert.Equal(GridColumnPinPosition.Inherit, settings.PinPosition);
        Assert.Null(settings.Hyperlink);
    }
}
