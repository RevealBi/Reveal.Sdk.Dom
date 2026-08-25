using Newtonsoft.Json;

namespace Reveal.Sdk.Dom.Visualizations.Settings
{
    /// <summary>
    /// Configures a hyperlink displayed by a grid column.
    /// </summary>
    public sealed class GridColumnHyperlink
    {
        public GridColumnHyperlink() { }

        public GridColumnHyperlink(IVisualizationLink link, string displayTextTemplate = null)
        {
            Link = link;
            DisplayTextTemplate = displayTextTemplate;
        }

        /// <summary>
        /// Gets or sets the link followed when a cell in the column is selected.
        /// </summary>
        [JsonProperty("Action")]
        public IVisualizationLink Link { get; set; }

        /// <summary>
        /// Gets or sets the template used to render the hyperlink text.
        /// When omitted, the formatted cell value is displayed.
        /// </summary>
        [JsonProperty("DisplayTextTemplate")]
        public string DisplayTextTemplate { get; set; }
    }
}
