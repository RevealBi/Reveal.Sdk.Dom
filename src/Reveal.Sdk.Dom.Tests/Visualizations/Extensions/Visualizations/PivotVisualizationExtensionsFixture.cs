using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Extensions.Visualizations
{
    public class PivotVisualizationExtensionsFixture
    {
        [Fact]
        public void ConfigureSettings_Works_NoConditions()
        {
            // Arrange
            var vs = new PivotVisualization();
            var expectedSettings = new PivotVisualizationSettings()
            {
                DateFieldAlignment = Alignment.Right,
                FontSize = FontSize.Large,
                NumericFieldAlignment = Alignment.Right,
                SchemaTypeName = "Test schema type name",
                Style = new GridVisualizationStyle()
                {
                    DateAlignment = Alignment.Right,
                    FixedLeftColumns = false,
                    NumericAlignment = Alignment.Center,
                    TextAlignment = Alignment.Right
                },
                TextFieldAlignment = Alignment.Center,
                VisualizationType = "Test VS Type",
                _visualizationDataSpec = new PivotVisualizationDataSpec()
            };
            expectedSettings.ShowGrandTotals = true;

            var action = (PivotVisualizationSettings s) =>
            {
                s.DateFieldAlignment = expectedSettings.DateFieldAlignment;
                s.FontSize = expectedSettings.FontSize;
                s.NumericFieldAlignment = expectedSettings.NumericFieldAlignment;
                s.SchemaTypeName = expectedSettings.SchemaTypeName;
                s.ShowGrandTotals = expectedSettings.ShowGrandTotals;
                s.Style = expectedSettings.Style;
                s.TextFieldAlignment = expectedSettings.TextFieldAlignment;
                s.VisualizationType = expectedSettings.VisualizationType;
            };

            // Act
            vs.ConfigureSettings(action);

            // Assert
            Assert.Equivalent(expectedSettings, vs.Settings);
        }

    }
}
