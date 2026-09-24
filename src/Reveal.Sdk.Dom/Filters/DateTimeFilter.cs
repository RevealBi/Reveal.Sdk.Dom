using System;
using Reveal.Sdk.Dom.Core.Constants;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Reveal.Sdk.Dom.Filters
{
    public sealed class DateTimeFilter : FilterBase, IDateRuleFilter
    {
        public int DateFiscalYearStartMonth { get; set; }

        public bool DisplayInLocalTimeZone { get; set; }

        // Pre-existing public schema fields — kept public for backward compatibility.
        [JsonConverter(typeof(StringEnumConverter))]
        public DateRuleType RuleType { get; set; } = DateRuleType.AllTime;

        public DateRange CustomDateRange { get; set; }

        public bool IncludeToday { get; set; } = true;

        // Introduced by this feature — internal; user code goes through Rule / DateFilterRule.
        [JsonProperty("CustomRule")]
        internal RelativePeriod CustomRule { get; set; }

        RelativePeriod IDateRuleFilter.CustomRule { get => CustomRule; set => CustomRule = value; }

        /// <summary>The date selection this filter applies. Build it with the <see cref="DateFilterRule"/> factories.</summary>
        [JsonIgnore]
        public DateFilterRule Rule
        {
            get => DateFilterRule.FromFilter(this);
            set => (value ?? throw new ArgumentNullException(nameof(value))).ApplyTo(this);
        }

        // Used by the deserializer.
        internal DateTimeFilter()
        {
            SchemaTypeName = SchemaTypeNames.DateTimeFilterType;
        }

        /// <summary>Creates a datetime filter with the given date selection.</summary>
        public DateTimeFilter(DateFilterRule rule) : this()
        {
            Rule = rule ?? throw new ArgumentNullException(nameof(rule));
        }
    }
}
