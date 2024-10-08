﻿using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Core.Utilities;
using Reveal.Sdk.Dom.Visualizations;
using Newtonsoft.Json;

namespace Reveal.Sdk.Dom.Filters
{
    public sealed class DashboardDataFilter : DashboardDataFilterBase
    {
        [JsonProperty("DataSpec")]
        internal TabularDataDefinition DataDefinition { get; set; } = new TabularDataDefinition();

        [JsonProperty("SelectedFieldName")]
        public string FieldName { get; set; }

        internal DashboardDataFilter() : this(null) { }

        public DashboardDataFilter(DataSourceItem dataSourceItem)
            : this(null, dataSourceItem) { }

        public DashboardDataFilter(string fieldName, DataSourceItem dataSourceItem)
            : this(fieldName, null, dataSourceItem) { }

        public DashboardDataFilter(string fieldName, string title, DataSourceItem dataSourceItem)
        {
            SchemaTypeName = SchemaTypeNames.TabularGlobalFilterType;
            FieldName = fieldName;
            Title = title ?? fieldName;
            DataDefinition.DataSourceItem = dataSourceItem;
            DataDefinition.Fields = dataSourceItem?.Fields.Clone();
        }

        public void SelectValues(params object[] values)
        {
            SelectedItems.Clear();
            foreach (var value in values)
            {
                SelectedItems.Add(new FilterItem(FieldName, value));
            }
        }
    }
}
