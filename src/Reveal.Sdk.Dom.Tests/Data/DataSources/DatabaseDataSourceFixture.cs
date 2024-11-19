using Reveal.Sdk.Dom.Data;
using Moq;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSources
{
    public class DatabaseDataSourceFixture
    {
        [Fact]
        public void GetDatabase_ReturnsSameValue_WithSetValue()
        {
            // Arrange
            var mock = new Mock<DatabaseDataSource>();
            var expectedDatabaseName = "database";

            // Act
            var dbDataSource = mock.Object;
            dbDataSource.Database = expectedDatabaseName;

            // Assert
            Assert.Equal(expectedDatabaseName, dbDataSource.Database);
        }
    }
}