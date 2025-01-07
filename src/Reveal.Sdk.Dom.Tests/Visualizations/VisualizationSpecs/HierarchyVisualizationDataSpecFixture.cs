using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.VisualizationSpecs
{
    public class HierarchyVisualizationDataSpecFixture
    {
        [Fact]
        public void Constructor_FieldsHaveDefaultValue_WithoutParameters()
        {
            // Act
            var hierarchyVSDataSpec = new HierarchyVisualizationDataSpec();

            // Assert
            Assert.Empty(hierarchyVSDataSpec.AdHocExpandedElements);
            Assert.Empty(hierarchyVSDataSpec.Rows);
            Assert.Null(hierarchyVSDataSpec.AdHocFields);
        }
    }
}
