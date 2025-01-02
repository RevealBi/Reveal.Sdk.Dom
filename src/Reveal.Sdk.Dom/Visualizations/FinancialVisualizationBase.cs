using Newtonsoft.Json;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;
using System.Collections.Generic;

namespace Reveal.Sdk.Dom.Visualizations
{
    public abstract class FinancialVisualizationBase<TSettings> : Visualization<TSettings>, ILabels, IFinance
        where TSettings : FinancialVisualizationSettingsBase, new()
    {
        protected FinancialVisualizationBase(string title, DataSourceItem dataSourceItem) : base(title, dataSourceItem) { }

        [JsonProperty(Order = 7)]
        internal FinancialVisualizationDataSpec VisualizationDataSpec { get; set; } = new FinancialVisualizationDataSpec();

        [JsonIgnore]
        public List<DimensionColumn> Labels { get { return VisualizationDataSpec.Rows; } }

        [JsonIgnore]
        public List<MeasureColumn> Opens { get { return VisualizationDataSpec.Open; } }

        [JsonIgnore]
        public List<MeasureColumn> Highs { get { return VisualizationDataSpec.High; } }

        [JsonIgnore]
        public List<MeasureColumn> Lows { get { return VisualizationDataSpec.Low; } }

        [JsonIgnore]
        public List<MeasureColumn> Closes { get { return VisualizationDataSpec.Close; } }
    }
}
