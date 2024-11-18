using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using System.Reflection;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data
{
    public class DataSourceFixture
    {
        [Fact]
        public void Constructor_SetDefaultSchemaTypeName_WithoutArguments()
        {
            // Arrange
            var expectedSchemaTypeName = SchemaTypeNames.DataSourceType;

            // Act
            var dataSource = new DataSource();

            // Assert
            Assert.Equal(expectedSchemaTypeName, dataSource.SchemaTypeName);
        }

        [Fact]
        public void Constructor_SetDefaultProperties_WithoutArguments()
        {
            // Arrange

            // Act
            var dataSource = new DataSource();

            // Assert
            Assert.NotNull(dataSource.Properties);
            Assert.Empty(dataSource.Properties);
        }

        [Fact]
        public void Constructor_GeneratesUniqueNotEmptyId_WithoutArguments()
        {
            // Arrange

            // Act
            var dataSource1 = new DataSource();
            var dataSource2 = new DataSource();

            // Assert
            Assert.NotEmpty(dataSource1.Id);
            Assert.NotEmpty(dataSource2.Id);
            Assert.NotEqual(dataSource1.Id, dataSource2.Id);
        }

        [Fact]
        public void SetId_GeneratesUniqueNotEmptyId_WithNullValue()
        {
            // Arrange
            var dataSource1 = new DataSource();
            var dataSource2 = new DataSource();

            // Act
            dataSource1.Id = null;
            dataSource2.Id = null;

            // Assert
            Assert.NotEmpty(dataSource1.Id);
            Assert.NotEmpty(dataSource2.Id);
            Assert.NotEqual(dataSource1.Id, dataSource2.Id);
        }

        [Fact]
        public void GetId_ReturnSameId_WhenSetNotNullId()
        {
            // Arrange
            var dataSource = new DataSource();
            var expectedId = "test-id";

            // Act
            dataSource.Id = expectedId;

            // Assert
            Assert.Equal(expectedId, dataSource.Id);
        }

        [Fact]
        public void GetDefaultRefreshRate_ReturnSameValue_WithSetValue()
        {
            // Arrange
            var dataSource = new DataSource();
            var expectedValue = "10 minutes";

            // Act
            dataSource.DefaultRefreshRate = expectedValue;
            var actualValue = dataSource.DefaultRefreshRate;

            // Assert
            Assert.Equal(expectedValue, actualValue);
            Assert.Equal(expectedValue, dataSource.Properties.GetValue<string>("DefaultRefreshRate"));
        }

        [Fact]
        public void Constructor_CreateNotNullObject_WithoutArguments()
        {
            // Arrange

            // Act
            var dataSource = new DataSource();
            var result = dataSource.Equals(null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void CheckEqual_ReturnTrue_WhenIdsAreTheSame()
        {
            // Arrange
            var dataSource1 = new DataSource { Id = "same-id" };
            var dataSource2 = new DataSource { Id = "same-id" };

            // Act
            var result = dataSource1.Equals(dataSource2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GetHashCode_ReturnsConsistentValue_ForTheSameValues()
        {
            // Arrange
            var dataSource1 = new DataSource() { Id = "id", Title = "Title", Subtitle = "Sub Title", Provider = DataSourceProvider.REST, SchemaTypeName = SchemaTypeNames.AssetVisualizationDataSpecType};
            var dataSource2 = new DataSource() { Id = "id", Title = "Title", Subtitle = "Sub Title", Provider = DataSourceProvider.REST, SchemaTypeName = SchemaTypeNames.AssetVisualizationDataSpecType };

            // Act
            var hashCode1 = dataSource1.GetHashCode();
            var hashCode2 = dataSource2.GetHashCode();

            // Assert
            Assert.Equal(hashCode1, hashCode2);
        }

        [Theory]
        [InlineData("SchemaTypeName", SchemaTypeNames.AssetVisualizationDataSpecType)]
        [InlineData("Id", "new-id")]
        [InlineData("Title", "new-title")]
        [InlineData("Subtitle", "new-subtitle")]
        [InlineData("Provider", DataSourceProvider.MicrosoftExcel)]
        public void GetHashCode_ReturnsDifferentValue_WithDifferentStringFieldValues(string propertyName, object propertyValue)
        {
            // Arrange
            var dataSource = new DataSource();

            // Act
            var hashCode1 = dataSource.GetHashCode();
            dataSource.GetType()
                .GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.Instance).SetValue(dataSource, propertyValue, null);
            var hashCode2 = dataSource.GetHashCode();
            

            // Assert
            Assert.NotEqual(hashCode1, hashCode2);
        }
    }
}
