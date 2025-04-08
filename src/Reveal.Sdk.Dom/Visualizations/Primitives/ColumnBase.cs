using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core;

namespace Reveal.Sdk.Dom.Visualizations
{
    public abstract class ColumnBase : SchemaType
    {
        [JsonProperty("Axis")]
        internal Axis Axis { get; set; }
    }
}
