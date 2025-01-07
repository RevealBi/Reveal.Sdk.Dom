using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.VisualizationSpecs
{
    public class AssetVisualizationDataSpecFixture
    {
        [Fact]
        public void Constructor_CreateObjWithDefaultValue_WithoutParameters()
        {
            // Act
            var assertVSDataSpec = new AssetVisualizationDataSpec();

            // Assert
            Assert.Equal(SchemaTypeNames.AssetVisualizationDataSpecType, assertVSDataSpec.SchemaTypeName);
            Assert.Null(assertVSDataSpec.UrlColumn);
        }
    }
}
