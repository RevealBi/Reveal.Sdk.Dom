using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class PostgreSqlDataSourceItemFixture
    {
        [Theory]
        [InlineData("DS Item Title", "DS Item Title", "DS Title", "DS Title")]
        [InlineData("DS Item Title", "DS Item Title", null, "DS Item Title")]
        public void Constructor_CreatePostgresDSItem_WithTitleAndPostgresDataSource(string dsItemTitle, string expectedDSItemTitle, string dsTitle, string expectedDSTitle)
        {
            // Arrange
            var dataSource = new PostgreSQLDataSource() { Title = dsTitle};

            // Act
            var item = new PostgreSqlDataSourceItem(dsItemTitle, dataSource);

            // Assert
            Assert.Equal(expectedDSItemTitle, item.Title);
            Assert.Equal(expectedDSTitle, item.DataSource.Title);
            Assert.Equal(dataSource.Id, item.DataSource.Id);
            Assert.Equal(dataSource.Id, item.DataSourceId);
            Assert.Same(dataSource, item.DataSource);
        }

        [Theory]
        [InlineData("DS Item Title", "DS Item Title", "DS Title", "DS Title")]
        [InlineData("DS Item Title", "DS Item Title", null, "DS Item Title")]
        public void Constructor_CreatePostgresDSItem_WithTitleAndDataSource(string dsItemTitle, string expectedDSItemTitle, string dsTitle, string expectedDSTitle)
        {
            // Arrange
            var dataSource = new DataSource() { Title = dsTitle };

            // Act
            var item = new PostgreSqlDataSourceItem(dsItemTitle, dataSource);

            // Assert
            Assert.Equal(expectedDSItemTitle, item.Title);
            Assert.Equal(expectedDSTitle, item.DataSource.Title);
            Assert.Equal(dataSource.Id, item.DataSource.Id);
            Assert.Equal(dataSource.Id, item.DataSourceId);
            Assert.IsType<PostgreSQLDataSource>(item.DataSource);
            Assert.NotSame(dataSource, item.DataSource);
        }

        [Theory]
        [InlineData("DS Item Title", "DS Item Title", "DS Title", "DS Title")]
        [InlineData("DS Item Title", "DS Item Title", null, "DS Item Title")]
        public void Constructor_CreatePostgresDSItem_WithCustomTableAndPostgresDataSource(string dsItemTitle, string expectedDSItemTitle, string dsTitle, string expectedDSTitle)
        {
            // Arrange
            var tableName = "Table";
            var dataSource = new PostgreSQLDataSource() { Title = dsTitle};

            // Act
            var postgresDSItem = new PostgreSqlDataSourceItem(dsItemTitle, tableName, dataSource);

            // Assert
            Assert.Equal(expectedDSTitle, postgresDSItem.DataSource.Title);
            Assert.Equal(expectedDSItemTitle, postgresDSItem.Title);
            Assert.Equal(tableName, postgresDSItem.Table);
            Assert.Equal(tableName, postgresDSItem.Properties.GetValue<string>("Table"));
            Assert.Equal(dataSource.Id, postgresDSItem.DataSource.Id);
            Assert.Equal(dataSource.Id, postgresDSItem.DataSourceId);
            Assert.Same(dataSource, postgresDSItem.DataSource);
        }

        [Theory]
        [InlineData("DS Item Title", "DS Item Title", "DS Title", "DS Title")]
        [InlineData("DS Item Title", "DS Item Title", null, "DS Item Title")]
        public void Constructor_CreatePostgresDSItem_WithCustomTableAndDataSource(string dsItemTitle, string expectedDSItemTitle, string dsTitle, string expectedDSTitle)
        {
            // Arrange
            var tableName = "Table";
            var dataSource = new DataSource() { Title = dsTitle };

            // Act
            var postgresDSItem = new PostgreSqlDataSourceItem(dsItemTitle, tableName, dataSource);

            // Assert
            Assert.Equal(expectedDSTitle, postgresDSItem.DataSource.Title);
            Assert.Equal(expectedDSItemTitle, postgresDSItem.Title);
            Assert.Equal(tableName, postgresDSItem.Table);
            Assert.Equal(tableName, postgresDSItem.Properties.GetValue<string>("Table"));
            Assert.Equal(dataSource.Id, postgresDSItem.DataSource.Id);
            Assert.Equal(dataSource.Id, postgresDSItem.DataSourceId);
            Assert.IsType<PostgreSQLDataSource>(postgresDSItem.DataSource);
            Assert.NotSame(dataSource, postgresDSItem.DataSource);
        }

        [Fact]
        public void GetProcessDataOnServer_ReturnSameValue_WhenSetValue()
        {
            // Arrange
            var item = new PostgreSqlDataSourceItem("Test Item", new PostgreSQLDataSource());

            // Act
            item.ProcessDataOnServer = true;

            // Assert
            Assert.True(item.ProcessDataOnServer);
            Assert.True(item.Properties.GetValue<bool>("ServerAggregation"));
        }

        [Fact]
        public void ToJsonString_CreatesFormattedJson_NoConditions()
        {
            // Arrange
            var expectedJson = @"{
              ""_type"": ""DataSourceItemType"",
              ""Id"": ""pgSqlDSItemId"",
              ""Title"": ""Postgres DSItem"",
              ""Subtitle"": ""Northwind Employees For DS Item"",
              ""DataSourceId"": ""pgSqlId"",
              ""HasTabularData"": true,
              ""HasAsset"": false,
              ""Properties"": {
                ""ServerAggregation"": true,
                ""Database"": ""northwind"",
                ""Table"": ""categories"",
                ""Schema"": """"
              },
              ""Parameters"": {}
            }";

            var dataSource = new PostgreSQLDataSource()
            {
                Id = "pgSqlId",
            };

            var dataSourceItem = new PostgreSqlDataSourceItem("Postgres DSItem", dataSource)
            {
                Id = "pgSqlDSItemId",
                Subtitle = "Northwind Employees For DS Item",
                HasTabularData = true,
                HasAsset = false,
                ProcessDataOnServer = true,
                Database = "northwind",
                Table = "categories",
                Schema = "",
            };

            // Act
            var json = dataSourceItem.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(json);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);

        }
    }
}