using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Reveal.Sdk.Dom.Visualizations.Settings
{
    /// <summary>
    /// Configures the summary calculation displayed for a grid column.
    /// </summary>
    public sealed class GridColumnSummary
    {
        public GridColumnSummary() { }

        public GridColumnSummary(string columnName, GridSummaryType summaryType = GridSummaryType.Sum)
        {
            ColumnName = columnName;
            SummaryType = summaryType;
        }

        /// <summary>
        /// Gets or sets the rendered column name to summarize.
        /// </summary>
        [JsonProperty("ColumnName")]
        public string ColumnName { get; set; }

        /// <summary>
        /// Gets or sets the calculation used to summarize the column.
        /// </summary>
        [JsonProperty("Operand")]
        [JsonConverter(typeof(StringEnumConverter))]
        public GridSummaryType SummaryType { get; set; } = GridSummaryType.Sum;
    }
}
