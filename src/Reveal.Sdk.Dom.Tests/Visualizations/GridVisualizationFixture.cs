using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Serialization;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Filters;
using Reveal.Sdk.Dom.Tests.TestHelpers;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations
{

    public class GridVisualizationFixture
    {
        [Fact]
        public void Constructor_InitializesDefaultValues_WhenInstanceIsCreated()
        {
            // Act
            var gridVisualization = new GridVisualization();

            // Assert
            Assert.NotNull(gridVisualization);
            Assert.Equal(ChartType.Grid, gridVisualization.ChartType);
            Assert.NotNull(gridVisualization.Columns);
            Assert.Empty(gridVisualization.Columns);
        }

        [Fact]
        public void Constructor_InitializesGridVisualizationWithDataSource_WhenDataSourceItemIsProvided()
        {
            // Arrange
            var dataSourceItem = new DataSourceItem { HasTabularData = true };

            // Act
            var gridVisualization = new GridVisualization(dataSourceItem);

            // Assert
            Assert.NotNull(gridVisualization);
            Assert.Equal(ChartType.Grid, gridVisualization.ChartType);
            Assert.Equal(dataSourceItem, gridVisualization.DataDefinition.DataSourceItem);
            Assert.NotNull(gridVisualization.Columns);
            Assert.Empty(gridVisualization.Columns);
        }


        [Theory]
        [InlineData("TestTitle", null)]
        [InlineData(null, null)]
        public void Constructor_SetsTitleAndDataSource_WhenArgumentsAreProvided(string title, DataSourceItem dataSourceItem)
        {
            // Act
            var gridVisualization = new GridVisualization(title, dataSourceItem);

            // Assert
            Assert.Equal(title, gridVisualization.Title);
            Assert.Equal(ChartType.Grid, gridVisualization.ChartType);
            Assert.NotNull(gridVisualization.Columns);
            Assert.Empty(gridVisualization.Columns);
        }

        [Theory]
        [InlineData(null, 0)]
        [InlineData(typeof(GridVisualizationDataSpec), 2)]
        public void Columns_ReturnsExpectedCount_WhenVisualizationDataSpecIsSet(System.Type dataSpecType, int expectedCount)
        {
            // Arrange
            var gridVisualization = new GridVisualization();
            if (dataSpecType == typeof(GridVisualizationDataSpec))
            {
                var gridVisualizationDataSpec = new GridVisualizationDataSpec
                {
                    Columns = new List<TabularColumn>
                {
                    new TabularColumn { FieldName = "Column1" },
                    new TabularColumn { FieldName = "Column2" }
                }
                };

                var property = typeof(GridVisualization).GetProperty("VisualizationDataSpec", BindingFlags.NonPublic | BindingFlags.Instance);
                property.SetValue(gridVisualization, gridVisualizationDataSpec);
            }

            // Act
            var columns = gridVisualization.Columns;

            // Assert
            Assert.NotNull(columns);
            Assert.Equal(expectedCount, columns.Count);
        }

        [Fact]
        public void VisualizationDataSpec_DefaultValue_IsGridVisualizationDataSpec()
        {
            // Arrange
            var gridVisualization = new GridVisualization();

            var property = typeof(GridVisualization).GetProperty("VisualizationDataSpec", BindingFlags.NonPublic | BindingFlags.Instance);
            var visualizationDataSpec = property.GetValue(gridVisualization);

            // Assert
            Assert.NotNull(visualizationDataSpec);
            Assert.IsType<GridVisualizationDataSpec>(visualizationDataSpec);
        }

        [Fact]
        public void UpdateDataSourceItem_UpdatesDataDefinition_WhenNewDataSourceIsProvided()
        {
            // Arrange
            var dataSourceItem = new DataSourceItem { HasTabularData = true };
            var gridVisualization = new GridVisualization("Test", dataSourceItem);

            var newDataSourceItem = new DataSourceItem();

            // Act
            gridVisualization.UpdateDataSourceItem(newDataSourceItem);

            // Assert
            Assert.Equal(newDataSourceItem, gridVisualization.DataDefinition.DataSourceItem);
        }

        [Fact]
        public void VisualizationDataSpec_DefaultValue_CanBeSetAndRetrieved()
        {
            // Arrange
            var customVisualizationDataSpec = new GridVisualizationDataSpec
            {
                Columns = new List<TabularColumn>
            {
                new TabularColumn { FieldName = "CustomColumn" }
            }
            };

            var gridVisualization = new GridVisualization();

            var property = typeof(GridVisualization).GetProperty("VisualizationDataSpec", BindingFlags.NonPublic | BindingFlags.Instance);
            property.SetValue(gridVisualization, customVisualizationDataSpec);

            // Act
            var retrievedVisualizationDataSpec = property.GetValue(gridVisualization);

            // Assert
            Assert.Equal(customVisualizationDataSpec, retrievedVisualizationDataSpec);
        }

        [Fact]
        public void ToJsonString_GeneratesCorrectJson_WhenGridVisualizationIsSerialized()
        {
            var expectedJson = """
            [
              {
                "Description": "Create Grid Visualization",
                "Title": "Grid",
                "IsTitleVisible": true,
                "ColumnSpan": 0,
                "RowSpan": 0,
                "VisualizationSettings": {
                  "_type": "GridVisualizationSettingsType",
                  "PagedRows": true,
                  "PagedRowsSize": 50,
                  "FontSize": "Large",
                  "Style": {
                    "FixedLeftColumns": true,
                    "TextAlignment": "Center",
                    "NumericAlignment": "Inherit",
                    "DateAlignment": "Center"
                  },
                  "VisualizationType": "GRID"
                },
                "DataSpec": {
                  "_type": "TabularDataSpecType",
                  "IsTransposed": false,
                  "Fields": [
                    {
                      "FieldName": "Date",
                      "FieldLabel": "Date",
                      "UserCaption": "Date",
                      "IsCalculated": false,
                      "Properties": {},
                      "Sorting": "None",
                      "FieldType": "Date"
                    },
                    {
                      "FieldName": "Spend",
                      "FieldLabel": "Spend",
                      "UserCaption": "Spend",
                      "IsCalculated": false,
                      "Properties": {},
                      "Sorting": "None",
                      "FieldType": "Number"
                    },
                    {
                      "FieldName": "Conversions",
                      "FieldLabel": "Conversions",
                      "UserCaption": "Conversions",
                      "IsCalculated": false,
                      "Properties": {},
                      "Sorting": "None",
                      "FieldType": "Number"
                    },
                    {
                      "FieldName": "Territory",
                      "FieldLabel": "Territory",
                      "UserCaption": "Territory",
                      "IsCalculated": false,
                      "Properties": {},
                      "Sorting": "None",
                      "FieldType": "String"
                    }
                  ],
                  "TransposedFields": [],
                  "QuickFilters": [],
                  "AdditionalTables": [],
                  "ServiceAdditionalTables": [],
                  "DataSourceItem": {
                    "_type": "DataSourceItemType",
                    "Title": "Marketing Sheet",
                    "Subtitle": "Excel Data Source Item",
                    "HasTabularData": true,
                    "HasAsset": false,
                    "Properties": {
                      "Sheet": "Marketing"
                    },
                    "Parameters": {},
                    "ResourceItem": {
                      "_type": "DataSourceItemType",
                      "Title": "Marketing Sheet",
                      "Subtitle": "Excel Data Source Item",
                      "HasTabularData": true,
                      "HasAsset": false,
                      "Properties": {
                        "Url": "http://dl.infragistics.com/reportplus/reveal/samples/Samples.xlsx"
                      },
                      "Parameters": {}
                    }
                  },
                  "Expiration": 1440,
                  "Bindings": {
                    "Bindings": []
                  }
                },
                "VisualizationDataSpec": {
                  "_type": "GridVisualizationDataSpecType",
                  "Columns": [
                    {
                      "_type": "TabularColumnSpecType",
                      "FieldName": "Territory",
                      "Sorting": "None"
                    },
                    {
                      "_type": "TabularColumnSpecType",
                      "FieldName": "Conversions",
                      "Sorting": "None"
                    },
                    {
                      "_type": "TabularColumnSpecType",
                      "FieldName": "Spend",
                      "Sorting": "None"
                    }
                  ]
                }
              }
            ]
            """;

            var document = new RdashDocument("My Dashboard");

            var excelDataSourceItem = new RestDataSourceItem("Marketing Sheet")
            {
                Subtitle = "Excel Data Source Item",
                Url = "http://dl.infragistics.com/reportplus/reveal/samples/Samples.xlsx",
                IsAnonymous = true,
                Fields = new List<IField>
                {
                    new DateField("Date"),
                    new NumberField("Spend"),
                    new NumberField("Conversions"),
                    new TextField("Territory")
                }
            };
            excelDataSourceItem.UseExcel("Marketing");

            document.Visualizations.Add(new GridVisualization("Grid", excelDataSourceItem)
            {
                IsTitleVisible = true,
                Description = "Create Grid Visualization"
            }
            .SetColumns("Territory", "Conversions", "Spend")
            .ConfigureSettings(settings =>
            {
                settings.IsPagingEnabled = true;
                settings.IsFirstColumnFixed = true;
                settings.FontSize = FontSize.Large;
                settings.DateFieldAlignment = Alignment.Center;
                settings.TextFieldAlignment = Alignment.Center;
            }));

            document.Filters.Add(new DashboardDataFilter("Spend", excelDataSourceItem));
            document.Filters.Add(new DashboardDateFilter("My Date Filter"));

            var json = document.ToJsonString();
            var actualJson = JObject.Parse(json)["Widgets"];
            var expected = JArray.Parse(expectedJson);

            var removeProps = new[] { "Id", "DataSourceId" };
            var expectedStr = JsonConvert.SerializeObject(expected.RemoveProperties(removeProps));
            var actualStr = JsonConvert.SerializeObject(actualJson.RemoveProperties(removeProps));

            Assert.Equal(expectedStr, actualStr);
        }
    }
}
