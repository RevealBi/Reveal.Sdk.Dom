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

public class SplineChartVisualizationFixture
{
    [Fact]
    public void Constructor_InitializesDefaultValues_WhenInstanceIsCreated()
    {
        var splineChartVisualization = new SplineChartVisualization();

        Assert.Equal(ChartType.Spline, splineChartVisualization.ChartType);
        Assert.Null(splineChartVisualization.Category);
        Assert.Equal(0, splineChartVisualization.ColumnSpan);
        Assert.Null(splineChartVisualization.DataDefinition);
        Assert.Null(splineChartVisualization.Description);
        Assert.NotNull(splineChartVisualization.Id);
        Assert.True(splineChartVisualization.IsTitleVisible);
        Assert.NotNull(splineChartVisualization.Labels);
        Assert.Empty(splineChartVisualization.Labels);
        Assert.Null(splineChartVisualization.Linker);
        Assert.Equal(0, splineChartVisualization.RowSpan);
        Assert.NotNull(splineChartVisualization.Settings);
        Assert.IsType<SplineChartVisualizationSettings>(splineChartVisualization.Settings);
        Assert.Null(splineChartVisualization.Title);
        Assert.NotNull(splineChartVisualization.Values);
        Assert.Empty(splineChartVisualization.Values);
        Assert.NotNull(splineChartVisualization.VisualizationDataSpec);
        Assert.IsType<CategoryVisualizationDataSpec>(splineChartVisualization.VisualizationDataSpec);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Constructor_InitializesSplineChartVisualizationWithDataSourceItem_WhenDataSourceItemIsProvided(
        bool hasTabularData)
    {
        // Arrange
        var dataSourceItem = new DataSourceItem { HasTabularData = hasTabularData };

        // Act
        var splineChartVisualization = new SplineChartVisualization(dataSourceItem);

        // Assert
        Assert.Equal(ChartType.Spline, splineChartVisualization.ChartType);
        Assert.Equal(dataSourceItem, splineChartVisualization.DataDefinition.DataSourceItem);
        Assert.Null(splineChartVisualization.Title);
        Assert.Equal(hasTabularData, splineChartVisualization.DataDefinition.DataSourceItem.HasTabularData);
        Assert.IsType(
            hasTabularData
                ? typeof(TabularDataDefinition)
                : typeof(XmlaDataDefinition),
            splineChartVisualization.DataDefinition);
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
        var splineChartVisualization = new SplineChartVisualization(title, dataSourceItem);

        // Assert
        Assert.Equal(title, splineChartVisualization.Title);
        Assert.Equal(ChartType.Spline, splineChartVisualization.ChartType);

        if (dataSourceItem == null)
        {
            Assert.Null(splineChartVisualization.DataDefinition);
        }
        else
        {
            Assert.NotNull(splineChartVisualization.DataDefinition);
            Assert.Equal(expectedHasTabularData, splineChartVisualization.DataDefinition.DataSourceItem.HasTabularData);
            Assert.IsType(
                hasTabularData.Value
                    ? typeof(TabularDataDefinition)
                    : typeof(XmlaDataDefinition),
                splineChartVisualization.DataDefinition);
        }
    }

    [Fact]
    public void ToJsonString_GeneratesCorrectJson_WhenSplineChartVisualizationIsSerialized()
    {
        // Arrange
        var expectedJson =
            """
            [ {
              "Description" : "Create Spline Visualization",
              "Id" : "b0d34732-ce1b-4201-9cd7-0f21e9153c4d",
              "Title" : "Spline",
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
                "ChartType" : "Spline",
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
                    "Id" : "d593dd79-7161-4929-afc9-c26393f5b650",
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
                Id = "d593dd79-7161-4929-afc9-c26393f5b650",
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
                new NumberField("Other Traffic"),
                new NumberField("Organic Traffic")
            }
        };
        excelDataSourceItem.UseExcel("Marketing");

        document.Visualizations.Add(new SplineChartVisualization("Spline", excelDataSourceItem)
            {
                Id = "b0d34732-ce1b-4201-9cd7-0f21e9153c4d",
                IsTitleVisible = true,
                Description = "Create Spline Visualization"
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