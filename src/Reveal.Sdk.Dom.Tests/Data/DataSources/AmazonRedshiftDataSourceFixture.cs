using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSources
{
    public class AmazonRedshiftDataSourceFixture
    {
        [Fact]
        public void Constructor_SetsProviderToAmazonRedshift_WithoutParameters()
        {
            // Act
            var dataSource = new AmazonRedshiftDataSource();

            // Assert
            Assert.Equal(DataSourceProvider.AmazonRedshift, dataSource.Provider);
        }

        [Fact]
        public void GetSchema_ReturnSameValue_WithSetValue()
        {
            // Arrange
            var dataSource = new AmazonRedshiftDataSource();
            var expectedSchema = "TestSchema";

            // Act
            dataSource.Schema = expectedSchema;

            // Assert
            Assert.Equal(expectedSchema, dataSource.Schema);
            Assert.Equal(expectedSchema, dataSource.Properties.GetValue<string>("Schema"));
        }

        [Fact]
        public void ToJsonString_CreatesFormattedJson_NoConditions()
        {
            // Arrange
            var expectedJson = """
            {
              "_type": "DataSourceType",
              "Id": "redshiftId",
              "Provider": "AMAZON_REDSHIFT",
              "Description": "Redshift DS",
              "Properties": {
                "Host": "RedshiftHost",
                "Database": "RedshiftDB",
                "Schema": "RedshiftSchema"
              },
              "Settings": {
                "DefaultRefreshRate": 180
              }
            }
            """;
            var dataSource = new AmazonRedshiftDataSource()
            {
                Id = "redshiftId",
                Title = "Redshift DS",
                Host = "RedshiftHost",
                Database = "RedshiftDB",
                Schema = "RedshiftSchema",
                DefaultRefreshRate = "180"
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