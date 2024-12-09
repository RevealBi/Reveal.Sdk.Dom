using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSources
{
    public class MicrosoftAzureSqlServerDataSourceFixture
    {
        [Fact]
        public void Constructor_SetProviderToMicrosoftAzureSqlServer_WhenConstructed()
        {
            // Act
            var dataSource = new MicrosoftAzureSqlServerDataSource();

            // Assert
            Assert.Equal(DataSourceProvider.MicrosoftAzureSqlServer, dataSource.Provider);
        }

        [Fact]
        public void GetTrustServerCertificate_ReturnSameValue_WhenSet()
        {
            // Arrange
            var dataSource = new MicrosoftAzureSqlServerDataSource();
            var trustServerCertificate = false;

            // Act
            dataSource.TrustServerCertificate = trustServerCertificate;

            // Assert
            Assert.Equal(trustServerCertificate, dataSource.TrustServerCertificate);
            Assert.Equal(trustServerCertificate, dataSource.Properties.GetValue<bool>("TrustServerCertificate"));
        }

        [Fact]
        public void ToJsonString_CreateExpectedRevealJson_NoConditions()
        {
            // Arrange
            var expectedJson = @"{
              ""_type"": ""DataSourceType"",
              ""Id"": ""azureSqlId"",
              ""Provider"": ""AZURE_SQL"",
              ""Description"": ""Azure SQL DS"",
              ""Subtitle"": ""Azure SQL DS Item"",
              ""Properties"": {
                ""ServerAggregationDefault"": false,
                ""ServerAggregationReadOnly"": true,
                ""Host"": ""revealtesting.database.windows.net"",
                ""Port"": 1433,
                ""Database"": ""reveal"",
                ""Schema"": ""azureSchema"",
                ""TrustServerCertificate"": false,
                ""Encrypt"": false
              },
             
            }";
            var dataSource = new MicrosoftAzureSqlServerDataSource()
            {
                Id = "azureSqlId",
                Title = "Azure SQL DS",
                Subtitle = "Azure SQL DS Item",
                ProcessDataOnServerDefaultValue = false,
                ProcessDataOnServerReadOnly = true,
                Host = "revealtesting.database.windows.net",
                Port = 1433,
                Database = "reveal",
                Schema = "azureSchema",
                TrustServerCertificate = false,
                Encrypt = false,
                //DefaultRefreshRate = "161" // TODO: Check the DefaultRefreshRate when get Json, after DefaultRefreshRate is moved to Settings field
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
    