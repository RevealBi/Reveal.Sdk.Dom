using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations.Settings;

namespace Reveal.Sdk.Dom.Visualizations
{
    public sealed class LineChartVisualization : CategoryVisualizationBase<LineChartVisualizationSettings>
    {
        internal LineChartVisualization() : this(null) { }

        public LineChartVisualization(DataSourceItem dataSourceItem) : base(dataSourceItem) { }

        public LineChartVisualization(string title, DataSourceItem dataSourceItem) : base(title, dataSourceItem) { }
    }
}
