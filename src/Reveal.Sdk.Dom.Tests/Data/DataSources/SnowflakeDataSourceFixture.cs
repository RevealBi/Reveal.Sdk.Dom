using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSources
{
    public class SnowflakeDataSourceFixture
    {
        [Fact]
        public void Constructor_SetDefaultProvider_WithoutParameter()
        {
            // Act
            var dataSource = new SnowflakeDataSource();

            // Assert
            Assert.Equal(DataSourceProvider.Snowflake, dataSource.Provider);
        }

        [Theory]
        [InlineData("testAccount")]
        [InlineData(null)]
        public void GetAccount_ReturnSameValue_AfterSetAccount(string account)
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
        public void GetRole_ReturnSameValue_AfterSetRole(string role)
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
        public void SetWarehouse_ReturnSameValue_AfterSetWarehouse(string warehouse)
        {
            // Arrange
            var dataSource = new SnowflakeDataSource();

            // Act
            dataSource.Warehouse = warehouse;

            // Assert
            Assert.Equal(warehouse, dataSource.Warehouse);
            Assert.Equal(warehouse, dataSource.Properties.GetValue<string>("Warehouse"));
        }

        [Fact]
        public void ToJsonString_CreatesFormattedJson_NoConditions()
        {
            // Arrange
            var expectedJson = @"{
              ""_type"": ""DataSourceType"",
              ""Id"": ""snowflake_ds"",
              ""Provider"": ""SNOWFLAKE"",
              ""Description"": ""Snowflake TEST"",
              ""Subtitle"": ""Snowflake TEST Subtitle"",
              ""Properties"": {
                ""ServerAggregationDefault"": true,
                ""ServerAggregationReadOnly"": false,
                ""Host"": ""gpiskyj-al16914.snowflakecomputing.com"",
                ""Database"": ""SNOWFLAKE_SAMPLE_DATA"",
                ""Account"": ""pqwkobs-xb90908"",
                ""Warehouse"": ""COMPUTE_WH"",
                ""Schema"": ""TPCDS_SF100TCL""
              },
              //""Settings"": {} // TODO: Update to check settings fields after Postgresql PR is merged
            }";

            var dataSource = new SnowflakeDataSource()
            {
                Id = "snowflake_ds",
                Title = "Snowflake TEST",
                Subtitle = "Snowflake TEST Subtitle",
                ProcessDataOnServerDefaultValue = true,
                ProcessDataOnServerReadOnly = false,
                Host = "gpiskyj-al16914.snowflakecomputing.com",
                Database = "SNOWFLAKE_SAMPLE_DATA",
                Account = "pqwkobs-xb90908",
                Warehouse = "COMPUTE_WH",
                Schema = "TPCDS_SF100TCL",
            };
            var expectedJObject = JObject.Parse(expectedJson);

            // Act
            var json = dataSource.ToJsonString();
            var actualJObject = JObject.Parse(json);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);

        }
    }
}
