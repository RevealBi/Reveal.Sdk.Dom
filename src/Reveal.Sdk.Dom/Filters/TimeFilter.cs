﻿using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Constants;
using Newtonsoft.Json.Converters;

namespace Reveal.Sdk.Dom.Filters
{
    public sealed class TimeFilter : FilterBase
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public TimeRuleType RuleType { get; set; } = TimeRuleType.AllTime;
        public DateRange CustomTimeRange { get; set; }

        public TimeFilter()
        {
            SchemaTypeName = SchemaTypeNames.TimeFilterType;
        }
    }
}
