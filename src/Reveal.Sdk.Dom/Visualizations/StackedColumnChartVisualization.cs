using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations.Settings;

namespace Reveal.Sdk.Dom.Visualizations
{
    public sealed class StackedColumnChartVisualization : CategoryVisualizationBase<StackedColumnChartVisualizationSettings>
    {
        internal StackedColumnChartVisualization() : this(null) { }

        public StackedColumnChartVisualization(DataSourceItem dataSourceItem) : base(dataSourceItem) { }

        public StackedColumnChartVisualization(string title, DataSourceItem dataSourceItem) : base(title, dataSourceItem) { }
    }
}
