using Reveal.Sdk.Dom.Visualizations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reveal.Sdk.Dom.Core.Utilities
{
    /// <summary>
    /// Provides methods for converting visualizations between different types.
    /// </summary>
    public static class VisualizationConverter
    {

        public static GridVisualization ToGrid(IVisualization sourceVisualization, GridConversionOptions options = null)
        {
            if (sourceVisualization == null)
                throw new ArgumentNullException(nameof(sourceVisualization));

            options = options ?? new GridConversionOptions();

            // Check if conversion is possible
            if (!CanConvertToGrid(sourceVisualization))
                return null;

            // Verify we have a TabularDataDefinition
            if (!(sourceVisualization.DataDefinition is TabularDataDefinition tabularDataDefinition))
                return null;

            // Get the data source item
            var dataSourceItem = sourceVisualization.GetDataSourceItem();
            if (dataSourceItem == null)
                return null;

            // Extract columns based on options
            var columns = options.IncludeAllFields
                ? ExtractAllFields(tabularDataDefinition)
                : ExtractVisualizationFields(sourceVisualization, tabularDataDefinition);

            if (columns == null || columns.Count == 0)
                return null;

            // Create new GridVisualization
            var gridVisualization = new GridVisualization(sourceVisualization.Title, dataSourceItem)
            {
                Id = sourceVisualization.Id,
                BackgroundColor = sourceVisualization.BackgroundColor,
                IsTitleVisible = sourceVisualization.IsTitleVisible,
                ColumnSpan = sourceVisualization.ColumnSpan,
                RowSpan = sourceVisualization.RowSpan,
                Description = sourceVisualization.Description
            };

            // Add columns
            foreach (var column in columns)
            {
                gridVisualization.Columns.Add(column);
            }

            // Copy filters and filter bindings
            if (sourceVisualization.Filters != null)
            {
                gridVisualization.Filters.Clear();
                foreach (var filter in sourceVisualization.Filters)
                {
                    gridVisualization.Filters.Add(filter);
                }
            }

            if (sourceVisualization.FilterBindings != null)
            {
                gridVisualization.FilterBindings.Clear();
                foreach (var binding in sourceVisualization.FilterBindings)
                {
                    gridVisualization.FilterBindings.Add(binding);
                }
            }

            return gridVisualization;
        }

        /// <summary>
        /// Determines whether a visualization can be converted to a grid.
        /// </summary>
        /// <param name="visualization">The visualization to check.</param>
        /// <returns><c>true</c> if the visualization can be converted; otherwise, <c>false</c>.</returns>
        public static bool CanConvertToGrid(IVisualization visualization)
        {
            if (visualization == null)
                return false;

            // These types cannot be converted
            var incompatibleTypes = new[]
            {
                ChartType.Grid,
                ChartType.TextBox,
                ChartType.Text,
                ChartType.Custom,
                ChartType.Image
            };

            return !incompatibleTypes.Contains(visualization.ChartType);
        }

        /// <summary>
        /// Extracts all fields from the TabularDataDefinition.
        /// </summary>
        private static List<TabularColumn> ExtractAllFields(TabularDataDefinition dataDefinition)
        {
            var columns = new List<TabularColumn>();
            var addedFields = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            if (dataDefinition.Fields == null)
                return columns;

            foreach (var field in dataDefinition.Fields)
            {
                if (!string.IsNullOrEmpty(field.FieldName) && addedFields.Add(field.FieldName))
                {
                    columns.Add(new TabularColumn(field.FieldName));
                }
            }

            return columns;
        }

        /// <summary>
        /// Extracts only the fields actively used in the visualization.
        /// </summary>
        private static List<TabularColumn> ExtractVisualizationFields(IVisualization visualization, TabularDataDefinition dataDefinition)
        {
            var columns = new List<TabularColumn>();
            var addedFields = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            // Extract from interface-based properties
            AddColumnsFromCategory(visualization, addedFields, columns);
            AddColumnsFromCategories(visualization, addedFields, columns);
            AddColumnsFromLabel(visualization, addedFields, columns);
            AddColumnsFromLabels(visualization, addedFields, columns);
            AddColumnsFromValues(visualization, addedFields, columns);
            AddColumnsFromRows(visualization, addedFields, columns);
            AddColumnsFromColumns(visualization, addedFields, columns);
            AddColumnsFromFinance(visualization, addedFields, columns);
            AddColumnsFromDate(visualization, addedFields, columns);
            AddColumnsFromTargets(visualization, addedFields, columns);
            AddColumnsFromMap(visualization, addedFields, columns);
            AddColumnsFromAxis(visualization, addedFields, columns);

            return columns;
        }

        private static void AddColumnsFromCategory(IVisualization visualization, HashSet<string> addedFields, List<TabularColumn> columns)
        {
            if (visualization is ICategory category)
            {
                AddDimensionColumn(category.Category, addedFields, columns);
            }
        }

        private static void AddColumnsFromCategories(IVisualization visualization, HashSet<string> addedFields, List<TabularColumn> columns)
        {
            if (visualization is ICategories categories)
            {
                AddDimensionColumns(categories.Categories, addedFields, columns);
            }
        }

        private static void AddColumnsFromLabel(IVisualization visualization, HashSet<string> addedFields, List<TabularColumn> columns)
        {
            if (visualization is ILabel label)
            {
                AddDimensionColumn(label.Label, addedFields, columns);
            }
        }

        private static void AddColumnsFromLabels(IVisualization visualization, HashSet<string> addedFields, List<TabularColumn> columns)
        {
            if (visualization is ILabels labels)
            {
                AddDimensionColumns(labels.Labels, addedFields, columns);
            }
        }

        private static void AddColumnsFromValues(IVisualization visualization, HashSet<string> addedFields, List<TabularColumn> columns)
        {
            if (visualization is IValues values)
            {
                AddMeasureColumns(values.Values, addedFields, columns);
            }
        }

        private static void AddColumnsFromRows(IVisualization visualization, HashSet<string> addedFields, List<TabularColumn> columns)
        {
            if (visualization is IRows rows)
            {
                AddDimensionColumns(rows.Rows, addedFields, columns);
            }
        }

        private static void AddColumnsFromColumns(IVisualization visualization, HashSet<string> addedFields, List<TabularColumn> columns)
        {
            if (visualization is IColumns cols)
            {
                AddDimensionColumns(cols.Columns, addedFields, columns);
            }
        }

        private static void AddColumnsFromFinance(IVisualization visualization, HashSet<string> addedFields, List<TabularColumn> columns)
        {
            if (visualization is IFinance finance)
            {
                AddMeasureColumns(finance.Opens, addedFields, columns);
                AddMeasureColumns(finance.Highs, addedFields, columns);
                AddMeasureColumns(finance.Lows, addedFields, columns);
                AddMeasureColumns(finance.Closes, addedFields, columns);
            }
        }

        private static void AddColumnsFromDate(IVisualization visualization, HashSet<string> addedFields, List<TabularColumn> columns)
        {
            if (visualization is IDate date)
            {
                AddDimensionColumn(date.Date, addedFields, columns);
            }
        }

        private static void AddColumnsFromTargets(IVisualization visualization, HashSet<string> addedFields, List<TabularColumn> columns)
        {
            if (visualization is ITargets targets)
            {
                AddMeasureColumns(targets.Targets, addedFields, columns);
            }
        }

        private static void AddColumnsFromMap(IVisualization visualization, HashSet<string> addedFields, List<TabularColumn> columns)
        {
            // ScatterMapVisualization has specific properties for map data
            if (visualization is ScatterMapVisualization scatterMap)
            {
                AddDimensionColumn(scatterMap.Label, addedFields, columns);
                AddDimensionColumn(scatterMap.Latitude, addedFields, columns);
                AddDimensionColumn(scatterMap.Longitude, addedFields, columns);
                AddDimensionColumn(scatterMap.MapColorCategory, addedFields, columns);
                AddMeasureColumns(scatterMap.MapColor, addedFields, columns);
                AddMeasureColumns(scatterMap.BubbleRadius, addedFields, columns);
            }
            // ChoroplethVisualization has locations and map color
            else if (visualization is ChoroplethVisualization choropleth)
            {
                AddDimensionColumns(choropleth.Locations, addedFields, columns);
                AddDimensionColumn(choropleth.MapColor, addedFields, columns);
            }
        }

        private static void AddColumnsFromAxis(IVisualization visualization, HashSet<string> addedFields, List<TabularColumn> columns)
        {
            if (visualization is IAxis axis)
            {
                AddMeasureColumns(axis.XAxes, addedFields, columns);
                AddMeasureColumns(axis.YAxes, addedFields, columns);
            }
        }

        private static void AddDimensionColumn(DimensionColumn column, HashSet<string> addedFields, List<TabularColumn> columns)
        {
            if (column?.DataField != null)
            {
                var fieldName = column.DataField.FieldName;
                if (!string.IsNullOrEmpty(fieldName) && addedFields.Add(fieldName))
                {
                    columns.Add(new TabularColumn(fieldName));
                }
            }
        }

        private static void AddDimensionColumns(IEnumerable<DimensionColumn> dimensionColumns, HashSet<string> addedFields, List<TabularColumn> columns)
        {
            if (dimensionColumns == null)
                return;

            foreach (var column in dimensionColumns)
            {
                AddDimensionColumn(column, addedFields, columns);
            }
        }

        private static void AddMeasureColumn(MeasureColumn column, HashSet<string> addedFields, List<TabularColumn> columns)
        {
            if (column?.DataField != null)
            {
                var fieldName = column.DataField.FieldName;
                if (!string.IsNullOrEmpty(fieldName) && addedFields.Add(fieldName))
                {
                    columns.Add(new TabularColumn(fieldName));
                }
            }
        }

        private static void AddMeasureColumns(IEnumerable<MeasureColumn> measureColumns, HashSet<string> addedFields, List<TabularColumn> columns)
        {
            if (measureColumns == null)
                return;

            foreach (var column in measureColumns)
            {
                AddMeasureColumn(column, addedFields, columns);
            }
        }
    }
}
