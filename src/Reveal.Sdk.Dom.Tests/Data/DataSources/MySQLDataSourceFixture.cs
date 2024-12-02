using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Serialization;
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



        [Fact]
        public void ToJsonString_CreatesFormattedJson_ForMySQLDataSource()
        {
            // Arrange
            var expectedJson = @"{
              ""_type"": ""DataSourceType"",
              ""Username"": ""username"",
              ""Password"": ""password"",
              ""Id"": ""mySqlId"",
              ""Provider"": ""MYSQL"",
              ""Description"": ""MySQL DS"",
              ""Subtitle"": ""Employees Table"",
              ""DefaultRefreshRate"": ""120"",
              ""Properties"": {
                ""ServerAggregationDefault"": true,
                ""ServerAggregationReadOnly"": false,
                ""Host"": ""mysqlserver.local"",
                ""Port"": ""3306"",
                ""Database"": ""northwind"",
                ""DefaultRefreshRate"": ""120"",
                ""Username"": ""username"",
                ""Password"": ""password""
              }
            }";

            var dataSource = new MySQLDataSource()
            {
                Id = "mySqlId",
                Title = "MySQL DS",
                Subtitle = "Employees Table",
                ProcessDataOnServerDefaultValue = true,
                ProcessDataOnServerReadOnly = false,
                Host = "mysqlserver.local",
                Port = "3306",
                Database = "northwind",
                DefaultRefreshRate = "120",
                Username = "username",
                Password = "password",
            };

            // Act
            var json = dataSource.ToJsonString();
            var serializedDataSource = RdashSerializer.SerializeObject(dataSource);
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(json);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }
    }
}