using Reveal.Sdk.Dom.Core.Constants;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Reveal.Sdk.Dom.Filters
{
    public sealed class XmlaDateFilter : FilterBase, IDateRuleFilter
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public DateRuleType RuleType { get; set; } = DateRuleType.AllTime;
        public DateRange CustomDateRange { get; set; }
        public bool IncludeToday { get; set; } = true;

        // Applied when RuleType is CustomRule; set via SetRelativePeriod. Wire name stays "CustomRule".
        [JsonProperty("CustomRule")]
        public RelativePeriod RelativePeriod { get; internal set; }

        public XmlaDateFilter()
        {
            SchemaTypeName = SchemaTypeNames.XmlaDateFilterType;
        }
    }
}
