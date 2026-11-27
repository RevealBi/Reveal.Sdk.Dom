using Reveal.Sdk.Dom.Core.Constants;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Reveal.Sdk.Dom.Visualizations
{
    public sealed class NumberFormatting : FormattingBase
    {
        public NumberFormatting()
        {
            SchemaTypeName = SchemaTypeNames.NumberFormattingSpecType;
        }

        public string CurrencySymbol { get; set; } = "$";

        public int DecimalDigits { get; set; } = 2;

        [JsonConverter(typeof(StringEnumConverter))]
        public NumberFormattingType FormatType { get; set; } = NumberFormattingType.Number;

        [JsonProperty("MKFormat")]
        [JsonConverter(typeof(StringEnumConverter))]        
        public LargeNumberFormat LargeNumberFormat { get; set; } = LargeNumberFormat.None;

        [JsonConverter(typeof(StringEnumConverter))]
        public NegativeFormatType NegativeFormat { get; set; } = NegativeFormatType.MinusSign;

        public bool ShowGroupingSeparator { get; set; }

        [JsonProperty("ShowDataLabelsInChart")]
        public bool ShowDataLabels { get; set; } = true;

        // Older dashboards have the boolean ApplyMkFormat instead of MKFormat; Reveal reads it only when MKFormat is missing.
        private const LargeNumberFormat MissingLargeNumberFormat = (LargeNumberFormat)(-1);
        private bool _applyMkFormat;

        [JsonProperty("ApplyMkFormat")]
        private bool ApplyMkFormat { set => _applyMkFormat = value; }

        [OnDeserializing]
        internal void ClearLargeNumberFormat(StreamingContext context)
        {
            LargeNumberFormat = MissingLargeNumberFormat;
        }

        [OnDeserialized]
        internal void ReadLegacyLargeNumberFormat(StreamingContext context)
        {
            if (LargeNumberFormat == MissingLargeNumberFormat)
                LargeNumberFormat = _applyMkFormat ? LargeNumberFormat.Auto : LargeNumberFormat.None;
        }
    }
}
