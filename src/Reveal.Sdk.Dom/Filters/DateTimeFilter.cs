using System;
using System.Runtime.Serialization;
using Reveal.Sdk.Dom.Core.Constants;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Reveal.Sdk.Dom.Filters
{
    public sealed class DateTimeFilter : FilterBase, IDateRuleFilter
    {
        [JsonProperty]
        internal int DateFiscalYearStartMonth { get; set; }

        [JsonProperty]
        internal bool DisplayInLocalTimeZone { get; set; }

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

        /// <summary>Updates the date selection and returns this filter for fluent configuration.</summary>
        public DateTimeFilter SetRule(DateFilterRule rule)
        {
            Rule = rule ?? throw new ArgumentNullException(nameof(rule));
            return this;
        }

        /// <summary>Creates a datetime filter with an AllTime selection.</summary>
        [JsonConstructor]
        public DateTimeFilter()
        {
            SchemaTypeName = SchemaTypeNames.DateTimeFilterType;
            Rule = DateFilterRule.AllTime;
        }

        [OnDeserializing]
        internal void ReadLegacyDefaults(StreamingContext context)
        {
            // Missing legacy FilterType means AllValues, even though new rules activate filtering.
            FilterType = FilterType.AllValues;
        }

        /// <summary>Creates a datetime filter with the given date selection.</summary>
        public DateTimeFilter(DateFilterRule rule) : this()
        {
            Rule = rule ?? throw new ArgumentNullException(nameof(rule));
        }
    }
}
