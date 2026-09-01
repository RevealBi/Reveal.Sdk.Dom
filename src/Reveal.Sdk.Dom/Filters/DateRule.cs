using Reveal.Sdk.Dom.Core;
using Reveal.Sdk.Dom.Core.Constants;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Reveal.Sdk.Dom.Filters
{
    /// <summary>
    /// A relative date rule (e.g. "Last 3 Years", "Next 7 Days") used by a date filter whose
    /// <c>RuleType</c> is <see cref="DateRuleType.CustomRule"/>. Serializes with <c>_type</c>
    /// "DateRuleType" to match the engine's dashboard model.
    /// </summary>
    public sealed class DateRule : SchemaType
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public PeriodRelation Relation { get; set; } = PeriodRelation.All;

        public int Count { get; set; } = 1;

        [JsonConverter(typeof(StringEnumConverter))]
        public PeriodType Period { get; set; } = PeriodType.Day;

        // Only meaningful when Relation is Last or ToDate; omitted when null.
        public bool? IncludeToday { get; set; }

        public DateRule()
        {
            SchemaTypeName = SchemaTypeNames.DateRuleType;
        }
    }
}
