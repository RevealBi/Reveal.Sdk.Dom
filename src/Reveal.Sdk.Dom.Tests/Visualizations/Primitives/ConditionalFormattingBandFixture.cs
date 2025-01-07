using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Visualizations;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Primitives
{
    public class ConditionalFormattingBandFixture
    {
        [Fact]
        public void Constructor_SetDefaultValues_WhenConstructed()
        {
            // Act
            var item = new ConditionalFormattingBand();

            // Assert
            Assert.Equal(SchemaTypeNames.ConditionalFormattingBandType, item.SchemaTypeName);
        }
    }
}
