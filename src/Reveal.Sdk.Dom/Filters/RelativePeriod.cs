using Reveal.Sdk.Dom.Core;
using Reveal.Sdk.Dom.Core.Constants;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Reveal.Sdk.Dom.Filters
{
    /// <summary>
    /// Serialized shape of a relative date window (the "CustomRule" object, <c>_type</c> "DateRuleType").
    /// Internal — user code works with the public <see cref="DateFilterRule"/> facade instead.
    /// </summary>
    internal sealed class RelativePeriod : SchemaType
    {
        // [JsonProperty] is required so the deserializer writes these internal setters.

        [JsonProperty]
        [JsonConverter(typeof(StringEnumConverter))]
        public PeriodRelation Relation { get; internal set; } = PeriodRelation.All;

        [JsonProperty]
        public int Count { get; internal set; } = 1;

        [JsonProperty]
        [JsonConverter(typeof(StringEnumConverter))]
        public PeriodType Period { get; internal set; } = PeriodType.Day;

        [JsonProperty]
        public bool? IncludeToday { get; internal set; }

        // Parameterless ctor is used by the deserializer.
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
