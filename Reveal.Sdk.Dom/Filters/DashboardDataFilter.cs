﻿using Reveal.Sdk.Dom.Core;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations.DataSpecs;
using Newtonsoft.Json;

namespace Reveal.Sdk.Dom.Filters
{
    public class DashboardDataFilter : DashboardDataFilterBase
    {
        
        public TabularDataSpec DataSpec { get; internal set; } = new TabularDataSpec();
        public string SelectedFieldName { get; set; }

        internal DashboardDataFilter() : this(null) { }

        public DashboardDataFilter(DataSourceItem dataSourceItem)
        {
            SchemaTypeName = SchemaTypeNames.TabularGlobalFilterType;
            DataSpec.DataSourceItem = dataSourceItem;
            DataSpec.Fields = dataSourceItem?.Fields.Clone();
        }
    }
}
