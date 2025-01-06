using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.VisualizationSpecs
{
    public class GridVisualizationDataSpecFixture
    {
        [Fact]
        public void Constructor_CreateObjWithDefaultValue_WithoutParameters()
        {
            // Act
            var gridVSDataSpec = new GridVisualizationDataSpec();

            // Assert
            Assert.Equal(SchemaTypeNames.GridVisualizationDataSpecType, gridVSDataSpec.SchemaTypeName);
            Assert.Empty(gridVSDataSpec.Columns);
        }
    }
}
