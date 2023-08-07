﻿using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core;
using Reveal.Sdk.Dom.Core.Serialization.Converters;
using System;

namespace Reveal.Sdk.Dom.Filters
{
    [JsonConverter(typeof(DashboardFilterConverter))]
    public abstract class DashboardFilter : SchemaType
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }

        //todo: figure this out
        [JsonProperty]
        internal string CrossFilteringSourceWidgetId { get; set; }
    }
}
