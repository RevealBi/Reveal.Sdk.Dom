﻿using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Filters;
using Reveal.Sdk.Dom.Visualizations.Primitives;
using System;
using System.Collections.Generic;

namespace Reveal.Sdk.Dom.Visualizations.Factories
{
    public static partial class VisualizationFactory
    {
        public static SparklineVisualization CreateSparkline(string title, DataSourceItem dataSourceItem,
            SummarizationDateField dateField, SummarizationValueField valueField, IEnumerable<SummarizationDimensionField> categories,
            IEnumerable<Binding> filterBindings = null, IEnumerable<string> filters = null)
        {
            if (string.IsNullOrEmpty(title))
                throw new ArgumentException($"'{nameof(title)}' cannot be null or empty.", nameof(title));
            if (dataSourceItem is null)
                throw new ArgumentNullException(nameof(dataSourceItem));
            if (dateField is null)
                throw new ArgumentNullException(nameof(dateField));
            if (valueField is null)
                throw new ArgumentNullException(nameof(valueField));
            if (categories is null)
                throw new ArgumentNullException(nameof(categories));

            var visualization = new SparklineVisualization(dataSourceItem)
            {
                Title = title,
            };

            visualization.ApplyFilters(filters, filterBindings);

            visualization.VisualizationDataSpec.Date = new DimensionColumnSpec() { SummarizationField = dateField };
            visualization.VisualizationDataSpec.Value.Add(new MeasureColumnSpec() { SummarizationField = valueField });

            foreach (var category in categories)
            {
                visualization.VisualizationDataSpec.Rows.Add(new DimensionColumnSpec() { SummarizationField = category });
            }

            return visualization;
        }
    }
}
