using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations;

public class CategoryVisualizationBaseFixture
{
    [Fact]
    public void Constructor_FieldsHaveDefaultValues_WhenInstanceIsCreated()
    {
        // Act
        var visualization = new TestCategoryVisualizationBase();

        // Assert
        Assert.Null(visualization.Title);
        Assert.Null(visualization.DataDefinition);
        Assert.NotNull(visualization.Labels);
        Assert.Empty(visualization.Labels);
        Assert.NotNull(visualization.Values);
        Assert.Empty(visualization.Values);
        Assert.Null(visualization.Category);
        Assert.NotNull(visualization.VisualizationDataSpec);
    }
    
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Constructor_SetsDataSourceItem_WhenArgumentsAreProvided(bool hasTabularData)
    {
        // Arrange
        var dataSourceItem = new DataSourceItem { HasTabularData = hasTabularData };

        // Act
        var visualization = new TestCategoryVisualizationBase(dataSourceItem);

        // Assert
        Assert.Equal(dataSourceItem, visualization.DataDefinition.DataSourceItem);
    }

    [Theory]
    [InlineData("Test Title", true)]
    [InlineData(null, false)]
    public void Constructor_SetsTitleAndDataSourceItem_WhenArgumentsAreProvided(string title, bool hasTabularData)
    {
        // Arrange
        var dataSourceItem = new DataSourceItem { HasTabularData = hasTabularData };

        // Act
        var visualization = new TestCategoryVisualizationBase(title, dataSourceItem);

        // Assert
        Assert.Equal(title, visualization.Title);
        Assert.Equal(dataSourceItem, visualization.DataDefinition.DataSourceItem);
    }

    [Fact]
    public void Labels_ReturnsVisualizationSpecRows_WhenInitialized()
    {
        // Arrange
        var expectedLabels = new List<DimensionColumn> { new DimensionColumn(), new DimensionColumn() };
        var visualization = new TestCategoryVisualizationBase();
        visualization.VisualizationDataSpec.Rows = expectedLabels;

        // Act
        var labels = visualization.Labels;

        // Assert
        Assert.Equal(expectedLabels, labels);
    }

    [Fact]
    public void Values_ReturnsVisualizationSpecValues_WhenInitialized()
    {
        // Arrange
        var expectedValues = new List<MeasureColumn> { new MeasureColumn(), new MeasureColumn() };
        var visualization = new TestCategoryVisualizationBase();
        visualization.VisualizationDataSpec.Values = expectedValues;

        // Act
        var values = visualization.Values;

        // Assert
        Assert.Equal(expectedValues, values);
    }

    [Fact]
    public void Category_GetAndSetVisualizationSpecCategory_WhenInitialized()
    {
        // Arrange
        var expectedCategory = new DimensionColumn();
        var visualization = new TestCategoryVisualizationBase();

        // Act
        visualization.Category = expectedCategory;

        // Assert
        Assert.Equal(expectedCategory, visualization.Category);
        Assert.Equal(expectedCategory, visualization.VisualizationDataSpec.Category);
    }

    [Fact]
    public void ToJsonString_GeneratesCorrectJson_WhenSerialized()
    {
        // Arrange
        var expectedJson =
            """
            [ {
              "Id" : "6f29dedb-c353-42ba-aea0-577af7409dcf",
              "Title" : "Test Title",
              "IsTitleVisible" : true,
              "ColumnSpan" : 0,
              "RowSpan" : 0,
              "VisualizationSettings" : {
                "_type" : "ChartVisualizationSettingsType",
                "ChartType" : "Column",
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
                "Values" : [ ],
                "FixedLines": [],
                "FormatVersion" : 1,
                "AdHocExpandedElements" : [ ],
                "Rows" : [ ]
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
            }
        };
        excelDataSourceItem.UseExcel("Marketing");

        var visualization = new TestCategoryVisualizationBase("Test Title", excelDataSourceItem)
        {
            Id = "6f29dedb-c353-42ba-aea0-577af7409dcf"
        };
        
        document.Visualizations.Add(visualization);

        // Act
        var json = document.ToJsonString();
        var actualJson = JObject.Parse(json)["Widgets"];
        var actualNormalized = JsonConvert.SerializeObject(actualJson, Formatting.Indented);
        var expectedNormalized = JArray.Parse(expectedJson).ToString(Formatting.Indented);

        // Assert
        Assert.Equal(expectedNormalized.Trim(), actualNormalized.Trim());
    }

    private class TestCategoryVisualizationBase : CategoryVisualizationBase<TestChartVisualizationSettings>
    {
        public TestCategoryVisualizationBase() : base(null) { }
        public TestCategoryVisualizationBase(string title, DataSourceItem dataSourceItem) : base(title, dataSourceItem) { }
        public TestCategoryVisualizationBase(DataSourceItem dataSourceItem) : base(null, dataSourceItem) { }
    }

    private class TestChartVisualizationSettings : ChartVisualizationSettingsBase
    {
    }
}