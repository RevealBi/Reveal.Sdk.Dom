using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations;

public class ScatterMapVisualizationFixture
{
    [Fact]
    public void Constructor_FieldsHaveDefaultValues_WhenInstanceIsCreated()
    {
        // Act
        var visualization = new ScatterMapVisualization();

        // Assert
        Assert.Null(visualization.Title);
        Assert.Null(visualization.DataDefinition);
        Assert.Equal(ChartType.ScatterMap, visualization.ChartType);
        Assert.Null(visualization.Label);
        Assert.Null(visualization.Map);
        Assert.Null(visualization.Latitude);
        Assert.Null(visualization.Longitude);
        Assert.NotNull(visualization.MapColor);
        Assert.Empty(visualization.MapColor);
        Assert.Null(visualization.MapColorCategory);
        Assert.NotNull(visualization.BubbleRadius);
        Assert.Empty(visualization.BubbleRadius);
        Assert.True(visualization.VisualizationDataSpec.IsColorByValue);
    }

    [Fact]
    public void Constructor_SetsDataSourceItem_WhenOnlyDataSourceItemProvided()
    {
        // Arrange
        var dataSourceItem = new DataSourceItem();

        // Act
        var visualization = new ScatterMapVisualization(dataSourceItem);

        // Assert
        Assert.Null(visualization.Title);
        Assert.Equal(dataSourceItem, visualization.DataDefinition.DataSourceItem);
        Assert.Equal(ChartType.ScatterMap, visualization.ChartType);
    }

    [Fact]
    public void Constructor_SetsTitleAndDataSourceItem_WhenBothArgumentsAreProvided()
    {
        // Arrange
        var title = "Scatter Map Test";
        var dataSourceItem = new DataSourceItem();

        // Act
        var visualization = new ScatterMapVisualization(title, dataSourceItem);

        // Assert
        Assert.Equal(title, visualization.Title);
        Assert.Equal(dataSourceItem, visualization.DataDefinition.DataSourceItem);
        Assert.Equal(ChartType.ScatterMap, visualization.ChartType);
    }

    [Fact]
    public void Label_ReturnsCorrectValue_WhenCalled()
    {
        // Arrange
        var labelColumn = new DimensionColumn();
        var visualization = new ScatterMapVisualization();

        // Act
        visualization.Label = labelColumn;

        // Assert
        Assert.Equal(labelColumn, visualization.Label);
        Assert.Equal(labelColumn, visualization.VisualizationDataSpec.Label);
    }

    [Fact]
    public void Map_ReturnsCorrectValue_WhenCalled()
    {
        // Arrange
        var settings = new ScatterMapVisualizationSettings { Region = "North America" };
        var visualization = new ScatterMapVisualization
        {
            Settings = settings
        };

        // Act
        visualization.Map = "Europe";

        // Assert
        Assert.Equal("Europe", visualization.Map);
        Assert.Equal("Europe", settings.Region);
    }

    [Fact]
    public void Latitude_ReturnsCorrectValue_WhenCalled()
    {
        // Arrange
        var latitudeColumn = new DimensionColumn();
        var visualization = new ScatterMapVisualization();

        // Act
        visualization.Latitude = latitudeColumn;

        // Assert
        Assert.Equal(latitudeColumn, visualization.Latitude);
        Assert.Equal(latitudeColumn, visualization.VisualizationDataSpec.Location);
    }

    [Fact]
    public void Longitude_ReturnsCorrectValue_WhenCalled()
    {
        // Arrange
        var longitudeColumn = new DimensionColumn();
        var visualization = new ScatterMapVisualization();

        // Act
        visualization.Longitude = longitudeColumn;

        // Assert
        Assert.Equal(longitudeColumn, visualization.Longitude);
        Assert.Equal(longitudeColumn, visualization.VisualizationDataSpec.Longitude);
    }

    [Fact]
    public void MapColor_ReturnsCorrectValue_WhenCalled()
    {
        // Arrange
        var mapColorColumns = new List<MeasureColumn> { new MeasureColumn(), new MeasureColumn() };
        var visualization = new ScatterMapVisualization();

        // Act
        visualization.MapColor = mapColorColumns;

        // Assert
        Assert.Equal(mapColorColumns, visualization.MapColor);
        Assert.Equal(mapColorColumns, visualization.VisualizationDataSpec.MapColor);
    }

    [Fact]
    public void MapColorCategory_UpdatesColorByValueFlag_WhenCalled()
    {
        // Arrange
        var categoryColumn = new DimensionColumn();
        var visualization = new ScatterMapVisualization();

        // Act
        visualization.MapColorCategory = categoryColumn;

        // Assert
        Assert.Equal(categoryColumn, visualization.MapColorCategory);
        Assert.Equal(categoryColumn, visualization.VisualizationDataSpec.MapColorCategory);
        Assert.False(visualization.VisualizationDataSpec.IsColorByValue);
    }

