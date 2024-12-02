using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Filters;
using System;

namespace Reveal.Sdk.Dom.Core.Serialization.Converters
{
    internal class BindingSourceConverter : TypeMapConverter<IBindingSource>
    {
        public BindingSourceConverter()
        {
            TypeMap = new System.Collections.Generic.Dictionary<string, Type>()
            {
                { SchemaTypeNames.FieldBindingSourceType, typeof(FieldBindingSource) },
                { SchemaTypeNames.ParameterBindingSourceType, typeof(ParameterBindingSource) },
            };
        }
    }
}
