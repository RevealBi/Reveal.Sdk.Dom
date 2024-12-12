using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using System.Collections.Generic;
using System.IO;
using System;
using Xunit;
using Reveal.Sdk.Dom.Visualizations;

namespace Reveal.Sdk.Dom.Tests.Data.DataSources
{
    public class MongoDbDataSourceFixture
    {
        [Fact]
        public void Constructor_SetsProviderToMongoDB_WhenConstructed()
        {
            // Act
            var dataSource = new MongoDBDataSource();

            // Assert
            Assert.Equal(DataSourceProvider.MongoDB, dataSource.Provider);
        }

        [Theory]
        [InlineData("mongodb://localhost:27017")]
        [InlineData(null)]
        public void ConnectionString_SetsAndGetsValue_WithDifferentInputs(string connectionString)
        {
            // Arrange
            var dataSource = new MongoDBDataSource();

            // Act
            dataSource.ConnectionString = connectionString;

            // Assert
            Assert.Equal(connectionString, dataSource.ConnectionString);
            Assert.Equal(connectionString, dataSource.Properties.GetValue<string>("ConnectionString"));
        }

        [Theory]
        [InlineData("test")]
        [InlineData(null)]
        public void Database_SetsAndGetsValue_WithDifferentInputs(string dbName)
        {
            // Arrange
            var dataSource = new MongoDBDataSource();

            // Act
            dataSource.Database = dbName;

            // Assert
            Assert.Equal(dbName, dataSource.Database);
            Assert.Equal(dbName, dataSource.Properties.GetValue<string>("Database"));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ProcessDataOnServerDefaultValue_SetsAndGetsValue_WithDifferentInputs(bool defaultValue)
        {
            // Arrange
            var dataSource = new MongoDBDataSource();

            // Act
            dataSource.ProcessDataOnServerDefaultValue = defaultValue;

            // Assert
            Assert.Equal(defaultValue, dataSource.ProcessDataOnServerDefaultValue);
            Assert.Equal(defaultValue, dataSource.Properties.GetValue<bool>("ServerAggregationDefault"));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ProcessDataOnServerReadOnly_SetsAndGetsValue_WithDifferentInputs(bool readOnlyValue)
        {
            // Arrange
            var dataSource = new MongoDBDataSource();

            // Act
            dataSource.ProcessDataOnServerReadOnly = readOnlyValue;

            // Assert
            Assert.Equal(readOnlyValue, dataSource.ProcessDataOnServerReadOnly);
            Assert.Equal(readOnlyValue, dataSource.Properties.GetValue<bool>("ServerAggregationReadOnly"));
        }

        [Fact]
        public void RDashDocument_HasCorrectDataSource_WhenLoadFromFile()
        {
            // Arrange
            var filePath = Path.Combine(Environment.CurrentDirectory, "Dashboards", "TestMongoDb.rdash");

            // Act
            var document = RdashDocument.Load(filePath);
            var dataSource = document.DataSources[0];

            // Assert
            Assert.Equal(DataSourceProvider.MongoDB, dataSource.Provider);
            Assert.NotNull(dataSource.Properties.GetValue<string>("ServerAggregationDefault"));
            Assert.NotNull(dataSource.Properties.GetValue<string>("ServerAggregationReadOnly"));
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
                      "_type": "DataSourceType",
                      "Id": "MyMongoDatasource",
                      "Provider": "MONGODB",
                      "Description": "MyMongoDatasource",
                      "Subtitle": "My MongoDB",
                      "Properties": {
                        "ServerAggregationDefault": true,
                        "ServerAggregationReadOnly": false
                      },
                      "Settings": {}
                }
                """;

            // Act
            var jsonData = document.ToJsonString();
            var jObject = JObject.Parse(jsonData);
            var datasourceJson = jObject["DataSources"][0];

            // Deserialize JSON strings to JObjects to make comparing them easier
           var expectedJObject = JObject.Parse(expectedDsJson);

            // Assert
            Assert.Equal(expectedJObject, datasourceJson);
        }
    }
}