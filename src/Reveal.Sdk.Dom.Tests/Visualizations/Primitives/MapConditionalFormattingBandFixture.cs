using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Core.Constants;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Primitives
{
    public class MapConditionalFormattingBandFixture
    {
        [Fact]
        public void Constructor_SetSchemaTypeNames_WhenConstructed()
        {
            // Act
            var item = new MapConditionalFormattingBand();

            // Arrange
            Assert.Equal(SchemaTypeNames.ConditionalFormattingBandType, item.SchemaTypeName);
        }
    }
}
