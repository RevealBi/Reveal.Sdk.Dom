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

        // Raw schema fields — internal. The public date selection is exposed through Rule.
        [JsonProperty("RuleType")]
        [JsonConverter(typeof(StringEnumConverter))]
        internal DateRuleType RuleType { get; set; } = DateRuleType.AllTime;

        [JsonProperty("CustomDateRange")]
        internal DateRange CustomDateRange { get; set; }

        [JsonProperty("CustomRule")]
        internal RelativePeriod CustomRule { get; set; }

        [JsonProperty("IncludeToday")]
        internal bool IncludeToday { get; set; } = true;

        DateRuleType IDateRuleFilter.RuleType { get => RuleType; set => RuleType = value; }
        RelativePeriod IDateRuleFilter.CustomRule { get => CustomRule; set => CustomRule = value; }
        DateRange IDateRuleFilter.CustomDateRange { get => CustomDateRange; set => CustomDateRange = value; }

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
