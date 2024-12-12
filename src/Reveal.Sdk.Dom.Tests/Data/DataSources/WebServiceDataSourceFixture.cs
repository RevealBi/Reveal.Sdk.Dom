using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Core.Serialization;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            var expectedJson =
                """
                [
                  {
                    "_type": "DataSourceType",
                    "Id": "__JSON",
                    "Provider": "JSON",
                    "Description": "DB Test",
                    "Properties": {},
                    "Settings": {}
                  },
                  {
                    "_type": "DataSourceType",
                    "Id": "9d2c1bbe-60f7-4708-bb2e-14611e64a2a4",
                    "Provider": "WEBSERVICE",
                    "Description": "JSON DS",
                    "Subtitle": "JSON DS Subtitle",
                    "Properties": {
                      "Url": "https://excel2json.io/api/share/6e0f06b3-72d3-4fec-7984-08da43f56bb9"
                    },
                    "Settings": {}
                  }
                ]
                """;

            var dataSourceItems = new WebServiceDataSourceItem("DB Test", new DataSource { Title = "JSON DS", Subtitle = "JSON DS Subtitle" })
            {
                Id = "webServiceItemId",
                Title = "Sales by Category",
                Subtitle = "Excel2Json",
                Url = "https://excel2json.io/api/share/6e0f06b3-72d3-4fec-7984-08da43f56bb9",
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

            var expectedJArray = JArray.Parse(expectedJson);

            // Act
            var json = document.ToJsonString();
            var jObject = JObject.Parse(json);
            var actualJArray = (JArray)jObject["DataSources"];

            // Assert
            Assert.Equal(expectedJArray.Count, actualJArray.Count);

            for (int i = 0; i < expectedJArray.Count; i++)
            {
                Assert.Equal(expectedJArray[i], actualJArray[i]);
            }
        }

        [Fact]
        public void ToJsonString_CreatesFormattedJsonUseCsv_ForWebServiceDataSource()
        {
            // Arrange
            var expectedJson =
                """
                [
                  {
                    "_type": "DataSourceType",
                    "Id": "__CSV",
                    "Provider": "CSVLOCALFILEPROVIDER",
                    "Properties": {},
                    "Settings": {}
                  },
                  {
                    "_type": "DataSourceType",
                    "Id": "f197cb8d-9bb3-4949-917d-0bd3225ae890",
                    "Provider": "WEBSERVICE",
                    "Description": "JSON DS",
                    "Subtitle": "JSON DS Subtitle",
                    "Properties": {
                      "Url": "https://query.data.world/s/y32gtgblzpemyyvtig47dz7tedgkto",
                      "Result-Type": ".csv"
                    },
                    "Settings": {}
                  }
                ]
                """;

            var dataSourceItems = new WebServiceDataSourceItem("DB Test", new DataSource { Title = "JSON DS", Subtitle = "JSON DS Subtitle" })
            {
                Id = "webServiceItemId",
                Title = "Sales by Category",
                Subtitle = "Excel2Json",
                Url = "https://query.data.world/s/y32gtgblzpemyyvtig47dz7tedgkto",
                Fields = new List<IField>
                {
                    new NumberField("CategoryID"),
                    new TextField("CategoryName"),
                    new TextField("ProductName"),
                    new NumberField("ProductSales"),
                }
            };

            dataSourceItems.UseCsv();

            var document = new RdashDocument("My Dashboard");
            document.Visualizations.Add(
                new ColumnChartVisualization("Test List", dataSourceItems)
                    .SetLabel("CategoryName")
                    .SetValue("ProductName")
                    );

            var expectedJArray = JArray.Parse(expectedJson);

            // Act
            var json = document.ToJsonString();
            var jObject = JObject.Parse(json);
            var actualJArray = (JArray)jObject["DataSources"];

            // Assert
            Assert.Equal(expectedJArray.Count, actualJArray.Count);

            for (int i = 0; i < expectedJArray.Count; i++)
            {
                Assert.Equal(expectedJArray[i], actualJArray[i]);
            }
        }

        [Fact]
        public void ToJsonString_CreatesFormattedJsonUseExcel_ForWebServiceDataSource()
        {
            // Arrange
            var expectedJson =
                """
                [
                  {
                    "_type": "DataSourceType",
                    "Id": "__EXCEL",
                    "Provider": "EXCELLOCALFILEPROVIDER",
                    "Properties": {},
                    "Settings": {}
                  },
                  {
                    "_type": "DataSourceType",
                    "Id": "3d19e7c5-9eae-427e-bef5-2634f397bc9b",
                    "Provider": "WEBSERVICE",
                    "Description": "JSON DS",
                    "Subtitle": "JSON DS Subtitle",
                    "Properties": {
                      "Url": "http://dl.infragistics.com/reportplus/reveal/samples/Samples.xlsx",
                      "Result-Type": ".xlsx"
                    },
                    "Settings": {}
                  }
                ]
                """;

            var dataSourceItems = new WebServiceDataSourceItem("DB Test", new DataSource { Title = "JSON DS", Subtitle = "JSON DS Subtitle" })
            {
                Id = "webServiceItemId",
                Title = "Sales by Category",
                Subtitle = "Excel2Json",
                Url = "http://dl.infragistics.com/reportplus/reveal/samples/Samples.xlsx",
                Fields = new List<IField>
                {
                    new NumberField("CategoryID"),
                    new TextField("CategoryName"),
                    new TextField("ProductName"),
                    new NumberField("ProductSales"),
                }
            };

            dataSourceItems.UseExcel();

            var document = new RdashDocument("My Dashboard");
            document.Visualizations.Add(
                new ColumnChartVisualization("Test List", dataSourceItems)
                    .SetLabel("CategoryName")
                    .SetValue("ProductName")
                    );

            var expectedJArray = JArray.Parse(expectedJson);

            // Act
            var json = document.ToJsonString();
            var jObject = JObject.Parse(json);
            var actualJArray = (JArray)jObject["DataSources"];

            // Assert
            Assert.Equal(expectedJArray.Count, actualJArray.Count);

            for (int i = 0; i < expectedJArray.Count; i++)
            {
                Assert.Equal(expectedJArray[i], actualJArray[i]);
            }
        }
    }
}