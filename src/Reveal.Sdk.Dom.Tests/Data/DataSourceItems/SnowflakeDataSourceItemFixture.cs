using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class SnowflakeDataSourceItemFixture
    {
        [Theory]
        [InlineData("DS Title", "DS Title", "DSI Title", "DSI Title")]
        [InlineData(null, "DSI Title", "DSI Title", "DSI Title")]
        public void Constructor_CreateSnowFlakeDSI_WithTitleAndDataSource(string dsTitle, string expectedDSTitle, string dsiTitle, string expectedDSITitle)
        {
            // Arrange
            var dataSource = new DataSource() { Title = dsTitle };

            // Act
            var item = new SnowflakeDataSourceItem(dsiTitle, dataSource);

            // Assert
            Assert.Equal(SchemaTypeNames.DataSourceItemType, item.SchemaTypeName);
            Assert.Equal(expectedDSTitle, item.DataSource.Title);
            Assert.Equal(dataSource.Id, item.DataSource.Id);
            Assert.Equal(dataSource.Id, item.DataSourceId);
            Assert.IsType<SnowflakeDataSource>(item.DataSource);
            Assert.NotSame(dataSource, item.DataSource);
            Assert.Equal(expectedDSITitle, item.Title);
        }

        [Theory]
        [InlineData("DS Title", "DS Title", "DSI Title", "DSI Title")]
        [InlineData(null, "DSI Title", "DSI Title", "DSI Title")]
        public void Constructor_CreateSnowFlakeDSI_WithTitleAndSnowFlakeDataSource(string dsTitle, string expectedDSTitle, string dsiTitle, string expectedDSITitle)
        {
            // Arrange
            var dataSource = new SnowflakeDataSource() { Title = dsTitle };

            // Act
            var item = new SnowflakeDataSourceItem(dsiTitle, dataSource);

            // Assert
            Assert.Equal(SchemaTypeNames.DataSourceItemType, item.SchemaTypeName);
            Assert.Equal(expectedDSTitle, item.DataSource.Title);
            Assert.Equal(dataSource.Id, item.DataSource.Id);
            Assert.Equal(dataSource.Id, item.DataSourceId);
            Assert.IsType<SnowflakeDataSource>(item.DataSource);
            Assert.Same(dataSource, item.DataSource);
            Assert.Equal(expectedDSITitle, item.Title);
        }

        [Fact]
        public void GetProcessDataOnServer_ReturnSameValue_AfterSet()
        {
            // Arrange
            var item = new SnowflakeDataSourceItem("Test Item", new SnowflakeDataSource());
            var expectedProcessDataOnServer = true;

            // Act
            item.ProcessDataOnServer = expectedProcessDataOnServer;

            // Assert
            Assert.Equal(expectedProcessDataOnServer, item.ProcessDataOnServer);
            Assert.Equal(expectedProcessDataOnServer, item.Properties.GetValue<bool>("ServerAggregation"));
        }

        [Fact]
        public void ToJsonString_CreatesFormattedJson_NoConditions()
        {
            // Arrange
            var expectedJson = @"{
              ""_type"": ""DataSourceItemType"",
              ""Id"": ""a60ab508-0ed4-46e4-8d90-c3a9c09c29b8"",
              ""Title"": ""SnowFlake DSI"",
              ""DataSourceId"": ""snowflake_ds"",
              ""HasTabularData"": true,
              ""HasAsset"": false,
              ""Properties"": {
                ""ServerAggregation"": true,
                ""Table"": ""CALL_CENTER"",
                ""Schema"": ""TPCDS_SF100TCL""
              },
              ""Parameters"": {
                ""RPCustomQuery"": ""Select CC_CALL_CENTER_ID, CC_CALL_CENTER_SK from CALL_CENTER""
              }
            }";

            var dataSource = new SnowflakeDataSource()
            {
                Id = "snowflake_ds",
            };
            var dataSourceItem = new SnowflakeDataSourceItem("SnowFlake DSI", dataSource)
            {
                Id = "a60ab508-0ed4-46e4-8d90-c3a9c09c29b8",
                HasTabularData = true,
                HasAsset = false,
                ProcessDataOnServer = true,
                Table = "CALL_CENTER",
                Schema = "TPCDS_SF100TCL",
                CustomQuery = "Select CC_CALL_CENTER_ID, CC_CALL_CENTER_SK from CALL_CENTER",
            };

            var expectedJObject = JObject.Parse(expectedJson);

            // Act
            var json = dataSourceItem.ToJsonString();
            var actualJObject = JObject.Parse(json);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);

        }
    }
}