    [Fact]
    public void ToJsonString_GeneratesCorrectJson_WhenSerialized()
    {
        // Arrange
        var expectedJson =
            """
            [ {
              "Id" : "01bf8a56-4832-4e4d-a123-bc123f41b30f",
              "Title" : "Scatter Map Visualization",
              "IsTitleVisible" : true,
              "ColumnSpan" : 0,
              "RowSpan" : 0,
              "VisualizationSettings" : {
                "_type" : "ScatterMapVisualizationSettingsType",
                "ConditionalFormatting" : {
                  "Bands" : [ {
                    "_type" : "ConditionalFormattingBandType",
                    "Type" : "Percentage",
                    "Color" : "Green",
                    "Value" : 90.0
                  }, {
                    "_type" : "ConditionalFormattingBandType",
                    "Type" : "Percentage",
                    "Color" : "Yellow",
                    "Value" : 60.0
                  }, {
                    "_type" : "ConditionalFormattingBandType",
                    "Type" : "Percentage",
                    "Color" : "Red"
                  } ]
                },
                "ColorizationMode" : "ConditionalFormatting",
                "ZoomThreshold" : 3,
                "ShowTileSource" : true,
                "UseDifferentMarkers" : false,
                "ZoomRectangle" : {
                  "X" : 1.38,
                  "Y" : 41.65,
                  "Width" : 1.04,
                  "Height" : 0.39
                },
                "ShowLegends" : true,
                "Region" : "Illinois",
                "ColorIndex" : 5,
                "VisualizationType" : "SCATTER_MAP"
              },
              "DataSpec" : {
                "_type" : "TabularDataSpecType",
                "IsTransposed" : false,
                "Fields" : [ {
                  "FieldName" : "School_Nm",
                  "FieldLabel" : "School_Nm",
                  "UserCaption" : "School_Nm",
                  "IsCalculated" : false,
                  "Properties" : { },
                  "Sorting" : "None",
                  "FieldType" : "String"
                }, {
                  "FieldName" : "Y",
                  "FieldLabel" : "Y",
                  "UserCaption" : "Y",
                  "IsCalculated" : false,
                  "Properties" : { },
                  "Sorting" : "None",
                  "FieldType" : "Number"
                }, {
                  "FieldName" : "X",
                  "FieldLabel" : "X",
                  "UserCaption" : "X",
                  "IsCalculated" : false,
                  "Properties" : { },
                  "Sorting" : "None",
                  "FieldType" : "Number"
                }, {
                  "FieldName" : "Grades",
                  "FieldLabel" : "Grades",
                  "UserCaption" : "Grades",
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
                "_type" : "ScatterMapVisualizationDataSpecType",
                "IsSingleLocationField" : false,
                "IsColorByValue" : true,
                "Location" : {
                  "_type" : "DimensionColumnSpecType",
                  "SummarizationField" : {
                    "_type" : "SummarizationRegularFieldType",
                    "DrillDownElements" : [ ],
                    "ExpandedItems" : [ ],
                    "FieldName" : "Y"
                  }
                },
                "Longitude" : {
                  "_type" : "DimensionColumnSpecType",
                  "SummarizationField" : {
                    "_type" : "SummarizationRegularFieldType",
                    "DrillDownElements" : [ ],
                    "ExpandedItems" : [ ],
                    "FieldName" : "X"
                  }
                },
                "Label" : {
                  "_type" : "DimensionColumnSpecType",
                  "SummarizationField" : {
                    "_type" : "SummarizationRegularFieldType",
                    "DrillDownElements" : [ ],
                    "ExpandedItems" : [ ],
                    "FieldName" : "School_Nm"
                  }
                },
                "MapColor" : [ {
                  "_type" : "MeasureColumnSpecType",
                  "SummarizationField" : {
                    "_type" : "SummarizationValueFieldType",
                    "FieldLabel" : "Grades",
                    "UserCaption" : "Grades",
                    "IsHidden" : false,
                    "AggregationType" : "Avg",
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
                    "FieldName" : "Grades"
                  }
                } ],
                "Radius" : [ ]
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
                new TextField("School_Nm"),
                new NumberField("Y"),
                new NumberField("X"),
                new NumberField("Grades"),
            }
        };
        excelDataSourceItem.UseExcel("Marketing");

        document.Visualizations.Add(new ScatterMapVisualization("Scatter Map Visualization", excelDataSourceItem)
        {
            Id = "01bf8a56-4832-4e4d-a123-bc123f41b30f",
        }
        .SetMap(Maps.NorthAmerica.UnitedStates.States.Illinois)
        .SetLongitude("X")
        .SetLatitude("Y")
        .SetLabel("School_Nm")
        .SetColorByValue(new NumberDataField("Grades") { AggregationType = AggregationType.Avg })             
        .ConfigureSettings(settings =>
        {
            settings.ColorIndex = 5;
            settings.ColorMode = MapColorMode.ConditionalFormatting;
                    
            settings.ConditionalFormatting.ValueComparisonType = ValueComparisonType.Percentage;
            settings.ConditionalFormatting.UpperBand.Value = 90;
            settings.ConditionalFormatting.MiddleBand.Value = 60;

            settings.Zoom.Longitude = 1.38;
            settings.Zoom.Latitude = 41.65;
            settings.Zoom.DegreesLongitude = 1.04;
            settings.Zoom.DegreesLatitude = 0.39;
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