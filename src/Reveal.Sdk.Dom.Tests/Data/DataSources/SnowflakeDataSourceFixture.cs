using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSources
{
    public class SnowflakeDataSourceFixture
    {
        [Fact]
        public void SnowflakeDataSource_ShouldSetProviderToSnowflake()
        {
            // Act
            var dataSource = new SnowflakeDataSource();

            // Assert
            Assert.Equal(DataSourceProvider.Snowflake, dataSource.Provider);
        }

        [Theory]
        [InlineData("testAccount")]
        [InlineData(null)]
        public void Account_ShouldSetAndGetValue_WithDifferentInputs(string account)
        {
            // Arrange
            var dataSource = new SnowflakeDataSource();

            // Act
            dataSource.Account = account;

            // Assert
            Assert.Equal(account, dataSource.Account);
            Assert.Equal(account, dataSource.Properties.GetValue<string>("Account"));
        }

        [Theory]
        [InlineData("testRole")]
        [InlineData(null)]
        public void Role_ShouldSetAndGetValue_WithDifferentInputs(string role)
        {
            // Arrange
            var dataSource = new SnowflakeDataSource();

            // Act
            dataSource.Role = role;

            // Assert
            Assert.Equal(role, dataSource.Role);
            Assert.Equal(role, dataSource.Properties.GetValue<string>("Role"));
        }

        [Theory]
        [InlineData("testWarehouse")]
        [InlineData(null)]
        public void Warehouse_ShouldSetAndGetValue_WithDifferentInputs(string warehouse)
        {
            // Arrange
            var dataSource = new SnowflakeDataSource();

            // Act
            dataSource.Warehouse = warehouse;

            // Assert
            Assert.Equal(warehouse, dataSource.Warehouse);
            Assert.Equal(warehouse, dataSource.Properties.GetValue<string>("Warehouse"));
        }
    }
}
