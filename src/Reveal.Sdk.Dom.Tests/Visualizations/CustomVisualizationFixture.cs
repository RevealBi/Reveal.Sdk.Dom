using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations;

public class CustomVisualizationFixture
{
    [Fact]
    public void Constructor_FieldsHaveDefaultValues_WhenInstanceIsCreated()
    {
        // Act
        var visualization = new CustomVisualization();

        // Assert
        Assert.Null(visualization.Title);
        Assert.Null(visualization.DataDefinition);
        Assert.Equal(ChartType.Custom, visualization.ChartType);
        Assert.NotNull(visualization.Rows);
        Assert.Empty(visualization.Rows);
        Assert.NotNull(visualization.Values);
        Assert.Empty(visualization.Values);
        Assert.NotNull(visualization.Columns);
        Assert.Empty(visualization.Columns);
        Assert.Null(visualization.Url);
    }

    [Fact]
    public void Constructor_SetsDataSourceItem_WhenOnlyDataSourceItemProvided()
    {
        // Arrange
        var dataSourceItem = new DataSourceItem();

        // Act
        var visualization = new CustomVisualization(dataSourceItem);

        // Assert
        Assert.Equal(dataSourceItem, visualization.DataDefinition.DataSourceItem);
        Assert.Equal(ChartType.Custom, visualization.ChartType);
    }

    [Fact]
    public void Constructor_SetsTitleAndDataSourceItem_WhenBothArgumentsAreProvided()
    {
        // Arrange
        var title = "Custom Visualization";
        var dataSourceItem = new DataSourceItem();

        // Act
        var visualization = new CustomVisualization(title, dataSourceItem);

        // Assert
        Assert.Equal(title, visualization.Title);
        Assert.Equal(dataSourceItem, visualization.DataDefinition.DataSourceItem);
        Assert.Equal(ChartType.Custom, visualization.ChartType);
    }

    [Fact]
    public void Rows_ReturnsVisualizationSpecRows_WhenCalled()
    {
        // Arrange
        var expectedRows = new List<DimensionColumn> { new DimensionColumn(), new DimensionColumn() };
        var visualization = new CustomVisualization();
        visualization.VisualizationDataSpec.Rows = expectedRows;

        // Act
        var rows = visualization.Rows;

        // Assert
        Assert.Equal(expectedRows, rows);
        Assert.Equal(expectedRows, visualization.VisualizationDataSpec.Rows);
    }

    [Fact]
    public void Values_ReturnsVisualizationSpecValues_WhenCalled()
    {
        // Arrange
        var expectedValues = new List<MeasureColumn> { new MeasureColumn(), new MeasureColumn() };
        var visualization = new CustomVisualization();
        visualization.VisualizationDataSpec.Values = expectedValues;

        // Act
        var values = visualization.Values;

        // Assert
        Assert.Equal(expectedValues, values);
        Assert.Equal(expectedValues, visualization.VisualizationDataSpec.Values);
    }

    [Fact]
    public void Columns_ReturnsVisualizationSpecColumns_WhenCalled()
    {
        // Arrange
        var expectedColumns = new List<DimensionColumn> { new DimensionColumn(), new DimensionColumn() };
        var visualization = new CustomVisualization();
        visualization.VisualizationDataSpec.Columns = expectedColumns;

        // Act
        var columns = visualization.Columns;

        // Assert
        Assert.Equal(expectedColumns, columns);
        Assert.Equal(expectedColumns, visualization.VisualizationDataSpec.Columns);
    }

    [Fact]
    public void Url_GetAndSet_ReturnsCorrectValue()
    {
        // Arrange
        var expectedUrl = "https://example.com";
        var settings = new CustomVisualizationSettings { Url = expectedUrl };
        var visualization = new CustomVisualization
        {
            Settings = settings
        };

        // Act
        visualization.Url = "https://test.com";

        // Assert
        Assert.Equal("https://test.com", visualization.Url);
        Assert.Equal("https://test.com", settings.Url);
    }

    [Fact]
    public void ToJsonString_GeneratesCorrectJson_WhenSerialized()
    {
        // Arrange
        var expectedJson =
            """
            [ {
              "Id" : "33077d1e-19c5-44fe-b981-6765af3156a8",
              "Title" : "Custom",
              "IsTitleVisible" : true,
              "ColumnSpan" : 0,
              "RowSpan" : 0,
              "VisualizationSettings" : {
                "_type" : "DIYVisualizationSettingsType",
                "Url" : "https://dl.infragistics.com/reportplus/diy/HelloWorld-Desktop-EN.html",
                "VisualizationType" : "JS_EXTENSION"
              },
              "DataSpec" : {
                "_type" : "TabularDataSpecType",
                "IsTransposed" : false,
                "Fields" : [ {
                  "FieldName" : "CampaignID",
                  "FieldLabel" : "CampaignID",
                  "UserCaption" : "CampaignID",
                  "IsCalculated" : false,
                  "Properties" : { },
                  "Sorting" : "None",
                  "FieldType" : "Number"
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
                "_type" : "PivotVisualizationDataSpecType",
                "Columns" : [ ],
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
                "ShowGrandTotals" : false,
                "FormatVersion" : 1,
                "AdHocExpandedElements" : [ ],
                "Rows" : [ {
                  "_type" : "DimensionColumnSpecType",
                  "SummarizationField" : {
                    "_type" : "SummarizationRegularFieldType",
                    "DrillDownElements" : [ ],
                    "ExpandedItems" : [ ],
                    "FieldName" : "Territory"
                  }
                }, {
                  "_type" : "DimensionColumnSpecType",
                  "SummarizationField" : {
                    "_type" : "SummarizationRegularFieldType",
                    "DrillDownElements" : [ ],
                    "ExpandedItems" : [ ],
                    "FieldName" : "CampaignID"
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
                new NumberField("CampaignID"),
                new NumberField("Territory"),
                new NumberField("Spend"),
                new NumberField("Budget"),
            }
        };
        excelDataSourceItem.UseExcel("Marketing");

        document.Visualizations.Add(new CustomVisualization("Custom", excelDataSourceItem)
            {
                Id = "33077d1e-19c5-44fe-b981-6765af3156a8"
            }
            .SetUrl("https://dl.infragistics.com/reportplus/diy/HelloWorld-Desktop-EN.html")
            .SetRows("Territory", "CampaignID")
            .SetValues("Spend", "Budget"));

        // Act
        var json = document.ToJsonString();
        var actualJson = JObject.Parse(json)["Widgets"];
        var actualNormalized = JsonConvert.SerializeObject(actualJson, Formatting.Indented);
        var expectedNormalized = JArray.Parse(expectedJson).ToString(Formatting.Indented);

        // Assert
        Assert.Equal(expectedNormalized.Trim(), actualNormalized.Trim());
    }
}