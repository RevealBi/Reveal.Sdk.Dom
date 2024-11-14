using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSources
{
    public class AmazonRedshiftDataSourceFixture
    {
        [Fact]
        public void Constructor_Should_SetProviderToAmazonRedshift()
        {
            // Arrange
            var dataSource = new AmazonRedshiftDataSource();

            // Assert
            Assert.Equal(DataSourceProvider.AmazonRedshift, dataSource.Provider);
        }

        [Fact]
        public void Schema_Should_GetAndSetSchemaProperty()
        {
            // Arrange
            var dataSource = new AmazonRedshiftDataSource();
            var expectedSchema = "TestSchema";

            // Act
            dataSource.Schema = expectedSchema;
            var result = dataSource.Schema;

            // Assert
            Assert.Equal(expectedSchema, result);
        }

        [Fact]
        public void Properties_Should_StoreSchemaValueCorrectly()
        {
            // Arrange
            var dataSource = new AmazonRedshiftDataSource();
            var expectedSchema = "TestSchema";

            // Act
            dataSource.Schema = expectedSchema;

            // Assert
            Assert.Equal(expectedSchema, dataSource.Properties.GetValue<string>("Schema"));
        }

        [Fact]
        public void Properties_Should_UpdateSchemaValueCorrectly()
        {
            // Arrange
            var dataSource = new AmazonRedshiftDataSource();
            var initialSchema = "InitialSchema";
            var updatedSchema = "UpdatedSchema";

            // Act & Assert for Initial Value
            dataSource.Schema = initialSchema;
            Assert.Equal(initialSchema, dataSource.Schema);

            // Act & Assert for Updated Value
            dataSource.Schema = updatedSchema;
            Assert.Equal(updatedSchema, dataSource.Schema);
        }

        [Fact]
        public void Schema_SetToNull_Should_HandleNullAssignment()
        {
            // Arrange
            var dataSource = new AmazonRedshiftDataSource();

            // Act
            dataSource.Schema = null;

            // Assert
            Assert.Null(dataSource.Schema);
            Assert.Null(dataSource.Properties.GetValue<string>("Schema"));
        }

        [Fact]
        public void Provider_Should_RemainAmazonRedshift_AfterSchemaSet()
        {
            // Arrange
            var dataSource = new AmazonRedshiftDataSource();

            // Act
            dataSource.Schema = "SomeSchema";

            // Assert
            Assert.Equal(DataSourceProvider.AmazonRedshift, dataSource.Provider);
        }
    }
}