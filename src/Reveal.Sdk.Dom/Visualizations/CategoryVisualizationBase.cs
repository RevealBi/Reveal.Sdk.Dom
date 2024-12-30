using Newtonsoft.Json;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;
using System.Collections.Generic;

namespace Reveal.Sdk.Dom.Visualizations
{
    public abstract class CategoryVisualizationBase<TSettings> : Visualization<TSettings>, ILabels, IValues, ICategory
        where TSettings : ChartVisualizationSettingsBase, new()
    {
        protected CategoryVisualizationBase(DataSourceItem dataSourceItem) : this(null, dataSourceItem) { }
        protected CategoryVisualizationBase(string title, DataSourceItem dataSourceItem) : base(title, dataSourceItem) { }

        [JsonIgnore]
        public List<DimensionColumn> Labels { get { return VisualizationDataSpec.Rows; } }

        [JsonIgnore]
        public List<MeasureColumn> Values { get { return VisualizationDataSpec.Values; } }

        [JsonIgnore]
        public DimensionColumn Category
        {
            get { return VisualizationDataSpec.Category; }
            set { VisualizationDataSpec.Category = value; }
        }

        [JsonProperty(Order = 7)]
        internal CategoryVisualizationDataSpec VisualizationDataSpec { get; set; } = new CategoryVisualizationDataSpec();
    }
}