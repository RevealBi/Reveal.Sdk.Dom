using Newtonsoft.Json;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;
using System.Collections.Generic;

namespace Reveal.Sdk.Dom.Visualizations
{
    /// <summary>
    /// The pivot table visualization is used as a data summarization tool, which among other functions allows you to automatically count, average and total the data stored in a table like format, typically grouped by values.
    /// </summary>
    public sealed class PivotVisualization : Visualization<PivotVisualizationSettings>, IValues, IRows, IColumns
    {
        internal PivotVisualization() : this(null) { }

        /// <summary>
        /// Creates a pivot table visualization from the supplied <see cref="DataSourceItem"/>.
        /// </summary>
        /// <param name="dataSourceItem">The data soure item used to represent a dataset.</param>
        public PivotVisualization(DataSourceItem dataSourceItem) : this(null, dataSourceItem) { }

        /// <summary>
        /// Creates a pivot table visualization from the supplied <see cref="DataSourceItem"/> and sets the title to the provided string.
        /// </summary>
        /// <param name="title">The string to use as the visualization's title.</param>
        /// <param name="dataSourceItem">The data soure item used to represent a dataset.</param>
        public PivotVisualization(string title, DataSourceItem dataSourceItem) : base(title, dataSourceItem) 
        {
            //this is a workaround because the json schemea has this property on the VisualizationDataSpec and not on the VisualizationSettings where it belongs
            Settings._visualizationDataSpec = VisualizationDataSpec;
            ChartType = ChartType.Pivot;
        }

        [JsonIgnore]
        public List<DimensionColumn> Columns { get { return VisualizationDataSpec.Columns; } }

        [JsonIgnore]
        public List<DimensionColumn> Rows { get { return VisualizationDataSpec.Rows; } }

        [JsonIgnore]
        public List<MeasureColumn> Values { get { return VisualizationDataSpec.Values; } }

        [JsonProperty(Order = 7)]
        internal PivotVisualizationDataSpec VisualizationDataSpec { get; set; } = new PivotVisualizationDataSpec();
    }
}