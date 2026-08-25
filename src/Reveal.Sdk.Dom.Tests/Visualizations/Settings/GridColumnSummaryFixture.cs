using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Settings;

public class GridColumnSummaryFixture
{
    [Fact]
    public void Constructor_UsesSumByDefault()
    {
        // Act
        var summary = new GridColumnSummary("Revenue");

        // Assert
        Assert.Equal("Revenue", summary.ColumnName);
        Assert.Equal(GridSummaryType.Sum, summary.SummaryType);
    }

    [Theory]
    [InlineData(GridSummaryType.Minimum, "Min")]
    [InlineData(GridSummaryType.Maximum, "Max")]
    [InlineData(GridSummaryType.Sum, "Sum")]
    [InlineData(GridSummaryType.Average, "Average")]
    [InlineData(GridSummaryType.Count, "Count")]
    [InlineData(GridSummaryType.Custom, "Custom")]
    public void SummaryType_UsesRevealOperandValue_WhenSerialized(
        GridSummaryType summaryType,
        string expectedOperand)
    {
        // Arrange
        var summary = new GridColumnSummary("Value", summaryType);

        // Act
        var json = JObject.Parse(summary.ToJsonString());

        // Assert
        Assert.Equal(expectedOperand, json["Operand"]?.Value<string>());
    }
}
