using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Reveal.Sdk.Dom.Core.Constants;

namespace Reveal.Sdk.Dom.Filters
{
    public sealed class DashboardDateFilter : DashboardFilter, IDateRuleFilter
    {
        // Raw schema fields — internal. The public surface is Title (inherited) + Rule.
        [JsonProperty("RuleType")]
        [JsonConverter(typeof(StringEnumConverter))]
        internal DateRuleType RuleType { get; set; } = DateRuleType.LastYear;

        [JsonProperty("CustomDateRange")]
        internal DateRange CustomDateRange { get; set; }

        [JsonProperty("IncludeToday")]
        internal bool IncludeToday { get; set; } = true;

        [JsonProperty("CustomRule")]
        internal RelativePeriod CustomRule { get; set; }

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
        internal DashboardDateFilter()
        {
            SchemaTypeName = SchemaTypeNames.DateGlobalFilterType;
            Title = "Date Filter";
            Id = "_date";
        }

        // Internal convenience for a titled filter without an explicit rule (keeps the historical
        // default). Not public — the public constructors require a rule.
        internal DashboardDateFilter(string title) : this()
        {
            Title = title;
        }

        /// <summary>Creates a date filter with the default title ("Date Filter").</summary>
        public DashboardDateFilter(DateFilterRule rule) : this()
        {
            Rule = rule ?? throw new ArgumentNullException(nameof(rule));
        }

        /// <summary>Creates a date filter with an explicit title.</summary>
        public DashboardDateFilter(string title, DateFilterRule rule) : this()
        {
            Title = title;
            Rule = rule ?? throw new ArgumentNullException(nameof(rule));
        }
    }
}
