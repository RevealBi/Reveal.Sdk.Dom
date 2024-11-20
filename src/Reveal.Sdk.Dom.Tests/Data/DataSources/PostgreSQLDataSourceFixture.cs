using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSources
{
    public class PostgreSQLDataSourceFixture
    {
        [Fact]
        public void Constructor_SetsProviderToPostgreSQL_WhenConstructed()
        {
            // Act
            var dataSource = new PostgreSQLDataSource();

            // Assert
            Assert.Equal(DataSourceProvider.PostgreSQL, dataSource.Provider);
        }
    }
}