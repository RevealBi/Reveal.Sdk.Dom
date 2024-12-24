using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Core.Serialization;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Filters;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations
{
    public class PivotVisualizationFixture
    {
        [Fact]
        public void Constructor_InitializesDefaultValues_WhenInstanceIsCreated()
        {
            // Act
            var pivotVisualization = new PivotVisualization();

            // Assert
            Assert.NotNull(pivotVisualization);
            Assert.Equal(ChartType.Pivot, pivotVisualization.ChartType);
            Assert.NotNull(pivotVisualization.Columns);
            Assert.Empty(pivotVisualization.Columns);
            Assert.NotNull(pivotVisualization.Rows);
            Assert.Empty(pivotVisualization.Rows);
            Assert.NotNull(pivotVisualization.Values);
            Assert.Empty(pivotVisualization.Values);
        }

        [Theory]
        [InlineData("TestTitle", null)]
        [InlineData(null, null)]
        public void Constructor_SetsTitleAndDataSource_WhenArgumentsAreProvided(string title, DataSourceItem dataSourceItem)
        {
            // Act
            var pivotVisualization = new PivotVisualization(title, dataSourceItem);

            // Assert
            Assert.Equal(title, pivotVisualization.Title);
            Assert.Equal(ChartType.Pivot, pivotVisualization.ChartType);
            Assert.NotNull(pivotVisualization.Columns);
            Assert.Empty(pivotVisualization.Columns);
            Assert.NotNull(pivotVisualization.Rows);
            Assert.Empty(pivotVisualization.Rows);
            Assert.NotNull(pivotVisualization.Values);
            Assert.Empty(pivotVisualization.Values);
        }

        [Fact]
        public void Settings_HasCorrectDefaultValues_WhenInstanceIsCreated()
        {
            // Act
            var settings = new PivotVisualizationSettings();

            // Assert
            Assert.Equal(SchemaTypeNames.PivotVisualizationSettingsType, settings.SchemaTypeName);
            Assert.Equal(VisualizationTypes.PIVOT, settings.VisualizationType);
        }

        [Fact]
        public void ToJsonString_GeneratesCorrectJson_WhenPivotVisualizationIsSerialized()
        {
            // Arrange
            var expectedJson =
                """
                [
                  {
                    "Description": "Create Grid Visualization",
                    "Id": "0535ea46-54f0-4918-b7fc-993c0531bb66",
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
                          "FieldName": "Budget",
                          "FieldLabel": "Budget",
                          "UserCaption": "Budget",
                          "IsCalculated": false,
                          "Properties": {},
                          "Sorting": "None",
                          "FieldType": "Number"
                        },
                        {
                          "FieldName": "CTR",
                          "FieldLabel": "CTR",
                          "UserCaption": "CTR",
                          "IsCalculated": false,
                          "Properties": {},
                          "Sorting": "None",
                          "FieldType": "Number"
                        },
                        {
                          "FieldName": "Avg. CPC",
                          "FieldLabel": "Avg. CPC",
                          "UserCaption": "Avg. CPC",
                          "IsCalculated": false,
                          "Properties": {},
                          "Sorting": "None",
                          "FieldType": "Number"
                        },
                        {
                          "FieldName": "Traffic",
                          "FieldLabel": "Traffic",
                          "UserCaption": "Traffic",
                          "IsCalculated": false,
                          "Properties": {},
                          "Sorting": "None",
                          "FieldType": "Number"
                        },
                        {
                          "FieldName": "Paid Traffic",
                          "FieldLabel": "Paid Traffic",
                          "UserCaption": "Paid Traffic",
                          "IsCalculated": false,
                          "Properties": {},
                          "Sorting": "None",
                          "FieldType": "Number"
                        },
                        {
                          "FieldName": "Other Traffic",
                          "FieldLabel": "Other Traffic",
                          "UserCaption": "Other Traffic",
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
                        },
                        {
                          "FieldName": "CampaignID",
                          "FieldLabel": "CampaignID",
                          "UserCaption": "CampaignID",
                          "IsCalculated": false,
                          "Properties": {},
                          "Sorting": "None",
                          "FieldType": "String"
                        },
                        {
                          "FieldName": "New Seats",
                          "FieldLabel": "New Seats",
                          "UserCaption": "New Seats",
                          "IsCalculated": false,
                          "Properties": {},
                          "Sorting": "None",
                          "FieldType": "Number"
                        },
                        {
                          "FieldName": "Paid %",
                          "FieldLabel": "Paid %",
                          "UserCaption": "Paid %",
                          "IsCalculated": false,
                          "Properties": {},
                          "Sorting": "None",
                          "FieldType": "Number"
                        },
                        {
                          "FieldName": "Organic %",
                          "FieldLabel": "Organic %",
                          "UserCaption": "Organic %",
                          "IsCalculated": false,
                          "Properties": {},
                          "Sorting": "None",
                          "FieldType": "Number"
                        }
                      ],
                      "TransposedFields": [],
                      "QuickFilters": [],
                      "AdditionalTables": [],
                      "ServiceAdditionalTables": [],
                      "DataSourceItem": {
                        "_type": "DataSourceItemType",
                        "Id": "fc7f86b9-7938-447c-b1d3-b5a87e8b46d6",
                        "Title": "Marketing Sheet",
                        "Subtitle": "Excel Data Source Item",
                        "DataSourceId": "__EXCEL",
                        "HasTabularData": true,
                        "HasAsset": false,
                        "Properties": {
                          "Sheet": "Marketing"
                        },
                        "Parameters": {},
                        "ResourceItem": {
                          "_type": "DataSourceItemType",
                          "Id": "22b06eb6-f1d2-462c-84a0-2a887d509a35",
                          "Title": "Marketing Sheet",
                          "Subtitle": "Excel Data Source Item",
                          "DataSourceId": "fc847a34-3d12-4477-a853-f83c7ef47faf",
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
                    new NumberField("Budget"),
                    new NumberField("CTR"),
                    new NumberField("Avg. CPC"),
                    new NumberField("Traffic"),
                    new NumberField("Paid Traffic"),
                    new NumberField("Other Traffic"),
                    new NumberField("Conversions"),
                    new TextField("Territory"),
                    new TextField("CampaignID"),
                    new NumberField("New Seats"),
                    new NumberField("Paid %"),
                    new NumberField("Organic %")
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

            // Act
            RdashSerializer.SerializeObject(document);
            var json = document.ToJsonString();
            var jObject = JObject.Parse(json);
            var actualJArray = (JArray)jObject["Widgets"];
            var expectedJArray = JArray.Parse(expectedJson);

            // Assert
            Assert.Equal(expectedJArray.Count, actualJArray.Count);

            for (int i = 0; i < expectedJArray.Count; i++)
            {
                Assert.Equal(expectedJArray[i], actualJArray[i]);
            }
        }
    }
}