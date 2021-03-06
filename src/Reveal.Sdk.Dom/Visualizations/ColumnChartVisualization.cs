using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations.Settings;

namespace Reveal.Sdk.Dom.Visualizations
{
    public sealed class ColumnChartVisualization : CategoryVisualizationBase<ColumnChartVisualizationSettings>
    {
        internal ColumnChartVisualization() : this(null) { }

        public ColumnChartVisualization(DataSourceItem dataSourceItem) : base(dataSourceItem) { }

        public ColumnChartVisualization(string title, DataSourceItem dataSourceItem) : base(title, dataSourceItem) { }
    }
}
