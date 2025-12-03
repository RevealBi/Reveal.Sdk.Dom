using Newtonsoft.Json;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Reveal.Sdk.Dom.Visualizations
{
    /// <summary>
    /// The KPI vs time visualization is used for measuring performance of a value field compared against a prior time period.
    /// </summary>
    public sealed class KpiTimeVisualization : Visualization<KpiTimeVisualizationSettings>, IDate, IValues, ICategories
    {
        internal KpiTimeVisualization() : this(null) { }

        /// <summary>
        /// Creates a KPI vs time visualization from the supplied <see cref="DataSourceItem"/>.
        /// </summary>
        /// <param name="dataSourceItem">The data soure item used to represent a dataset.</param>
        public KpiTimeVisualization(DataSourceItem dataSourceItem) : this(null, dataSourceItem) { }

        /// <summary>
        /// Creates a KPI vs time visualization from the supplied <see cref="DataSourceItem"/> and sets the title to the provided string.
        /// </summary>
        /// <param name="title">The string to use as the visualization's title.</param>
        /// <param name="dataSourceItem">The data soure item used to represent a dataset.</param>
        public KpiTimeVisualization(string title, DataSourceItem dataSourceItem) : base(title, dataSourceItem)
        {
            Settings.VisualizationDataSpec = VisualizationDataSpec;
            ChartType = ChartType.KpiTime;
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

        [JsonProperty(Order = 7)]
        internal IndicatorVisualizationDataSpec VisualizationDataSpec { get; set; } = new IndicatorVisualizationDataSpec();
    }
}