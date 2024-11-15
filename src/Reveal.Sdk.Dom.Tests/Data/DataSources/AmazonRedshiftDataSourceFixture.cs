using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSources
{
    public class AmazonRedshiftDataSourceFixture
    {
        [Fact]
        public void Constructor_SetsProviderToAmazonRedshift_Always()
        {
            // Arrange
            var dataSource = new AmazonRedshiftDataSource();

            // Act
            var actualProvider = dataSource.Provider;

            // Assert
            Assert.Equal(DataSourceProvider.AmazonRedshift, actualProvider);
        }

        [Fact]
        public void Schema_SetsAndGetsValue_ValidSchema()
        {
            // Arrange
            var dataSource = new AmazonRedshiftDataSource();
            var expectedSchema = "TestSchema";

            // Act
            dataSource.Schema = expectedSchema;
            var actualSchema = dataSource.Schema;

            // Assert
            Assert.Equal(expectedSchema, actualSchema);
        }

        [Fact]
        public void Schema_SetsValue_NullSchema()
        {
            // Arrange
            var dataSource = new AmazonRedshiftDataSource();

            // Act
            dataSource.Schema = null;
            var actualSchema = dataSource.Schema;
            var actualPropertySchema = dataSource.Properties.GetValue<string>("Schema");

            // Assert
            Assert.Null(actualSchema);
            Assert.Null(actualPropertySchema);
        }

        [Fact]
        public void Properties_StoresSchemaValueCorrectly_ValidSchema()
        {
            // Arrange
            var dataSource = new AmazonRedshiftDataSource();
            var expectedSchema = "TestSchema";

            // Act
            dataSource.Schema = expectedSchema;
            var actualPropertySchema = dataSource.Properties.GetValue<string>("Schema");

            // Assert
            Assert.Equal(expectedSchema, actualPropertySchema);
        }
    }
}