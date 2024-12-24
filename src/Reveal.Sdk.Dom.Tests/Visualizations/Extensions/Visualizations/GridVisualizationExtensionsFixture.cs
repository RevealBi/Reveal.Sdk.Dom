using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.Settings;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Extensions.Visualizations
{
    public class GridVisualizationExtensionsFixture
    {
        [Fact]
        public void ConfigureSettings_Works_NoConditions()
        {
            // Arrange
            var vs = new GridVisualization();
            var expectedSettings = new GridVisualizationSettings()
            {
                DateFieldAlignment = Alignment.Center,
                FontSize = FontSize.Large,
                IsFirstColumnFixed = false,
                IsPagingEnabled = true,
                NumericFieldAlignment = Alignment.Left,
                PageSize = 5,
                SchemaTypeName = "Test Schema Type Name",
                Style = new GridVisualizationStyle()
                {
                    DateAlignment = Alignment.Left,
                    FixedLeftColumns = false,
                    NumericAlignment = Alignment.Right,
                    TextAlignment = Alignment.Right
                },
                TextFieldAlignment = Alignment.Right,
                VisualizationType = "Test VS Type"
            };

            var action = (GridVisualizationSettings s) =>
            {
                s.DateFieldAlignment = expectedSettings.DateFieldAlignment;
                s.FontSize = expectedSettings.FontSize;
                s.IsFirstColumnFixed = expectedSettings.IsFirstColumnFixed;
                s.IsPagingEnabled = expectedSettings.IsPagingEnabled;
                s.NumericFieldAlignment = expectedSettings.NumericFieldAlignment;
                s.PageSize = expectedSettings.PageSize;
                s.SchemaTypeName = expectedSettings.SchemaTypeName;
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
