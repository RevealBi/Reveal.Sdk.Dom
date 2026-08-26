using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace Reveal.Sdk.Dom.Visualizations.Settings
{
    /// <summary>
    /// This class is the base class for common category chart types such as Column, Bar, Area, Line, Step Area, Step Line, Spline Area, Spline, and Time Series charts.
    /// </summary>
    public abstract class CategoryChartVisualizationSettings : SharedChartVisualizationSettings
    {
        /// <summary>
        /// Initializes a new instance of category-chart visualization settings.
        /// </summary>
        protected CategoryChartVisualizationSettings() : base() { }

        /// <summary>
        /// Gets or sets the annotations displayed on the chart.
        /// </summary>
        [JsonProperty("Annotations")]
        public List<ChartAnnotation> Annotations { get; set; } = new List<ChartAnnotation>();

        /// <summary>
        /// Gets or sets whether a total (sum) of values will be displayed in the tooltip. Only applied when categories have been added to the visualization.
        /// </summary>
        [JsonProperty]
        internal bool ShowTotalsInTooltip { get; set; }

        /// <summary>
        /// Gets or sets a trendline to apply to the visualization
        /// </summary>
        [JsonProperty("TrendlineType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public TrendlineType Trendline { get; set; }
    }
}
