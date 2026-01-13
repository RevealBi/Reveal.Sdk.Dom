using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Extensions.Visualizations
{
    public class AreaChartVisualizationExtensionsFixture
    {
        [Fact]
        public void ConfigureSettings_Works_NoConditions()
        {
            // Arrange 
            var vs = new AreaChartVisualization();
            var expectedSettings = new AreaChartVisualizationSettings()
            {
                AutomaticLabelRotation = false,
                ChartType = RdashChartType.Bar,
                SchemaTypeName = "Updated Type Name",
                ShowLegend = false,
                ShowTotalsInTooltip = true,
                StartColorIndex = 2,
                SyncAxis = false,
                Trendline = TrendlineType.QuarticFit,
                VisualizationType = "Updated VS Type",
                YAxisIsLogarithmic = true,
                YAxisMaxValue = 500,
                YAxisMinValue = 200,
                ZoomLevel = 2,
                ZoomScaleHorizontal = 3,
                ZoomScaleVertical = 2.5,
            };

            var action = (AreaChartVisualizationSettings s) =>
            {
                s.AutomaticLabelRotation = expectedSettings.AutomaticLabelRotation;
                s.ChartType = expectedSettings.ChartType;
                s.SchemaTypeName = expectedSettings.SchemaTypeName;
                s.ShowLegend = expectedSettings.ShowLegend;
                s.ShowTotalsInTooltip = expectedSettings.ShowTotalsInTooltip;
                s.StartColorIndex = expectedSettings.StartColorIndex;
                s.SyncAxis = expectedSettings.SyncAxis;
                s.Trendline = expectedSettings.Trendline;
                s.VisualizationType = expectedSettings.VisualizationType;
                s.YAxisIsLogarithmic = expectedSettings.YAxisIsLogarithmic;
                s.YAxisMaxValue = expectedSettings.YAxisMaxValue;
                s.YAxisMinValue = expectedSettings.YAxisMinValue;
                s.ZoomLevel = expectedSettings.ZoomLevel;
                s.ZoomScaleHorizontal = expectedSettings.ZoomScaleHorizontal;
                s.ZoomScaleVertical = expectedSettings.ZoomScaleVertical;
            };

            // Act 
            vs.ConfigureSettings(action);

            // Assert 
            Assert.Equivalent(expectedSettings, vs.Settings);
        }
    }
}