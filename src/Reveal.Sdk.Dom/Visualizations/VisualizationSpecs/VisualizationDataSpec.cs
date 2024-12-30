using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core;
using Reveal.Sdk.Dom.Core.Serialization.Converters;

namespace Reveal.Sdk.Dom.Visualizations.VisualizationSpecs
{
    [JsonConverter(typeof(VisualizationDataSpecConverter))]
    internal class VisualizationDataSpec : SchemaType
    {
    }
}
