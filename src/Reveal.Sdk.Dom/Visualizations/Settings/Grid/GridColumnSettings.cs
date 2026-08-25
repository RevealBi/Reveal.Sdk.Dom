using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Reveal.Sdk.Dom.Visualizations.Settings
{
    /// <summary>
    /// Configures the appearance and behavior of a grid column.
    /// </summary>
    public sealed class GridColumnSettings
    {
        public GridColumnSettings() { }

        public GridColumnSettings(string columnName)
        {
            ColumnName = columnName;
        }

        /// <summary>
        /// Gets or sets the rendered column name that these settings apply to.
        /// </summary>
        [JsonProperty("ColumnName")]
        public string ColumnName { get; set; }

        /// <summary>
        /// Gets or sets the column width.
        /// </summary>
        [JsonProperty("Width")]
        public double? Width { get; set; }

        /// <summary>
        /// Gets or sets the column's text alignment.
        /// </summary>
        [JsonProperty("TextAlignment")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Alignment TextAlignment { get; set; } = Alignment.Inherit;

        /// <summary>
        /// Gets or sets whether and where the column is pinned.
        /// </summary>
        [JsonProperty("Pinning")]
        [JsonConverter(typeof(StringEnumConverter))]
        public GridColumnPinPosition PinPosition { get; set; } = GridColumnPinPosition.Inherit;

        /// <summary>
        /// Gets or sets the hyperlink displayed by the column.
        /// </summary>
        [JsonProperty("Hyperlink")]
        public GridColumnHyperlink Hyperlink { get; set; }
    }
}
