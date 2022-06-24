﻿using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Filters;
using Reveal.Sdk.Dom.Visualizations.Primitives;
using System.Collections.Generic;
using System.Linq;

namespace Reveal.Sdk.Dom.Visualizations.Factories
{
    public static partial class VisualizationFactory
    {
        public static ColumnChartVisualization CreateColumnChart(string title, DataSourceItem dataSourceItem,
            IEnumerable<SummarizationDimensionField> labels, IEnumerable<SummarizationValueField> values,
            IEnumerable<Binding> filterBindings = null, IEnumerable<string> filters = null)
        {
            return CreateChartVisualization<ColumnChartVisualization>(title, dataSourceItem, labels, values, filterBindings, filters);
        }

        public static ColumnChartVisualization CreateColumnChart(string title, DataSourceItem dataSourceItem,
            IEnumerable<SummarizationDimensionField> labels, IEnumerable<string> values,
            IEnumerable<Binding> filterBindings = null, IEnumerable<string> filters = null)
        {
            return CreateChartVisualization<ColumnChartVisualization>(title, dataSourceItem, labels, values, filterBindings, filters);
        }
    }
}
