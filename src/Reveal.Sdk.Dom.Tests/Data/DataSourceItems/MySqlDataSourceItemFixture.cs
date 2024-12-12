using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using System.IO;
using System;
using Xunit;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Core.Serialization;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class MySqlDataSourceItemFixture
    {
        [Theory]
        [InlineData("Test Item")]
        [InlineData(null)]
        public void Constructor_SetsTitleAndDataSource_WhenCalled(string title)
        {
            // Arrange
            var dataSource = new MySqlDataSource();

            // Act
            var item = new MySqlDataSourceItem(title, dataSource);

            // Assert
            Assert.Equal(title, item.Title);
            Assert.Equal(dataSource, item.DataSource);
        }


        [Fact]
        public void ProcessDataOnServer_SetsAndGetsServerAggregationValue_WithInputs()
        {
            // Arrange
            var item = new MySqlDataSourceItem("Test Item", new MySqlDataSource());

            // Act
            item.ProcessDataOnServer = true;

            // Assert
            Assert.True(item.ProcessDataOnServer);
            Assert.True(item.Properties.GetValue<bool>("ServerAggregation"));
        }

        [Fact]
        public void RDashDocument_HasCorrectDataSourceItem_WhenLoadFromFile()
        {
            // Arrange
            var filePath = Path.Combine(Environment.CurrentDirectory, "Dashboards", "TestMySQL.rdash");

            // Act
            var document = RdashDocument.Load(filePath);
            var dataSource = document.DataSources.FirstOrDefault();
            var dataSourceItem = document.Visualizations.FirstOrDefault().DataDefinition.DataSourceItem;

            // Assert
            Assert.Equal(dataSource.Id, dataSourceItem.DataSourceId);
            Assert.Equal(DataSourceProvider.MySQL, dataSource.Provider);
            Assert.NotNull(dataSourceItem.Properties.GetValue<string>("Table"));
            Assert.True(dataSourceItem.Properties.GetValue<bool>("ServerAggregation"));
        }

        [Fact]
        public void ToJsonString_CreatesFormattedJson_ForMySQLDataSource()
        {
            // Arrange
            var expectedJson =
                """
                {
                  "_type": "DataSourceItemType",
                  "Id": "mySqlItemId",
                  "Title": "MySQL DS Item",
                  "DataSourceId": "mySqlId",
                  "HasTabularData": true,
                  "HasAsset": false,
                  "Properties": {},
                  "Parameters": {}
                }
                """;

            var dataSource = new MySqlDataSource()
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
            var expectedJObject = JObject.Parse(expectedJson);

            // Act
            RdashSerializer.SerializeObject(document);
            var json = document.ToJsonString();
            var jObject = JObject.Parse(json);
            var actualJObject = jObject["Widgets"][0]["DataSpec"]["DataSourceItem"];

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }
    }
}