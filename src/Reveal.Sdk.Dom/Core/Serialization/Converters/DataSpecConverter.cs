using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Visualizations;
using System;

namespace Reveal.Sdk.Dom.Core.Serialization.Converters
{
    internal class DataSpecConverter : TypeMapConverter<IDataDefinition>
    {
        public DataSpecConverter()
        {
            TypeMap = new System.Collections.Generic.Dictionary<string, Type>()
            {
                { SchemaTypeNames.ResourceDataSpecType, typeof(ResourceDataDefinition)},
                { SchemaTypeNames.TabularDataSpecType, typeof(TabularDataDefinition)},
                { SchemaTypeNames.TextBoxDataSpecType, typeof(TextBoxDataDefinition)},
                { SchemaTypeNames.XmlaDataSpecType, typeof(XmlaDataDefinition)},
            };
        }
    }
}
