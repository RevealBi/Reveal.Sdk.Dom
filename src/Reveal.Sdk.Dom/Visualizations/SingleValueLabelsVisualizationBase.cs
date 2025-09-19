using Newtonsoft.Json;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;
using System.Collections.Generic;

namespace Reveal.Sdk.Dom.Visualizations
{
    public abstract class SingleValueLabelsVisualizationBase<TSettings> : Visualization<TSettings>, ILabels, IValues
        where TSettings : VisualizationSettings, new()
    {
        protected SingleValueLabelsVisualizationBase(string title, DataSourceItem dataSourceItem) : base(title, dataSourceItem) { }

        [JsonIgnore]
        public List<DimensionColumn> Labels { 
            get
            {
                if (VisualizationDataSpec is SingleValueLabelsVisualizationDataSpec vizSpec)
                {
                    return vizSpec.Rows;
                }
                else if (VisualizationDataSpec is CategoryVisualizationDataSpec categorySpec)
                {
                    return categorySpec.Rows;
                }
                else
                {
                    return new List<DimensionColumn>();
                }
            }
        }

        [JsonIgnore]
        public List<MeasureColumn> Values { 
            get
            {
                if (VisualizationDataSpec is SingleValueLabelsVisualizationDataSpec vizSpec)
                {
                    return vizSpec.Value;
                }
                else if(VisualizationDataSpec is CategoryVisualizationDataSpec categorySpec)
                {
                    return categorySpec.Values;
                }
                else
                {
                    return new List<MeasureColumn>();
                }
            } 
            }


        //Some visualizations can be assigned a CategoryVisualizationDataSpec instead of a SingleValueLabelsVisualizationDataSpec
        [JsonProperty(Order = 7)]
        internal VisualizationDataSpec VisualizationDataSpec { get; set; } = new SingleValueLabelsVisualizationDataSpec();
    }
}