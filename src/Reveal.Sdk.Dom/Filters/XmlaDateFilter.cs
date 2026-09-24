using System;
using Reveal.Sdk.Dom.Core.Constants;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Reveal.Sdk.Dom.Filters
{
    public sealed class XmlaDateFilter : FilterBase, IDateRuleFilter
    {
        // Raw schema fields — internal. The public date selection is exposed through Rule.
        [JsonProperty("RuleType")]
        [JsonConverter(typeof(StringEnumConverter))]
        internal DateRuleType RuleType { get; set; } = DateRuleType.AllTime;

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
        internal XmlaDateFilter()
        {
            SchemaTypeName = SchemaTypeNames.XmlaDateFilterType;
        }

        /// <summary>Creates an XMLA date filter with the given date selection.</summary>
        public XmlaDateFilter(DateFilterRule rule) : this()
        {
            Rule = rule ?? throw new ArgumentNullException(nameof(rule));
        }
    }
}
