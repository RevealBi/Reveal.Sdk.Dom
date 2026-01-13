namespace Reveal.Sdk.Dom.Core.Utilities
{
    /// <summary>
    /// Provides options for converting visualizations to grid format.
    /// </summary>
    public class GridConversionOptions
    {
        /// <summary>
        /// Gets or sets a value indicating whether to include all fields from the data source 
        /// or only the fields currently used in the visualization.
        /// </summary>
        /// <remarks>
        /// When <c>true</c>, all fields from the TabularDataDefinition will be included in the grid.
        /// When <c>false</c> (default), only the fields actively used in the visualization will be included.
        /// </remarks>
        public bool IncludeAllFields { get; set; }
    }
}
