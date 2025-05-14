using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace Reveal.Sdk.Dom.Visualizations.Settings
{
    public abstract class GridVisualizationSettingsBase : VisualizationSettings
    {
        protected GridVisualizationSettingsBase() { }

        /// <summary>
        /// Gets or sets the format settings for the columns in the grid.
        /// </summary>
        [JsonProperty("VisualizationColumns")]
        public List<GridColumnFormat> ColumnFormats { get; set; }

        /// <summary>
        /// Gets or sets the alignment of text in a date field.
        /// </summary>
        [JsonIgnore]
        public Alignment DateFieldAlignment
        {
            get { return Style.DateAlignment; }
            set { Style.DateAlignment = value; }
        }

        /// <summary>
        /// Gets or set the size of the font
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public FontSize FontSize { get; set; } = FontSize.Small;

        /// <summary>
        /// Gets or sets the alignment of text in a number field.
        /// </summary>
        [JsonIgnore]
        public Alignment NumericFieldAlignment
        {
            get { return Style.NumericAlignment; }
            set { Style.NumericAlignment = value; }
        }

        /// <summary>
        /// Gets or sets the alignment of text in a text field.
        /// </summary>
        [JsonIgnore]
        public Alignment TextFieldAlignment 
        {
            get { return Style.TextAlignment; }
            set { Style.TextAlignment = value; }
        }

        [JsonProperty]
        internal GridVisualizationStyle Style { get; set; } = new GridVisualizationStyle();
    }
}
