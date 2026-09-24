using Reveal.Sdk.Dom.Core;
using Reveal.Sdk.Dom.Core.Constants;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Reveal.Sdk.Dom.Filters
{
    /// <summary>
    /// A relative date window (e.g. "Last 3 Years", "Next 7 Days") applied to a date filter.
    /// Create one through <see cref="IDateRuleFilterExtensions.SetRelativePeriod{T}"/> rather than
    /// directly; the setters are internal so the filter's rule type and range stay consistent.
    /// Serialized with <c>_type</c> "DateRuleType" to match the dashboard schema.
    /// </summary>
    public sealed class RelativePeriod : SchemaType
    {
        // [JsonProperty] is required so the deserializer writes these internal setters.

        /// <summary>How the window relates to the current date (Last, Previous, This, ToDate, Next).</summary>
        [JsonProperty]
        [JsonConverter(typeof(StringEnumConverter))]
        public PeriodRelation Relation { get; internal set; } = PeriodRelation.All;

        /// <summary>Number of periods in the window (e.g. 3 for "Last 3 Years").</summary>
        [JsonProperty]
        public int Count { get; internal set; } = 1;

        /// <summary>The unit of time the window is measured in.</summary>
        [JsonProperty]
        [JsonConverter(typeof(StringEnumConverter))]
        public PeriodType Period { get; internal set; } = PeriodType.Day;

        /// <summary>
        /// Whether today is included in the window. Only meaningful for <see cref="PeriodRelation.Last"/>
        /// and <see cref="PeriodRelation.ToDate"/>; omitted from the JSON when null. For a relative
        /// period this is the flag the engine reads (not the filter-level IncludeToday).
        /// </summary>
        [JsonProperty]
        public bool? IncludeToday { get; internal set; }

        // Public parameterless ctor is required by the deserializer; user code builds instances
        // through the SetRelativePeriod extension, not this ctor.
        public RelativePeriod()
        {
            SchemaTypeName = SchemaTypeNames.DateRuleType;
        }

        internal RelativePeriod(PeriodRelation relation, int count, PeriodType period, bool? includeToday)
            : this()
        {
            Relation = relation;
            Count = count;
            Period = period;
            IncludeToday = includeToday;
        }
    }
}
