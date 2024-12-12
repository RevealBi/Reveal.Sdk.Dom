using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using System.IO;
using System;
using Xunit;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Reveal.Sdk.Dom.Visualizations;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class MongoDbDataSourceItemFixture
    {
        [Theory]
        [InlineData("Test Item")]
        [InlineData(null)]
        public void Constructor_SetsTitleAndDataSource_WhenCalled(string title)
        {
            // Arrange
            var dataSource = new MongoDBDataSource();

            // Act
            var item = new MongoDbDataSourceItem(title, dataSource);

            // Assert
            Assert.Equal(title, item.Title);
            Assert.Equal(dataSource, item.DataSource);
        }

        [Fact]
        public void Collection_SetsAndGetsValue_WithInputs()
        {
            // Arrange
            var item = new MongoDbDataSourceItem("Test Item", new MongoDBDataSource());

            // Act
            item.Collection = "TestCollection";

            // Assert
            Assert.Equal("TestCollection", item.Collection);
            Assert.Equal("TestCollection", item.Properties.GetValue<string>("Table"));
        }

        [Fact]
        public void ProcessDataOnServer_SetsAndGetsValue_WithInputs()
        {
            // Arrange
            var item = new MongoDbDataSourceItem("Test Item", new MongoDBDataSource());

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
            var filePath = Path.Combine(Environment.CurrentDirectory, "Dashboards", "TestMongoDb.rdash");

            // Act
            var document = RdashDocument.Load(filePath);
            var dataSource = document.DataSources[0];
            var dataSourceItem = document.Visualizations[0].DataDefinition.DataSourceItem;

            // Assert
            Assert.Equal(dataSource.Id, dataSourceItem.DataSourceId);
            Assert.Equal(DataSourceProvider.MongoDB, dataSource.Provider);
            Assert.NotNull(dataSourceItem.Properties.GetValue<string>("Table"));
            Assert.True(dataSourceItem.Properties.GetValue<bool>("ServerAggregation"));
        }

        [Fact]
        public void RDashDocument_ProduceAsExpected_WhenExportJson()
        {
            // Arrange
            var mongoDbDs = new MongoDBDataSource()
            {
                Id = "MyMongoDatasource",
                Title = "MyMongoDatasource",
                Subtitle = "My MongoDB",
                ProcessDataOnServerDefaultValue = true,
                ProcessDataOnServerReadOnly = false,
            };

            var testCollection = new MongoDbDataSourceItem("DB Test", mongoDbDs)
            {
                Id = "MyMongoDatasourceItem",
                Title = "MyMongoDatasourceItem",
                Subtitle = "Test Collection",
                Collection = "data",
                Fields = new List<IField>
                {
                    new TextField("_id"),
                    new TextField("name"),
                }
            };

            var document = new RdashDocument("My Dashboard");

            document.Visualizations.Add(new GridVisualization("Test List", testCollection).SetColumns("name"));

            var expectedDsJson = """
                {
                  "_type": "DataSourceItemType",
                  "Id": "MyMongoDatasourceItem",
                  "Title": "MyMongoDatasourceItem",
                  "Subtitle": "Test Collection",
                  "DataSourceId": "MyMongoDatasource",
                  "HasTabularData": true,
                  "HasAsset": false,
                  "Properties": {
                    "Collection": "data"
                  },
                  "Parameters": {}
                }
                """;

            // Act
            var jsonData = document.ToJsonString();
            var jObject = JObject.Parse(jsonData);
            var datasourceJson = jObject["Widgets"][0]["DataSpec"]["DataSourceItem"];

            // Deserialize JSON strings to JObjects to make comparing them easier
            var expectedJObject = JObject.Parse(expectedDsJson);

            // Assert
            Assert.Equal(expectedJObject, datasourceJson);
        }
    }
}