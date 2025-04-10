using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Serialization;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Filters;
using Reveal.Sdk.Dom.Visualizations;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations;

public class CandleStickVisualizationFixture
{
    [Fact]
    public void Constructor_InitializesDefaultValues_WhenInstanceIsCreated()
    {
        // Act
        var candleStickVisualization = new CandleStickVisualization();

        // Assert
        Assert.Equal(ChartType.Candlestick, candleStickVisualization.ChartType);
        Assert.Null(candleStickVisualization.Title);
        Assert.Null(candleStickVisualization.DataDefinition);
        Assert.NotNull(candleStickVisualization.Closes);
        Assert.Empty(candleStickVisualization.Closes);
        Assert.NotNull(candleStickVisualization.Highs);
        Assert.Empty(candleStickVisualization.Highs);
        Assert.NotNull(candleStickVisualization.Lows);
        Assert.Empty(candleStickVisualization.Lows);
        Assert.NotNull(candleStickVisualization.Opens);
        Assert.Empty(candleStickVisualization.Opens);
        Assert.Equal(0, candleStickVisualization.ColumnSpan);
        Assert.Equal(0, candleStickVisualization.RowSpan);
        Assert.Null(candleStickVisualization.Description);
        Assert.NotNull(candleStickVisualization.Labels);
        Assert.Empty(candleStickVisualization.Labels);
        Assert.True(candleStickVisualization.IsTitleVisible);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Constructor_InitializesCandleStickVisualizationWithDataSourceItem_WhenDataSourceItemIsProvided(
        bool hasTabularData)
    {
        // Arrange
        var dataSourceItem = new DataSourceItem { HasTabularData = hasTabularData };

        // Act
        var candleStickVisualization = new CandleStickVisualization(dataSourceItem);

        // Assert
        Assert.Equal(ChartType.Candlestick, candleStickVisualization.ChartType);
        Assert.Equal(dataSourceItem, candleStickVisualization.DataDefinition.DataSourceItem);
        Assert.Null(candleStickVisualization.Title);
        Assert.Equal(hasTabularData, candleStickVisualization.DataDefinition.DataSourceItem.HasTabularData);
        Assert.IsType(
          hasTabularData
            ? typeof(TabularDataDefinition)
            : typeof(XmlaDataDefinition),
          candleStickVisualization.DataDefinition);
    }

    [Theory]
    [InlineData("Stock Prices", null, null)]
    [InlineData(null, null, null)]
    [InlineData("Stock Prices with Data Source", true, true)]
    [InlineData("Stock Prices without Tabular Data", false, false)]
    public void Constructor_SetsTitleAndDataSource_WhenArgumentsAreProvided(string title, bool? hasTabularData,
        bool? expectedHasTabularData)
    {
        // Arrange
        var dataSourceItem = hasTabularData.HasValue
            ? new DataSourceItem { HasTabularData = hasTabularData.Value }
            : null;

        // Act
        var candleStickVisualization = new CandleStickVisualization(title, dataSourceItem);

        // Assert
        Assert.Equal(title, candleStickVisualization.Title);
        Assert.Equal(ChartType.Candlestick, candleStickVisualization.ChartType);

        if (dataSourceItem == null)
        {
            Assert.Null(candleStickVisualization.DataDefinition);
        }
        else
        {
            Assert.NotNull(candleStickVisualization.DataDefinition);
            Assert.Equal(expectedHasTabularData, candleStickVisualization.DataDefinition.DataSourceItem.HasTabularData);
            Assert.IsType(
              hasTabularData.Value
                ? typeof(TabularDataDefinition)
                : typeof(XmlaDataDefinition),
              candleStickVisualization.DataDefinition);
        }
    }

    [Fact]
    public void ToJsonString_GeneratesCorrectJson_WhenCandleStickVisualizationIsSerialized()
    {
        //Arrange
        var expectedJson =
            """
            [ {
              "Description" : "Create Candle Stick Visualization",
              "Id" : "60344a4a-d0ce-4364-9f8c-acfbb8caa32e",
              "Title" : "Candle Stick Visualization",
              "IsTitleVisible" : true,
              "ColumnSpan" : 0,
              "RowSpan" : 0,
              "VisualizationSettings" : {
                "_type" : "SampleSchema",
                "LeftAxisLogarithmic" : false,
                "AxisTitlesMode" : "None",
                "ChartType" : "Candlestick",
                "VisualizationType" : "CHART"
              },
              "DataSpec" : {
                "_type" : "TabularDataSpecType",
                "IsTransposed" : false,
                "Fields" : [ {
                  "FieldName" : "Open",
                  "FieldLabel" : "Open",
                  "UserCaption" : "Open",
                  "IsCalculated" : false,
                  "Properties" : { },
                  "Sorting" : "None",
                  "FieldType" : "Number"
                }, {
                  "FieldName" : "High",
                  "FieldLabel" : "High",
                  "UserCaption" : "High",
                  "IsCalculated" : false,
                  "Properties" : { },
                  "Sorting" : "None",
                  "FieldType" : "Number"
                }, {
                  "FieldName" : "Low",
                  "FieldLabel" : "Low",
                  "UserCaption" : "Low",
                  "IsCalculated" : false,
                  "Properties" : { },
                  "Sorting" : "None",
                  "FieldType" : "Number"
                }, {
                  "FieldName" : "Close",
                  "FieldLabel" : "Close",
                  "UserCaption" : "Close",
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
                "_type" : "FinancialVisualizationDataSpecType",
                "Open" : [ {
                  "_type" : "MeasureColumnSpecType",
                  "SummarizationField" : {
                    "_type" : "SummarizationValueFieldType",
                    "FieldLabel" : "Open",
                    "UserCaption" : "Open",
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
                    "FieldName" : "Open"
                  }
                } ],
                "High" : [ {
                  "_type" : "MeasureColumnSpecType",
                  "SummarizationField" : {
                    "_type" : "SummarizationValueFieldType",
                    "FieldLabel" : "High",
                    "UserCaption" : "High",
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
                    "FieldName" : "High"
                  }
                } ],
                "Low" : [ {
                  "_type" : "MeasureColumnSpecType",
                  "SummarizationField" : {
                    "_type" : "SummarizationValueFieldType",
                    "FieldLabel" : "Low",
                    "UserCaption" : "Low",
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
                    "FieldName" : "Low"
                  }
                } ],
                "Close" : [ {
                  "_type" : "MeasureColumnSpecType",
                  "SummarizationField" : {
                    "_type" : "SummarizationValueFieldType",
                    "FieldLabel" : "Close",
                    "UserCaption" : "Close",
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
                    "FieldName" : "Close"
                  }
                } ],
                "FormatVersion" : 0,
                "AdHocExpandedElements" : [ ],
                "Rows" : [ {
                  "_type" : "DimensionColumnSpecType",
                  "SummarizationField" : {
                    "_type" : "SummarizationDateFieldType",
                    "DateAggregationType" : "Day",
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
                new NumberField("Open"),
                new NumberField("High"),
                new NumberField("Low"),
                new NumberField("Close")
            }
        };
        excelDataSourceItem.UseExcel("Marketing");

        document.Visualizations.Add(new CandleStickVisualization("Candle Stick Visualization", excelDataSourceItem)
            {
                Id = "60344a4a-d0ce-4364-9f8c-acfbb8caa32e",
                IsTitleVisible = true,
                Description = "Create Candle Stick Visualization"
            }
            .SetLabel(new DateDataField("Date") { AggregationType = DateAggregationType.Day })
            .SetOpen("Open")
            .SetHigh("High")
            .SetLow("Low")
            .SetClose("Close")
            .ConfigureSettings(settings =>
            {
                settings.SchemaTypeName = "SampleSchema";
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