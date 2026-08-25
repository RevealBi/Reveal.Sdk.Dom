using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations;

public class SparklineVisualizationFixture
{
    [Fact]
    public void Constructor_FieldsHaveDefaultValues_WhenInstanceIsCreated()
    {
        // Act
        var visualization = new SparklineVisualization();

        // Assert
        Assert.Null(visualization.Title);
        Assert.Null(visualization.DataDefinition);
        Assert.Equal(ChartType.Sparkline, visualization.ChartType);
        Assert.NotNull(visualization.Values);
        Assert.Empty(visualization.Values);
        Assert.NotNull(visualization.Categories);
        Assert.Empty(visualization.Categories);
        Assert.Null(visualization.Date);
    }

    [Fact]
    public void Constructor_SetsDataSourceItem_WhenOnlyDataSourceItemProvided()
    {
        // Arrange
        var dataSourceItem = new DataSourceItem();

        // Act
        var visualization = new SparklineVisualization(dataSourceItem);

        // Assert
        Assert.Equal(dataSourceItem, visualization.DataDefinition.DataSourceItem);
        Assert.Equal(ChartType.Sparkline, visualization.ChartType);
    }

    [Fact]
    public void Constructor_SetsTitleAndDataSourceItem_WhenBothArgumentsAreProvided()
    {
        // Arrange
        var title = "Sparkline Visualization";
        var dataSourceItem = new DataSourceItem();

        // Act
        var visualization = new SparklineVisualization(title, dataSourceItem);

        // Assert
        Assert.Equal(title, visualization.Title);
        Assert.Equal(dataSourceItem, visualization.DataDefinition.DataSourceItem);
        Assert.Equal(ChartType.Sparkline, visualization.ChartType);
    }

    [Fact]
    public void Date_ReturnsCorrectValue_WhenCalled()
    {
        // Arrange
        var expectedDate = new DimensionColumn();
        var visualization = new SparklineVisualization();

        // Act
        visualization.Date = expectedDate;

        // Assert
        Assert.Equal(expectedDate, visualization.Date);
        Assert.Equal(expectedDate, visualization.VisualizationDataSpec.Date);
    }

    [Fact]
    public void Values_ReturnsVisualizationSpecValue_WhenCalled()
    {
        // Arrange
        var expectedValues = new List<MeasureColumn> { new MeasureColumn(), new MeasureColumn() };
        var visualization = new SparklineVisualization();
        visualization.VisualizationDataSpec.Value = expectedValues;

        // Act
        var values = visualization.Values;

        // Assert
        Assert.Equal(expectedValues, values);
    }

    [Fact]
    public void Categories_ReturnsVisualizationSpecRows_WhenCalled()
    {
        // Arrange
        var expectedCategories = new List<DimensionColumn> { new DimensionColumn(), new DimensionColumn() };
        var visualization = new SparklineVisualization();
        visualization.VisualizationDataSpec.Rows = expectedCategories;

        // Act
        var categories = visualization.Categories;

        // Assert
        Assert.Equal(expectedCategories, categories);
    }

    [Fact]
    public void ToJsonString_GeneratesCorrectJson_WhenSerialized()
    {
        // Arrange
        var expectedJson =
            """
            [ {
              "Id" : "d23a48c2-1234-4a2b-bd6e-cf4d5f6b1243",
              "Title" : "Sparkline Visualization",
              "IsTitleVisible" : true,
              "ColumnSpan" : 0,
              "RowSpan" : 0,
              "VisualizationSettings" : {
                "_type" : "SparklineVisualizationSettingsType",
                "ChartType" : "Line",
                "PositiveIsRed" : false,
                "ShowLastTwoValues" : false,
                "ShowDifference" : true,
                "VisualizationColumns" : [],
                "FontSize" : "Small",
                "Style" : {
                  "FixedLeftColumns" : false,
                  "TextAlignment" : "Inherit",
                  "NumericAlignment" : "Inherit",
                  "DateAlignment" : "Inherit"
                },
                "VisualizationType" : "SPARKLINE"
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
                  "FieldName" : "Territory",
                  "FieldLabel" : "Territory",
                  "UserCaption" : "Territory",
                  "IsCalculated" : false,
                  "Properties" : { },
                  "Sorting" : "None",
                  "FieldType" : "Number"
                }, {
                  "FieldName" : "Spend",
                  "FieldLabel" : "Spend",
                  "UserCaption" : "Spend",
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
                "_type" : "SparklineVisualizationDataSpecType",
                "NumberOfPeriods" : 10,
                "ShowIndicator" : true,
                "IndicatorType" : "LastMonths",
                "Date" : {
                  "_type" : "DimensionColumnSpecType",
                  "SummarizationField" : {
                    "_type" : "SummarizationDateFieldType",
                    "DateAggregationType" : "Year",
                    "DrillDownElements" : [ ],
                    "ExpandedItems" : [ ],
                    "FieldName" : "Date"
                  }
                },
                "Value" : [ {
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
                new NumberField("Territory"),
                new NumberField("Spend"),
            }
        };
        excelDataSourceItem.UseExcel("Marketing");

        document.Visualizations.Add(new SparklineVisualization("Sparkline Visualization", excelDataSourceItem)
        {
            Id = "d23a48c2-1234-4a2b-bd6e-cf4d5f6b1243",
            
        }
        .SetDate("Date")
        .SetValue("Spend")
        .SetCategory("Territory")
        .ConfigureSettings(settings =>
        {
            settings.ShowLastTwoValues = false;
            settings.NumberOfPeriods = 10;
            settings.AggregationType = SparklineAggregationType.Months;
        }));

        // Act
        var json = document.ToJsonString();
        var actualJson = JObject.Parse(json)["Widgets"];
        var actualNormalized = JsonConvert.SerializeObject(actualJson, Formatting.Indented);
        var expectedNormalized = JArray.Parse(expectedJson).ToString(Formatting.Indented);

        // Assert
        Assert.Equal(expectedNormalized.Trim(), actualNormalized.Trim());
    }
}