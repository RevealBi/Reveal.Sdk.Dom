using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Serialization;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Filters;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations;

public class LineChartVisualizationFixture
{
    [Fact]
    public void Constructor_InitializesDefaultValues_WhenInstanceIsCreated()
    {
        var lineChartVisualization = new LineChartVisualization();

        Assert.Equal(ChartType.Line, lineChartVisualization.ChartType);
        Assert.Null(lineChartVisualization.Category);
        Assert.Equal(0, lineChartVisualization.ColumnSpan);
        Assert.Null(lineChartVisualization.DataDefinition);
        Assert.Null(lineChartVisualization.Description);
        Assert.NotNull(lineChartVisualization.Id);
        Assert.True(lineChartVisualization.IsTitleVisible);
        Assert.NotNull(lineChartVisualization.Labels);
        Assert.Empty(lineChartVisualization.Labels);
        Assert.Null(lineChartVisualization.Linker);
        Assert.Equal(0, lineChartVisualization.RowSpan);
        Assert.NotNull(lineChartVisualization.Settings);
        Assert.IsType<LineChartVisualizationSettings>(lineChartVisualization.Settings);
        Assert.Null(lineChartVisualization.Title);
        Assert.NotNull(lineChartVisualization.Values);
        Assert.Empty(lineChartVisualization.Values);
        Assert.NotNull(lineChartVisualization.VisualizationDataSpec);
        Assert.IsType<CategoryVisualizationDataSpec>(lineChartVisualization.VisualizationDataSpec);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Constructor_InitializesLineChartVisualizationWithDataSourceItem_WhenDataSourceItemIsProvided(
        bool hasTabularData)
    {
        // Arrange
        var dataSourceItem = new DataSourceItem { HasTabularData = hasTabularData };

        // Act
        var lineChartVisualization = new LineChartVisualization(dataSourceItem);

        // Assert
        Assert.Equal(ChartType.Line, lineChartVisualization.ChartType);
        Assert.Equal(dataSourceItem, lineChartVisualization.DataDefinition.DataSourceItem);
        Assert.Null(lineChartVisualization.Title);
        Assert.Equal(hasTabularData, lineChartVisualization.DataDefinition.DataSourceItem.HasTabularData);
        Assert.IsType(
            hasTabularData
                ? typeof(TabularDataDefinition)
                : typeof(XmlaDataDefinition),
            lineChartVisualization.DataDefinition);
    }

    [Theory]
    [InlineData("Test Title", null, null)]
    [InlineData(null, null, null)]
    [InlineData("Test Title with Data Source", true, true)]
    [InlineData("Test Title without Tabular Data", false, false)]
    public void Constructor_SetsTitleAndDataSourceItem_WhenArgumentsAreProvided(string title, bool? hasTabularData,
        bool? expectedHasTabularData)
    {
        // Arrange
        var dataSourceItem = hasTabularData.HasValue
            ? new DataSourceItem { HasTabularData = hasTabularData.Value }
            : null;
        // Act
        var lineChartVisualization = new LineChartVisualization(title, dataSourceItem);

        // Assert
        Assert.Equal(title, lineChartVisualization.Title);
        Assert.Equal(ChartType.Line, lineChartVisualization.ChartType);

        if (dataSourceItem == null)
        {
            Assert.Null(lineChartVisualization.DataDefinition);
        }
        else
        {
            Assert.NotNull(lineChartVisualization.DataDefinition);
            Assert.Equal(expectedHasTabularData, lineChartVisualization.DataDefinition.DataSourceItem.HasTabularData);
            Assert.IsType(
                hasTabularData.Value
                    ? typeof(TabularDataDefinition)
                    : typeof(XmlaDataDefinition),
                lineChartVisualization.DataDefinition);
        }
    }

    [Fact]
    public void ToJsonString_GeneratesCorrectJson_WhenLineChartVisualizationIsSerialized()
    {
        // Arrange
        var expectedJson =
            """
            [ {
              "Description" : "Create Line Visualization",
              "Id" : "95c27125-ed73-4770-ac41-c52a27900a8a",
              "Title" : "Line Visualization",
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
                "AxisTitlesMode" : "None",
                "ShowLegends" : true,
                "ChartType" : "Line",
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
                  "FieldName" : "Other Traffic",
                  "FieldLabel" : "Other Traffic",
                  "UserCaption" : "Other Traffic",
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
                } ],
                "TransposedFields" : [ ],
                "QuickFilters" : [ ],
                "AdditionalTables" : [ ],
                "ServiceAdditionalTables" : [ ],
                "DataSourceItem" : {
                  "_type" : "DataSourceItemType",
                  "Id" : "080cc17d-4a0a-4837-aa3f-ef2571ea443a",
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
                    "Id" : "4500e32b-4992-4003-8329-3b48ca245563",
                    "Title" : "Marketing Sheet",
                    "Subtitle" : "Excel Data Source Item",
                    "DataSourceId" : "30911a6f-2c9a-425a-8c79-73056a6eb0d2",
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
                    "Formatting": {
                      "_type": "NumberFormattingSpecType",
                      "CurrencySymbol": "$",
                      "DecimalDigits": 2,
                      "FormatType": "Number",
                      "MKFormat": "None",
                      "NegativeFormat": "MinusSign",
                      "ShowGroupingSeparator": false,
                      "ShowDataLabelsInChart": true,
                      "OverrideDefaultSettings": false
                    },
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
                    "Formatting": {
                      "_type": "NumberFormattingSpecType",
                      "CurrencySymbol": "$",
                      "DecimalDigits": 2,
                      "FormatType": "Number",
                      "MKFormat": "None",
                      "NegativeFormat": "MinusSign",
                      "ShowGroupingSeparator": false,
                      "ShowDataLabelsInChart": true,
                      "OverrideDefaultSettings": false
                    },
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
                    "Formatting": {
                      "_type": "NumberFormattingSpecType",
                      "CurrencySymbol": "$",
                      "DecimalDigits": 2,
                      "FormatType": "Number",
                      "MKFormat": "None",
                      "NegativeFormat": "MinusSign",
                      "ShowGroupingSeparator": false,
                      "ShowDataLabelsInChart": true,
                      "OverrideDefaultSettings": false
                    },
                    "FieldName" : "Other Traffic"
                  }
                } ],
                "FixedLines": [],
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
            Id = "080cc17d-4a0a-4837-aa3f-ef2571ea443a",
            Subtitle = "Excel Data Source Item",
            Url = "http://dl.infragistics.com/reportplus/reveal/samples/Samples.xlsx",
            IsAnonymous = true,
            ResourceItem = new DataSourceItem
            {
                Id = "4500e32b-4992-4003-8329-3b48ca245563",
                DataSourceId = "30911a6f-2c9a-425a-8c79-73056a6eb0d2",
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
                new NumberField("Other Traffic"),
                new NumberField("Organic Traffic")
            }
        };
        excelDataSourceItem.UseExcel("Marketing");

        document.Visualizations.Add(new LineChartVisualization("Line Visualization", excelDataSourceItem)
            {
                Id = "95c27125-ed73-4770-ac41-c52a27900a8a",
                IsTitleVisible = true,
                Description = "Create Line Visualization"
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

        RdashSerializer.SerializeObject(document);
        var json = document.ToJsonString();
        var actualJson = JObject.Parse(json)["Widgets"];
        var actualNormalized = JsonConvert.SerializeObject(actualJson, Formatting.Indented);
        var expectedNormalized = JArray.Parse(expectedJson).ToString(Formatting.Indented);

        // Assert
        Assert.Equal(expectedNormalized.Trim(), actualNormalized.Trim());
    }
}