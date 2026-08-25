using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Settings;

public class GridVisualizationSettingsFixture
{
    [Fact]
    public void Constructor_FieldsHaveDefaultValues_WhenInstanceIsCreated()
    {
        // Act
        var settings = new GridVisualizationSettings();

        // Assert
        Assert.Equal(SchemaTypeNames.GridVisualizationSettingsType, settings.SchemaTypeName);
        Assert.Equal(VisualizationTypes.GRID, settings.VisualizationType);
        Assert.Empty(settings.ColumnSettings);
        Assert.Empty(settings.GroupedColumns);
        Assert.Empty(settings.SortedColumns);
        Assert.Empty(settings.SummarizedColumns);
        Assert.False(settings.IsPagingEnabled);
        Assert.Equal(50, settings.PageSize);
        Assert.False(settings.IsFirstColumnFixed);
    }

    [Fact]
    public void ToJsonString_GeneratesCorrectJson_WhenSerialized()
    {
        // Arrange
        var expectedJson =
            """
            {
              "_type" : "GridVisualizationSettingsType",
              "GroupedColumns" : [],
              "SortedColumns" : [],
              "SummarizedColumns" : [],
              "PagedRows" : true,
              "PagedRowsSize" : 25,
              "FontSize" : "Small",
              "VisualizationColumns" : [],
              "Style" : {
                "FixedLeftColumns" : false,
                "TextAlignment" : "Left",
                "NumericAlignment" : "Right",
                "DateAlignment" : "Center"
              },
              "VisualizationType" : "GRID"
            }
            """;

        var settings = new GridVisualizationSettings
        {
            IsPagingEnabled = true,
            PageSize = 25,
            FontSize = FontSize.Small,
            DateFieldAlignment = Alignment.Center,
            NumericFieldAlignment = Alignment.Right,
            TextFieldAlignment = Alignment.Left,
            IsFirstColumnFixed = false
        };

        // Act
        var actualJson = settings.ToJsonString();
        var expectedJObject = JObject.Parse(expectedJson);
        var actualJObject = JObject.Parse(actualJson);

        // Assert
        Assert.Equal(expectedJObject, actualJObject);
    }

    [Fact]
    public void ColumnCollections_GenerateCorrectJson_WhenSerialized()
    {
        // Arrange
        var settings = new GridVisualizationSettings();
        settings.ColumnSettings.Add(new GridColumnSettings("Website")
        {
            Width = 240,
            TextAlignment = Alignment.Left,
            PinPosition = GridColumnPinPosition.Left,
            Hyperlink = new GridColumnHyperlink(
                new UrlLink("Open website", "https://example.com", UrlLinkTarget.SameTab),
                "Visit website")
        });
        settings.GroupedColumns.Add(new GridColumnGrouping("Country"));
        settings.GroupedColumns.Add(new GridColumnGrouping("State", SortingType.Desc));
        settings.SortedColumns.Add(new GridColumnSort("Revenue", SortingType.Desc));
        settings.SortedColumns.Add(new GridColumnSort("Date"));
        settings.SummarizedColumns.Add(new GridColumnSummary("Revenue"));
        settings.SummarizedColumns.Add(new GridColumnSummary("Margin", GridSummaryType.Average));

        var expectedCollections = JObject.Parse(
            """
            {
              "VisualizationColumns": [
                {
                  "ColumnName": "Website",
                  "Width": 240.0,
                  "TextAlignment": "Left",
                  "Pinning": "Left",
                  "Hyperlink": {
                    "Action": {
                      "Url": "https://example.com",
                      "Target": "Self",
                      "Parameters": [],
                      "Title": "Open website",
                      "Type": "OpenUrl"
                    },
                    "DisplayTextTemplate": "Visit website"
                  }
                }
              ],
              "GroupedColumns": [
                { "ColumnName": "Country", "SortDirection": "Asc" },
                { "ColumnName": "State", "SortDirection": "Desc" }
              ],
              "SortedColumns": [
                { "ColumnName": "Revenue", "SortDirection": "Desc" },
                { "ColumnName": "Date", "SortDirection": "Asc" }
              ],
              "SummarizedColumns": [
                { "ColumnName": "Revenue", "Operand": "Sum" },
                { "ColumnName": "Margin", "Operand": "Average" }
              ]
            }
            """);

        // Act
        var actualJson = JObject.Parse(settings.ToJsonString());

        // Assert
        Assert.Equal(expectedCollections["VisualizationColumns"], actualJson["VisualizationColumns"]);
        Assert.Equal(expectedCollections["GroupedColumns"], actualJson["GroupedColumns"]);
        Assert.Equal(expectedCollections["SortedColumns"], actualJson["SortedColumns"]);
        Assert.Equal(expectedCollections["SummarizedColumns"], actualJson["SummarizedColumns"]);
    }

