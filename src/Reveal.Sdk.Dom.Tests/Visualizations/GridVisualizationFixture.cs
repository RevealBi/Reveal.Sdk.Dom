using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Serialization;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Filters;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations;

public class GridVisualizationFixture
{
    [Fact]
    public void Constructor_InitializesDefaultValues_WhenInstanceIsCreated()
    {
        // Act
        var gridVisualization = new GridVisualization();

        // Assert
        Assert.Equal(ChartType.Grid, gridVisualization.ChartType);
        Assert.NotNull(gridVisualization.Columns);
        Assert.Empty(gridVisualization.Columns);
        Assert.Null(gridVisualization.Title);
        Assert.NotNull(gridVisualization.VisualizationDataSpec);
        Assert.IsType<GridVisualizationDataSpec>(gridVisualization.VisualizationDataSpec);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Constructor_InitializesGridVisualizationWithDataSource_WhenDataSourceItemIsProvided(bool hasTabularData)
    {
        // Arrange
        var dataSourceItem = new DataSourceItem { HasTabularData = hasTabularData };

        // Act
        var gridVisualization = new GridVisualization(dataSourceItem);

        // Assert
        Assert.Equal(ChartType.Grid, gridVisualization.ChartType);
        Assert.NotNull(gridVisualization.Columns);
        Assert.Empty(gridVisualization.Columns);
        Assert.Null(gridVisualization.Title);
        Assert.IsType(
            hasTabularData
                ? typeof(TabularDataDefinition)
                : typeof(XmlaDataDefinition),
            gridVisualization.DataDefinition);    }

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
        var gridVisualization = new GridVisualization(title, dataSourceItem);

        // Assert
        Assert.Equal(title, gridVisualization.Title);
        Assert.Equal(ChartType.Grid, gridVisualization.ChartType);
        Assert.NotNull(gridVisualization.Columns);
        Assert.Empty(gridVisualization.Columns);
        Assert.NotNull(gridVisualization.VisualizationDataSpec);
        if (dataSourceItem == null)
        {
            Assert.Null(gridVisualization.DataDefinition);
        }
        else
        {
            Assert.NotNull(gridVisualization.DataDefinition);
            Assert.Equal(expectedHasTabularData, gridVisualization.DataDefinition.DataSourceItem.HasTabularData);
            Assert.IsType(
                hasTabularData.Value
                    ? typeof(TabularDataDefinition)
                    : typeof(XmlaDataDefinition),
                gridVisualization.DataDefinition);
        }
    }

    [Theory]
    [InlineData(null, 0)]
    [InlineData(typeof(GridVisualizationDataSpec), 2)]
    public void Columns_ReturnsExpectedCount_WhenVisualizationDataSpecIsSet(Type dataSpecType, int expectedCount)
    {
        // Arrange
        var gridVisualization = new GridVisualization();
        if (dataSpecType == typeof(GridVisualizationDataSpec))
            gridVisualization.VisualizationDataSpec = new GridVisualizationDataSpec
            {
                Columns = new List<TabularColumn>
                {
                    new() { FieldName = "Column1" },
                    new() { FieldName = "Column2" }
                }
            };

        // Act
        var columns = gridVisualization.Columns;

        // Assert
        Assert.NotNull(columns);
        Assert.Equal(expectedCount, columns.Count);
    }

    [Fact]
    public void VisualizationDataSpec_DefaultValue_IsGridVisualizationDataSpec()
    {
        // Arrange
        var gridVisualization = new GridVisualization();

        // Assert
        Assert.NotNull(gridVisualization.VisualizationDataSpec);
        Assert.IsType<GridVisualizationDataSpec>(gridVisualization.VisualizationDataSpec);
    }

    [Fact]
    public void ToJsonString_GeneratesCorrectJson_WhenGridVisualizationIsSerialized()
    {
        //Arrange
        var expectedJson =
            """
            [ {
              "Description" : "Create Grid Visualization",
              "Id" : "f4aec0ff-a73f-4797-840d-6553be4fddee",
              "Title" : "Grid",
              "IsTitleVisible" : true,
              "ColumnSpan" : 0,
              "RowSpan" : 0,
              "VisualizationSettings" : {
                "_type" : "GridVisualizationSettingsType",
                "PagedRows" : true,
                "PagedRowsSize" : 50,
                "FontSize" : "Large",
                "Style" : {
                  "FixedLeftColumns" : true,
                  "TextAlignment" : "Center",
                  "NumericAlignment" : "Inherit",
                  "DateAlignment" : "Center"
                },
                "VisualizationType" : "GRID"
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
                  "FieldName" : "Spend",
                  "FieldLabel" : "Spend",
                  "UserCaption" : "Spend",
                  "IsCalculated" : false,
                  "Properties" : { },
                  "Sorting" : "None",
                  "FieldType" : "Number"
                }, {
                  "FieldName" : "Conversions",
                  "FieldLabel" : "Conversions",
                  "UserCaption" : "Conversions",
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
                  "FieldType" : "String"
                } ],
                "TransposedFields" : [ ],
                "QuickFilters" : [ ],
                "AdditionalTables" : [ ],
                "ServiceAdditionalTables" : [ ],
                "DataSourceItem" : {
                  "_type" : "DataSourceItemType",
                  "Id" : "51030e9e-7f1d-4552-a252-0256f1880fac",
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
                    "Id" : "12aaddb8-42f4-4429-8c14-a4f212cca70f",
                    "Title" : "Marketing Sheet",
                    "Subtitle" : "Excel Data Source Item",
                    "DataSourceId" : "0bc5b4b0-8d9c-4a7e-9ac3-326c79dd28b4",
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
                "_type" : "GridVisualizationDataSpecType",
                "Columns" : [ {
                  "_type" : "TabularColumnSpecType",
                  "FieldName" : "Territory",
                  "Sorting" : "None"
                }, {
                  "_type" : "TabularColumnSpecType",
                  "FieldName" : "Conversions",
                  "Sorting" : "None"
                }, {
                  "_type" : "TabularColumnSpecType",
                  "FieldName" : "Spend",
                  "Sorting" : "None"
                } ]
              }
            } ]
            """;

        var document = new RdashDocument("My Dashboard");

        var excelDataSourceItem = new RestDataSourceItem("Marketing Sheet")
        {
            Id = "51030e9e-7f1d-4552-a252-0256f1880fac",
            Subtitle = "Excel Data Source Item",
            Url = "http://dl.infragistics.com/reportplus/reveal/samples/Samples.xlsx",
            IsAnonymous = true,
            ResourceItem = new DataSourceItem
            {
                Id = "12aaddb8-42f4-4429-8c14-a4f212cca70f",
                DataSourceId = "0bc5b4b0-8d9c-4a7e-9ac3-326c79dd28b4",
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
                new NumberField("Spend"),
                new NumberField("Conversions"),
                new TextField("Territory")
            }
        };
        excelDataSourceItem.UseExcel("Marketing");

        document.Visualizations.Add(new GridVisualization("Grid", excelDataSourceItem)
            {
                Id = "f4aec0ff-a73f-4797-840d-6553be4fddee",
                IsTitleVisible = true,
                Description = "Create Grid Visualization"
            }
            .SetColumns("Territory", "Conversions", "Spend")
            .ConfigureSettings(settings =>
            {
                settings.IsPagingEnabled = true;
                settings.IsFirstColumnFixed = true;
                settings.FontSize = FontSize.Large;
                settings.DateFieldAlignment = Alignment.Center;
                settings.TextFieldAlignment = Alignment.Center;
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