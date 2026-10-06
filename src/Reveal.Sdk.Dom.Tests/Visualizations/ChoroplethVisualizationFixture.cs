using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations;

public class ChoroplethVisualizationFixture
{
    [Fact]
    public void Constructor_FieldsHaveDefaultValues_WhenInstanceIsCreated()
    {
        // Act
        var visualization = new ChoroplethVisualization();

        // Assert
        Assert.Null(visualization.Title);
        Assert.Null(visualization.DataDefinition);
        Assert.Equal(ChartType.Choropleth, visualization.ChartType);
        Assert.NotNull(visualization.Locations);
        Assert.Empty(visualization.Locations);
        Assert.NotNull(visualization.Values);
        Assert.Empty(visualization.Values);
        Assert.Null(visualization.MapColor);
        Assert.Null(visualization.Map);
    }

    [Fact]
    public void Constructor_SetsDataSourceItem_WhenOnlyDataSourceItemProvided()
    {
        // Arrange
        var dataSourceItem = new DataSourceItem();

        // Act
        var visualization = new ChoroplethVisualization(dataSourceItem);

        // Assert
        Assert.Equal(dataSourceItem, visualization.DataDefinition.DataSourceItem);
        Assert.Equal(ChartType.Choropleth, visualization.ChartType);
    }

    [Fact]
    public void Constructor_SetsTitleAndDataSourceItem_WhenBothArgumentsAreProvided()
    {
        // Arrange
        var title = "Test Choropleth Visualization";
        var dataSourceItem = new DataSourceItem();

        // Act
        var visualization = new ChoroplethVisualization(title, dataSourceItem);

        // Assert
        Assert.Equal(title, visualization.Title);
        Assert.Equal(dataSourceItem, visualization.DataDefinition.DataSourceItem);
        Assert.Equal(ChartType.Choropleth, visualization.ChartType);
    }

    [Fact]
    public void Map_ReturnsCorrectValue_WhenCalled()
    {
        // Arrange
        var settings = new ChoroplethVisualizationSettings { Region = "US" };
        var visualization = new ChoroplethVisualization
        {
            Settings = settings
        };

        // Act
        visualization.Map = "Canada";

        // Assert
        Assert.Equal("Canada", visualization.Map);
        Assert.Equal("Canada", settings.Region);
    }

    [Fact]
    public void Locations_ReturnsVisualizationSpecRows_WhenCalled()
    {
        // Arrange
        var expectedLocations = new List<DimensionColumn> { new DimensionColumn(), new DimensionColumn() };
        var visualization = new ChoroplethVisualization();
        visualization.VisualizationDataSpec.Rows = expectedLocations;

        // Act
        var locations = visualization.Locations;

        // Assert
        Assert.Equal(expectedLocations, locations);
    }

    [Fact]
    public void MapColor_GetAndSet_ReturnsCorrectValue()
    {
        // Arrange
        var expectedMapColor = new DimensionColumn();
        var visualization = new ChoroplethVisualization();

        // Act
        visualization.MapColor = expectedMapColor;

        // Assert
        Assert.Equal(expectedMapColor, visualization.MapColor);
        Assert.Equal(expectedMapColor, visualization.VisualizationDataSpec.MapColor);
    }

    [Fact]
    public void Values_ReturnsVisualizationSpecValues_WhenCalled()
    {
        // Arrange
        var expectedValues = new List<MeasureColumn> { new MeasureColumn(), new MeasureColumn() };
        var visualization = new ChoroplethVisualization();
        visualization.VisualizationDataSpec.Value = expectedValues;

        // Act
        var values = visualization.Values;

        // Assert
        Assert.Equal(expectedValues, values);
    }

