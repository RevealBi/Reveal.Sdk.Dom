using Newtonsoft.Json;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Reveal.Sdk.Dom.Visualizations
{
    /// <summary>
    /// The KPI vs target visualization is used for measuring performance against a target through a prior time period.
    /// </summary>
    public sealed class KpiTargetVisualization : Visualization<KpiTargetVisualizationSettings>, ITargets, IDate, IValues, ICategories
    {
        internal KpiTargetVisualization() : this(null) { }

        /// <summary>
        /// Creates a KPI vs target visualization from the supplied <see cref="DataSourceItem"/>.
        /// </summary>
        /// <param name="dataSourceItem">The data soure item used to represent a dataset.</param>
        public KpiTargetVisualization(DataSourceItem dataSourceItem) : this(null, dataSourceItem) { }

        /// <summary>
        /// Creates a KPI vs target visualization from the supplied <see cref="DataSourceItem"/> and sets the title to the provided string.
        /// </summary>
        /// <param name="title">The string to use as the visualization's title.</param>
        /// <param name="dataSourceItem">The data soure item used to represent a dataset.</param>
        public KpiTargetVisualization(string title, DataSourceItem dataSourceItem) : base(title, dataSourceItem) 
        {
            Settings.VisualizationDataSpec = VisualizationDataSpec;
            ChartType = ChartType.KpiTarget;
        }

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            //needed to update the settings object after deserialization
            Settings.VisualizationDataSpec = VisualizationDataSpec;
        }


        [JsonIgnore]
        public DimensionColumn Date
        {
            get { return VisualizationDataSpec.Date; }
            set { VisualizationDataSpec.Date = value; }
        }

        [JsonIgnore]
        public List<MeasureColumn> Values
        {
            get { return VisualizationDataSpec.Value; }
        }

        [JsonIgnore]
        public List<DimensionColumn> Categories
        {
            get { return VisualizationDataSpec.Rows; }
        }

        [JsonIgnore]
        public List<MeasureColumn> Targets
        {
            get { return VisualizationDataSpec.Target; }
        }

        [JsonProperty(Order = 7)]
        internal IndicatorTargetVisualizationDataSpec VisualizationDataSpec { get; set; } = new IndicatorTargetVisualizationDataSpec();
    }
}