    [Fact]
    public void ColumnCollections_CreatePublicDomTypes_WhenDeserialized()
    {
        // Arrange
        var json =
            """
            {
              "VisualizationColumns": [
                {
                  "ColumnName": "Website",
                  "Width": 180.0,
                  "TextAlignment": "Center",
                  "Pinning": "Right",
                  "Hyperlink": {
                    "Action": {
                      "Type": "OpenUrl",
                      "Title": "Details",
                      "Url": "https://example.com/details",
                      "Target": "Blank"
                    },
                    "DisplayTextTemplate": "View details"
                  }
                }
              ],
              "GroupedColumns": [
                { "ColumnName": "Region", "SortDirection": "Desc" }
              ],
              "SortedColumns": [
                { "ColumnName": "OrderDate", "SortDirection": "Asc" }
              ],
              "SummarizedColumns": [
                { "ColumnName": "OrderId", "Operand": "Count" }
              ]
            }
            """;

        // Act
        var settings = JsonConvert.DeserializeObject<GridVisualizationSettings>(json);

        // Assert
        var column = Assert.Single(settings.ColumnSettings);
        Assert.Equal("Website", column.ColumnName);
        Assert.Equal(180, column.Width);
        Assert.Equal(Alignment.Center, column.TextAlignment);
        Assert.Equal(GridColumnPinPosition.Right, column.PinPosition);
        Assert.Equal("View details", column.Hyperlink.DisplayTextTemplate);

        var link = Assert.IsType<UrlLink>(column.Hyperlink.Link);
        Assert.Equal("https://example.com/details", link.Url);
        Assert.Equal(UrlLinkTarget.NewTab, link.Target);

        var grouping = Assert.Single(settings.GroupedColumns);
        Assert.IsType<GridColumnGrouping>(grouping);
        Assert.Equal("Region", grouping.ColumnName);
        Assert.Equal(SortingType.Desc, grouping.SortDirection);

        var sort = Assert.Single(settings.SortedColumns);
        Assert.IsType<GridColumnSort>(sort);
        Assert.Equal("OrderDate", sort.ColumnName);
        Assert.Equal(SortingType.Asc, sort.SortDirection);

        var summary = Assert.Single(settings.SummarizedColumns);
        Assert.IsType<GridColumnSummary>(summary);
        Assert.Equal("OrderId", summary.ColumnName);
        Assert.Equal(GridSummaryType.Count, summary.SummaryType);
    }

    [Fact]
    public void GridOnlyColumnCollections_AreNotSerializedForPivotOrSparklineSettings()
    {
        // Act
        var pivotJson = JObject.Parse(new PivotVisualizationSettings().ToJsonString());
        var sparklineJson = JObject.Parse(new SparklineVisualizationSettings().ToJsonString());

        // Assert
        Assert.Null(pivotJson["GroupedColumns"]);
        Assert.Null(pivotJson["SortedColumns"]);
        Assert.Null(pivotJson["SummarizedColumns"]);
        Assert.Null(sparklineJson["GroupedColumns"]);
        Assert.Null(sparklineJson["SortedColumns"]);
        Assert.Null(sparklineJson["SummarizedColumns"]);
    }

    [Fact]
    public void IsFirstColumnFixed_SetAndGet_UpdatesValues()
    {
        // Arrange
        var settings = new GridVisualizationSettings();

        // Act
        settings.IsFirstColumnFixed = true;

        // Assert
        Assert.True(settings.IsFirstColumnFixed);
        Assert.True(settings.Style.FixedLeftColumns);
    }
}
