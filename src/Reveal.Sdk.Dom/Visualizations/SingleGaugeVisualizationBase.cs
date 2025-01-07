using Newtonsoft.Json;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;
using System.Collections.Generic;

namespace Reveal.Sdk.Dom.Visualizations
{
    public abstract class SingleGaugeVisualizationBase<TSettings> : Visualization<TSettings>, ILabel, IValues
        where TSettings : VisualizationSettings, new()
    {
        protected SingleGaugeVisualizationBase(string title, DataSourceItem dataSourceItem) : base(title, dataSourceItem) { }

        [JsonIgnore]
        public DimensionColumn Label
        {
            get { return VisualizationDataSpec.Label; }
            set { VisualizationDataSpec.Label = value; }
        }

        [JsonIgnore]
        public List<MeasureColumn> Values { get { return VisualizationDataSpec.Value; } }

        [JsonProperty(Order = 7)]
        internal SingleGaugeVisualizationDataSpec VisualizationDataSpec { get; set; } = new SingleGaugeVisualizationDataSpec();
    }
}