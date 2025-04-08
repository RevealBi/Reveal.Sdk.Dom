using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Reveal.Sdk.Dom.Visualizations.Settings
{
    /// <summary>
    /// This class is the base class for charts the has a left axis (Y axis) such as the Combo, Stacked Column, Stacked Bar, Stacked Area, Bubble, and Scatter charts.
    /// </summary>
    public abstract class YAxisVisualizationSettings : ChartVisualizationSettings
    {
        protected YAxisVisualizationSettings() : base() { }

        /// <summary>
        /// Gets or sets whether the Y axis will use the Logarithmic scale. Linear is used by default.
        /// </summary>
        [JsonProperty("LeftAxisLogarithmic")]
        public bool YAxisIsLogarithmic { get; set; }

        /// <summary>
        /// Gets or sets the minimum value for the Y axis. Default value is null, which also equates to zero.
        /// </summary>
        [JsonProperty("LeftAxisMinValue")]
        public double? YAxisMinValue { get; set; }

        /// <summary>
        /// Gets or sets the maximum value for the Y axis. Default value is calculated automatically depending on the values.
        /// </summary>
        [JsonProperty("LeftAxisMaxValue")]
        public double? YAxisMaxValue { get; set; }

        /// <summary>
        /// Gets or sets if axis titles are shown. Default value is None.
        /// </summary>
        [JsonProperty("AxisTitlesMode")]
        [JsonConverter(typeof(StringEnumConverter))]
        public AxisTitleMode AxisTitleMode { get; set; } = AxisTitleMode.None;
    }
}
