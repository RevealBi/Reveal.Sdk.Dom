using Reveal.Sdk.Dom.Core;
using Reveal.Sdk.Dom.Core.Constants;

namespace Reveal.Sdk.Dom.Filters
{
    public sealed class ParameterBindingSource : SchemaType, IBindingSource
    {
        public string ParameterName { get; set; }

        public ParameterBindingSource()
        {
            SchemaTypeName = SchemaTypeNames.ParameterBindingSourceType;
        }
    }
}
