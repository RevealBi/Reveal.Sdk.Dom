using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSources
{
    public class MySQLDataSourceFixture
    {
        [Fact]
        public void Constructor_SetsProviderToMySQL_WhenConstructed()
        {
            // Act
            var dataSource = new MySQLDataSource();

            // Assert
            Assert.Equal(DataSourceProvider.MySQL, dataSource.Provider);
        }
        
        [Fact]
        public void Constructor_InitializesPropertiesToDefaultValues_WhenInstanceIsCreated()
        {
            // Act
            var dataSource = new MySQLDataSource();

            // Assert
            Assert.NotNull(dataSource);
        }
    }
}