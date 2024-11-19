using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data
{
    public class SchemaDataSourceFixture
    {
        [Fact]
        public void GetSchema_ReturnSameValue_AsSetValue()
        {
            // Arrange
            var schemaDataSource = new SchemaDataSource();
            var expectedSchema = "Schema";

            // Act
            schemaDataSource.Schema = "Schema";

            // Assert
            Assert.Equal(expectedSchema, schemaDataSource.Schema);
        }
    }
}
