using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations;

public class KpiTimeVisualizationFixture
{
    [Fact]
    public void Constructor_FieldsHaveDefaultValues_WhenInstanceIsCreated()
    {
        // Act
        var visualization = new KpiTimeVisualization();

        // Assert
        Assert.Null(visualization.Title);
        Assert.Null(visualization.DataDefinition);
        Assert.Equal(ChartType.KpiTime, visualization.ChartType);
        Assert.Null(visualization.Date);
        Assert.NotNull(visualization.Values);
        Assert.Empty(visualization.Values);
        Assert.NotNull(visualization.Categories);
        Assert.Empty(visualization.Categories);
        Assert.NotNull(visualization.VisualizationDataSpec);
    }

    [Fact]
    public void Constructor_SetsDataSourceItem_WhenOnlyDataSourceItemProvided()
    {
        // Arrange
        var dataSourceItem = new DataSourceItem();

        // Act
        var visualization = new KpiTimeVisualization(dataSourceItem);

        // Assert
        Assert.Equal(dataSourceItem, visualization.DataDefinition.DataSourceItem);
        Assert.Equal(ChartType.KpiTime, visualization.ChartType);
        Assert.Null(visualization.Title);
    }

    [Fact]
    public void Constructor_SetsTitleAndDataSourceItem_WhenBothArgumentsAreProvided()
    {
        // Arrange
        var title = "KPI Time Visualization";
        var dataSourceItem = new DataSourceItem();

        // Act
        var visualization = new KpiTimeVisualization(title, dataSourceItem);

        // Assert
        Assert.Equal(title, visualization.Title);
        Assert.Equal(dataSourceItem, visualization.DataDefinition.DataSourceItem);
        Assert.Equal(ChartType.KpiTime, visualization.ChartType);
    }

    [Fact]
    public void Date_ReturnsCorrectValue_WhenCalled()
    {
        // Arrange
        var expectedDateColumn = new DimensionColumn();
        var visualization = new KpiTimeVisualization();

        // Act
        visualization.Date = expectedDateColumn;

        // Assert
        Assert.Equal(expectedDateColumn, visualization.Date);
        Assert.Equal(expectedDateColumn, visualization.VisualizationDataSpec.Date);
    }

    [Fact]
    public void Values_ReturnsVisualizationSpecValues_WhenCalled()
    {
        // Arrange
        var expectedValues = new List<MeasureColumn> { new MeasureColumn(), new MeasureColumn() };
        var visualization = new KpiTimeVisualization();
        visualization.VisualizationDataSpec.Value = expectedValues;

        // Act
        var values = visualization.Values;

        // Assert
        Assert.Equal(expectedValues, values);
        Assert.Equal(expectedValues, visualization.VisualizationDataSpec.Value);
    }

    [Fact]
    public void Categories_ReturnsVisualizationSpecRows_WhenCalled()
    {
        // Arrange
        var expectedCategories = new List<DimensionColumn> { new DimensionColumn(), new DimensionColumn() };
        var visualization = new KpiTimeVisualization();
        visualization.VisualizationDataSpec.Rows = expectedCategories;

        // Act
        var categories = visualization.Categories;

        // Assert
        Assert.Equal(expectedCategories, categories);
        Assert.Equal(expectedCategories, visualization.VisualizationDataSpec.Rows);
        
    }

    [Fact]
    public void ToJsonString_GeneratesCorrectJson_WhenSerialized()
    {
        // Arrange
        var expectedJson =
            """
            [ {
              "Id" : "a12345c6-7b8d-90e1-2345-6789fghijklm",
              "Title" : "KPI Time",
              "IsTitleVisible" : true,
              "ColumnSpan" : 0,
              "RowSpan" : 0,
              "VisualizationSettings" : {
                "_type" : "IndicatorVisualizationSettingsType",
                "DifferenceMode" : "ValueAndPercentage",
                "PositiveIsRed" : false,
                "IncludeToday" : true,
                "VisualizationType" : "INDICATOR"
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
                  "FieldName" : "Traffic",
                  "FieldLabel" : "Traffic",
                  "UserCaption" : "Traffic",
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
                "_type" : "IndicatorVisualizationDataSpecType",
                "IndicatorType" : "MonthToDatePreviousMonth",
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
                    "FieldLabel" : "Traffic",
                    "UserCaption" : "Traffic",
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
                    "FieldName" : "Traffic"
                  }
                } ],
                "FormatVersion" : 0,
                "AdHocExpandedElements" : [ ],
                "Rows" : [ ]
              }
            } ]
            """;

        var document = new RdashDocument("KPI Dashboard");

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
                new NumberField("Traffic"),
            }
        };
        excelDataSourceItem.UseExcel("Marketing");

        document.Visualizations.Add(new KpiTimeVisualization("KPI Time", excelDataSourceItem)
        {
            Id = "a12345c6-7b8d-90e1-2345-6789fghijklm"
        }
        .SetDate("Date")
        .SetValue("Traffic")
        .ConfigureSettings(settings =>
        {
            settings.DifferenceMode = IndicatorDifferenceMode.ValueAndPercentage;
            settings.TimePeriod = KpiTimePeriod.MonthToDatePreviousMonth;
        }));

        // Act
        var json = document.ToJsonString();
        var actualJson = JObject.Parse(json)["Widgets"];
        var actualNormalized = JsonConvert.SerializeObject(actualJson, Formatting.Indented);
        var expectedNormalized = JArray.Parse(expectedJson).ToString(Formatting.Indented);

        // Assert
        Assert.Equal(expectedNormalized.Trim(), actualNormalized.Trim());
    }

    [Fact]
    public void Deserialize_MaintainsSharedReference_BetweenVisualizationAndSettings()
    {
        // Arrange - Create and serialize
        var document = new RdashDocument("KPI Dashboard");
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
                new NumberField("Traffic"),
            }
        };
        excelDataSourceItem.UseExcel("Marketing");

        document.Visualizations.Add(new KpiTimeVisualization("KPI Time", excelDataSourceItem)
        .SetDate("Date")
        .SetValue("Traffic")
        .ConfigureSettings(settings =>
        {
            settings.DifferenceMode = IndicatorDifferenceMode.ValueAndPercentage;
            settings.TimePeriod = KpiTimePeriod.MonthToDatePreviousMonth;
        }));

        var json = document.ToJsonString();

        // Act - ACTUALLY DESERIALIZE from JSON (not just reference the existing object!)
        var deserializedDocument = RdashDocument.LoadFromJson(json);
        var visualization = (KpiTimeVisualization)deserializedDocument.Visualizations[0];

        // Assert - verify the shared reference is maintained AFTER deserialization
        Assert.NotNull(visualization.Settings.VisualizationDataSpec);
        Assert.Same(visualization.VisualizationDataSpec, visualization.Settings.VisualizationDataSpec);

        // Verify TimePeriod works correctly after deserialization
        visualization.Settings.TimePeriod = KpiTimePeriod.QuarterToDatePreviousQuarter;
        Assert.Equal(IndicatorVisualizationType.QuarterToDatePreviousQuarter,
                     visualization.VisualizationDataSpec.IndicatorType);
    }
}