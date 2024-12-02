using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Reveal.Sdk.Dom.Core.Serialization.Converters;

namespace Reveal.Sdk.Dom.Filters
{
    public abstract class Binding<TTarget> : Binding
        where TTarget : BindingTarget, new()
    {        
        public IBindingSource Source { get; set; }

        //todo: how does this work? does this need to be an interface too?
        //if so then the parent classes may not be needed at all
        //the filter bidnings might need a redesign
        [JsonConverter(typeof(BindingTargetConverter))]
        public TTarget Target { get; set; }
    }

    [JsonConverter(typeof(BindingConverter))]
    public abstract class Binding
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public BindingOperatorType Operator { get; set; } = BindingOperatorType.Equals;
    }
}
