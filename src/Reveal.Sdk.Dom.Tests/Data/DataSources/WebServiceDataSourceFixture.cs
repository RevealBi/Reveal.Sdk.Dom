using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Core.Serialization;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSources
{
    public class WebServiceDataSourceFixture
    {
        [Fact]
        public void Constructor_SetsProviderToWebService_WhenConstructed()
        {
            // Act
            var dataSource = new WebServiceDataSource();

            // Assert
            Assert.Equal(DataSourceProvider.WebService, dataSource.Provider);
        }

        [Theory]
        [InlineData("https://example.com/api")]
        [InlineData(null)]
        public void Url_SetsAndGetsValue_WithDifferentInputs(string url)
        {
            // Arrange
            var dataSource = new WebServiceDataSource();

            // Act
            dataSource.Url = url;

            // Assert
            Assert.Equal(url, dataSource.Url);
            Assert.Equal(url, dataSource.Properties.GetValue<string>("URL"));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void UseAnonymousAuthentication_SetsAndGetsValue_WithDifferentInputs(bool value)
        {
            // Arrange
            var dataSource = new WebServiceDataSource();

            // Act
            dataSource.UseAnonymousAuthentication = value;

            // Assert
            Assert.Equal(value, dataSource.UseAnonymousAuthentication);
            Assert.Equal(value, dataSource.Properties.GetValue<bool>("UseAnonymousAuthentication"));
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
              ""_type"": ""DataSourceType"",
              ""Id"": ""webServiceId"",
              ""Provider"": ""WEBSERVICE"",
              ""Description"": ""Web Data Source"",
              ""Subtitle"": ""Web Data Source Subtitle"",
              ""Properties"": {
                ""URL"": ""https://excel2json.io/api/share/6e0f06b3-72d3-4fec-7984-08da43f56bb9"",
                ""UseAnonymousAuthentication"": true
              }
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
            var actualJObject = jObject["DataSources"].FirstOrDefault();

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }
    }
}