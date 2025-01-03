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

public class StackedBarChartVisualizationFixture
{
    [Fact]
    public void Constructor_InitializesDefaultValues_WhenInstanceIsCreated()
    {
        //Act
        var visualization = new StackedBarChartVisualization();

        //Assert
        Assert.Equal(ChartType.StackedBar, visualization.ChartType);
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
        Assert.IsType<StackedBarChartVisualizationSettings>(visualization.Settings);
        Assert.Null(visualization.Title);
        Assert.NotNull(visualization.Values);
        Assert.Empty(visualization.Values);
        Assert.NotNull(visualization.VisualizationDataSpec);
        Assert.IsType<CategoryVisualizationDataSpec>(visualization.VisualizationDataSpec);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Constructor_InitializesStackedBarChartVisualizationWithDataSourceItem_WhenDataSourceItemIsProvided(
        bool hasTabularData)
    {
        // Arrange
        var dataSourceItem = new DataSourceItem { HasTabularData = hasTabularData };

        // Act
        var stackedBarChartVisualization = new StackedBarChartVisualization(dataSourceItem);

        // Assert
        Assert.Equal(ChartType.StackedBar, stackedBarChartVisualization.ChartType);
        Assert.Equal(dataSourceItem, stackedBarChartVisualization.DataDefinition.DataSourceItem);
        Assert.Null(stackedBarChartVisualization.Title);
        Assert.Equal(hasTabularData, stackedBarChartVisualization.DataDefinition.DataSourceItem.HasTabularData);
        Assert.IsType(
            hasTabularData
                ? typeof(TabularDataDefinition)
                : typeof(XmlaDataDefinition),
            stackedBarChartVisualization.DataDefinition);
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
        var stackedBarChartVisualization = new StackedBarChartVisualization(title, dataSourceItem);

        // Assert
        Assert.Equal(title, stackedBarChartVisualization.Title);
        Assert.Equal(ChartType.StackedBar, stackedBarChartVisualization.ChartType);

        if (dataSourceItem == null)
        {
            Assert.Null(stackedBarChartVisualization.DataDefinition);
        }
        else
        {
            Assert.NotNull(stackedBarChartVisualization.DataDefinition);
            Assert.Equal(expectedHasTabularData,
                stackedBarChartVisualization.DataDefinition.DataSourceItem.HasTabularData);
            Assert.IsType(
                hasTabularData.Value
                    ? typeof(TabularDataDefinition)
                    : typeof(XmlaDataDefinition),
                stackedBarChartVisualization.DataDefinition);
        }
    }

    [Fact]
    public void ToJsonString_GeneratesCorrectJson_WhenStackedBarChartVisualizationIsSerialized()
    {
        //Arrange
        var expectedJson =
            """
            [ {
              "Description" : "Stack Bar Visualization Description",
              "Id" : "60344a4a-d0ce-4364-9f8c-acfbb8caa32e",
              "Title" : "Stacked Bar Visualization",
              "IsTitleVisible" : true,
              "ColumnSpan" : 0,
              "RowSpan" : 0,
              "VisualizationSettings" : {
                "_type" : "ChartVisualizationSettingsType",
                "ShowTotalsInTooltip" : false,
                "AutomaticLabelRotation" : true,
                "SyncAxisVisibleRange" : false,
                "ZoomScaleHorizontal" : 1.0,
                "ZoomScaleVertical" : 1.0,
                "LeftAxisLogarithmic" : false,
                "ShowLegends" : true,
                "ChartType" : "StackedBar",
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
                new NumberField("Organic Traffic"),
                new NumberField("Other Traffic"),
            }
        };
        excelDataSourceItem.UseExcel("Marketing");

        document.Visualizations.Add(new StackedBarChartVisualization("Stacked Bar Visualization", excelDataSourceItem)
            {
                Id = "60344a4a-d0ce-4364-9f8c-acfbb8caa32e",
                IsTitleVisible = true,
                Description = "Stack Bar Visualization Description",
            }
            .SetLabel(new DateDataField("Date") { AggregationType = DateAggregationType.Month })
            .SetValues("Paid Traffic", "Organic Traffic", "Other Traffic")
            .ConfigureSettings(settings => { settings.ShowLegend = true; }));

        document.Filters.Add(new DashboardDateFilter("My Date Filter"));

        //Act
        var json = document.ToJsonString();
        var actualJson = JObject.Parse(json)["Widgets"];
        var actualNormalized = JsonConvert.SerializeObject(actualJson, Formatting.Indented);
        var expectedNormalized = JArray.Parse(expectedJson).ToString(Formatting.Indented);

        // Assert
        Assert.Equal(expectedNormalized.Trim(), actualNormalized.Trim());
    }
}