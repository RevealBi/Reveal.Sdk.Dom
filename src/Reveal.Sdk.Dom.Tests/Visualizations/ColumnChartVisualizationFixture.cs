﻿using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Serialization;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Filters;
using Reveal.Sdk.Dom.Visualizations;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations;

public class ColumnChartVisualizationFixture
{
    [Fact]
    public void Constructor_InitializesDefaultValues_WhenInstanceIsCreated()
    {
        // Act
        var columnChartVisualization = new ColumnChartVisualization();

        // Assert
        Assert.NotNull(columnChartVisualization);
        Assert.Equal(ChartType.Column, columnChartVisualization.ChartType);
        Assert.Null(columnChartVisualization.Title);
        Assert.Null(columnChartVisualization.DataDefinition);
    }

    [Fact]
    public void Constructor_InitializesBarChartVisualizationWithDataSource_WhenDataSourceItemIsProvided()
    {
        // Arrange
        var dataSourceItem = new DataSourceItem { HasTabularData = true };

        // Act
        var columnChartVisualization = new ColumnChartVisualization(dataSourceItem);

        // Assert
        Assert.NotNull(columnChartVisualization);
        Assert.Equal(ChartType.Column, columnChartVisualization.ChartType);
        Assert.Equal(dataSourceItem, columnChartVisualization.DataDefinition.DataSourceItem);
        Assert.Null(columnChartVisualization.Title);
    }

    [Theory]
    [InlineData("TestTitle", null)]
    [InlineData(null, null)]
    [InlineData("TestTitle", "DataSource")]
    public void Constructor_SetsTitleAndDataSource_WhenArgumentsAreProvided(string title, string dataSourceName)
    {
        // Arrange
        var dataSourceItem =
            string.IsNullOrEmpty(dataSourceName) ? null : new DataSourceItem { Title = dataSourceName };

        // Act
        var columnChartVisualization = new ColumnChartVisualization(title, dataSourceItem);

        // Assert
        Assert.Equal(title, columnChartVisualization.Title);
        Assert.Equal(ChartType.Column, columnChartVisualization.ChartType);
        Assert.Equal(dataSourceItem, columnChartVisualization.DataDefinition?.DataSourceItem);
    }

