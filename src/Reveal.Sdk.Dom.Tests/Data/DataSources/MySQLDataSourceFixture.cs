using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Core.Serialization;
using Reveal.Sdk.Dom.Data;
using System.IO;
using System;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using Reveal.Sdk.Dom.Visualizations;
using Xunit.Sdk;

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
        public void RDashDocument_HasCorrectDataSource_WhenLoadFromFile()
        {
            // Arrange
            var filePath = Path.Combine(Environment.CurrentDirectory, "Dashboards", "TestMySQL.rdash");

            // Act
            var document = RdashDocument.Load(filePath);
            var dataSource = document.DataSources.FirstOrDefault();

            // Assert
            Assert.Equal(DataSourceProvider.MySQL, dataSource.Provider);
            Assert.NotNull(dataSource.Properties.GetValue<string>("ServerAggregationDefault"));
            Assert.NotNull(dataSource.Properties.GetValue<string>("ServerAggregationReadOnly"));
        }

        [Fact]
        public void ToJsonString_CreatesFormattedJson_ForMySQLDataSource()
        {
            // Arrange
            var expectedJson = @"
            {
                  ""_type"": ""DataSourceType"",
                  ""Id"": ""mySqlId"",
                  ""Provider"": ""MYSQL"",
                  ""Description"": ""MySQL DS"",
                  ""DefaultRefreshRate"": ""120"",
                  ""Properties"": {
                    ""ServerAggregationDefault"": true,
                    ""ServerAggregationReadOnly"": false,
                    ""Host"": ""mysqlserver.local"",
                    ""Port"": ""3306"",
                    ""Database"": ""northwind"",
                    ""DefaultRefreshRate"": ""120""
                  }
            }";

            var dataSource = new MySQLDataSource()
            {
                Id = "mySqlId",
                Title = "MySQL DS",
                ProcessDataOnServerDefaultValue = true,
                ProcessDataOnServerReadOnly = false,
                Host = "mysqlserver.local",
                Port = 3306,
                Database = "northwind",
                DefaultRefreshRate = "120",
            };

            var dataSourceItems = new MySqlDataSourceItem("DB Test", dataSource)
            {
                Id = "mySqlItemId",
                Title = "MySQL DS Item",
                Fields = new List<IField>
                {
                    new TextField("_id"),
                    new TextField("name"),
                }
            };

            var document = new RdashDocument("My Dashboard");
            document.Visualizations.Add(new GridVisualization("Test List", dataSourceItems).SetColumns("name"));

            // Act
            RdashSerializer.SerializeObject(document);
            var json = document.ToJsonString();
            var jObject = JObject.Parse(json);
            var actualJObject = jObject["DataSources"].FirstOrDefault();
            var expectedJObject = JObject.Parse(expectedJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }
    }
}