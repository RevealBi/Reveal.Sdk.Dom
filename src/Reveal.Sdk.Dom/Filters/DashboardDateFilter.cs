using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Reveal.Sdk.Dom.Core.Constants;

namespace Reveal.Sdk.Dom.Filters
{
    public sealed class DashboardDateFilter : DashboardFilter, IDateRuleFilter
    {
        // Pre-existing public schema fields — kept public for backward compatibility.
        [JsonConverter(typeof(StringEnumConverter))]
        public DateRuleType RuleType { get; set; } = DateRuleType.LastYear;

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
