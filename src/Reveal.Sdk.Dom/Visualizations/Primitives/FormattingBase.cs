using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core;

namespace Reveal.Sdk.Dom.Visualizations
{
    public abstract class FormattingBase : SchemaType
    {
        [JsonProperty]
        internal bool OverrideDefaultSettings { get; set; }

        [JsonProperty]
        internal string Locale { get; set; }
    }
}