    [Fact]
    public void ToJsonString_GeneratesCorrectJson_WhenColumnChartVisualizationIsSerialized()
    {
        // Arrange
        var expectedJson =
            """
            [ {
              "Description" : "Create Column Visualization",
              "Id" : "439a95a0-08d4-4e75-8ae8-485f55c80d22",
              "Title" : "Column",
              "IsTitleVisible" : true,
              "ColumnSpan" : 0,
              "RowSpan" : 0,
              "VisualizationSettings" : {
                "_type" : "ChartVisualizationSettingsType",
                "ShowTotalsInTooltip" : false,
                "TrendlineType" : "LinearFit",
                "AutomaticLabelRotation" : true,
                "SyncAxisVisibleRange" : true,
                "ZoomScaleHorizontal" : 1.0,
                "ZoomScaleVertical" : 1.0,
                "LeftAxisLogarithmic" : false,
                "ShowLegends" : true,
                "ChartType" : "Column",
                "VisualizationType" : "CHART"
              },
              "DataSpec" : {
                "_type" : "TabularDataSpecType",
                "IsTransposed" : false,
                "Fields" : [ {
                  "FieldName" : "Date",
                  "FieldLabel" : "Date",
                  "UserCaption" : "Date",
                  "IsCalculated" : false,
                  "Properties" : { },
                  "Sorting" : "None",
                  "FieldType" : "Date"
                }, {
                  "FieldName" : "Paid Traffic",
                  "FieldLabel" : "Paid Traffic",
                  "UserCaption" : "Paid Traffic",
                  "IsCalculated" : false,
                  "Properties" : { },
                  "Sorting" : "None",
                  "FieldType" : "Number"
                }, {
                  "FieldName" : "Organic Traffic",
                  "FieldLabel" : "Organic Traffic",
                  "UserCaption" : "Organic Traffic",
                  "IsCalculated" : false,
                  "Properties" : { },
                  "Sorting" : "None",
                  "FieldType" : "Number"
                }, {
                  "FieldName" : "Other Traffic",
                  "FieldLabel" : "Other Traffic",
                  "UserCaption" : "Other Traffic",
                  "IsCalculated" : false,
                  "Properties" : { },
                  "Sorting" : "None",
                  "FieldType" : "Number"
                } ],
                "TransposedFields" : [ ],
                "QuickFilters" : [ ],
                "AdditionalTables" : [ ],
                "ServiceAdditionalTables" : [ ],
                "DataSourceItem" : {
                  "_type" : "DataSourceItemType",
                  "Id" : "0c7f6ae5-776b-42e3-8a10-5afc423da76f",
                  "Title" : "Marketing Sheet",
                  "Subtitle" : "Excel Data Source Item",
                  "DataSourceId" : "__EXCEL",
                  "HasTabularData" : true,
                  "HasAsset" : false,
                  "Properties" : {
                    "Sheet" : "Marketing"
                  },
                  "Parameters" : { },
                  "ResourceItem" : {
                    "_type" : "DataSourceItemType",
                    "Id" : "41f049de-2b31-433e-99ad-82fa3b01b021",
                    "Title" : "Marketing Sheet",
                    "Subtitle" : "Excel Data Source Item",
                    "DataSourceId" : "33077d1e-19c5-44fe-b981-6765af3156a6",
                    "HasTabularData" : true,
                    "HasAsset" : false,
                    "Properties" : {
                      "Url" : "http://dl.infragistics.com/reportplus/reveal/samples/Samples.xlsx"
                    },
                    "Parameters" : { }
                  }
                },
                "Expiration" : 1440,
                "Bindings" : {
                  "Bindings" : [ ]
                }
              },
              "VisualizationDataSpec" : {
                "_type" : "CategoryVisualizationDataSpecType",
                "Values" : [ {
                  "_type" : "MeasureColumnSpecType",
                  "SummarizationField" : {
                    "_type" : "SummarizationValueFieldType",
                    "FieldLabel" : "Paid Traffic",
                    "UserCaption" : "Paid Traffic",
                    "IsHidden" : false,
                    "AggregationType" : "Sum",
                    "Sorting" : "None",
                    "IsCalculated" : false,
                    "FieldName" : "Paid Traffic"
                  }
                }, {
                  "_type" : "MeasureColumnSpecType",
                  "SummarizationField" : {
                    "_type" : "SummarizationValueFieldType",
                    "FieldLabel" : "Organic Traffic",
                    "UserCaption" : "Organic Traffic",
                    "IsHidden" : false,
                    "AggregationType" : "Sum",
                    "Sorting" : "None",
                    "IsCalculated" : false,
                    "FieldName" : "Organic Traffic"
                  }
                }, {
                  "_type" : "MeasureColumnSpecType",
                  "SummarizationField" : {
                    "_type" : "SummarizationValueFieldType",
                    "FieldLabel" : "Other Traffic",
                    "UserCaption" : "Other Traffic",
                    "IsHidden" : false,
                    "AggregationType" : "Sum",
                    "Sorting" : "None",
                    "IsCalculated" : false,
                    "FieldName" : "Other Traffic"
                  }
                } ],
                "FormatVersion" : 0,
                "AdHocExpandedElements" : [ ],
                "Rows" : [ {
                  "_type" : "DimensionColumnSpecType",
                  "SummarizationField" : {
                    "_type" : "SummarizationDateFieldType",
                    "DateAggregationType" : "Month",
                    "DrillDownElements" : [ ],
                    "ExpandedItems" : [ ],
                    "FieldName" : "Date"
                  }
                } ]
              }
            } ]
            """;

        var document = new RdashDocument("My Dashboard");

        var excelDataSourceItem = new RestDataSourceItem("Marketing Sheet")
        {
            Id = "0c7f6ae5-776b-42e3-8a10-5afc423da76f",
            Subtitle = "Excel Data Source Item",
            Url = "http://dl.infragistics.com/reportplus/reveal/samples/Samples.xlsx",
            IsAnonymous = true,
            ResourceItem = new DataSourceItem
            {
                Id = "41f049de-2b31-433e-99ad-82fa3b01b021",
                DataSourceId = "33077d1e-19c5-44fe-b981-6765af3156a6",
                Title = "Marketing Sheet",
                Subtitle = "Excel Data Source Item",
                HasTabularData = true,
                HasAsset = false,
                Properties = new Dictionary<string, object>
                {
                    { "Url", "http://dl.infragistics.com/reportplus/reveal/samples/Samples.xlsx" }
                }
            },
            Fields = new List<IField>
            {
                new DateField("Date"),
                new NumberField("Paid Traffic"),
                new NumberField("Organic Traffic"),
                new NumberField("Other Traffic")
            }
        };
        excelDataSourceItem.UseExcel("Marketing");

        document.Visualizations.Add(new ColumnChartVisualization("Column", excelDataSourceItem)
            {
                Id = "439a95a0-08d4-4e75-8ae8-485f55c80d22",
                IsTitleVisible = true,
                Description = "Create Column Visualization"
            }
            .SetLabel(new DateDataField("Date") { AggregationType = DateAggregationType.Month })
            .SetValues("Paid Traffic", "Organic Traffic", "Other Traffic")
            .ConfigureSettings(settings =>
            {
                settings.Trendline = TrendlineType.LinearFit;
                settings.ShowLegend = true;
                settings.ZoomLevel = 1;
                settings.AutomaticLabelRotation = true;
                settings.SyncAxis = true;
            }));

        document.Filters.Add(new DashboardDateFilter("My Date Filter"));

        // Act
        RdashSerializer.SerializeObject(document);
        var json = document.ToJsonString();
        var actualJson = JObject.Parse(json)["Widgets"];
        var expected = JArray.Parse(expectedJson);

        var expectedStr = JsonConvert.SerializeObject(expected);
        var actualStr = JsonConvert.SerializeObject(actualJson);

        // Assert
        Assert.Equal(expectedStr, actualStr);
    }
}