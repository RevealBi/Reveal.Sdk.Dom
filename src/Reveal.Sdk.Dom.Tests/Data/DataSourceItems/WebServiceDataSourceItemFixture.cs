using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using Xunit;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Core.Serialization;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class WebServiceDataSourceItemFixture
    {
        [Fact]
        public void Constructor_SetsTitleAndDataSource_WhenConstructed()
        {
            // Arrange
            string title = "Test Item";
            var dataSource = new WebServiceDataSource();
            var item = new WebServiceDataSourceItem(title, dataSource);

            // Assert
            Assert.Equal(title, item.Title);
            Assert.Equal(dataSource, item.DataSource);
        }

        [Theory]
        [InlineData("https://example.com/api")]
        [InlineData(null)]
        public void Url_SetsAndGetsValue_WithDifferentInputs(string url)
        {
            // Arrange
            var item = new WebServiceDataSourceItem("Test Item", new WebServiceDataSource());

            // Act
            item.Url = url;

            // Assert
            Assert.Equal(url, item.Url);
            Assert.Equal(url, item.Properties.GetValue<string>("Url"));
        }

        [Fact]
        public void RDashDocument_HasCorrectDataSource_WhenLoadFromFile()
        {
            // Arrange
            var filePath = Path.Combine(Environment.CurrentDirectory, "Dashboards", "TestWebResource.rdash");

            // Act
            var document = RdashDocument.Load(filePath);
            var jsonDataSource = document.DataSources.FirstOrDefault();
            var webServiceDataSource = document.DataSources.LastOrDefault();
            var webServiceDataSourceItem = document.Visualizations.FirstOrDefault().DataDefinition.DataSourceItem;

            // Assert
            Assert.Equal(DataSourceProvider.JSON, jsonDataSource.Provider);
            Assert.Equal(DataSourceProvider.WebService, webServiceDataSource.Provider);
            Assert.NotNull(webServiceDataSource.Properties.GetValue<string>("Url"));
            Assert.NotNull(webServiceDataSource.Properties.GetValue<string>("_rpUseAnonymousAuthentication"));
        }

        [Fact]
        public void ToJsonString_CreatesFormattedJson_ForWebServiceDataSource()
        {
            // Arrange
            var expectedJson = @"
            {
              ""_type"": ""DataSourceItemType"",
              ""Id"": ""webServiceItemId"",
              ""Title"": ""Sales by Category"",
              ""Subtitle"": ""Excel2Json"",
              ""DataSourceId"": ""webServiceId"",
              ""HasTabularData"": true,
              ""HasAsset"": false,
              ""Properties"": {},
              ""Parameters"": {}
            }";

            var dataSource = new WebServiceDataSource()
            {
                Id = "webServiceId",
                Title = "Web Data Source",
                Subtitle = "Web Data Source Subtitle",
                Url = "https://excel2json.io/api/share/6e0f06b3-72d3-4fec-7984-08da43f56bb9",
                UseAnonymousAuthentication = true,
            };

            var dataSourceItems = new WebServiceDataSourceItem("DB Test", dataSource)
            {
                Id = "webServiceItemId",
                Title = "Sales by Category",
                Subtitle = "Excel2Json",
                Fields = new List<IField>
                {
                    new NumberField("CategoryID"),
                    new TextField("CategoryName"),
                    new TextField("ProductName"),
                    new NumberField("ProductSales"),
                }
            };

            var document = new RdashDocument("My Dashboard");
            document.Visualizations.Add(
                new ColumnChartVisualization("Test List", dataSourceItems)
                    .SetLabel("CategoryName")
                    .SetValue("ProductName")
                    );

            var expectedJObject = JObject.Parse(expectedJson);

            // Act
            RdashSerializer.SerializeObject(document);
            var json = document.ToJsonString();
            var jObject = JObject.Parse(json);
            var actualJObject = jObject["Widgets"].FirstOrDefault()["DataSpec"]["DataSourceItem"];

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }
    }
}