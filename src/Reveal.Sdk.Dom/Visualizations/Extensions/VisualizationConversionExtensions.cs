using Reveal.Sdk.Dom.Core.Utilities;

namespace Reveal.Sdk.Dom.Visualizations
{
    /// <summary>
    /// Provides extension methods for converting visualizations.
    /// </summary>
    public static class VisualizationConversionExtensions
    {
        /// <summary>
        /// Converts this visualization to a <see cref="GridVisualization"/>.
        /// </summary>
        /// <param name="visualization">The visualization to convert.</param>
        /// <param name="includeAllFields">
        /// If <c>true</c>, includes all fields from the data source; 
        /// if <c>false</c>, includes only fields used in the visualization.
        /// </param>
        /// <returns>
        /// A new <see cref="GridVisualization"/> instance with the same data and configuration,
        /// or <c>null</c> if the visualization cannot be converted.
        /// </returns>
        /// <example>
        /// <code>
        /// var columnChart = new ColumnChartVisualization(dataSourceItem)
        ///     .SetTitle("Sales by Region")
        ///     .SetLabel(new DimensionColumn(new TextDataField("Region")))
        ///     .AddValue(new MeasureColumn(new NumberDataField("Sales")));
        /// 
        /// var gridVisualization = columnChart.ToGrid();
        /// </code>
        /// </example>
        public static GridVisualization ToGrid(this IVisualization visualization, bool includeAllFields = false)
        {
            var options = new GridConversionOptions
            {
                IncludeAllFields = includeAllFields
            };

            return VisualizationConverter.ToGrid(visualization, options);
        }
    }
}
