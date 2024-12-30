using Newtonsoft.Json;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;
using System.Collections.Generic;

namespace Reveal.Sdk.Dom.Visualizations
{
    /// <summary>
    /// A time series visualization is used to display data points indexed in time order. It is commonly used to detect trends at a glance, allowing an easy observation of development over time.
    /// </summary>
    public sealed class TimeSeriesVisualization : Visualization<TimeSeriesVisualizationSettings>, IDate, IValues, ICategory
    {
        internal TimeSeriesVisualization() : this(null) { }

        /// <summary>
        /// Creates a time series visualization from the supplied <see cref="DataSourceItem"/>.
        /// </summary>
        /// <param name="dataSourceItem">The data soure item used to represent a dataset.</param>
        public TimeSeriesVisualization(DataSourceItem dataSourceItem) : this(null, dataSourceItem) { }

        /// <summary>
        /// Creates a time series visualization from the supplied <see cref="DataSourceItem"/> and sets the title to the provided string.
        /// </summary>
        /// <param name="title">The string to use as the visualization's title.</param>
        /// <param name="dataSourceItem">The data soure item used to represent a dataset.</param>
        public TimeSeriesVisualization(string title, DataSourceItem dataSourceItem) : base(title, dataSourceItem) { ChartType = ChartType.TimeSeries; }

        [JsonProperty(Order = 7)]
        internal TimeSeriesVisualizationDataSpec VisualizationDataSpec { get; set; } = new TimeSeriesVisualizationDataSpec();

        [JsonIgnore]
        public List<MeasureColumn> Values { get { return VisualizationDataSpec.Values; } }

        [JsonIgnore]
        public DimensionColumn Category
        {
            get { return VisualizationDataSpec.Category; }
            set { VisualizationDataSpec.Category = value; }
        }

        [JsonIgnore]
        public DimensionColumn Date
        {
            get
            {
                if (VisualizationDataSpec.Rows.Count > 0)
                    return VisualizationDataSpec.Rows[0];
                else 
                    return null;
            }
            set
            {
                VisualizationDataSpec.Rows.Clear();
                VisualizationDataSpec.Rows.Add(value);
            }
        }
    }
}
