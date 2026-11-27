using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations;
using System;
using System.Collections.Generic;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations;

public class ChartGridLinesModeFixture
{
    // The charts Reveal's editor offers grid lines for: every chart except Pie, Doughnut, Funnel and Radial.
    public static IEnumerable<object[]> Charts => new List<object[]>
    {
        new object[] { typeof(AreaChartVisualization) },
        new object[] { typeof(BarChartVisualization) },
        new object[] { typeof(BubbleVisualization) },
        new object[] { typeof(CandleStickVisualization) },
        new object[] { typeof(ColumnChartVisualization) },
        new object[] { typeof(ComboChartVisualization) },
        new object[] { typeof(LineChartVisualization) },
        new object[] { typeof(OHLCVisualization) },
        new object[] { typeof(ScatterVisualization) },
        new object[] { typeof(SplineChartVisualization) },
        new object[] { typeof(SplineAreaChartVisualization) },
        new object[] { typeof(StackedAreaChartVisualization) },
        new object[] { typeof(StackedBarChartVisualization) },
        new object[] { typeof(StackedColumnChartVisualization) },
        new object[] { typeof(StepAreaChartVisualization) },
        new object[] { typeof(StepLineChartVisualization) },
        new object[] { typeof(TimeSeriesVisualization) },
    };

    [Theory]
    [MemberData(nameof(Charts))]
    public void LoadFromJson_KeepsGridLinesMode_WhenChartHasIt(Type visualizationType)
    {
        // Arrange: Reveal defaults GridLinesMode to None, so dropping it on load and save removed the chart's grid lines (#456)
        var document = new RdashDocument();
        document.Visualizations.Add((Visualization)Activator.CreateInstance(visualizationType, DataSource()));
        var json = JObject.Parse(document.ToJsonString());
        json["Widgets"][0]["VisualizationSettings"]["GridLinesMode"] = "Horizontal";

        // Act
        var saved = JObject.Parse(RdashDocument.LoadFromJson(json.ToString()).ToJsonString());

        // Assert
        Assert.Equal("Horizontal", (string)saved["Widgets"][0]["VisualizationSettings"]["GridLinesMode"]);
    }

    [Fact]
    public void ToJsonString_WritesGridLinesMode_WhenSetOnCategoryChart()
    {
        // Arrange
        var visualization = new ColumnChartVisualization(DataSource());
        visualization.Settings.GridLinesMode = GridLinesMode.Both;
        var document = new RdashDocument();
        document.Visualizations.Add(visualization);

        // Act
        var json = JObject.Parse(document.ToJsonString());

        // Assert
        Assert.Equal("Both", (string)json["Widgets"][0]["VisualizationSettings"]["GridLinesMode"]);
    }

    [Fact]
    public void ToJsonString_WritesGridLinesMode_WhenSetOnFinancialChart()
    {
        // Arrange
        var visualization = new CandleStickVisualization(DataSource());
        visualization.Settings.GridLinesMode = GridLinesMode.Vertical;
        var document = new RdashDocument();
        document.Visualizations.Add(visualization);

        // Act
        var json = JObject.Parse(document.ToJsonString());

        // Assert
        Assert.Equal("Vertical", (string)json["Widgets"][0]["VisualizationSettings"]["GridLinesMode"]);
    }

    private static DataSourceItem DataSource()
    {
        return new DataSourceItemFactory().Create(DataSourceType.REST, "", "")
            .SetFields(new List<IField> { new TextField("Category") });
    }
}
