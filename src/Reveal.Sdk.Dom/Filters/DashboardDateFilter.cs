using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Reveal.Sdk.Dom.Core.Constants;

namespace Reveal.Sdk.Dom.Filters
{
    public sealed class DashboardDateFilter : DashboardFilter, IDateRuleFilter
    {
        public DateRange CustomDateRange { get; set; }

        public bool IncludeToday { get; set; } = true;

        [JsonConverter(typeof(StringEnumConverter))]
        public DateRuleType RuleType { get; set; } = DateRuleType.LastYear;

        // Applied when RuleType is CustomRule; set via SetRelativePeriod. Wire name stays "CustomRule".
        [JsonProperty("CustomRule")]
        public RelativePeriod RelativePeriod { get; internal set; }

        public DashboardDateFilter() : this("Date Filter") { }

        public DashboardDateFilter(string title)
        {
            SchemaTypeName = SchemaTypeNames.DateGlobalFilterType;
            Title = title;
            Id = "_date";
        }
    }
}
