using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Reveal.Sdk.Dom.Visualizations.Settings
{
    /// <summary>
    /// Configures a grid column used to group rows.
    /// </summary>
    public sealed class GridColumnGrouping
    {
        public GridColumnGrouping() { }

        public GridColumnGrouping(string columnName, SortingType sortDirection = SortingType.Asc)
        {
            ColumnName = columnName;
            SortDirection = sortDirection;
        }

        /// <summary>
        /// Gets or sets the rendered column name used for grouping.
        /// </summary>
        [JsonProperty("ColumnName")]
        public string ColumnName { get; set; }

        /// <summary>
        /// Gets or sets the direction used to sort the grouped values.
        /// </summary>
        [JsonProperty("SortDirection")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SortingType SortDirection { get; set; } = SortingType.Asc;
    }
}
