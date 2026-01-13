using Reveal.Sdk.Dom.Core.Utilities;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Converters
{
    public class VisualizationConverterFixture
    {
        [Fact]
        public void ToGrid_ThrowsArgumentNullException_WhenSourceVisualizationIsNull()
        {
            // Act & Assert
            Assert.Throws<System.ArgumentNullException>(() => VisualizationConverter.ToGrid(null));
        }

        [Fact]
        public void ToGrid_ReturnsNull_WhenVisualizationIsGrid()
        {
            // Arrange
            var dataSourceItem = new RestDataSourceItem("Test");
            var gridViz = new GridVisualization(dataSourceItem);

            // Act
            var result = VisualizationConverter.ToGrid(gridViz);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void ToGrid_ReturnsNull_WhenVisualizationIsTextBox()
        {
            // Arrange
            var textBoxViz = new TextBoxVisualization();

            // Act
            var result = VisualizationConverter.ToGrid(textBoxViz);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void ToGrid_ReturnsNull_WhenVisualizationIsCustom()
        {
            // Arrange
            var dataSourceItem = new RestDataSourceItem("Test");
            var customViz = new CustomVisualization(dataSourceItem);

            // Act
            var result = VisualizationConverter.ToGrid(customViz);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void ToGrid_ReturnsNull_WhenVisualizationIsImage()
        {
            // Arrange
            var imageViz = new ImageVisualization();

            // Act
            var result = VisualizationConverter.ToGrid(imageViz);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void ToGrid_ConvertsColumnChart_WithVisualizationFields()
        {
            // Arrange
            var dataSourceItem = new RestDataSourceItem("Test");
            var columnChart = new ColumnChartVisualization(dataSourceItem)
                .SetLabel("Region")
                .SetValue(new NumberDataField("Sales"));
            columnChart.Title = "Sales by Region";
            columnChart.Id = "test-id";

            // Act
            var result = VisualizationConverter.ToGrid(columnChart);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("test-id", result.Id);
            Assert.Equal("Sales by Region", result.Title);
            Assert.Equal(2, result.Columns.Count);
            Assert.Contains(result.Columns, c => c.FieldName == "Region");
            Assert.Contains(result.Columns, c => c.FieldName == "Sales");
        }

        [Fact]
        public void ToGrid_ConvertsColumnChart_WithAllFields()
        {
            // Arrange
            var dataSourceItem = new RestDataSourceItem("Test");
            dataSourceItem.Fields.Add(new TextField("Region"));
            dataSourceItem.Fields.Add(new NumberField("Sales"));
            dataSourceItem.Fields.Add(new NumberField("Profit"));
            dataSourceItem.Fields.Add(new DateField("Date"));

            var columnChart = new ColumnChartVisualization(dataSourceItem)
                .SetLabel("Region")
                .SetValue(new NumberDataField("Sales"));

            var options = new GridConversionOptions { IncludeAllFields = true };

            // Act
            var result = VisualizationConverter.ToGrid(columnChart, options);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(4, result.Columns.Count);
            Assert.Contains(result.Columns, c => c.FieldName == "Region");
            Assert.Contains(result.Columns, c => c.FieldName == "Sales");
            Assert.Contains(result.Columns, c => c.FieldName == "Profit");
            Assert.Contains(result.Columns, c => c.FieldName == "Date");
        }

        [Fact]
        public void ToGrid_PreservesVisualizationProperties()
        {
            // Arrange
            var dataSourceItem = new RestDataSourceItem("Test");
            var columnChart = new ColumnChartVisualization(dataSourceItem)
            {
                Id = "viz-123",
                Title = "Test Chart",
                IsTitleVisible = false,
                BackgroundColor = "#FF0000",
                ColumnSpan = 6,
                RowSpan = 4,
                Description = "Test Description"
            };
            columnChart.SetLabel(new TextDataField("Category"));

            // Act
            var result = VisualizationConverter.ToGrid(columnChart);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("viz-123", result.Id);
            Assert.Equal("Test Chart", result.Title);
            Assert.False(result.IsTitleVisible);
            Assert.Equal("#FF0000", result.BackgroundColor);
            Assert.Equal(6, result.ColumnSpan);
            Assert.Equal(4, result.RowSpan);
            Assert.Equal("Test Description", result.Description);
        }

        [Fact]
        public void ToGrid_ConvertsPivotVisualization()
        {
            // Arrange
            var dataSourceItem = new RestDataSourceItem("Test");
            var pivotViz = new PivotVisualization(dataSourceItem);
            pivotViz.Rows.Add(new DimensionColumn(new TextDataField("Category")));
            pivotViz.Columns.Add(new DimensionColumn(new TextDataField("Region")));
            pivotViz.Values.Add(new MeasureColumn(new NumberDataField("Sales")));

            // Act
            var result = VisualizationConverter.ToGrid(pivotViz);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Columns.Count);
            Assert.Contains(result.Columns, c => c.FieldName == "Category");
            Assert.Contains(result.Columns, c => c.FieldName == "Region");
            Assert.Contains(result.Columns, c => c.FieldName == "Sales");
        }

        [Fact]
        public void ToGrid_ConvertsScatterMapVisualization()
        {
            // Arrange
            var dataSourceItem = new RestDataSourceItem("Test");
            var scatterMap = new ScatterMapVisualization(dataSourceItem)
            {
                Label = new DimensionColumn(new TextDataField("City")),
                Latitude = new DimensionColumn(new TextDataField("Lat")),
                Longitude = new DimensionColumn(new TextDataField("Long")),
                MapColorCategory = new DimensionColumn(new TextDataField("Category"))
            };
            scatterMap.MapColor.Add(new MeasureColumn(new NumberDataField("Value")));
            scatterMap.BubbleRadius.Add(new MeasureColumn(new NumberDataField("Size")));

            // Act
            var result = VisualizationConverter.ToGrid(scatterMap);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Columns.Count >= 5);
            Assert.Contains(result.Columns, c => c.FieldName == "City");
            Assert.Contains(result.Columns, c => c.FieldName == "Lat");
            Assert.Contains(result.Columns, c => c.FieldName == "Long");
        }

        [Fact]
        public void ToGrid_PreventsDuplicateColumns()
        {
            // Arrange
            var dataSourceItem = new RestDataSourceItem("Test");
            var columnChart = new ColumnChartVisualization(dataSourceItem);
            // Add the same field multiple times in different roles
            columnChart.Labels.Add(new DimensionColumn(new TextDataField("Category")));
            columnChart.Labels.Add(new DimensionColumn(new TextDataField("Category")));
            columnChart.Values.Add(new MeasureColumn(new NumberDataField("Sales")));

            // Act
            var result = VisualizationConverter.ToGrid(columnChart);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Columns.Count); // Should only have Category and Sales, no duplicates
        }

        [Fact]
        public void CanConvertToGrid_ReturnsTrue_ForSupportedVisualization()
        {
            // Arrange
            var dataSourceItem = new RestDataSourceItem("Test");
            var columnChart = new ColumnChartVisualization(dataSourceItem);

            // Act
            var result = VisualizationConverter.CanConvertToGrid(columnChart);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CanConvertToGrid_ReturnsFalse_ForUnsupportedVisualization()
        {
            // Arrange
            var textBox = new TextBoxVisualization();

            // Act
            var result = VisualizationConverter.CanConvertToGrid(textBox);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void CanConvertToGrid_ReturnsFalse_WhenVisualizationIsNull()
        {
            // Act
            var result = VisualizationConverter.CanConvertToGrid(null);

            // Assert
            Assert.False(result);
        }
    }
}
