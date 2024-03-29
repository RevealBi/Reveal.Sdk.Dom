﻿using Newtonsoft.Json;

namespace Reveal.Sdk.Dom.Visualizations.Settings
{
    public abstract class FinancialVisualizationSettingsBase : ChartVisualizationSettingsBase
    {
        protected FinancialVisualizationSettingsBase() : base() { }

        /// <summary>
        /// Gets or sets whether the left axis will use the Logarithmic scale. Linear is used by default.
        /// </summary>
        [JsonProperty("LeftAxisLogarithmic")]
        public bool LeftAxisIsLogarithmic { get; set; }

        /// <summary>
        /// Gets or sets the minimum value for the left axis. Default value is null, which also equates to zero.
        /// </summary>
        [JsonProperty]
        public double? LeftAxisMinValue { get; set; }

        /// <summary>
        /// Gets or sets the maximum value for the left axis. Default value is calculated automatically depending on the values.
        /// </summary>
        [JsonProperty]
        public double? LeftAxisMaxValue { get; set; }
    }
}
