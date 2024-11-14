using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data
{
    public class DataSourceFixture
    {
        [Fact]
        public void Constructor_SetDefaultDataSource_WithoutArguments()
        {
            // Arrange
            var dataSource = new DataSource();

            // Act

            // Assert
            Assert.Equal(SchemaTypeNames.DataSourceType, dataSource.SchemaTypeName);
        }

        [Fact]
        public void Constructor_SetDefaultProperties_WithoutArguments()
        {
            // Arrange
            var dataSource = new DataSource();

            // Act

            // Assert
            Assert.NotNull(dataSource.Properties);
            Assert.Empty(dataSource.Properties);
        }

        [Fact]
        public void Constructor_GeneratesUniqueNotEmptyId_WithoutArguments()
        {
            // Arrange
            var dataSource1 = new DataSource();
            var dataSource2 = new DataSource();

            // Act

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
        public void DefaultRefreshRateGetter_ReturnSameValue_WithSetValue()
        {
            // Arrange
            var dataSource = new DataSource();
            var expectedValue = "10 minutes";

            // Act
            dataSource.DefaultRefreshRate = expectedValue;
            var actualValue = dataSource.DefaultRefreshRate;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Constructor_CreateNotNullObject_WithoutArguments()
        {
            // Arrange
            var dataSource = new DataSource();

            // Act
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
        public void GetHashCode_ReturnsConsistentValue_ForTheSameDataSource()
        {
            // Arrange
            var dataSource = new DataSource();

            // Act
            var hashCode1 = dataSource.GetHashCode();
            var hashCode2 = dataSource.GetHashCode();

            // Assert
            Assert.Equal(hashCode1, hashCode2);
        }

        [Fact]
        public void CheckEqual_ReturnFalse_WhenComparedWithNull()
        {
            // Arrange
            var dataSource = new DataSource();

            // Act
            var result = dataSource.Equals(null);

            // Assert
            Assert.False(result);
        }
    }
}
