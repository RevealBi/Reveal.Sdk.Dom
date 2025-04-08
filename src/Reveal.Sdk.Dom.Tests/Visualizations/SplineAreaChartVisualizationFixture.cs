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

public class SplineAreaChartVisualizationFixture
{
    [Fact]
    public void Constructor_InitializesDefaultValues_WhenInstanceIsCreated()
    {
        var visualization = new SplineAreaChartVisualization();

        Assert.Equal(ChartType.SplineArea, visualization.ChartType);
        Assert.Null(visualization.Category);
        Assert.Equal(0, visualization.ColumnSpan);
        Assert.Null(visualization.DataDefinition);
        Assert.Null(visualization.Description);
        Assert.NotNull(visualization.Id);
        Assert.True(visualization.IsTitleVisible);
        Assert.NotNull(visualization.Labels);
        Assert.Empty(visualization.Labels);
        Assert.Null(visualization.Linker);
        Assert.Equal(0, visualization.RowSpan);
        Assert.NotNull(visualization.Settings);
        Assert.IsType<SplineAreaChartVisualizationSettings>(visualization.Settings);
        Assert.Null(visualization.Title);
        Assert.NotNull(visualization.Values);
        Assert.Empty(visualization.Values);
        Assert.NotNull(visualization.VisualizationDataSpec);
        Assert.IsType<CategoryVisualizationDataSpec>(visualization.VisualizationDataSpec);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Constructor_InitializesSplineAreaChartVisualizationWithDataSourceItem_WhenDataSourceItemIsProvided(
        bool hasTabularData)
    {
        // Arrange
        var dataSourceItem = new DataSourceItem { HasTabularData = hasTabularData };

        // Act
        var splineAreaChartVisualization = new SplineAreaChartVisualization(dataSourceItem);

        // Assert
        Assert.Equal(ChartType.SplineArea, splineAreaChartVisualization.ChartType);
        Assert.Equal(dataSourceItem, splineAreaChartVisualization.DataDefinition.DataSourceItem);
        Assert.Null(splineAreaChartVisualization.Title);
        Assert.Equal(hasTabularData, splineAreaChartVisualization.DataDefinition.DataSourceItem.HasTabularData);
        Assert.IsType(
            hasTabularData
                ? typeof(TabularDataDefinition)
                : typeof(XmlaDataDefinition),
            splineAreaChartVisualization.DataDefinition);
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
        var splineAreaChartVisualization = new SplineAreaChartVisualization(title, dataSourceItem);

        // Assert
        Assert.Equal(title, splineAreaChartVisualization.Title);
        Assert.Equal(ChartType.SplineArea, splineAreaChartVisualization.ChartType);

        if (dataSourceItem == null)
        {
            Assert.Null(splineAreaChartVisualization.DataDefinition);
        }
        else
        {
            Assert.NotNull(splineAreaChartVisualization.DataDefinition);
            Assert.Equal(expectedHasTabularData,
                splineAreaChartVisualization.DataDefinition.DataSourceItem.HasTabularData);
            Assert.IsType(
                hasTabularData.Value
                    ? typeof(TabularDataDefinition)
                    : typeof(XmlaDataDefinition),
                splineAreaChartVisualization.DataDefinition);
        }
    }

    [Fact]
    public void ToJsonString_GeneratesCorrectJson_WhenSplineAreaChartVisualizationIsSerialized()
    {
        var expectedJson =
            """
            [ {
              "Description" : "Create Spline Area Visualization",
              "Id" : "ff37d2c3-62fd-4a8c-80f4-aeb0c087deea",
              "Title" : "SplineArea",
              "IsTitleVisible" : true,
              "ColumnSpan" : 0,
              "RowSpan" : 0,
              "VisualizationSettings" : {
                "_type" : "ChartVisualizationSettingsType",
                "ShowTotalsInTooltip" : true,
                "TrendlineType" : "ExponentialFit",
                "AutomaticLabelRotation" : true,
                "SyncAxisVisibleRange" : false,
                "ZoomScaleHorizontal" : 1.0,
                "ZoomScaleVertical" : 1.0,
                "LeftAxisLogarithmic" : false,
                "AxisTitlesMode" : "None",
                "ShowLegends" : true,
                "ChartType" : "SplineArea",
                "VisualizationType" : "Area"
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
                  "FieldName" : "Spend",
                  "FieldLabel" : "Spend",
                  "UserCaption" : "Spend",
                  "IsCalculated" : false,
                  "Properties" : { },
                  "Sorting" : "None",
                  "FieldType" : "Number"
                }, {
                  "FieldName" : "Budget",
                  "FieldLabel" : "Budget",
                  "UserCaption" : "Budget",
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
                    "Id" : "82d63339-3119-4210-835b-6d77baae145c",
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
                    "FieldLabel" : "Spend",
                    "UserCaption" : "Spend",
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
                    "FieldName" : "Spend"
                  }
                }, {
                  "_type" : "MeasureColumnSpecType",
                  "SummarizationField" : {
                    "_type" : "SummarizationValueFieldType",
                    "FieldLabel" : "Budget",
                    "UserCaption" : "Budget",
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
                    "FieldName" : "Budget"
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
                Id = "82d63339-3119-4210-835b-6d77baae145c",
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
                new NumberField("Spend"),
                new NumberField("Budget")
            }
        };
        excelDataSourceItem.UseExcel("Marketing");

        document.Visualizations.Add(new SplineAreaChartVisualization("SplineArea", excelDataSourceItem)
            {
                Id = "ff37d2c3-62fd-4a8c-80f4-aeb0c087deea",
                IsTitleVisible = true,
                Description = "Create Spline Area Visualization"
            }
            .SetLabel(new DateDataField("Date") { AggregationType = DateAggregationType.Month })
            .SetValues("Spend", "Budget")
            .ConfigureSettings(settings =>
            {
                settings.Trendline = TrendlineType.ExponentialFit;
                settings.AutomaticLabelRotation = true;
                settings.ShowTotalsInTooltip = true;
                settings.ShowLegend = true;
                settings.VisualizationType = "Area";
            }));

        document.Filters.Add(new DashboardDataFilter("Spend", excelDataSourceItem));
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