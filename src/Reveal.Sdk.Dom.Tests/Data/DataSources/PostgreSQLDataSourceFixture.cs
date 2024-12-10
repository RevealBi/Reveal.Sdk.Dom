using Reveal.Sdk.Dom.Core.Serialization;
using Reveal.Sdk.Dom.Data;
using Xunit;
using Newtonsoft.Json.Linq;

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

        [Fact]
        public void ToJsonString_CreatesFormattedJson_NoConditions()
        {
            // Arrange
            var expectedJson = @"{
              ""_type"": ""DataSourceType"",
              ""Id"": ""pgSqlId"",
              ""Provider"": ""POSTGRES"",
              ""Description"": ""Postgres DS"",
              ""Subtitle"": ""Northwind Employees"",
              ""Properties"": {
                ""ServerAggregationDefault"": true,
                ""ServerAggregationReadOnly"": true,
                ""Host"": ""revealdb01.infragistics.local"",
                ""Port"": 5432,
                ""Database"": ""northwind"",
                ""Schema"": ""public""
              },
              ""Settings"": {
                ""DefaultRefreshRate"": 180
              }
            }";

            var dataSource = new PostgreSQLDataSource()
            {
                Id = "pgSqlId",
                Title = "Postgres DS",
                Subtitle = "Northwind Employees",
                ProcessDataOnServerDefaultValue = true,
                ProcessDataOnServerReadOnly = true,
                Host = "revealdb01.infragistics.local",
                Port = 5432,
                Database = "northwind",
                Schema = "public",
                DefaultRefreshRate = "180",
                
            };

            // Act
            var json = dataSource.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(json);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);

        }
    }
}