    [Fact]
    public void ToJsonString_GeneratesCorrectJson_WhenSerialized()
    {
        // Arrange
        var expectedJson =
            """
            [ {
              "Id" : "60344a4a-d0ce-4364-9f8c-acfbb8caa32e",
              "Title" : "Choropleth",
              "IsTitleVisible" : true,
              "ColumnSpan" : 0,
              "RowSpan" : 0,
              "VisualizationSettings" : {
                "_type" : "ChoroplethMapVisualizationSettingsType",
                "ColorizationStyle" : "SINGLE",
                "LabelVisibility" : "HAS VALUES",
                "LabelStyle" : "VALUES",
                "DataLocale" : "en",
                "ShowLegends" : true,
                "Region" : "USA States",
                "ColorIndex" : 5,
                "VisualizationType" : "CHOROPLETH_MAP"
              },
              "DataSpec" : {
                "_type" : "TabularDataSpecType",
                "IsTransposed" : false,
                "Fields" : [ {
                  "FieldName" : "State",
                  "FieldLabel" : "State",
                  "UserCaption" : "State",
                  "IsCalculated" : false,
                  "Properties" : { },
                  "Sorting" : "None",
                  "FieldType" : "String"
                }, {
                  "FieldName" : "Revenue",
                  "FieldLabel" : "Revenue",
                  "UserCaption" : "Revenue",
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
                  "Subtitle" : "Choropleth Data Source Item",
                  "DataSourceId" : "__JSON",
                  "HasTabularData" : true,
                  "HasAsset" : false,
                  "Properties" : { },
                  "Parameters" : {
                    "config" : {
                      "iterationDepth" : 0,
                      "columnsConfig" : [ {
                        "key" : "State",
                        "type" : 0
                      }, {
                        "key" : "Revenue",
                        "type" : 1
                      } ]
                    }
                  },
                  "ResourceItem" : {
                    "_type" : "DataSourceItemType",
                    "Id" : "d593dd79-7161-4929-afc9-c26393f5b650",
                    "Title" : "Marketing Sheet",
                    "Subtitle" : "Excel Data Source Item",
                    "DataSourceId" : "33077d1e-19c5-44fe-b981-6765af3156a6",
                    "HasTabularData" : true,
                    "HasAsset" : false,
                    "Properties" : {
                      "Url" : "https://excel2json.io/api/share/818e7b9a-f463-4565-435d-08da496bf5f2"
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
                "_type" : "ChoroplethMapVisualizationDataSpecType",
                "Value" : [ {
                  "_type" : "MeasureColumnSpecType",
                  "SummarizationField" : {
                    "_type" : "SummarizationValueFieldType",
                    "FieldLabel" : "Revenue",
                    "UserCaption" : "Revenue",
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
                    "FieldName" : "Revenue"
                  }
                } ],
                "FormatVersion" : 1,
                "AdHocExpandedElements" : [ ],
                "Rows" : [ {
                  "_type" : "DimensionColumnSpecType",
                  "SummarizationField" : {
                    "_type" : "SummarizationRegularFieldType",
                    "DrillDownElements" : [ ],
                    "ExpandedItems" : [ ],
                    "FieldName" : "State"
                  }
                } ]
              }
            } ]
            """;

        var document = new RdashDocument("My Dashboard");

        var excelDataSourceItem = new RestDataSourceItem("Marketing Sheet")
        {
            Id = "080cc17d-4a0a-4837-aa3f-ef2571ea443a",
            Subtitle = "Choropleth Data Source Item",
            Url = "https://excel2json.io/api/share/818e7b9a-f463-4565-435d-08da496bf5f2",
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
                    { "Url", "https://excel2json.io/api/share/818e7b9a-f463-4565-435d-08da496bf5f2" }
                }
            },
            Fields = new List<IField>
            {
                new TextField("State"),
                new NumberField("Revenue"),
            }
        };

        document.Visualizations.Add(new ChoroplethVisualization("Choropleth", excelDataSourceItem)
            {
                Id = "60344a4a-d0ce-4364-9f8c-acfbb8caa32e",
            }
            .SetMap(Maps.NorthAmerica.UnitedStates.States.AllStates)
            .SetLocation("State")
            .SetValue("Revenue")
            .ConfigureSettings(settings =>
            {
                settings.ColorIndex = 5;
                settings.ColorStyle = MapColorStyle.SingleColor;
                settings.LabelStyle = MapLabelStyle.Values;
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