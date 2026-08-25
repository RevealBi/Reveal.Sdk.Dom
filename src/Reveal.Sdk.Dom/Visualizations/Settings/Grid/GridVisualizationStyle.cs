using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Reveal.Sdk.Dom.Visualizations.Settings
{
    internal sealed class GridVisualizationStyle
    {
        public bool FixedLeftColumns { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Alignment TextAlignment { get; set; } = Alignment.Inherit;

        [JsonConverter(typeof(StringEnumConverter))]
        public Alignment NumericAlignment { get; set; } = Alignment.Inherit;

        [JsonConverter(typeof(StringEnumConverter))]
        public Alignment DateAlignment { get; set; } = Alignment.Inherit;
    }
}
