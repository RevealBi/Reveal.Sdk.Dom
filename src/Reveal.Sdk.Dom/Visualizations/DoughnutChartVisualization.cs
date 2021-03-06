using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations.Settings;

namespace Reveal.Sdk.Dom.Visualizations
{
    public sealed class DoughnutChartVisualization : SingleValueLabelsVisualizationBase<DoughnutChartVisualizationSettings>
    {
        internal DoughnutChartVisualization() : this(null) { }
        public DoughnutChartVisualization(DataSourceItem dataSourceItem) : this(null, dataSourceItem) { }
        public DoughnutChartVisualization(string title, DataSourceItem dataSourceItem) : base(title, dataSourceItem) { }
    }
}
