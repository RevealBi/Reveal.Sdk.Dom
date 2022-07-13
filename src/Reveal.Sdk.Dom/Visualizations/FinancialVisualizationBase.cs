﻿using Newtonsoft.Json;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;
using System.Collections.Generic;

namespace Reveal.Sdk.Dom.Visualizations
{
    public abstract class FinancialVisualizationBase<TSettings> : TabularVisualizationBase<TSettings>, ILabels, IFinance
        where TSettings : ChartVisualizationSettings, new()
    {
        protected FinancialVisualizationBase(string title, DataSourceItem dataSourceItem) : base(title, dataSourceItem) { }

        [JsonProperty(Order = 7)]
        FinancialVisualizationDataSpec VisualizationDataSpec { get; set; } = new FinancialVisualizationDataSpec();

        [JsonIgnore]
        public List<DimensionColumnSpec> Labels { get { return VisualizationDataSpec.Rows; } }

        [JsonIgnore]
        public List<MeasureColumnSpec> Open { get { return VisualizationDataSpec.Open; } }

        [JsonIgnore]
        public List<MeasureColumnSpec> High { get { return VisualizationDataSpec.High; } }

        [JsonIgnore]
        public List<MeasureColumnSpec> Low { get { return VisualizationDataSpec.Low; } }

        [JsonIgnore]
        public List<MeasureColumnSpec> Close { get { return VisualizationDataSpec.Close; } }
    }
}
