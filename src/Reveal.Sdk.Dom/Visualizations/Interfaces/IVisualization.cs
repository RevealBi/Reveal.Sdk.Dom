﻿using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Serialization.Converters;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Filters;
using System.Collections.Generic;

namespace Reveal.Sdk.Dom.Visualizations
{
    [JsonConverter(typeof(VisualizationConverter))]
    public interface IVisualization
    {
        string Id { get; set; }
        ChartType ChartType { get; }
        string BackgroundColor { get; set; }
        string Title { get; set; }
        bool IsTitleVisible { get; set; }
        int ColumnSpan { get; set; }
        int RowSpan { get; set; }
        string Description { get; set; }
        IDataDefinition DataDefinition { get; }
        List<VisualizationFilter> Filters { get; set; }
        List<Binding> FilterBindings { get; set; }
        DataSourceItem GetDataSourceItem();
    }
}