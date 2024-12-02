using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Serialization.Converters;

namespace Reveal.Sdk.Dom.Filters
{
    [JsonConverter(typeof(BindingSourceConverter))]
    public interface IBindingSource
    {
    }
}
