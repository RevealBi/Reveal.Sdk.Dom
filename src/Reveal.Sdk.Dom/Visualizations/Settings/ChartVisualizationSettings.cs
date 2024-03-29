﻿using Newtonsoft.Json;

namespace Reveal.Sdk.Dom.Visualizations.Settings
{
    public abstract class ChartVisualizationSettings : ChartVisualizationSettingsBase
    {
        protected ChartVisualizationSettings() : base() { }

        /// <summary>
        /// Gets or sets if the chart legend is displayed in the RevealView
        /// </summary>
        [JsonProperty("ShowLegends")]
        public bool ShowLegend { get; set; } = true;

        /// <summary>
        /// Gets or sets the color index for the visualization's starting color. A zero-based index is used to set colors instead of a color name.
        /// For example, an index of 5 would be the 6th color in the color scheme regardless of the theme colors being used.
        /// </summary>
        [JsonProperty("BrushOffsetIndex")]
        public int? StartColorIndex { get; set; }
    }
}
