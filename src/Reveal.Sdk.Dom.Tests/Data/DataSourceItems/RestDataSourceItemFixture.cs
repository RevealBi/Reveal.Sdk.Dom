using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Core.Serialization;
using Reveal.Sdk.Dom.Core.Utilities;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class RestDataSourceItemFixture
    {
        [Fact]
        public void IsAnonymous_Should_Set_IsAnonymous_Property()
        {
            // Arrange
            var restDataSourceItem = new RestDataSourceItem("Test");

            // Act
            restDataSourceItem.IsAnonymous = true;

            // Assert
            Assert.True(restDataSourceItem.IsAnonymous);
        }

        [Fact]
        public void HasTabularData_Should_Be_True()
        {
            // Arrange
            var restDataSourceItem = new RestDataSourceItem("Test");

            // Assert
            Assert.True(restDataSourceItem.HasTabularData);
        }

        [Fact]
        public void HasAsset_Should_Be_False()
        {
            // Arrange
            var restDataSourceItem = new RestDataSourceItem("Test");

            // Assert
            Assert.False(restDataSourceItem.HasAsset);
        }

        [Fact]
        public void Id_Should_Set_Id_And_ResourceItem_Id_Property()
        {
            // Arrange
            var restDataSourceItem = new RestDataSourceItem("Test");
            var id = "Test Id";

            // Act
            restDataSourceItem.Id = id;

            // Assert
            Assert.Equal(id, restDataSourceItem.Id);
            Assert.Equal(id, restDataSourceItem.ResourceItem.Id);
        }

        [Fact]
        public void Title_Should_Set_Title_Property()
        {
            // Arrange
            var restDataSourceItem = new RestDataSourceItem("Test");
            var title = "Test Title";

            // Act
            restDataSourceItem.Title = title;

            // Assert
            Assert.Equal(title, restDataSourceItem.Title);
        }

        [Fact]
        public void Subtitle_Should_Set_Subtitle_And_ResourceItem_Subtitle_Property()
        {
            // Arrange
            var restDataSourceItem = new RestDataSourceItem("Test");
            var subtitle = "Test Subtitle";

            // Act
            restDataSourceItem.Subtitle = subtitle;

            // Assert
            Assert.Equal(subtitle, restDataSourceItem.Subtitle);
            Assert.Equal(subtitle, restDataSourceItem.ResourceItem.Subtitle);
        }

        [Fact]
        public void Uri_Should_Set_Uri_Property()
        {
            // Arrange
            var restDataSourceItem = new RestDataSourceItem("Test");
            var uri = "https://example.com/api/data";

            // Act
            restDataSourceItem.Uri = uri;

            // Assert
            Assert.Equal(uri, restDataSourceItem.Uri);
        }

        [Fact]
        public void UseCsv_Should_Set_Id_And_Provider_And_ResultType_Properties()
        {
            // Arrange
            var restDataSourceItem = new RestDataSourceItem("Test");

            // Act
            restDataSourceItem.UseCsv();

            // Assert
            Assert.Equal(DataSourceIds.CSV, restDataSourceItem.DataSource.Id);
            Assert.Equal(DataSourceIds.CSV, restDataSourceItem.DataSourceId);
            Assert.Equal(DataSourceProvider.CSV, restDataSourceItem.DataSource.Provider);
            Assert.Equal(".csv", restDataSourceItem.ResourceItemDataSource.Properties.GetValue<string>("Result-Type"));
        }

        [Fact]
        public void UseCsv_Clears_Config()
        {
            // Arrange
            var restDataSourceItem = new RestDataSourceItem("Test")
            {
                Fields = new List<IField>
                {
                    new NumberField { FieldName = "Field1" },
                    new DateField { FieldName = "Field2" },
                    new DateTimeField { FieldName = "Field3" },
                    new TimeField { FieldName = "Field4" },
                    new TextField { FieldName = "Field5" }
                }
            };

            // Act
            restDataSourceItem.UseCsv();

            // Assert
            Assert.False(restDataSourceItem.Parameters.ContainsKey("config"));
        }

        [Fact]
        public void UseExcel_Should_Set_Id_And_Provider_And_ResultType_Properties()
        {
            // Arrange
            var restDataSourceItem = new RestDataSourceItem("Test");
            var sheet = "Sheet1";
            var fileType = ExcelFileType.Xlsx;

            // Act
            restDataSourceItem.UseExcel(sheet, fileType);

            // Assert
            Assert.Equal(DataSourceIds.Excel, restDataSourceItem.DataSource.Id);
            Assert.Equal(DataSourceIds.Excel, restDataSourceItem.DataSourceId);
            Assert.Equal(DataSourceProvider.MicrosoftExcel, restDataSourceItem.DataSource.Provider);
            Assert.Equal(fileType == ExcelFileType.Xlsx ? ".xlsx" : ".xls", restDataSourceItem.ResourceItemDataSource.Properties.GetValue<string>("Result-Type"));
            Assert.Equal(sheet, restDataSourceItem.Properties.GetValue<string>("Sheet"));
        }

        [Fact]
        public void UseExcel_Clears_Config()
        {
            // Arrange
            var restDataSourceItem = new RestDataSourceItem("Test")
            {
                Fields = new List<IField>
                {
                    new NumberField { FieldName = "Field1" },
                    new DateField { FieldName = "Field2" },
                    new DateTimeField { FieldName = "Field3" },
                    new TimeField { FieldName = "Field4" },
                    new TextField { FieldName = "Field5" }
                }
            };

            // Act
            restDataSourceItem.UseExcel();

            // Assert
            Assert.False(restDataSourceItem.Parameters.ContainsKey("config"));
        }

        [Fact]
        public void InitializeDataSource_Should_Set_Provider_And_DataSourceId_Properties()
        {
            // Arrange
            var restDataSourceItem = new RestDataSourceItem("Test");

            // Assert
            Assert.Equal(DataSourceProvider.JSON, restDataSourceItem.DataSource.Provider);
            Assert.Equal(DataSourceIds.JSON, restDataSourceItem.DataSource.Id);
        }

        [Fact]
        public void FieldsPropertyChanged_Should_Add_Config_Parameter()
        {
            // Arrange
            var restDataSourceItem = new RestDataSourceItem("Test");
            var fields = new List<IField>
            {
                new NumberField { FieldName = "Field1" },
                new DateField { FieldName = "Field2" },
                new DateTimeField { FieldName = "Field3" },
                new TimeField { FieldName = "Field4" },
                new TextField { FieldName = "Field5" }
            };

            // Act
            restDataSourceItem.Fields = fields;

            // Assert
            Assert.True(restDataSourceItem.Parameters.ContainsKey("config"));
            var config = restDataSourceItem.Parameters["config"] as Dictionary<string, object>;
            Assert.NotNull(config);
            Assert.Equal(0, config["iterationDepth"]);
            var columnConfigs = config["columnsConfig"] as List<ColumnConfig>;
            Assert.NotNull(columnConfigs);
            Assert.Equal(5, columnConfigs.Count);
            Assert.Equal("Field1", columnConfigs[0].Key);
            Assert.Equal(1, columnConfigs[0].Type);
            Assert.Equal("Field2", columnConfigs[1].Key);
            Assert.Equal(2, columnConfigs[1].Type);
            Assert.Equal("Field3", columnConfigs[2].Key);
            Assert.Equal(3, columnConfigs[2].Type);
            Assert.Equal("Field4", columnConfigs[3].Key);
            Assert.Equal(4, columnConfigs[3].Type);
        }

        [Fact]
        public void AddHeader_Should_AddHeaderToList_When_PropertyKeyDoesNotExist()
        {
            // Arrange
            var dataSourceItem = new RestDataSourceItem("Test");
            var headerType = HeaderType.Authorization;
            var value = "Bearer token";

            // Act
            dataSourceItem.AddHeader(headerType, value);

            // Assert
            Assert.Single((IEnumerable)dataSourceItem.ResourceItemDataSource.Properties["Headers"]);
            Assert.Equal("Authorization=Bearer token", ((IEnumerable)dataSourceItem.ResourceItemDataSource.Properties["Headers"]).Cast<string>().First());
        }

        [Fact]
        public void AddHeader_Should_AddHeaderToList_When_PropertyKeyExists()
        {
            // Arrange
            var dataSourceItem = new RestDataSourceItem("Test");
            var headerType = HeaderType.Authorization;
            var value1 = "Bearer token1";
            var value2 = "Bearer token2";

            // Act
            dataSourceItem.AddHeader(headerType, value1);
            dataSourceItem.AddHeader(headerType, value2);

            // Assert
            var headers = dataSourceItem.ResourceItemDataSource.Properties["Headers"] as List<string>;
            Assert.NotNull(headers);
            Assert.Equal(2, headers.Count);
            Assert.Equal("Authorization=Bearer token1", headers[0]);
            Assert.Equal("Authorization=Bearer token2", headers[1]);
        }

        [Fact]
        public void AddHeader_ShouldAddHeaderWithCorrectEnumName()
        {
            // Arrange
            var dataSourceItem = new RestDataSourceItem("Test");
            var headerType = HeaderType.UserAgent;
            var value = "token";

            // Act
            dataSourceItem.AddHeader(headerType, value);

            // Assert
            var headers = dataSourceItem.ResourceItemDataSource.Properties["Headers"] as List<string>;
            Assert.NotNull(headers);
            Assert.Single(headers);
            Assert.Equal("User-Agent=token", headers[0]);
        }

        [Fact]
        public void DataSource_Properties_Should_Set_ResourceItemDataSource_Properties()
        {
            // Arrange
            var dataSource = new DataSource() { Id = "DS-ID", Title = "DS-TITLE", Subtitle = "DS-SUBTITLE" };
            var dataSourceItem = new RestDataSourceItem("Test", dataSource);

            // Assert
            Assert.Equal(dataSource.Id, dataSourceItem.ResourceItemDataSource.Id);
            Assert.Equal(dataSource.Title, dataSourceItem.ResourceItemDataSource.Title);
            Assert.Equal(dataSource.Subtitle, dataSourceItem.ResourceItemDataSource.Subtitle);
            Assert.NotEqual(dataSource.Id, dataSourceItem.DataSource.Id);
            Assert.NotEqual(dataSource.Title, dataSourceItem.DataSource.Title);
            Assert.NotEqual(dataSource.Subtitle, dataSourceItem.DataSource.Subtitle);
        }

        [Fact]
        public void DataSource_No_Title_Or_Subtitle_Should_Use_ResourceItem_Title_Or_Subtitle()
        {
            // Arrange
            var dataSource = new DataSource();
            var dataSourceItem = new RestDataSourceItem("Test", dataSource);

            // Assert
            Assert.Equal(dataSourceItem.ResourceItemDataSource.Title, dataSourceItem.ResourceItem.Title);
            Assert.Equal(dataSourceItem.ResourceItemDataSource.Subtitle, dataSourceItem.ResourceItem.Subtitle);
        }

        [Fact]
        public void Constructor_WithTitle_Should_Add_TwoDataSources()
        {
            // Arrange
            var document = new RdashDocument("Test");
            var dataSourceItem = new RestDataSourceItem("Test").SetFields(new List<IField>() { new TextField("Test") });

            document.Visualizations.Add(new GridVisualization(dataSourceItem));

            // Ensure data sources are added to the data sources collection
            document.Validate();

            // Assert
            Assert.Equal(2, document.DataSources.Count);
            Assert.Equal(DataSourceIds.JSON, document.DataSources[0].Id);
            Assert.Equal(DataSourceProvider.JSON, document.DataSources[0].Provider);
        }

        [Fact]
        public void Constructor_WithTitleAndUri_Should_Add_TwoDataSources()
        {
            // Arrange
            var document = new RdashDocument("Test");
            var dataSourceItem = new RestDataSourceItem("TITLE", "URI").SetFields(new List<IField>() { new TextField("Test") });

            document.Visualizations.Add(new GridVisualization(dataSourceItem));

            // Ensure data sources are added to the data sources collection
            document.Validate();

            // Assert
            Assert.Equal(2, document.DataSources.Count);
            Assert.Equal(DataSourceIds.JSON, document.DataSources[0].Id);
            Assert.Equal(DataSourceProvider.JSON, document.DataSources[0].Provider);
        }

        [Fact]
        public void Constructor_WithNullDataSource_Should_Add_TwoDataSourcese()
        {
            // Arrange
            var document = new RdashDocument("Test");
            var dataSourceItem = new RestDataSourceItem("Test", "URI", null).SetFields(new List<IField>() { new TextField("Test") });

            document.Visualizations.Add(new GridVisualization(dataSourceItem));

            // Ensure data sources are added to the data sources collection
            document.Validate();

            // Assert
            Assert.Equal(2, document.DataSources.Count);
            Assert.Equal(DataSourceIds.JSON, document.DataSources[0].Id);
            Assert.Equal(DataSourceProvider.JSON, document.DataSources[0].Provider);
        }

        [Fact]
        public void UseExcel_Should_Add_TwoDataSources()
        {
            // Arrange
            var document = new RdashDocument("Test");
            var dataSourceItem = new RestDataSourceItem("Test").SetFields(new List<IField>() { new TextField("Test") });
            dataSourceItem.UseExcel();

            document.Visualizations.Add(new GridVisualization(dataSourceItem));

            // Ensure data sources are added to the data sources collection
            document.Validate();

            // Assert
            Assert.Equal(2, document.DataSources.Count);
            Assert.Equal(DataSourceIds.Excel, document.DataSources[0].Id);
            Assert.Equal(DataSourceProvider.MicrosoftExcel, document.DataSources[0].Provider);
        }

        [Fact]
        public void UseCsv_Should_Add_TwoDataSources()
        {
            //Arrange
            var document = new RdashDocument("Test");
            var dataSourceItem = new RestDataSourceItem("Test").SetFields(new List<IField>() { new TextField("Test") });
            dataSourceItem.UseCsv();

            document.Visualizations.Add(new GridVisualization(dataSourceItem));

            // Ensure data sources are added to the data sources collection
            document.Validate();

            // Assert
            Assert.Equal(2, document.DataSources.Count);
            Assert.Equal(DataSourceIds.CSV, document.DataSources[0].Id);
            Assert.Equal(DataSourceProvider.CSV, document.DataSources[0].Provider);
        }

        [Fact]
        public void RDashDocument_HasCorrectDataSourceItem_WhenLoadFromFile()
        {
            // Arrange
            var filePath = Path.Combine(Environment.CurrentDirectory, "Dashboards", "TestRest.rdash");

            // Act
            var document = RdashDocument.Load(filePath);
            var dataSource = document.DataSources.LastOrDefault();
            var dataSourceItem = document.Visualizations.LastOrDefault().DataDefinition.DataSourceItem;

            // Assert
            Assert.Equal(DataSourceProvider.REST, dataSource.Provider);
            Assert.False(dataSourceItem.Properties.GetValue<bool>("ServerAggregation"));
        }

        [Fact]
        public void ToJsonString_CreatesFormattedJson_ForWebServiceDataSource()
        {
            // Arrange
            var expectedJson = @"
            {
              ""_type"": ""DataSourceItemType"",
              ""Id"": ""RestItem"",
              ""Title"": ""Rest DS Item"",
              ""DataSourceId"": ""__JSON"",
              ""HasTabularData"": true,
              ""HasAsset"": false,
              ""Properties"": {},
              ""Parameters"": {
                ""config"": {
                  ""iterationDepth"": 0,
                  ""columnsConfig"": [
                    {
                      ""key"": ""_id"",
                      ""type"": 0
                    },
                    {
                      ""key"": ""name"",
                      ""type"": 0
                    }
                  ]
                }
              },
              ""ResourceItem"": {
                ""_type"": ""DataSourceItemType"",
                ""Id"": ""RestItem"",
                ""Title"": ""DB Test"",
                ""DataSourceId"": ""Rest"",
                ""HasTabularData"": true,
                ""HasAsset"": false,
                ""Properties"": {},
                ""Parameters"": {}
              }
            }";

            var dataSource = new RestDataSource()
            {
                Id = "Rest",
                Title = "Rest DS",
                DefaultRefreshRate = "120",
                Url = "https://excel2json.io/api/share/6e0f06b3-72d3-4fec-7984-08da43f56bb9",
                Subtitle = "Excel2Json"
            };

            var dataSourceItems = new RestDataSourceItem("DB Test", dataSource)
            {
                Id = "RestItem",
                Title = "Rest DS Item",
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
            var actualJObject = jObject["Widgets"].FirstOrDefault()["DataSpec"]["DataSourceItem"];

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }
    }
}
