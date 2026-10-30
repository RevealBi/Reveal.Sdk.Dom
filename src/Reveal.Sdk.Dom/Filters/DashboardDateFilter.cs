using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Reveal.Sdk.Dom.Core.Constants;

namespace Reveal.Sdk.Dom.Filters
{
    public sealed class DashboardDateFilter : DashboardFilter, IDateRuleFilter
    {
        // Retained only for RDASH serialization; callers select a rule through Rule.
        [JsonProperty]
        [JsonConverter(typeof(StringEnumConverter))]
        internal DateRuleType RuleType { get; set; } = DateRuleType.AllTime;

        [JsonProperty]
        internal DateRange CustomDateRange { get; set; }

        [JsonProperty]
        internal bool IncludeToday { get; set; } = true;

        [JsonProperty("CustomRule")]
        internal RelativePeriod CustomRule { get; set; }

        DateRuleType IDateRuleFilter.RuleType { get => RuleType; set => RuleType = value; }
        DateRange IDateRuleFilter.CustomDateRange { get => CustomDateRange; set => CustomDateRange = value; }
        bool IDateRuleFilter.IncludeToday { get => IncludeToday; set => IncludeToday = value; }
        RelativePeriod IDateRuleFilter.CustomRule { get => CustomRule; set => CustomRule = value; }

        /// <summary>The date selection this filter applies. Build it with the <see cref="DateFilterRule"/> factories.</summary>
        [JsonIgnore]
        public DateFilterRule Rule
        {
            get => DateFilterRule.FromFilter(this);
            set => (value ?? throw new ArgumentNullException(nameof(value))).ApplyTo(this);
        }

        /// <summary>Creates a date filter with the default title ("Date Filter") and an AllTime selection.</summary>
        [JsonConstructor]
        public DashboardDateFilter()
        {
            SchemaTypeName = SchemaTypeNames.DateGlobalFilterType;
            Title = "Date Filter";
        }

        /// <summary>Creates a date filter with the default title ("Date Filter").</summary>
        public DashboardDateFilter(DateFilterRule rule) : this()
        {
            Rule = rule;
        }

        /// <summary>Creates a date filter with the given title and an AllTime selection.</summary>
        public DashboardDateFilter(string title) : this()
        {
            Title = title;
        }

        /// <summary>Creates a date filter with an explicit title.</summary>
        public DashboardDateFilter(string title, DateFilterRule rule) : this(title)
        {
            Rule = rule;
        }
    }
}
