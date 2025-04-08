using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations;

public class ComboChartVisualizationFixture
{
    [Fact]
    public void Constructor_FieldsHaveDefaultValues_WhenInstanceIsCreated()
    {
        // Act
        var visualization = new ComboChartVisualization();

        // Assert
        Assert.Null(visualization.Title);
        Assert.Null(visualization.DataDefinition);
        Assert.Equal(ChartType.Combo, visualization.ChartType);
        Assert.NotNull(visualization.Chart1);
        Assert.Empty(visualization.Chart1);
        Assert.NotNull(visualization.Chart2);
        Assert.Empty(visualization.Chart2);
        Assert.NotNull(visualization.Labels);
        Assert.Empty(visualization.Labels);
    }

    [Fact]
    public void Constructor_SetsDataSourceItem_WhenOnlyDataSourceItemProvided()
    {
        // Arrange
        var dataSourceItem = new DataSourceItem();

        // Act
        var visualization = new ComboChartVisualization(dataSourceItem);

        // Assert
        Assert.Equal(dataSourceItem, visualization.DataDefinition.DataSourceItem);
        Assert.Equal(ChartType.Combo, visualization.ChartType);
    }

    [Fact]
    public void Constructor_SetsTitleAndDataSourceItem_WhenBothArgumentsAreProvided()
    {
        // Arrange
        var title = "Combo Chart Visualization";
        var dataSourceItem = new DataSourceItem();

        // Act
        var visualization = new ComboChartVisualization(title, dataSourceItem);

        // Assert
        Assert.Equal(title, visualization.Title);
        Assert.Equal(dataSourceItem, visualization.DataDefinition.DataSourceItem);
        Assert.Equal(ChartType.Combo, visualization.ChartType);
    }

    [Fact]
    public void Chart1_ReturnsVisualizationSpecChart1_WhenCalled()
    {
        // Arrange
        var expectedChart1 = new List<MeasureColumn> { new MeasureColumn(), new MeasureColumn() };
        var visualization = new ComboChartVisualization();
        visualization.VisualizationDataSpec.Chart1 = expectedChart1;

        // Act
        var chart1 = visualization.Chart1;

        // Assert
        Assert.Equal(expectedChart1, chart1);
        Assert.Equal(expectedChart1, visualization.VisualizationDataSpec.Chart1);
    }

    [Fact]
    public void Chart2_ReturnsVisualizationSpecChart2_WhenCalled()
    {
        // Arrange
        var expectedChart2 = new List<MeasureColumn> { new MeasureColumn(), new MeasureColumn() };
        var visualization = new ComboChartVisualization();
        visualization.VisualizationDataSpec.Chart2 = expectedChart2;

        // Act
        var chart2 = visualization.Chart2;

        // Assert
        Assert.Equal(expectedChart2, chart2);
        Assert.Equal(expectedChart2, visualization.VisualizationDataSpec.Chart2);
    }

    [Fact]
    public void Labels_ReturnsVisualizationSpecRows_WhenCalled()
    {
        // Arrange
        var expectedLabels = new List<DimensionColumn> { new DimensionColumn(), new DimensionColumn() };
        var visualization = new ComboChartVisualization();
        visualization.VisualizationDataSpec.Rows = expectedLabels;

        // Act
        var labels = visualization.Labels;

        // Assert
        Assert.Equal(expectedLabels, labels);
        Assert.Equal(expectedLabels, visualization.VisualizationDataSpec.Rows);
    }

    [Fact]
    public void ToJsonString_GeneratesCorrectJson_WhenSerialized()
    {
        // Arrange
        var expectedJson =
            """
            [ {
              "Id" : "080cc17d-4a0a-4837-aa3f-ef2571ea4432",
              "Title" : "Combo",
              "IsTitleVisible" : true,
              "ColumnSpan" : 0,
              "RowSpan" : 0,
              "VisualizationSettings" : {
                "_type" : "ChartVisualizationSettingsType",
                "CompositeChartTypesSwapped" : false,
                "CompositeChartType1" : "Column",
                "CompositeChartType2" : "Line",
                "RightAxisLogarithmic" : false,
                "SingleAxisMode" : true,
                "ShowAxisX" : true,
                "ShowAxisY" : true,
                "LeftAxisLogarithmic" : false,
                "AxisTitlesMode" : "None",
                "ShowLegends" : true,
                "BrushOffsetIndex" : 5,
                "ChartType" : "Composite",
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
                  "FieldName" : "Budget",
                  "FieldLabel" : "Budget",
                  "UserCaption" : "Budget",
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
                "_type" : "CompositeChartVisualizationDataSpecType",
                "Chart1" : [ {
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
                "Chart2" : [ {
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
                new NumberField("Budget"),
                new NumberField("Spend"),
            }
        };
        excelDataSourceItem.UseExcel("Marketing");

        document.Visualizations.Add(new ComboChartVisualization("Combo", excelDataSourceItem)
                { Id = "080cc17d-4a0a-4837-aa3f-ef2571ea4432" }
            .SetLabel(new DateDataField("Date") { AggregationType = DateAggregationType.Month })
            .SetChart1Value("Spend")
            .SetChart2Value("Budget")
            .ConfigureSettings(settings =>
            {
                settings.Chart1Type = ComboChartType.Column;
                settings.Chart2Type = ComboChartType.Line;
                settings.ShowRightAxis = false;
                settings.StartColorIndex = 5;
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