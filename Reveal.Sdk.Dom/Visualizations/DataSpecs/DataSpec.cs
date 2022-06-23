﻿using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations.Primitives;

namespace Reveal.Sdk.Dom.Visualizations.DataSpecs
{
    public class DataSpec : SchemaType
    {
        public DataSourceItem DataSourceItem { get; set; }

        //not sure what this is for yet
        [JsonProperty]
        internal int Expiration { get; set; } = 1440;

        /// <summary>
        /// This is exposed via the Visualization.FilterBindings property
        /// </summary>
        [JsonProperty]
        internal DataSpecBindings Bindings { get; set; } = new DataSpecBindings();
    }
}
