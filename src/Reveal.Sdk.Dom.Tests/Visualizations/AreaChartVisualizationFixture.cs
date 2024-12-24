using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Serialization;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Filters;
using Reveal.Sdk.Dom.Visualizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations
{
    public class AreaChartVisualizationFixture
    {
        [Fact]
        public void Constructor_InitializesDefaultValues_WhenInstanceIsCreated()
        {
            // Act
            var areaChartVisualization = new AreaChartVisualization();

            // Assert
            Assert.NotNull(areaChartVisualization);
            Assert.Equal(ChartType.Area, areaChartVisualization.ChartType);
            Assert.Null(areaChartVisualization.Title);
            Assert.Null(areaChartVisualization.DataDefinition);
        }

        [Fact]
        public void Constructor_InitializesAreaChartVisualizationWithDataSource_WhenDataSourceItemIsProvided()
        {
            // Arrange
            var dataSourceItem = new DataSourceItem { HasTabularData = true };

            // Act
            var areaChartVisualization = new AreaChartVisualization(dataSourceItem);

            // Assert
            Assert.NotNull(areaChartVisualization);
            Assert.Equal(ChartType.Area, areaChartVisualization.ChartType);
            Assert.Equal(dataSourceItem, areaChartVisualization.DataDefinition.DataSourceItem);
            Assert.Null(areaChartVisualization.Title);
        }

        [Theory]
        [InlineData("TestTitle", null)]
        [InlineData(null, null)]
        [InlineData("TestTitle", "DataSource")]
        public void Constructor_SetsTitleAndDataSource_WhenArgumentsAreProvided(string title, string dataSourceName)
        {
            // Arrange
            var dataSourceItem = string.IsNullOrEmpty(dataSourceName) ? null : new DataSourceItem { Title = dataSourceName };

            // Act
            var areaChartVisualization = new AreaChartVisualization(title, dataSourceItem);

            // Assert
            Assert.Equal(title, areaChartVisualization.Title);
            Assert.Equal(ChartType.Area, areaChartVisualization.ChartType);
            Assert.Equal(dataSourceItem, areaChartVisualization.DataDefinition?.DataSourceItem);
        }

        [Fact]
        public void ToJsonString_GeneratesCorrectJson_WhenAreaChartVisualizationIsSerialized()
        {
            // Arrange
            var expectedJson =
                """
                [
                  {
                    "Description": "Create Area Visualization",
                    "Id": "cf573e0f-2a7f-4c3a-8340-95dcf21bc3cb",
                    "Title": "Area",
                    "IsTitleVisible": true,
                    "ColumnSpan": 0,
                    "RowSpan": 0,
                    "VisualizationSettings": {
                      "_type": "ChartVisualizationSettingsType",
                      "ShowTotalsInTooltip": false,
                      "TrendlineType": "LinearFit",
                      "AutomaticLabelRotation": true,
                      "SyncAxisVisibleRange": false,
                      "ZoomScaleHorizontal": 1.0,
                      "ZoomScaleVertical": 1.0,
                      "LeftAxisLogarithmic": false,
                      "ShowLegends": true,
                      "ChartType": "Area",
                      "VisualizationType": "CHART"
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
                        "Id": "07104de8-aa3b-4271-b8a7-e2bca5bbd1c0",
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
                          "Id": "8bbf98f1-333b-4dce-a457-646ccf255e51",
                          "Title": "Marketing Sheet",
                          "Subtitle": "Excel Data Source Item",
                          "DataSourceId": "66028cec-3190-471c-aeef-f53f4289e99a",
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
                      "_type": "CategoryVisualizationDataSpecType",
                      "Values": [
                        {
                          "_type": "MeasureColumnSpecType",
                          "SummarizationField": {
                            "_type": "SummarizationValueFieldType",
                            "FieldLabel": "Paid Traffic",
                            "UserCaption": "Paid Traffic",
                            "IsHidden": false,
                            "AggregationType": "Sum",
                            "Sorting": "None",
                            "IsCalculated": false,
                            "FieldName": "Paid Traffic"
                          }
                        },
                        {
                          "_type": "MeasureColumnSpecType",
                          "SummarizationField": {
                            "_type": "SummarizationValueFieldType",
                            "FieldLabel": "Organic Traffic",
                            "UserCaption": "Organic Traffic",
                            "IsHidden": false,
                            "AggregationType": "Sum",
                            "Sorting": "None",
                            "IsCalculated": false,
                            "FieldName": "Organic Traffic"
                          }
                        },
                        {
                          "_type": "MeasureColumnSpecType",
                          "SummarizationField": {
                            "_type": "SummarizationValueFieldType",
                            "FieldLabel": "Other Traffic",
                            "UserCaption": "Other Traffic",
                            "IsHidden": false,
                            "AggregationType": "Sum",
                            "Sorting": "None",
                            "IsCalculated": false,
                            "FieldName": "Other Traffic"
                          }
                        }
                      ],
                      "FormatVersion": 0,
                      "AdHocExpandedElements": [],
                      "Rows": [
                        {
                          "_type": "DimensionColumnSpecType",
                          "SummarizationField": {
                            "_type": "SummarizationDateFieldType",
                            "DateAggregationType": "Month",
                            "DrillDownElements": [],
                            "ExpandedItems": [],
                            "FieldName": "Date"
                          }
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

            document.Visualizations.Add(new AreaChartVisualization("Area", excelDataSourceItem)
            {
                IsTitleVisible = true,
                Description = "Create Area Visualization"
            }
            .SetLabel(new DateDataField("Date") { AggregationType = DateAggregationType.Month })
            .SetValues("Paid Traffic", "Organic Traffic", "Other Traffic")
            .ConfigureSettings(settings =>
            {
                settings.Trendline = TrendlineType.LinearFit;
                settings.ShowLegend = true;
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
