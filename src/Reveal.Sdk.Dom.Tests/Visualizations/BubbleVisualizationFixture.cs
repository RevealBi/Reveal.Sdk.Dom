using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations;

public class BubbleVisualizationFixture
{
    [Fact]
    public void Constructor_FieldsHaveDefaultValues_WhenInstanceIsCreated()
    {
        // Act
        var visualization = new BubbleVisualization();

        // Assert
        Assert.Null(visualization.Title);
        Assert.Null(visualization.DataDefinition);
        Assert.Equal(ChartType.Bubble, visualization.ChartType);
        Assert.NotNull(visualization.Labels);
        Assert.Empty(visualization.Labels);
        Assert.NotNull(visualization.XAxes);
        Assert.Empty(visualization.XAxes);
        Assert.NotNull(visualization.YAxes);
        Assert.Empty(visualization.YAxes);
        Assert.NotNull(visualization.Radius);
        Assert.Empty(visualization.Radius);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Constructor_SetsTitleAndDataSourceItem_WhenArgumentsAreProvided(bool hasTabularData)
    {
        // Arrange
        var title = "Bubble Chart";
        var dataSourceItem = new DataSourceItem { HasTabularData = hasTabularData };

        // Act
        var visualization = new BubbleVisualization(title, dataSourceItem);

        // Assert
        Assert.Equal(title, visualization.Title);
        Assert.Equal(dataSourceItem, visualization.DataDefinition.DataSourceItem);
        Assert.Equal(ChartType.Bubble, visualization.ChartType);
    }
    
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Constructor_SetsDataSourceItem_WhenArgumentsAreProvided(bool hasTabularData)
    {
        // Arrange
        var dataSourceItem = new DataSourceItem { HasTabularData = hasTabularData };

        // Act
        var visualization = new BubbleVisualization(dataSourceItem);

        // Assert
        Assert.Equal(dataSourceItem, visualization.DataDefinition.DataSourceItem);
        Assert.Equal(ChartType.Bubble, visualization.ChartType);
    }
    
    [Fact]
    public void Labels_ReturnsVisualizationSpecRows_WhenInitialized()
    {
        // Arrange
        var expectedLabels = new List<DimensionColumn> { new DimensionColumn(), new DimensionColumn() };
        var visualization = new BubbleVisualization();
        visualization.VisualizationDataSpec.Rows = expectedLabels;

        // Act
        var labels = visualization.Labels;

        // Assert
        Assert.Equal(expectedLabels, labels);
    }

    [Fact]
    public void XAxes_ReturnsVisualizationSpecXAxis_WhenInitialized()
    {
        // Arrange
        var expectedXAxes = new List<MeasureColumn> { new MeasureColumn(), new MeasureColumn() };
        var visualization = new BubbleVisualization();
        visualization.VisualizationDataSpec.XAxis = expectedXAxes;

        // Act
        var xAxes = visualization.XAxes;

        // Assert
        Assert.Equal(expectedXAxes, xAxes);
    }

    [Fact]
    public void YAxes_ReturnsVisualizationSpecYAxis_WhenInitialized()
    {
        // Arrange
        var expectedYAxes = new List<MeasureColumn> { new MeasureColumn(), new MeasureColumn() };
        var visualization = new BubbleVisualization();
        visualization.VisualizationDataSpec.YAxis = expectedYAxes;

        // Act
        var yAxes = visualization.YAxes;

        // Assert
        Assert.Equal(expectedYAxes, yAxes);
    }

    [Fact]
    public void Radius_ReturnsVisualizationSpecRadius_WhenInitialized()
    {
        // Arrange
        var expectedRadius = new List<MeasureColumn> { new MeasureColumn(), new MeasureColumn() };
        var visualization = new BubbleVisualization();
        visualization.VisualizationDataSpec.Radius = expectedRadius;

        // Act
        var radius = visualization.Radius;

        // Assert
        Assert.Equal(expectedRadius, radius);
    }

    [Fact]
    public void ToJsonString_GeneratesCorrectJson_WhenSerialized()
    {
        // Arrange
        var expectedJson =
            """
            [
              {
                "Description": "Bubble Visualization",
                "Id": "60344a4a-d0ce-4364-9f8c-acfbb8caa32e",
                "Title": "Bubble Visualization",
                "IsTitleVisible": true,
                "ColumnSpan": 0,
                "RowSpan": 0,
                "VisualizationSettings": {
                  "_type": "ChartVisualizationSettingsType",
                  "RightAxisLogarithmic": false,
                  "ShowAxisX": true,
                  "ShowAxisY": true,
                  "LeftAxisLogarithmic": true,
                  "AxisTitlesMode" : "None",
                  "ShowLegends": true,
                  "BrushOffsetIndex": 1,
                  "ChartType": "Bubble",
                  "VisualizationType": "CHART"
                },
                "DataSpec": {
                  "_type": "TabularDataSpecType",
                  "IsTransposed": false,
                  "Fields": [
                    {
                      "FieldName": "CampaignID",
                      "FieldLabel": "CampaignID",
                      "UserCaption": "CampaignID",
                      "IsCalculated": false,
                      "Properties": {},
                      "Sorting": "None",
                      "FieldType": "String"
                    },
                    {
                      "FieldName": "Budget",
                      "FieldLabel": "Budget",
                      "UserCaption": "Budget",
                      "IsCalculated": false,
                      "Properties": {},
                      "Sorting": "None",
                      "FieldType": "Number"
                    },
                    {
                      "FieldName": "Spend",
                      "FieldLabel": "Spend",
                      "UserCaption": "Spend",
                      "IsCalculated": false,
                      "Properties": {},
                      "Sorting": "None",
                      "FieldType": "Number"
                    },
                    {
                      "FieldName": "Traffic",
                      "FieldLabel": "Traffic",
                      "UserCaption": "Traffic",
                      "IsCalculated": false,
                      "Properties": {},
                      "Sorting": "None",
                      "FieldType": "Number"
                    }
                  ],
                  "TransposedFields": [],
                  "QuickFilters": [],
                  "AdditionalTables": [],
                  "ServiceAdditionalTables": [],
                  "DataSourceItem": {
                    "_type": "DataSourceItemType",
                    "Id": "080cc17d-4a0a-4837-aa3f-ef2571ea443a",
                    "Title": "Marketing Sheet",
                    "Subtitle": "Excel Data Source Item",
                    "DataSourceId": "__EXCEL",
                    "HasTabularData": true,
                    "HasAsset": false,
                    "Properties": {
                      "Sheet": "Marketing"
                    },
                    "Parameters": {},
                    "ResourceItem": {
                      "_type": "DataSourceItemType",
                      "Id": "d593dd79-7161-4929-afc9-c26393f5b650",
                      "Title": "Marketing Sheet",
                      "Subtitle": "Excel Data Source Item",
                      "DataSourceId": "33077d1e-19c5-44fe-b981-6765af3156a6",
                      "HasTabularData": true,
                      "HasAsset": false,
                      "Properties": {
                        "Url": "http://dl.infragistics.com/reportplus/reveal/samples/Samples.xlsx"
                      },
                      "Parameters": {}
                    }
                  },
                  "Expiration": 1440,
                  "Bindings": {
                    "Bindings": []
                  }
                },
                "VisualizationDataSpec": {
                  "_type": "BubbleVisualizationDataSpecType",
                  "Radius": [
                    {
                      "_type": "MeasureColumnSpecType",
                      "SummarizationField": {
                        "_type": "SummarizationValueFieldType",
                        "FieldLabel": "Traffic",
                        "UserCaption": "Traffic",
                        "IsHidden": false,
                        "AggregationType": "Sum",
                        "Sorting": "None",
                        "IsCalculated": false,
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
                        "FieldName": "Traffic"
                      }
                    }
                  ],
                  "XAxis": [
                    {
                      "_type": "MeasureColumnSpecType",
                      "SummarizationField": {
                        "_type": "SummarizationValueFieldType",
                        "FieldLabel": "Budget",
                        "UserCaption": "Budget",
                        "IsHidden": false,
                        "AggregationType": "Sum",
                        "Sorting": "None",
                        "IsCalculated": false,
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
                        "FieldName": "Budget"
                      }
                    }
                  ],
                  "YAxis": [
                    {
                      "_type": "MeasureColumnSpecType",
                      "SummarizationField": {
                        "_type": "SummarizationValueFieldType",
                        "FieldLabel": "Spend",
                        "UserCaption": "Spend",
                        "IsHidden": false,
                        "AggregationType": "Sum",
                        "Sorting": "None",
                        "IsCalculated": false,
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
                        "FieldName": "Spend"
                      }
                    }
                  ],
                  "FormatVersion": 0,
                  "AdHocExpandedElements": [],
                  "Rows": [
                    {
                      "_type": "DimensionColumnSpecType",
                      "SummarizationField": {
                        "_type": "SummarizationRegularFieldType",
                        "DrillDownElements": [],
                        "ExpandedItems": [],
                        "FieldName": "CampaignID"
                      }
                    }
                  ]
                }
              }
            ]
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
                new TextField("CampaignID"),
                new NumberField("Budget"),
                new NumberField("Spend"),
                new NumberField("Traffic"),
            }
        };
        excelDataSourceItem.UseExcel("Marketing");

        document.Visualizations.Add(new BubbleVisualization("Bubble Visualization", excelDataSourceItem)
            {
                Id = "60344a4a-d0ce-4364-9f8c-acfbb8caa32e",
                IsTitleVisible = true,
                Description = "Bubble Visualization"
            }
            .SetLabel("CampaignID")
            .SetXAxis("Budget")
            .SetYAxis("Spend")
            .SetRadius("Traffic")
            .ConfigureSettings(settings =>
            {
                settings.ShowLegend = true;
                settings.YAxisIsLogarithmic = true;
                settings.StartColorIndex = 1;
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