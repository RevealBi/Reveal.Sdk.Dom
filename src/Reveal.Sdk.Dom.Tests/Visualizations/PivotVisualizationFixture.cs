using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Core.Serialization;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Filters;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations;

public class PivotVisualizationFixture
{
    [Fact]
    public void Constructor_InitializesDefaultValues_WhenInstanceIsCreated()
    {
        // Act
        var pivotVisualization = new PivotVisualization();

        // Assert
        Assert.Equal(ChartType.Pivot, pivotVisualization.ChartType);
        Assert.NotNull(pivotVisualization.Columns);
        Assert.Empty(pivotVisualization.Columns);
        Assert.NotNull(pivotVisualization.Rows);
        Assert.Empty(pivotVisualization.Rows);
        Assert.NotNull(pivotVisualization.Values);
        Assert.Empty(pivotVisualization.Values);
        Assert.Null(pivotVisualization.Title);
        Assert.NotNull(pivotVisualization.VisualizationDataSpec);
        Assert.IsType<PivotVisualizationDataSpec>(pivotVisualization.VisualizationDataSpec);
    }

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
        var pivotVisualization = new PivotVisualization(title, dataSourceItem);

        // Assert
        Assert.Equal(title, pivotVisualization.Title);
        Assert.Equal(ChartType.Pivot, pivotVisualization.ChartType);
        Assert.NotNull(pivotVisualization.Columns);
        Assert.Empty(pivotVisualization.Columns);
        Assert.NotNull(pivotVisualization.Rows);
        Assert.Empty(pivotVisualization.Rows);
        Assert.NotNull(pivotVisualization.Values);
        Assert.Empty(pivotVisualization.Values);
        Assert.NotNull(pivotVisualization.VisualizationDataSpec);

