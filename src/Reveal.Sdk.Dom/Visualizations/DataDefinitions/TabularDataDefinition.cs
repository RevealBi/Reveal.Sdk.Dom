using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Filters;
using System.Collections.Generic;

namespace Reveal.Sdk.Dom.Visualizations
{
    public sealed class TabularDataDefinition : DataDefinitionBase, ITabularDataDefinitionInternal
    {
        public TabularDataDefinition()
        {
            SchemaTypeName = SchemaTypeNames.TabularDataSpecType;
        }

        //not sure what this is for yet
        [JsonProperty]
        internal bool IsTransposed { get; set; }

        [JsonProperty]
        public List<IField> Fields { get; internal set; } = new List<IField>();

        //not sure what this is for yet
        [JsonProperty]
        internal List<IField> TransposedFields { get; set; } = new List<IField>();

        //this is used as visualization filters. let's find a better way to expose this
        [JsonProperty]
        internal List<VisualizationFilter> QuickFilters { get; set; } = new List<VisualizationFilter>();

        //not sure what this is for yet
        [JsonProperty]
        internal SummarizationSpec SummarizationSpec { get; set; }

        //used when joining tables from multiple data sources
        [JsonProperty("AdditionalTables")]
        List<AdditionalTable> ITabularDataDefinitionInternal.AdditionalTables { get; set; } = new List<AdditionalTable>();

        //not sure what this is for yet
        [JsonProperty]
        internal List<ServiceAdditionalTable> ServiceAdditionalTables { get; set; } = new List<ServiceAdditionalTable>();

    }
}
