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

public class DoughnutChartVisualizationFixture
{
    [Fact]
    public void Constructor_InitializesDefaultValues_WhenInstanceIsCreated()
    {
        var visualization = new DoughnutChartVisualization();

        Assert.Equal(ChartType.Doughnut, visualization.ChartType);
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
        Assert.IsType<DoughnutChartVisualizationSettings>(visualization.Settings);
        Assert.Null(visualization.Title);
        Assert.NotNull(visualization.Values);
        Assert.Empty(visualization.Values);
        Assert.NotNull(visualization.VisualizationDataSpec);
        Assert.IsType<SingleValueLabelsVisualizationDataSpec>(visualization.VisualizationDataSpec);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Constructor_InitializesDoughnutChartVisualizationWithDataSourceItem_WhenDataSourceItemIsProvided(
        bool hasTabularData)
    {
        // Arrange
        var dataSourceItem = new DataSourceItem { HasTabularData = hasTabularData };

        // Act
        var doughtnutVisualization = new DoughnutChartVisualization(dataSourceItem);

        // Assert
        Assert.Equal(ChartType.Doughnut, doughtnutVisualization.ChartType);
        Assert.Equal(dataSourceItem, doughtnutVisualization.DataDefinition.DataSourceItem);
        Assert.Null(doughtnutVisualization.Title);
        Assert.Equal(hasTabularData, doughtnutVisualization.DataDefinition.DataSourceItem.HasTabularData);
        Assert.IsType(
            hasTabularData
                ? typeof(TabularDataDefinition)
                : typeof(XmlaDataDefinition),
            doughtnutVisualization.DataDefinition);
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
        var doughtnutVisualization = new DoughnutChartVisualization(title, dataSourceItem);

        // Assert
        Assert.Equal(title, doughtnutVisualization.Title);
        Assert.Equal(ChartType.Doughnut, doughtnutVisualization.ChartType);

        if (dataSourceItem == null)
        {
            Assert.Null(doughtnutVisualization.DataDefinition);
        }
        else
        {
            Assert.NotNull(doughtnutVisualization.DataDefinition);
            Assert.Equal(expectedHasTabularData, doughtnutVisualization.DataDefinition.DataSourceItem.HasTabularData);
            Assert.IsType(
                hasTabularData.Value
                    ? typeof(TabularDataDefinition)
                    : typeof(XmlaDataDefinition),
                doughtnutVisualization.DataDefinition);
        }
    }

    [Fact]
    public void ToJsonString_GeneratesCorrectJson_WhenDoughtnutVisualizationIsSerialized()
    {
        //Arrange
        var expectedJson =
            """
            [ {
              "Description" : "Doughnut Chart Visualization",
              "Id" : "60344a4a-d0ce-4364-9f8c-acfbb8caa32e",
              "Title" : "Doughnut Chart Visualization",
              "IsTitleVisible" : true,
              "ColumnSpan" : 0,
              "RowSpan" : 0,
              "VisualizationSettings" : {
                "_type" : "ChartVisualizationSettingsType",
                "PieStartPosition" : 90,
                "ShowZeroValuesInLegend" : false,
                "ShowLegends" : true,
                "LabelDisplayMode" : "ValueAndPercentage",
                "OthersSliceThreshold" : 3.0,
                "ChartType" : "Doughnut",
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
                  "FieldName" : "Conversions",
                  "FieldLabel" : "Conversions",
                  "UserCaption" : "Conversions",
                  "IsCalculated" : false,
                  "Properties" : { },
                  "Sorting" : "None",
                  "FieldType" : "String"
                }, {
                  "FieldName" : "Territory",
                  "FieldLabel" : "Territory",
                  "UserCaption" : "Territory",
                  "IsCalculated" : false,
                  "Properties" : { },
                  "Sorting" : "None",
                  "FieldType" : "String"
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
                "_type" : "SingleValueLabelsVisualizationDataSpecType",
                "Value" : [ {
                  "_type" : "MeasureColumnSpecType",
                  "SummarizationField" : {
                    "_type" : "SummarizationValueFieldType",
                    "FieldLabel" : "Conversions",
                    "UserCaption" : "Conversions",
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
                    "FieldName" : "Conversions"
                  }
                } ],
                "FormatVersion" : 0,
                "AdHocExpandedElements" : [ ],
                "Rows" : [ {
                  "_type" : "DimensionColumnSpecType",
                  "SummarizationField" : {
                    "_type" : "SummarizationRegularFieldType",
                    "DrillDownElements" : [ ],
                    "ExpandedItems" : [ ],
                    "FieldName" : "Territory"
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
                new TextField("Conversions"),
                new TextField("Territory"),
            }
        };
        excelDataSourceItem.UseExcel("Marketing");

        document.Visualizations.Add(new DoughnutChartVisualization("Doughnut Chart Visualization", excelDataSourceItem)
            {
                Id = "60344a4a-d0ce-4364-9f8c-acfbb8caa32e",
                IsTitleVisible = true,
                Description = "Doughnut Chart Visualization"
            }
            .SetLabel("Territory").SetValue("Conversions")
            .ConfigureSettings(settings =>
            {
                settings.SliceLabelDisplay = LabelDisplayMode.ValueAndPercentage;
                settings.StartPosition = 90;
            }));

        document.Filters.Add(new DashboardDataFilter("Spend", excelDataSourceItem));
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