        if (dataSourceItem != null)
        {
            Assert.Equal(dataSourceItem, pivotVisualization.DataDefinition.DataSourceItem);
            Assert.IsType(
                hasTabularData.Value
                    ? typeof(TabularDataDefinition)
                    : typeof(XmlaDataDefinition),
                pivotVisualization.DataDefinition);
        }
    }
    
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Constructor_InitializesPivotVisualizationWithDataSource_WhenDataSourceItemIsProvided(bool hasTabularData)
    {
        // Arrange
        var dataSourceItem = new DataSourceItem { HasTabularData = hasTabularData};

        // Act
        var pivotVisualization = new PivotVisualization(dataSourceItem);

        // Assert
        Assert.Equal(ChartType.Pivot, pivotVisualization.ChartType);
        Assert.NotNull(pivotVisualization.Columns);
        Assert.Empty(pivotVisualization.Columns);
        Assert.Null(pivotVisualization.Title);
        Assert.IsType(
            hasTabularData
                ? typeof(TabularDataDefinition)
                : typeof(XmlaDataDefinition),
            pivotVisualization.DataDefinition);    }
    
    [Fact]
    public void Columns_ReturnsExpectedColumns_WhenInitialize()
    {
        // Arrange
        var expectedColumns = new List<DimensionColumn>
        {
            new() { DataField = new MockDimensionDataField("Column1") },
            new() { DataField = new MockDimensionDataField("Column2") }
        };

        var pivotVisualization = new PivotVisualization
        {
            VisualizationDataSpec = new PivotVisualizationDataSpec
            {
                Columns = expectedColumns
            }
        };

        // Act
        var columns = pivotVisualization.Columns;

        // Assert
        Assert.Equal(expectedColumns, columns);
    }

    [Fact]
    public void Rows_ReturnsExpectedRows_WhenInitialize()
    {
        // Arrange
        var expectedRows = new List<DimensionColumn>
        {
            new() { DataField = new MockDimensionDataField("Row1") },
            new() { DataField = new MockDimensionDataField("Row2") }
        };

        var pivotVisualization = new PivotVisualization
        {
            VisualizationDataSpec = new PivotVisualizationDataSpec
            {
                Rows = expectedRows
            }
        };

        // Act
        var rows = pivotVisualization.Rows;

        // Assert
        Assert.Equal(expectedRows, rows);
    }

    [Fact]
    public void Values_ReturnsExpectedValues_WhenInitialize()
    {
        // Arrange
        var expectedValues = new List<MeasureColumn>
        {
            new() { DataField = new NumberDataField("Value1") },
            new() { DataField = new NumberDataField("Value2") }
        };

        var pivotVisualization = new PivotVisualization
        {
            VisualizationDataSpec = new PivotVisualizationDataSpec
            {
                Values = expectedValues
            }
        };

        // Act
        var values = pivotVisualization.Values;

        // Assert
        Assert.Equal(expectedValues, values);
    }

    [Fact]
    public void ToJsonString_GeneratesCorrectJson_WhenPivotVisualizationIsSerialized()
    {
        //Arrange
        var expectedJson = """
                           [
                             {
                               "Description": "Create Grid Visualization",
                               "Id": "60344a4a-d0ce-4364-9f8c-acfbb8caa32e",
                               "Title": "Grid",
                               "IsTitleVisible": true,
                               "ColumnSpan": 0,
                               "RowSpan": 0,
                               "VisualizationSettings": {
                                 "_type": "PivotVisualizationSettingsType",
                                 "FontSize": "Large",
                                 "Style": {
                                   "FixedLeftColumns": false,
                                   "TextAlignment": "Center",
                                   "NumericAlignment": "Inherit",
                                   "DateAlignment": "Center"
                                 },
                                 "VisualizationType": "PIVOT"
                               },
                               "DataSpec": {
                                 "_type": "TabularDataSpecType",
                                 "IsTransposed": false,
                                 "Fields": [
                                   {
                                     "FieldName": "Date",
                                     "FieldLabel": "Date",
                                     "UserCaption": "Date",
                                     "IsCalculated": false,
                                     "Properties": {},
                                     "Sorting": "None",
                                     "FieldType": "Date"
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
                                     "FieldName": "Conversions",
                                     "FieldLabel": "Conversions",
                                     "UserCaption": "Conversions",
                                     "IsCalculated": false,
                                     "Properties": {},
                                     "Sorting": "None",
                                     "FieldType": "Number"
                                   },
                                   {
                                     "FieldName": "Territory",
                                     "FieldLabel": "Territory",
                                     "UserCaption": "Territory",
                                     "IsCalculated": false,
                                     "Properties": {},
                                     "Sorting": "None",
                                     "FieldType": "String"
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
                                 "_type": "PivotVisualizationDataSpecType",
                                 "Columns": [
                                   {
                                     "_type": "DimensionColumnSpecType",
                                     "SummarizationField": {
                                       "_type": "SummarizationRegularFieldType",
                                       "DrillDownElements": [],
                                       "ExpandedItems": [],
                                       "FieldName": "Territory"
                                     }
                                   },
                                   {
                                     "_type": "DimensionColumnSpecType",
                                     "SummarizationField": {
                                       "_type": "SummarizationRegularFieldType",
                                       "DrillDownElements": [],
                                       "ExpandedItems": [],
                                       "FieldName": "Conversions"
                                     }
                                   },
                                   {
                                     "_type": "DimensionColumnSpecType",
                                     "SummarizationField": {
                                       "_type": "SummarizationRegularFieldType",
                                       "DrillDownElements": [],
                                       "ExpandedItems": [],
                                       "FieldName": "Spend"
                                     }
                                   }
                                 ],
                                 "Values": [],
                                 "ShowGrandTotals": false,
                                 "FormatVersion": 0,
                                 "AdHocExpandedElements": [],
                                 "Rows": []
                               }
                             }
                           ]
                           """;

        //Act
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
                new NumberField("Spend"),
                new NumberField("Conversions"),
                new TextField("Territory")
            }
        };
        excelDataSourceItem.UseExcel("Marketing");

        document.Visualizations.Add(new PivotVisualization("Grid", excelDataSourceItem)
            {
                Id = "60344a4a-d0ce-4364-9f8c-acfbb8caa32e",
                IsTitleVisible = true,
                Description = "Create Grid Visualization"
            }
            .SetColumns("Territory", "Conversions", "Spend")
            .ConfigureSettings(settings =>
            {
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
    
    private class MockDimensionDataField : IDimensionDataField
    {
        public MockDimensionDataField(string fieldName)
        {
            FieldName = fieldName;
        }

        public string Caption { get; set; }
        public IDictionary<string, object> Properties { get; } = new Dictionary<string, object>();

        public string FieldName { get; set; }
        public string Description { get; set; }
    }
}