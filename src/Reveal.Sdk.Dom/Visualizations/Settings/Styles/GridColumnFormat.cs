using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Reveal.Sdk.Dom.Visualizations.Settings
{
    /// <summary>
	/// Represents the format settings for a column in a grid visualization.
	/// </summary>
    public sealed class GridColumnFormat
    {
        /// <summary>
        /// Gets or sets the name of the column to which this format applies.
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Gets or sets the width of the column.
        /// </summary>
		public double? Width { get; set; }

        /// <summary>
        /// Gets or sets the text alignment for the column.
        /// </summary>
		[JsonConverter(typeof(StringEnumConverter))]
		public Alignment TextAlignment { get; set; } = Alignment.Inherit;
	}
}
