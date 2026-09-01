using Reveal.Sdk.Dom.Core.Constants;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Reveal.Sdk.Dom.Filters
{
    public sealed class DateTimeFilter : FilterBase
    {
        public DateTimeFilter()
        {
            SchemaTypeName = SchemaTypeNames.DateTimeFilterType;
        }
        
        public int DateFiscalYearStartMonth { get; set; }
        
        public bool DisplayInLocalTimeZone { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public DateRuleType RuleType { get; set; } = DateRuleType.AllTime;

        public DateRange CustomDateRange { get; set; }

        // The relative rule applied when RuleType is CustomRule; ignored otherwise. Omitted from
        // the JSON when null.
        public DateRule CustomRule { get; set; }
        
        public bool IncludeToday { get; set; } = true;
    }
}
